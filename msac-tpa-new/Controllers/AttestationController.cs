using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace msac_tpa_new.Controllers
{
    [Authorize]
    public class AttestationController : Controller
    {
        private readonly SportmenContext _context;
        protected readonly ILogger _logger;
        private readonly SelectList _regionsList;
        private readonly RegionsHandler _regionsHandler;

        public AttestationController(SportmenContext context, ILogger<AttestationController> logger)
        {
            _context = context;
            _logger = logger;
            _regionsHandler = new RegionsHandler(_context);
            _regionsList = _regionsHandler.GetSelectedListRegionsWithNonSelection();
        }

        // GET: Attestation
        public async Task<IActionResult> Index(int? region, string dateFrom, string dateTo, int page = 1)
        {
            int pageSize = 5;   // количество элементов на странице

            IQueryable<Attestation> attestations = _context.Attestations.Include(s => s.Region).Include(s=>s.Comission).Include(s => s.AttestationUserBelts).OrderByDescending(a=>a.IssueDate);

            ViewBag.Regions = _regionsList;

            if (region != null && region != 0)
            {
                attestations = attestations.Where(p => p.RegionId == region);
                ViewBag.Regions = _regionsHandler.GetSelectedListRegionsWithSelection((int)region);
            }
            if (!string.IsNullOrEmpty(dateFrom) || !string.IsNullOrEmpty(dateTo))
            {
                var _dateFrom = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom);
                var _dateTo = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo);
                attestations = attestations.Where(p => p.IssueDate >= _dateFrom && p.IssueDate <= _dateTo);
            }

            var count = await attestations.CountAsync();
            var items = await attestations.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AttestationsViewModel viewModel = new AttestationsViewModel
            {
                PageViewModel = pageViewModel,
                Attestations = items
            };

            return View(viewModel);
        }

        // GET: Attestation/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewBag.Comissions = new SelectList(_context.Comissions, "Id", "Name");
            ViewBag.Regions = _regionsHandler.GetSelectedListRegions();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile OrderFilePath, IFormFile DescisionFilePath, [Bind("Id,Address,IssueDate,ComissionId,RegionId")] Attestation attestation)
        {
            if (ModelState.IsValid)
            {
                attestation.OrderFilePath = await FileHandler.SaveDocumentAsync(OrderFilePath, FileTypes.Order, attestation.IssueDate.ToString(CultureInfo.InvariantCulture), attestation.RegionId);
                attestation.DescisionFilePath = await FileHandler.SaveDocumentAsync(DescisionFilePath, FileTypes.Descision, attestation.IssueDate.ToString(CultureInfo.InvariantCulture), attestation.RegionId);
                _context.Add(attestation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Comissions = new SelectList(_context.Comissions, "Id", "Name", attestation.ComissionId);
            return View(attestation);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var attestation = await _context.Attestations
                .Include(a => a.Comission)
                .Include(a => a.Region)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attestation == null) return NotFound();

            var comissionMembers = _context.Comissions.Where(a => a.Id == attestation.ComissionId).SelectMany(s => s.SportmanComissions);
            ViewBag.ComissionMembers = comissionMembers.Select(a => a.Sportman).Include(s => s.Region);
            ViewBag.AttestationUserBelts = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == id);
            return View(attestation);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attestation = await _context.Attestations
                .Include(a => a.Comission)
                .Include(b=>b.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attestation == null)
            {
                return NotFound();
            }
            var comissionMembers = _context.Comissions.Where(a => a.Id == attestation.ComissionId).SelectMany(s => s.SportmanComissions);
            ViewBag.ComissionMembers = comissionMembers.Select(a => a.Sportman).Include(s => s.Region);
            ViewBag.AttestationUserBelts = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == id);
            return View(attestation);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context != null)
            {
                var attestation = await _context.Attestations.Include(a=>a.AttestationUserBelts).FirstOrDefaultAsync(s=>s.Id == id);
                FileHandler.RemoveDoc(attestation.OrderFilePath, FileTypes.Order);
                FileHandler.RemoveDoc(attestation.DescisionFilePath, FileTypes.Descision);

                _context.Attestations.Remove(attestation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AttestationExists(int id)
        {
            return _context.Attestations.Any(e => e.Id == id);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddSportmanAttestation(string attestationId, string sportmanId, string beltId)
        {
            if (string.IsNullOrEmpty(attestationId) || string.IsNullOrEmpty(sportmanId) || string.IsNullOrEmpty(beltId))
            {
                return Json(new { success = false, responseText = @Resources.Spans.NotAllParams });
            }
            var exists = _context.AttestationUserBelts.FirstOrDefault(a=>a.AttestationId == int.Parse(attestationId) && a.SportmanId == int.Parse(sportmanId) && a.BeltId == int.Parse(beltId));
            if (exists != null)
            {
                return Json(new { success = false, responseText = @Resources.Spans.ResultExists });
            }
            _context.AttestationUserBelts.Add(new AttestationUserBelt() { AttestationId = int.Parse(attestationId), SportmanId = int.Parse(sportmanId), BeltId = int.Parse(beltId) });
            _context.SaveChanges();
            return Json(new { success = true, responseText = @Resources.Spans.SportmanAddedSuccess });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSportmanAttestation(string id, string attestationId, string sportmanId, string beltId)
        {

            if (string.IsNullOrEmpty(attestationId) || string.IsNullOrEmpty(sportmanId) || string.IsNullOrEmpty(beltId))
            {
                return Json(new { success = false, responseText = @Resources.Spans.NotAllParams });
            }
            var attestationUserBeltsToDel = _context.AttestationUserBelts.FirstOrDefault(a =>
                a.AttestationId == int.Parse(attestationId) && a.SportmanId == int.Parse(sportmanId) &&
                a.BeltId == int.Parse(beltId));
            if (attestationUserBeltsToDel == null)
            {
                var attestationUserBelts1 = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == int.Parse(attestationId));
                return PartialView("_sportsmanAttestation", attestationUserBelts1?.ToList());
            }
            _context.AttestationUserBelts.Remove(attestationUserBeltsToDel);
            _context.SaveChanges();

            //var attestationUserBelts2 = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == int.Parse(attestationId));
            //return PartialView("_sportsmanAttestation", attestationUserBelts2?.ToList());
            return RedirectToAction(nameof(Edit), new { id = int.Parse(attestationId) });
            //return NoContent();
        }

        // GET: Attestation/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attestation = await _context.Attestations.Include(s => s.Region).FirstOrDefaultAsync(a => a.Id == id);
            if (attestation == null)
            {
                return NotFound();
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name", attestation.RegionId);
            var sprtmn = _context.SportMans.Include(s => s.Region).OrderBy(a => a.Surname).ToList();
            ViewBag.Sportmen = sprtmn.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Fullname }).ToList();
            ViewBag.Belts = _context.Belts.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            ViewBag.Comissions = new SelectList(_context.Comissions, "Id", "Name", attestation.ComissionId);
            ViewBag.AttestationUserBelts = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == id);
            return View(attestation);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile OrderFilePathNew, IFormFile DescisionFilePathNew, int id, [Bind("Id,Address,IssueDate,ComissionId,RegionId,OrderFilePath,DescisionFilePath")] Attestation attestation)
        {
            if (id != attestation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldAttestation = await _context.Attestations.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                    if (OrderFilePathNew != null)
                    {
                        FileHandler.RemoveDoc(oldAttestation.OrderFilePath, FileTypes.Order);
                        attestation.OrderFilePath = await FileHandler.SaveDocumentAsync(OrderFilePathNew, FileTypes.Order, attestation.IssueDate.ToString(CultureInfo.InvariantCulture), attestation.RegionId);
                    }
                    if (DescisionFilePathNew != null)
                    {
                        FileHandler.RemoveDoc(oldAttestation.DescisionFilePath, FileTypes.Descision);
                        attestation.DescisionFilePath = await FileHandler.SaveDocumentAsync(DescisionFilePathNew, FileTypes.Descision, attestation.IssueDate.ToString(CultureInfo.InvariantCulture), attestation.RegionId);
                    }
                    _context.Update(attestation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttestationExists(attestation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComissionId"] = new SelectList(_context.Comissions, "Id", "Name", attestation.ComissionId);
            return View(attestation);
        }

        [HttpGet]
        public ActionResult PartialViewSportsmanAttestation(int attestationId)
        {
            var attestationUserBelts = _context.AttestationUserBelts.Include(a => a.Sportman).Include(b => b.Belt).Where(s => s.AttestationId == attestationId);
            return PartialView("_sportsmanAttestation", attestationUserBelts?.ToList());
        }

    }
}
