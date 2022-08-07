using Microsoft.AspNetCore.Identity;
using AlexPWeb.Models;
using AlexPWeb.Data;

namespace AlexPWeb.Classes
{
    public class PasswordHelper
    {
        private readonly ApplicationDbContext _db;

        public PasswordHelper(ApplicationDbContext db)
        {
            _db = db;
        }
        public static string getPasswordHash(SignUpModel signUpModel)
        {
            var passwordHasher = new PasswordHasher<SignUpModel>();
            return passwordHasher.HashPassword(signUpModel, signUpModel.UserPassword);
        }

        public PasswordVerificationResult isPasswordValid(LoginModel loginModel)
        {
            User user = null;

            if (_db.Users.Where(u => u.Email == loginModel.UserEmail).Any())
                user = _db.Users.Where(u => u.Email == loginModel.UserEmail).First();

            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.UserPassword);
            }
                
            return PasswordVerificationResult.Failed;
 
        }
    }
}
