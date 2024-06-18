using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using portal_backend.Entities;
using portal_backend.models;

namespace portal_backend.Services;

public class AuthService
{
    private readonly string _staticSalt = "TGwXdJ44";

    private readonly RvipsContext _rvipsContext;

    public AuthService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public int RegisterUser(UserRegistrationRequest request)
    {
        if (!ValidateEmail(request.Email)) throw new Exception("Not valid email");
        if (!ValidatePassword(request.Password)) throw new Exception("Not valid password");

        var existingUser = _rvipsContext.Users.FirstOrDefault(user => user.Email == request.Email);
        if (existingUser != null) throw new Exception("User already exists");

        var dynamicSalt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            LastName = request.Surname,
            Password = ComputeSha256Hash(request.Password + _staticSalt + dynamicSalt),
            CreationDate = DateTimeOffset.Now,
            Salt = dynamicSalt
        };
        _rvipsContext.Users.Add(user);
        _rvipsContext.SaveChanges();

        return user.Id;
    }


    public void ChangeUserPassword(int userId, UserChangePasswordRequest request)
    {
        if (!ValidatePassword(request.NewPassword)) throw new Exception("Not valid password");

        var existingUser = _rvipsContext.Users.FirstOrDefault(user => user.Id == userId);
        if (existingUser == null) throw new Exception("User doesn't exist");

        var dynamicSalt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        existingUser.Password = ComputeSha256Hash(request.NewPassword + _staticSalt + dynamicSalt);
        existingUser.Salt = dynamicSalt;
      
        _rvipsContext.Users.Update(existingUser);
        _rvipsContext.SaveChanges();
    }

    public User? LoginUser(string email, string password)
    {
        var existingUser = _rvipsContext.Users.FirstOrDefault(user => user.Email == email);
        if (existingUser == null) throw new Exception("User not found");

        var passwordHash = ComputeSha256Hash(password + _staticSalt + existingUser.Salt);

        return existingUser.Password.Equals(passwordHash) ? existingUser : null;
    }

    private static bool ValidateEmail(string email)
    {
        var emailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(email);
    }

    private static bool ValidatePassword(string password)
    {
        if (password.Length < 8) return false;

        var containsCapitalRegex = new Regex(@"[A-Z]");
        var containsNonCapitalRegex = new Regex(@"[a-z]");
        var containsNumberRegex = new Regex(@"[0-9]");
        var containsSpecialRegex = new Regex(@"[\p{P}\p{S}]");

        if (!containsCapitalRegex.Match(password).Success) return false;
        if (!containsNonCapitalRegex.Match(password).Success) return false;
        if (!containsNumberRegex.Match(password).Success) return false;
        if (!containsSpecialRegex.Match(password).Success) return false;

        return true;
    }

    private static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using var sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array  
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        // Convert byte array to a string   
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}