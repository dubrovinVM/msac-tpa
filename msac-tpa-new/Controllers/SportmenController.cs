using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using Microsoft.AspNetCore.Http;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;


namespace msac_tpa_new.Controllers
{
    [Authorize]
    public class SportmenController : Controller
    {
        private readonly SportmenContext _context;
        private IConfiguration _configuration;
        public string SportmenFolder { get; set; }

        public SportmenController(SportmenContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            SportmenFolder = _configuration.GetValue<string>("avatar:sportman");
        }

        public async Task<IActionResult> Index(int? region, string surname, int page = 1, int pageSize = 5)
        {
            var pageSizes = new[]
            {
                new {Id = "0", Count = "5"},
                new {Id = "1", Count = "10"},
                new {Id = "2", Count = "15"},
                new {Id = "4", Count = "30"},
                new {Id = "5", Count = "50"}
            };

            IQueryable<Sportman> sportman = _context.SportMans.Include(s => s.Region).Include(a=>a.AttestationUserBelts).OrderBy(z=>z.Surname);

            List<Region> regions = _context.Regions.ToList();
            regions.Insert(0, new Region { Name = "Всi", Id = 0 });
            ViewBag.Regions = new SelectList(regions, "Id", "Name", 0);
            ViewBag.AttestationUserBelts = _context.AttestationUserBelts.Include(b=>b.Belt).ToList();

            if (region != null && region != 0)
            {
                sportman = sportman.Where(p => p.RegionId == region);
                ViewBag.Regions = new SelectList(regions, "Id", "Name", region);
            }
            if (!string.IsNullOrEmpty(surname))
            {
                sportman = sportman.Where(p => p.Surname.Contains(surname));
            }

            var count = await sportman.CountAsync();
            var items = await sportman.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            SportmenViewModel viewModel = new SportmenViewModel
            {
                PageViewModel = pageViewModel,
                Sportmen = items
            };

            ViewBag.PageSizes = new SelectList(pageSizes, "Count", "Count");

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportman = await _context.SportMans.Include(s => s.Region).Include(a=>a.AttestationUserBelts).FirstOrDefaultAsync(m => m.Id == id);
            var attest = _context.AttestationUserBelts.Where(c => c.SportmanId== sportman.Id).Include(a=>a.Attestation).Include(b => b.Belt).OrderByDescending(q=>q.Attestation.IssueDate);
            if (sportman == null)
            {
                return NotFound();
            }
            ViewBag.Attestations = attest;
            return View(sportman);
        }

        // GET: Sportmen/Create
        [Authorize(Roles = "admin, header")]
        public IActionResult Create()
        {
            ViewBag.Regions = new SelectList(_context.Regions, "Id","Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, header")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile avatar, [Bind("Id,Name,Surname,LastName,BirthDay,RegionId")] Sportman sportman)
        {
            if (ModelState.IsValid)
            {
                sportman.AvatarFilePath = await FileHandler.SaveAvatarAsync(avatar, sportman.Surname, SportmenFolder);
                _context.Add(sportman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name");
            return View(sportman);
        }

        // GET: Sportmen/Edit/5
        [Authorize(Roles = "admin, header")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportman = await _context.SportMans.FindAsync(id);
            if (sportman == null)
            {
                return NotFound();
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name", sportman.RegionId);
            return View(sportman);
        }

        // POST: Sportmen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin, header")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile avatar, [Bind("Id,Name,Surname,LastName,AvatarFilePath, BirthDay,RegionId")] Sportman sportman)
        {
            if (id != sportman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (avatar != null && avatar.Length != 0)
                    {
                       FileHandler.RemoveAvatar(sportman.AvatarFilePath, SportmenFolder);
                       sportman.AvatarFilePath = await FileHandler.SaveAvatarAsync(avatar, sportman.Surname, SportmenFolder);
                    }

                    _context.Update(sportman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportmanExists(sportman.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name", sportman.RegionId);
            return View(sportman);
        }

        // GET: Sportmen/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportman = await _context.SportMans
                .Include(s => s.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportman == null)
            {
                return NotFound();
            }

            return View(sportman);
        }

        // POST: Sportmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportman = await _context.SportMans.FindAsync(id);
            _context.SportMans.Remove(sportman);
            FileHandler.RemoveAvatar(sportman.AvatarFilePath, SportmenFolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportmanExists(int id)
        {
            return _context.SportMans.Any(e => e.Id == id);
        }

        private string GetUserBelt(int id)
        {
            return _context.AttestationUserBelts.Include(a=>a.Belt).FirstOrDefault(e => e.AttestationId == id)?.Belt.Name;
        }
    }
}
