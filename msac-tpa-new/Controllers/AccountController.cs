using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using msac_tpa_new.Entities.Authentication;
using msac_tpa_new.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using msac_tpa_new.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace msac_tpa_new.Controllers
{
    public class AccountController : Controller
    {
        private readonly SportmenContext _context;

        public AccountController(SportmenContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(int? region, string login)
        {
            //var sportmenContext = _context.SportMans.Include(s => s.Region);
            IQueryable<User> users = _context.Users.Include(s => s.Role).Include(c=>c.Region);

            List<Region> regions = _context.Regions.ToList();
            regions.Insert(0, new Region { Name = "Всi", Id = 0 });
            ViewBag.Regions = new SelectList(regions, "Id", "Name", 0);

            if (region != null && region != 0)
            {
                users = users.Where(p => p.RegionId == region);
                ViewBag.Regions = new SelectList(regions, "Id", "Name", region);
            }
            if (!string.IsNullOrEmpty(login))
            {
                users = users.Where(p => p.Email.Contains(login));
            }

            return View(await users.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Include(a=>a.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == Hashing.GetHash(model.Password));
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", Resources.Error.BadAuthentication);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Description");
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = Hashing.GetHash(model.Password)};
                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == model.RoleId);
                    Region userRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == model.RegionId);
                    if (userRole != null) user.Role = userRole;
                    if (userRegion != null) user.Region = userRegion;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Account");
                }
                else
                    ModelState.AddModelError("Error", Resources.Error.BadAuthentication);
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(s => s.Region).FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Sportmen/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),"Account");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name", user.RegionId);
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Description", user.RoleId);
            return View(user);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Password, [Bind("Id,Email,RoleId,RegionId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Password))
                    {
                        user.Password = Hashing.GetHash(Password);
                        _context.Update(user);
                    }
                    else
                    {
                        var oldUser = _context.Users.FirstOrDefault(a => a.Id == id);
                        oldUser.Email = user.Email;
                        oldUser.RoleId = user.RoleId;
                        oldUser.RegionId = user.RegionId;
                        oldUser.Email = user.Email;
                        _context.Update(oldUser);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Regions = new SelectList(_context.Regions, "Id", "Name", user.RegionId);
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}