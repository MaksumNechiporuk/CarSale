using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarSale.Entities;
using CarSale.Models;
using CarSale.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CarSale.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment hostingEnvironment;


        public ProfileController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;

            _httpContextAccessor = httpContextAccessor;

        }
        [HttpGet]
        async public Task<IActionResult> Show()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var User = await userManager.FindByIdAsync(userId);
            if (User != null)
            {

                return View(User);
            }
            else
            {
                return View("NotFound");

            }



        }
        [HttpGet]
        async public Task<IActionResult> Edit()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var User = await userManager.FindByIdAsync(userId);
            if (User != null)
            {
                var EditUser = new EditProfileViewModel
                {
                    Id = User.Id,
                    City = User.City,
                    Email = User.Email,
                    ExistImgPath = User.Img,
                    Country = User.Country,
                };
                return View(EditUser);
            }
            else
            {
                return View("NotFound");

            }

        }
        [HttpPost]
        async public Task<IActionResult> Edit(EditProfileViewModel user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await userManager.FindByIdAsync(user.Id);
                if (!String.IsNullOrEmpty(user.Password))
                {
                    DBContent db = new DBContent();

                    var store = new UserStore<AppUser>(db);

                    var newPasswordHash = userManager.PasswordHasher.HashPassword(oldUser, user.Password);
                    await store.SetPasswordHashAsync(oldUser, newPasswordHash);


                }
                oldUser.Country = user.Country;
                oldUser.City = user.City;
                oldUser.Email = user.Email;
                oldUser.UserName = user.Email;

                if (user.Img != null)
                {
                    if (user.ExistImgPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img", user.ExistImgPath);
                        System.IO.File.Delete(filePath);
                    }
                    oldUser.Img = UploadedFile(user);
                }
                var result = await userManager.UpdateAsync(oldUser);

                if (result.Succeeded)
                {
                    await signInManager.SignOutAsync();
                    var resultSignIn = await signInManager.PasswordSignInAsync(
                                       user.Email, user.Password, false, false);
                    return RedirectToAction("Show");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(user);

            }
            return View(user);


        }
        private string UploadedFile(EditProfileViewModel model)
        {
            string uniqFileName = null;
            if (model.Img != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                uniqFileName = Guid.NewGuid().ToString() + "_" + model.Img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Img.CopyTo(fileStream);
                }
            }

            return uniqFileName;
        }
    }
}