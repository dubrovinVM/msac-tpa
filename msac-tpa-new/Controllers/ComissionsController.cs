using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace msac_tpa_new.Controllers
{
    [Authorize]
    public class ComissionsController : Controller
    {
        private readonly SportmenContext _context;
        protected readonly ILogger _logger;
        private readonly SelectList _regionsList;
        private readonly RegionsHandler _regionsHandler;

        public ComissionsController(SportmenContext context, ILogger<ComissionsController> logger)
        {
            _context = context;
            _logger = logger;
            _regionsHandler = new RegionsHandler(_context);
            _regionsList = _regionsHandler.GetSelectedListRegionsWithNonSelection();
        }

        // GET: Comissions
        public async Task<IActionResult> Index(int? region, string title)
        {
            IQueryable<Comission> comissions = _context.Comissions.Include(s => s.Region)
                .Include(s => s.SportmanComissions).ThenInclude(a => a.Sportman);
            ViewBag.Regions = _regionsList;

            if (region != null && region != 0)
            {
                comissions = comissions.Where(p => p.RegionId == region);
                ViewBag.Regions = _regionsHandler.GetSelectedListRegionsWithSelection((int) region);
            }
            if (!string.IsNullOrEmpty(title))
            {
                comissions = comissions.Where(p => p.Name.Contains(title));
            }

            return View(await comissions.ToListAsync());
        }

        // GET: Comissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comission = await _context.Comissions.Include(s => s.Region).FirstOrDefaultAsync(a=>a.Id==id);

            if (comission == null)
            {
                return NotFound();
            }

            var comissionMembers = _context.Comissions.Where(a => a.Id == id).SelectMany(s => s.SportmanComissions);
            ViewBag.ComissionMembers = comissionMembers.Select(a => a.Sportman).Include(s=>s.Region);

            return View(comission);
        }

        // GET: Comissions/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name");
            ViewBag.Sportmen = _context.SportMans
                .Select(r => new SelectListItem {Value = r.Id.ToString(), Text = r.Fullname}).ToList();
            return View();
        }

        // POST: Comissions/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string[] sportman, [Bind("Id, Name, RegionId")] Comission comission)
        {
            if (ModelState.IsValid)
            {
                if (sportman.Length != 0)
                {
                    foreach (var sportmanId in sportman)
                    {
                        comission.SportmanComissions.Add(new SportmanComission()
                        {
                            SportmanId = int.Parse(sportmanId)
                        });
                    }
                }
                _context.Add(comission);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Comissions Created.");
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Regions = _regionsHandler.GetSelectedListRegionsWithSelection(comission.RegionId);
            return View(comission);
        }

        // GET: Comissions/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comission = await _context.Comissions.FindAsync(id);

            if (comission == null)
            {
                return NotFound();
            }
            ViewBag.Regions = _regionsHandler.GetSelectedListRegionsWithSelection(comission.RegionId);
            ViewBag.Sportmen = _context.SportMans.Include(s=>s.Region)
                .Select(r => new SelectListItem {Value = r.Id.ToString(), Text = r.Fullname }).ToList();

            var comissionMembers = _context.Comissions.Where(a => a.Id == id)
                .SelectMany(comissionMember => comissionMember.SportmanComissions);
            ViewBag.ComissionMembers = comissionMembers.Select(a => a.Sportman);
            return View(comission);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string[] sportman, int id, [Bind("Id,Name,RegionId")] Comission comission)
        {
            if (id != comission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldComission = await _context.Comissions.Include(a=>a.SportmanComissions).ThenInclude(b=>b.Sportman).Where(p => p.Id == id).FirstOrDefaultAsync();

                    if (sportman.Length != 0)
                    {
                        foreach (var sportmanComission in _context.SportmanComissions.Where(a=>a.ComissionId == id))
                        {
                            if (!sportman.Contains(sportmanComission.Sportman.Id.ToString()))
                            {
                                var sportmanComissionToDel = await _context.SportmanComissions.FirstOrDefaultAsync(a => a.ComissionId == id && a.SportmanId== sportmanComission.Sportman.Id);
                                oldComission.SportmanComissions.Remove(sportmanComissionToDel);
                            }
                        }

                        foreach (var sportmanId in sportman)
                        {
                            if (oldComission.SportmanComissions.All(a => a.SportmanId != int.Parse(sportmanId)))
                            {
                                oldComission.SportmanComissions.Add(new SportmanComission()
                                {
                                    SportmanId = int.Parse(sportmanId)
                                });
                            }
                        }
                    }

                    oldComission.Name = comission.Name;
                    oldComission.RegionId = comission.RegionId;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComissionExists(comission.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Regions = _regionsHandler.GetSelectedListRegionsWithSelection(comission.RegionId);
            return View(comission);
        }
        
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comission = await _context.Comissions.Include(a => a.Region).FirstOrDefaultAsync(m => m.Id == id);
            var comissionMembers = _context.Comissions.Where(a => a.Id == id).SelectMany(s => s.SportmanComissions);
            ViewBag.ComissionMembers = comissionMembers.Select(a => a.Sportman).Include(s=>s.Region);

            return View(comission);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Comission comission = _context.Comissions.FirstOrDefault(a => a.Id == id);
            if (comission != null)
            {
                _context.Comissions.Remove(comission);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ComissionExists(int id)
        {
            return _context.Comissions.Any(e => e.Id == id);
        }

       
    }
}
