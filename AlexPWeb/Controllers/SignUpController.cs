using AlexPWeb.Classes;
using AlexPWeb.Data;
using AlexPWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlexPWeb.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SignUpController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var newSignUpModel = new SignUpModel();
            return View(newSignUpModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpModel signUpModel)
        {
            try
            {
                if (_db.Users.Where(u => u.DisplayName == signUpModel.UserDisplayName).Any() || _db.Users.Where(u => u.Email == signUpModel.UserEmail).Any())
                {
                    TempData["error"] = "Unable to sign up. Please check the fields entered. If you already have an account, please login";
                    return RedirectToAction("Index", "SignUp");
                }

                var newUser = new User()
                {
                    DisplayName = signUpModel.UserDisplayName,
                    Email = signUpModel.UserEmail,
                    PasswordHash = PasswordHelper.getPasswordHash(signUpModel)
                };

                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Unable to sign up. Please check the fields entered. If you already have an account, please login";
                    return RedirectToAction("Index", "SignUp");
                }

                _db.Users.Add(newUser);
                _db.SaveChanges();
                TempData["success"] = "You have successfully signed up. Please go ahead and login";
                return RedirectToAction("Index", "Login");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
