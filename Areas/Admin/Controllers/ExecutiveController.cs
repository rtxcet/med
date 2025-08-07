using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using med.Models;

namespace med.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExecutiveController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ExecutiveController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanılıyor.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName // eğer email olarak da aynı girilecekse
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Admin rolü var mı kontrol et
                var roleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Admin rolü oluşturulamadı.");
                        return View(model);
                    }
                }

                // Kullanıcıya admin rolünü ata
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Kullanıcıya admin rolü atanamadı.");
                    return View(model);
                }

                TempData["Message"] = "Kullanıcı başarıyla eklendi ve admin rolü atandı.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }




        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            // Kullanıcı adını güncelle
            user.UserName = model.UserName;
            user.Email = model.UserName; // Eğer e-posta da aynı olacaksa

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            // Şifre alanı boş değilse şifreyi güncelle
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View(model);
                }
            }

            TempData["Message"] = "Kullanıcı başarıyla güncellendi.";
            return RedirectToAction("Index");
        }



        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var usersCount = _userManager.Users.Count();
            if (usersCount <= 1)
            {
                TempData["Message"] = "Sistemde en az bir kullanıcı olmalıdır. Son kullanıcı silinemez.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "Kullanıcı başarıyla silindi.";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Kullanıcı silinemedi.";
            return RedirectToAction("Index");
        }


    }
}
