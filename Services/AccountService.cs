using courseraClone.Models;
using Microsoft.AspNetCore.Identity;

namespace courseraClone.Services{

    public class AccountService{

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;




        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool>  RegisterUserAsync(string fullName, string email, string password){

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email, // `UserName` is required for Identity users
                FullName = fullName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {

                return  true;
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
            return false;
        }

        public async Task<bool> LoginUserAsync(string email, string password , bool rememberMe){

            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

            if (result != null) return true;
            else return false;
        }


    }
}