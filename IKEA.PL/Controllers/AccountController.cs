using IKEA.DAL.Models.Identity;
using IKEA.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        #region Services
        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager) 
        {
            userManager = _userManager;
            signInManager = _signInManager;
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await userManager.FindByEmailAsync(loginViewModel.Email);
            
            if (user is not null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password,loginViewModel.RememberMe,true);
                if (result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your account is not allowed");

                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your account is locked out");

                if (result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ModelState.AddModelError(string.Empty, "Login Failed");
            return View(loginViewModel);

        }
        #endregion
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
