using IKEA.DAL.Models.Identity;
using IKEA.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        #region Services
        public AccountController(UserManager<ApplicationUser> _userManager) 
        {
            userManager = _userManager;
        }
        #endregion
        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var User = await userManager.FindByNameAsync(signUpViewModel.Email);
            if (User != null)
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This email already exists");
                return View(signUpViewModel);
            }
            User = new ApplicationUser()
            {
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                FName = signUpViewModel.FirstName,
                LName = signUpViewModel.LastName,
                IsAgreed = signUpViewModel.IsAgreed
            };
            var result = await userManager.CreateAsync(User, signUpViewModel.Password);
            if (result.Succeeded)
            {
                //await userManager.AddToRoleAsync(User, "Employee");
                return RedirectToAction(nameof(Login));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(signUpViewModel);
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        #endregion
    }
}
