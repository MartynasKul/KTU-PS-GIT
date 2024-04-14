using System.Security.Cryptography;
using WebApplication4.Models;
using WebApplication4.Repositories;
// password salt  = 8jM45Bq9BJBTrSxJvr3X5g=
public class AuthenticationService
{
    private readonly UserRepo _userRepo;
    

    public AuthenticationService(UserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public bool AuthenticateUser(string email, string password, out int userType)
    {
        // Retrieve user from the database based on the entered username
        User userFromDatabase = _userRepo.FindUserByEmail(email);
        Console.WriteLine(userFromDatabase.UserName);
        if(userFromDatabase.UserName == null)
        {
            Console.WriteLine("findByEmail method failed");
            userType = 0;

        }
        if (userFromDatabase != null)
        {
            // Authenticate the entered password
            userType= userFromDatabase.UserType;

            return AuthenticatePassword(password, userFromDatabase);
        }
        Console.WriteLine("User Not Found");
        // User not found
        userType=0;
        return false;
    }

    public  string HashPassword(string password, string salt )
    {
        //salt = "8jM45Bq9BJBTrSxJvr3X5g=";
        // Combine entered password with stored salt
        //string combinedPassword = password + salt;
        //return combinedPassword;

        // Hash the combined password
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000))
        {
            byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
            return Convert.ToBase64String(hash);
        }
    }


    private bool AuthenticatePassword(string password, User userFromDatabase)
    {
        // Hash the combined password
        string hashedPassword = HashPassword(password, userFromDatabase.Salt);

        Console.WriteLine("Got entered password: " + password);
        Console.WriteLine("Got salt: " + userFromDatabase.Salt);
        Console.WriteLine("Generated hashed password for comparison: "+ hashedPassword);
        Console.WriteLine("Got stored hashed password: "+ userFromDatabase.Password);

        // Compare hashed password with stored hashed password
        //return hashedPassword == userFromDatabase.Password;
        return password == userFromDatabase.Password;
    }
}
