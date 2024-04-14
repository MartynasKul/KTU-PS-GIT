using System.Security.Cryptography;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class UserRepo
    {
        private readonly AuthenticationService _authenticationService;


        public static User FindUserById(int userId)
        {
            var query = $@"SELECT * FROM `users` WHERE UserID = ?userId";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?userId", userId);
            });

            try
            {
                var result = Sql.MapOne<User>(drc, (dre, t) =>
                {
                    t.ID = dre.From<int>("UserID"); // Use nullable types for fields that can be NULL
                    t.Name = dre.From<string>("Name");
                    t.SurName = dre.From<string>("Surname");
                    t.Password = dre.From<string>("Password");
                    t.Email = dre.From<string>("Email");
                    t.Points = (int)dre.From<int?>("Points"); // Use nullable types for fields that can be NULL
                    t.UserName = dre.From<string>("Username");
                    t.UserType = dre.From<int>("UserType"); // Use nullable types for fields that can be NULL
                    t.Salt = dre.From<string>("Salt"); // Include salt in the mapping
                    t.PhoneNumber = dre.From<string>("PhoneNumber");
                    t.Address = dre.From<string>("Address");
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mapping user: {ex.Message}");
                Console.WriteLine($"SQL query result: {drc}");
                Console.WriteLine("User with such ID '{0}' not found", userId);
                return new User();
            }
        }

        public User FindUserByUsername(string username)
        {
            //Console.WriteLine("FindUserByEmail: "+email);
            if(username==null){Console.WriteLine("neperduota informacija");}
            var query = $@"SELECT * FROM `users` WHERE Username = ?username";
            //var query = $@"SELECT * FROM `users` WHERE LOWER(Email) = LOWER(?email)";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?username", username);
            });
            //Console.WriteLine($"Searching for user with email: {email}");
            try
            {
                var result = Sql.MapOne<User>(drc, (dre, t) =>
                {
                    t.ID = dre.From<int>("UserID"); // Use nullable types for fields that can be NULL
                    t.Name = dre.From<string>("Name");
                    t.SurName = dre.From<string>("Surname");
                    t.Password = dre.From<string>("Password");
                    t.Email = dre.From<string>("Email");
                    t.Points = (int)dre.From<int?>("Points"); // Use nullable types for fields that can be NULL
                    t.UserName = dre.From<string>("Username");
                    t.UserType = dre.From<int>("UserType"); // Use nullable types for fields that can be NULL

                    t.Salt = dre.From<string>("Salt"); // Include salt in the mapping
                    t.PhoneNumber = dre.From<string>("PhoneNumber");
                    t.Address=dre.From<string>("Address");
                      // Convert VARBINARY salt to string
                    
                });
                //Console.WriteLine("User found: " + result.Name); // Output the found user's name
                //Console.WriteLine("Salty user: " + result.Salt); // Output the found user's name
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mapping user: {ex.Message}");
                Console.WriteLine($"SQL query result: {drc}");
                Console.WriteLine("User with such username '{0}' not found", username);
                return new User();
            }
        }

        public  User FindUserByEmail(string email)
        {
            //Console.WriteLine("FindUserByEmail: "+email);
            if(email==null){Console.WriteLine("neperduota informacija");}
            var query = $@"SELECT * FROM `users` WHERE Email = ?email";
            //var query = $@"SELECT * FROM `users` WHERE LOWER(Email) = LOWER(?email)";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?email", email);
            });
            //Console.WriteLine($"Searching for user with email: {email}");
            try
            {
                var result = Sql.MapOne<User>(drc, (dre, t) =>
                {
                    t.ID = dre.From<int>("UserID"); // Use nullable types for fields that can be NULL
                    t.Name = dre.From<string>("Name");
                    t.SurName = dre.From<string>("Surname");
                    t.Password = dre.From<string>("Password");
                    t.Email = dre.From<string>("Email");
                    t.Points = (int)dre.From<int?>("Points"); // Use nullable types for fields that can be NULL
                    t.UserName = dre.From<string>("Username");
                    t.UserType = dre.From<int>("UserType"); // Use nullable types for fields that can be NULL

                    t.Salt = dre.From<string>("Salt"); // Include salt in the mapping
                    t.PhoneNumber = dre.From<string>("PhoneNumber");
                    t.Address=dre.From<string>("Address");
                    
                });
                //Console.WriteLine("User found: " + result.Name); // Output the found user's name
                //Console.WriteLine("Salty user: " + result.Salt); // Output the found user's name
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mapping user: {ex.Message}");
                Console.WriteLine($"SQL query result: {drc}");
                Console.WriteLine("User with such email '{0}' not found", email);
                return new User();
            }
        }

        public void UpdateUserPassword(User user)
        {
            var query = "UPDATE users SET Password = @Password WHERE Email = @Email";

            Sql.Query(query, args =>
            {
                args.Add("?Password", user.Password);
                args.Add("?Email", user.Email);
                // Add other parameters if needed (e.g., Name, Surname, etc.)
            });
        }

        public void AddUser(User newUser)
        {
            //Console.WriteLine(newUser.UserName);
            //Console.WriteLine(newUser.Name);
            //Console.WriteLine(newUser.Password);
            //Console.WriteLine(newUser.Name);
            //Console.WriteLine(newUser.Email);
            //Console.WriteLine(newUser.Salt);

            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Convert the byte array to a Base64-encoded string
            string salt = Convert.ToBase64String(saltBytes);
            //string salt = "8jM45Bq9BJBTrSxJvr3X5g=";
            //string psw =_authenticationService.HashPassword(newUser.Password,newUser.Salt);
            // Hash the password using PBKDF2 with 10000 iterations
           /* using (var pbkdf2 = new Rfc2898DeriveBytes(newUser.Password, Convert.FromBase64String(salt), 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                newUser.Password = Convert.ToBase64String(hash);
                newUser.Salt = salt;
            }*/
            newUser.Salt = salt;
           // newUser.Password=psw;

            var query = @"INSERT INTO `users` (Name, Surname, Password, Salt, Email, Username) 
                  VALUES (?, ?, ?, ?, ?, ?)";

            Sql.Query(query, args =>
            {
                //Console.WriteLine(newUser.Name + newUser.Password);
                args.Add("?Name", newUser.Name);
                args.Add("?Surname", newUser.SurName);
                args.Add("?Password", newUser.Password);
                args.Add("?Salt", newUser.Salt);
                args.Add("?Email", newUser.Email);
                args.Add("?Username", newUser.UserName);
                
            });
        }

        public void UpdatePhoneNumber(string email, string newPhoneNumber)
        {
            // Use SQL UPDATE statement to update the phone number for the user with the specified email
            var query = "UPDATE users SET PhoneNumber = ?NewPhoneNumber WHERE Email = ?Email";

            Console.WriteLine("got mail: " +email);
            Console.WriteLine("got phone: " +newPhoneNumber);
            Sql.Query(query, args =>
            {
                args.Add("?NewPhoneNumber", newPhoneNumber);
                args.Add("?Email", email);
            });
        }

        public void UpdateAddress(string email, string newAddress)
        {
            // Use SQL UPDATE statement to update the phone number for the user with the specified email
            var query = "UPDATE users SET Address = ?Address WHERE Email = ?Email";

            Console.WriteLine("got mail: " +email);
            Console.WriteLine("got phone: " +newAddress);
            Sql.Query(query, args =>
            {
                args.Add("?Address", newAddress);
                args.Add("?Email", email);
            });
        }



        public void DeleteUser(string email)
        {
            var query = "DELETE FROM `users`WHERE Email = ?Email";

            Sql.Query(query, args =>
            {
                args.Add("?Email", email);
            });
        }
        

        public static List<User> GetAllUsers()//cia reiks pakeisti priklausomai nuo duomenu lenteles , kokia bus users visa lentele kad viska grazintu , nes naudojamas Promote items dalyke kur siuncia visiem useriam i ju emailus laiskus apie itemus
        {
            var query = "SELECT * FROM `users`";

            var users = new List<User>();

            Sql.MapAll<User>(Sql.Query(query), (extractor, user) =>
            {
                user.ID = extractor.From<int>("id");
                user.Name = extractor.From<string>("Name");
                user.SurName = extractor.From<string>("Surname");
                user.Password = extractor.From<string>("Password");
                user.Email = extractor.From<string>("Email");
                user.UserName = extractor.From<string>("Username");

                users.Add(user);
            });

            return users;
        }

        public static void AddPointsToUser(int userId, int pointsToAdd)
        {
            var query = @"UPDATE `users` SET Points = Points + ?pointsToAdd WHERE UserID = ?userId";

            Sql.Query(query, args =>
            {
                args.Add("?pointsToAdd", pointsToAdd);
                args.Add("?userId", userId);
            });
        }

        public bool AuthenticateUser(string enteredEmail, string enteredPassword)
        {
            // Retrieve user from the database based on the entered email
            User userFromDatabase = FindUserByEmail(enteredEmail);

            if (userFromDatabase != null)
            {
                // Authenticate the entered password
                return AuthenticatePassword(enteredPassword, userFromDatabase);
            }

            // User not found
            return false;
        }

        private bool AuthenticatePassword(string enteredPassword, User userFromDatabase)    
        {
            // Combine entered password with stored salt
            string combinedPassword = enteredPassword + userFromDatabase.Salt;

            // Hash the combined password
            using (var pbkdf2 = new Rfc2898DeriveBytes(combinedPassword, Convert.FromBase64String(userFromDatabase.Salt), 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                string hashedPassword = Convert.ToBase64String(hash);

                // Compare hashed password with stored hashed password
                return hashedPassword == userFromDatabase.Password;
            }
        }
    }
}
