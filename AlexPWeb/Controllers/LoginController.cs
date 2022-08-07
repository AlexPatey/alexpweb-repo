using AlexPWeb.Data;
using AlexPWeb.Models;
using AlexPWeb.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlexPWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var newLoginModel = new LoginModel();
            return View(newLoginModel);
        }

        public IActionResult Login(LoginModel loginModel)
        {
            if (!_db.Users.Where(u => u.Email == loginModel.UserEmail).Any())
            {
                TempData["error"] = "Incorrect username or password.";
                return RedirectToAction("Index", "Login");
            }

            var newPasswordHelper = new PasswordHelper(_db);

            //check password
            PasswordVerificationResult result = newPasswordHelper.isPasswordValid(loginModel);

            if (result == PasswordVerificationResult.Success)
            {
                var newSignedInUser = new SignedInUser()
                {
                    DisplayName = _db.Users.Where(u => u.Email == loginModel.UserEmail).First().DisplayName,
                    Email = loginModel.UserEmail
                };
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("signedInUser", JsonConvert.SerializeObject(newSignedInUser), option);
                TempData["success"] = "You have successfully logged in.";
                return RedirectToAction("Index", "Home");
            }

            //return error
            TempData["error"] = "Incorrect username or password.";
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("signedInUser"))
            {
                Response.Cookies.Delete("signedInUser");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
