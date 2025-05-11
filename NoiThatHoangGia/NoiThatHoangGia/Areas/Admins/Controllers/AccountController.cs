using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoiThatHoangGia.Areas.Admins.Models;
using NoiThatHoangGia.Models;

namespace NoiThatHoangGia.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class AccountController : Controller
    {
        private readonly NoithathoangiaContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(NoithathoangiaContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admins/AspNetUsers
        public async Task<IActionResult> Index()
        {
            var users = await _context.AspNetUsers
            .Include(u => u.Roles) // Include Roles
            .ToListAsync();

            return View(users);
        }

        // GET: Admins/AspNetUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _userManager
                .FindByIdAsync(id);

            if (aspNetUser == null)
            {
                return NotFound();
            }

            // Lấy danh sách Role của User
            var roles = await _userManager.GetRolesAsync(aspNetUser);
            ViewBag.UserRoles = roles;

            return View(aspNetUser);
        }

        // GET: Admins/AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleManager = HttpContext.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await roleManager.Roles.ToListAsync();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                SelectedRoles = userRoles.ToList(),
                AvailableRoles = allRoles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name,
                    Selected = userRoles.Contains(r.Name)
                }).ToList()
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    await _userManager.UpdateAsync(user);

                    // Cập nhật Roles
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var rolesToAdd = model.SelectedRoles.Except(userRoles);
                    var rolesToRemove = userRoles.Except(model.SelectedRoles);

                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    await _userManager.AddToRolesAsync(user, rolesToAdd);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.AspNetUsers.Any(e => e.Id == model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }


        // GET: Admins/AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _userManager.FindByIdAsync(id);

            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // POST: Admins/AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
