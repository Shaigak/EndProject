using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Areas.View_Models.Account;
using MyEndProjectCode.Helpers.Enums;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels.Account;

namespace MyEndProjectCode.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;   
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Random rnd = new Random();

            AppUser newUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.Lastname,
                Email = model.Email,               
                UserName = model.FirstName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View(model);

            }

            await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

            //await _signInManager.SignInAsync(newUser, false);

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

            string subject = "Register confirmation";

            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Hello");

            _emailService.Send(newUser.Email, subject, html);


            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.Email);

            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(model);

            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult VerifyEmail()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult ForgotPassWord()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassWord(ForgotPasswordVM forgotPassword)
        {

            if (!ModelState.IsValid) return View();


            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser == null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }


            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token },

                Request.Scheme, Request.Host.ToString());



            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }
            string subject = "Verify Password Reset Email";

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", existUser.FirstName);

            _emailService.Send(existUser.Email, subject, html);


            return RedirectToAction(nameof(VerifyEmail));

        }

        [HttpGet]
        public IActionResult ResetPassword(string UserId, string token)
        {

            return View(new ResetPasswordVM { Token = token, UserId = UserId });
        }



        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordVM resetPassword)
        {

            if (!ModelState.IsValid) return View(resetPassword);

            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);

            if (existUser == null) return NotFound();

            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);


            return RedirectToAction(nameof(Login));
        }



        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {

                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }

            }
        }




    }
}
