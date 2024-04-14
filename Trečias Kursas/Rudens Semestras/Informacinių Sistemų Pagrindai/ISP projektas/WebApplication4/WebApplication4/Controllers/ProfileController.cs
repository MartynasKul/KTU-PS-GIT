using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;


namespace WebApplication4.Controllers
{
    public class ProfileController : Controller
    {
    
        private readonly UserRepo _userRepo;
        private readonly AuthenticationService _authenticationService;
        private readonly IEmailSender _emailSender;
        private readonly TitleService _titleService;
        
        public ProfileController(UserRepo userRepo, AuthenticationService authenticationService , IEmailSender emailSender, TitleService titleService)
        {
            _userRepo = userRepo;
            _authenticationService = authenticationService;
            _emailSender = emailSender;
            _titleService = titleService;
        }   

        [HttpGet]
        public IActionResult Register()
        {
          return View("Register");
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //|| !IsUserNameInUse(model.UserName)
                if (IsEmailInUse(model.Email) )
                {
                    ModelState.AddModelError("Email", "Šiuo pašto adresu jau yra naudotojas.");
                    return RedirectToAction("Login");
                }
               
                 // Generate a unique salt for the user
                string salt = GenerateSalt();

                // Hash the entered password with the generated salt
                //string hashedPassword = _authenticationService.HashPassword(model.Password, salt);

            // Validate model and create a new User instance
                User newUser = new User
                {
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password, // Note: Don't forget to hash the password before storing it
                    Salt = salt
                // Other properties...
                };

                // Call your UserRepo to add the new user
                _userRepo.AddUser(newUser);

                SendWelcomeEmail(model.Email);

                // Redirect to a success page or login page
                return RedirectToAction("Login");
            }

            // If model validation fails, return to the registration page with errors
            return View(model);
        }

         private string GenerateSalt()
         {
            // Generate a random salt (using a more secure method)
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private bool IsEmailInUse(string email)
        {
            // Check if the email is already associated with an existing user
            var existingUser = _userRepo.FindUserByEmail(email);
            return existingUser == null;
        }   
        private bool IsUserNameInUse(string username)
        {
            // Check if the email is already associated with an existing user
            var existingUser = _userRepo.FindUserByUsername(username);
            return existingUser == null;
        } 

        private void SendWelcomeEmail(string userEmail)
        {
            try
            {
                // Create the email message
                var subject = "Welcome to CS2";
                var message = "Thank you for registering on CS2. We're excited to have you!";

                // Send the email using the EmailSender
                _emailSender.SendEmailAsync(userEmail, subject, message);
            }
            catch (Exception ex)
            {
               // Handle any exceptions related to email sending
                Console.WriteLine($"Error sending welcome email: {ex.Message}");
            }
        }

     [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        //Console.WriteLine($"Received email: {Email}, password: {Password}");
        if (_authenticationService.AuthenticateUser(Email, Password, out int userType))
        {
            // Authentication successful, redirect to the user's profile or another page
            User currentUser = _userRepo.FindUserByEmail(Email);

            HttpContext.Session.SetString("Email", currentUser.Email);
            HttpContext.Session.SetInt32("UserType", userType);
            HttpContext.Session.SetInt32("UserID", currentUser.ID);
            HttpContext.Session.SetString("Username", currentUser.UserName);

                return RedirectToAction("Profile");
        }
        // Authentication failed, return to the login page with an error message
        ViewBag.ErrorMessage = "Invalid email or password";
        return View();
    }

        private void SendMail(string userEmail, string header, string mailmessage)
        {
            try
            {
                // Create the email message
                var subject = header;
                var message = mailmessage;

                // Send the email using the EmailSender
                _emailSender.SendEmailAsync(userEmail, subject, message);
            }
            catch (Exception ex)
            {
                // Handle any exceptions related to email sending
                Console.WriteLine($"Error sending welcome email: {ex.Message}");
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
      
        public IActionResult Profile()
        {
            string userEmail = HttpContext.Session.GetString("Email");
            int? userType = HttpContext.Session.GetInt32("UserType");

            if(!string.IsNullOrEmpty(userEmail) && userType.HasValue)
            {
                User currentUser = _userRepo.FindUserByEmail(userEmail);
                int points = currentUser.Points;
                var availableTitles = _titleService.GetAvailableTitles(points);
                ViewBag.AvailableTitles = availableTitles; // Pass the titles to the view

                return View("Profile", currentUser);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
    public IActionResult ChangePassword(string oldPassword, string newPassword, string repeatNewPassword)
    {
        if (newPassword != repeatNewPassword)
            {
                // New passwords don't match
                //ModelState.AddModelError("repeatNewPassword", "Passwords do not match.");
                return View("Profile"); // or return RedirectToAction("Profile");
            }
        // Retrieve user information from session
        string userEmail = HttpContext.Session.GetString("Email");

        // Check if the user is authenticated
        if (!string.IsNullOrEmpty(userEmail))
        {
            // Call your UserRepo to get the user based on userEmail
            User user = _userRepo.FindUserByEmail(userEmail);


            if(oldPassword!=user.Password)
            {
                // Old password doesn't match
                //ModelState.AddModelError("oldPassword", "Old password is incorrect.");
                return View("Profile"); // or return RedirectToAction("Profile");
            }
            // Hash the new password with the user's existing salt
            string newSalt = user.Salt;
            //string newHashedPassword = _authenticationService.HashPassword(newPassword, newSalt);

            // Update the user's password in the database
            user.Password = newPassword;
            _userRepo.UpdateUserPassword(user);

            SendMail(userEmail, "Dėl pakeisto slaptažodžio", "Jūsų "+ user.UserName +" CS2 paskyros slaptažodis buvo pakeistas");
            // Optionally, you might want to redirect to a success page or display a success message
            return RedirectToAction("Profile");
        }
            // Redirect to the login page if the user is not authenticated
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult ChangePhoneNumber( string newNumber, string repeatNewNumber)
        {
            // Call your UserRepo to get the user based on userEmail
            Console.WriteLine("got phone:"+newNumber);
            Console.WriteLine("got repeat phone:"+repeatNewNumber);
            string userEmail = HttpContext.Session.GetString("Email");
            User user = _userRepo.FindUserByEmail(userEmail);

            if(newNumber!=repeatNewNumber)
            {
                // Old password doesn't match
                //ModelState.AddModelError("oldPassword", "Old password is incorrect.");
                return View("Profile"); // or return RedirectToAction("Profile");
            }
    
            // Update the user's password in the database
            user.PhoneNumber = newNumber;
            _userRepo.UpdatePhoneNumber(userEmail, newNumber);
            SendMail(userEmail, "Dėl pakeisto Telefono numerio", "Jūsų "+ user.UserName +" CS2 paskyros telefono numeris buvo pakeistas");
       
            // Optionally, you might want to redirect to a success page or display a success message
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult ChangeAddress( string newAddress)
        {
            // Call your UserRepo to get the user based on userEmail
            Console.WriteLine("got phone:"+newAddress);
            string userEmail = HttpContext.Session.GetString("Email");
            User user = _userRepo.FindUserByEmail(userEmail);

    
            // Update the user's password in the database
            user.Address = newAddress;
            _userRepo.UpdateAddress(userEmail, newAddress);
            SendMail(userEmail, "Dėl pakeisto adreso", "Jūsų "+ user.UserName +" CS2 paskyros pristatymo adresas buvo pakeistas");

            // Optionally, you might want to redirect to a success page or display a success message
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult RemoveAccount()
        {
            string userEmail = HttpContext.Session.GetString("Email");

            // Delete the user from the database
            SendMail(userEmail, "Dėl ištrintos paskyros", "Jūsų "+ _userRepo.FindUserByEmail(userEmail).UserName +" CS2 paskyra buvo ištrinta. \n Linkime gero gyvenimo ir lauksime sugrįžtant!");
        
            _userRepo.DeleteUser(userEmail);

            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to a logout or home page
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            // Clear user-related session data
            HttpContext.Session.Clear();
        
            // Redirect to the home page or any other desired page
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        

    }
}
