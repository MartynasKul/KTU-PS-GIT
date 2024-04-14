using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Repositories;


namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        List<Item> items = new List<Item>();
        private readonly List<User> users = new List<User>();
        
        public void ReadUsers()
        {

            users.AddRange(UserRepo.GetAllUsers());

        }
        Cart cart = new Cart();
         private readonly IEmailSender _emailSender;

        public void ReadItems()
        {

        items.AddRange(ItemRepo.GetAllItems());

        }

        public IActionResult Store()
        {
           // ReadItems(); nereikia nes jau apacioj padarysim read items ir read users
            return View(items);
        }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,IEmailSender emailSender)
        {
            _logger = logger; ;
            _configuration = configuration;
            _emailSender = emailSender;
            ReadItems();
            //ReadUsers(); //veliau atkomentuoti kai bus susitvarkyta su useriais kad visiem useriam siustu emailus
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PromoteItems()
        {

            //Testinis variantas
            /* try  
             {
                 // Your logic to get email, subject, message
                 var userEmail = "tomass.sakalauskas@gmail.com";//kam siunciam
                 var subject = "Testukas mannnn";//laisko subject nu tai tema
                 var message = "Your Message for the niggas";//pats laiskas



                 await _emailSender.SendEmailAsync(userEmail, subject, message);

                 // Your remaining logic

                 // Return a success message
                 return Json(new { success = true, message = "Item promotion sent successfully!" });
             }
             catch (Exception ex)
             {
                 // Handle exceptions
                 return Json(new { success = false, message = "Error sending email: " + ex.Message });
             }*/
            //Tikrasis variantas, kai bus pilnai implementuota atkomentuoti :
                 try
            {
                // Your logic to get subject
                var subject = "Menesio pasiulymas !";

                // Get the earliest timestamp for the current month
                var earliestTimestamp = items
                    .Where(item => item.AddedTimestamp.Month == DateTime.Now.Month && item.AddedTimestamp.Year == DateTime.Now.Year)
                    .Min(item => item.AddedTimestamp);

                // Find the item with the earliest timestamp for the current month
                var itemWithEarliestTimestamp = items
                    .Where(item => item.AddedTimestamp == earliestTimestamp)
                    .FirstOrDefault();

                if (itemWithEarliestTimestamp != null)
                {
                    // Use the item's name as the message
                    var message = $"<div>Naujausia Menesio preke tik jums !</div>" +
                                  $"<div><img src='{itemWithEarliestTimestamp.Picture}' width='500' height='500'></div>" +
                                  $"<div style='font-size: 25px;'><strong>{itemWithEarliestTimestamp.Name}</strong></div>" +
                                  $"<div style='font-size: 20px;'>Price: {itemWithEarliestTimestamp.Price}</div>";


                    //<div><img src=@Model[i].Picture alt=@Model[i].Name></div>
                    // Iterate through all users and send emails
                    foreach (var user in users)
                    {
                        // Use the user's email for sending the email
                        var userEmail = user.Email;

                        // Call the SendEmailAsync method
                        await _emailSender.SendEmailAsync(userEmail, subject, message);
                    }
                    //var userEmail = "tomass.sakalauskas@gmail.com";//kam siunciam
                    // await _emailSender.SendEmailAsync(userEmail, subject, message);
                    // Your remaining logic

                    // Return a success message
                    return Json(new { success = true, message = "Item promotion sent successfully!" });
                }
                else
                {
                    // Handle the case where no items were added this month
                    return Json(new { success = false, message = "No items added this month." });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { success = false, message = "Error sending email: " + ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



       
    }
}