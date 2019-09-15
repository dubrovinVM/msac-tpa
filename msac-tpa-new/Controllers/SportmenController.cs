using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using msac_tpa.DAL.EF;
using msac_tpa.DAL.Entities;
using Microsoft.AspNetCore.Http;
using msac_tpa_new.BusinessLogic;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;


namespace msac_tpa_new.Controllers
{
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

        // GET: Sportmen
        public async Task<IActionResult> Index(int? region, string surname)
        {
            //var sportmenContext = _context.SportMans.Include(s => s.Region);
            IQueryable<Sportman> sportman = _context.SportMans.Include(s => s.Region);

            List<Region> regions = _context.Regions.ToList();
            regions.Insert(0, new Region { Name = "Всi", Id = 0 });
            ViewBag.Regions = new SelectList(regions, "Id", "Name", 0);

            if (region != null && region != 0)
            {
                sportman = sportman.Where(p => p.RegionId == region);
                ViewBag.Regions = new SelectList(regions, "Id", "Name", region);
            }
            if (!string.IsNullOrEmpty(surname))
            {
                sportman = sportman.Where(p => p.Surname.Contains(surname));
            }

            return View(await sportman.ToListAsync());
        }

        // GET: Sportmen/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Sportmen/Create
        public IActionResult Create()
        {
            ViewBag.Regions = new SelectList(_context.Regions, "Id","Name");
            return View();
        }

        // POST: Sportmen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
    }
}
