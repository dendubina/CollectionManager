using System;
using System.Threading.Tasks;
using Contracts;
using Entities.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult SignIn()
            => View();
       

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            try
            {
                await _authService.SignInAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToHomePage();
        }

        [HttpGet]
        public IActionResult SignUp()
            => View();
        

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
                await _authService.SignUpAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToHomePage();
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return RedirectToHomePage();
        }

        private RedirectToActionResult RedirectToHomePage()
            => RedirectToAction("Index", "Home");
    }
}
