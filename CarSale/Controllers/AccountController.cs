using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSale.Entities;
using CarSale.ViewModel;
using Entities.Models;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSale.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, DBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }


        [HttpGet("GetUsers")]
        public IActionResult GetUsers(string id)
        {
            string path = "images";
            var users = (from g in _context.Users
                        select g).ToList();
            var resultUsers = (from c in users
                             select
                             new AppUser
                             {
                                 Id = c.Id,                         
                                 Name = c.Name,
                                 Surname = c.Surname,
                                 Country = c.Country,
                                 City = c.City
                             }).ToList();

            return Ok(resultUsers);
        }
        

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errrors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errrors);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Country = model.Country,
                City = model.City,
                PhoneNumber = model.PhoneNumber,
                Img = "https://img.icons8.com/officel/2x/user.png"
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errrors = CustomValidator.GetErrorsByIdentityResult(result);
                return BadRequest(errrors);
            }
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                await userManager.AddToRoleAsync(user, "User");
                return Ok();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            _context.SaveChanges();


            //userService.AddUserProfile(user.Id, model);
            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok(model);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errrors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errrors);
            }

            var result = await signInManager
                .PasswordSignInAsync(model.Email, model.Password,
                false, false);
            if (!result.Succeeded)
            {
                return BadRequest(new { invalid = "Не правильно введені дані!" });
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok(model);
        }

    }
}