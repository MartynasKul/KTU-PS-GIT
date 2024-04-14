using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using System.Globalization;
using WebApplication4.Repositories;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Security.Cryptography.Xml;
using PayPal.Api;

namespace WebApplication4.Controllers
{


    public class OrderController : Controller
    {   
        Cart cart = new Cart();
        private readonly IConfiguration _configuration;
        private readonly UserRepo _userRepo;

        public OrderController( IConfiguration configuration, UserRepo userRepo)
        {       
            _configuration = configuration;
            _userRepo = userRepo;
        }
        public IActionResult CreateOrder()
        {
            /*if (!HttpContext.Session.Keys.Contains("username"))
            {
                return View("~/Views/Login/Login.cshtml");
            }*/
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));

            return View(cart);
        }

        public IActionResult AddOrder(string Address)
        {
            Order order = new Order();
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            foreach (KeyValuePair<Item, int> item in cart.Items)
            {
                order.Add(item.Key, item.Value);
            }
            string currency = "EUR";           
            order.Price = cart.Price();
            User user = _userRepo.FindUserByUsername(HttpContext.Session.GetString("Username"));
            order.User = user;
            //order.User1 = 1;
            order.Adress = Address;
            order.Status = "Processing";
            order = OrderRepo.InsertOrder(order);           
            var config = new Dictionary<string, string>
            {
                { "mode", "sandbox" }, // "live" or "sandbox"
                { "clientId", _configuration["PayPal:ClientId"] },
                { "clientSecret", _configuration["PayPal:ClientSecret"] }
            };

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Order payment",
                        invoice_number = order.ID.ToString(),
                        amount = new Amount
                        {   currency = currency,
                            total = order.Price.ToString("F2",CultureInfo.InvariantCulture)
                        },
                        item_list = new ItemList
                        {
                            items = order.items.Select(i => new PayPal.Api.Item
                            {
                                name = i.Key.Name,
                                currency = currency,
                                price = i.Key.Price.ToString("F2",CultureInfo.InvariantCulture),
                                quantity = i.Value.ToString(),
                                sku = i.Key.Id.ToString()
                            }).ToList()
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = Url.Action("ExecutePayment", "Order", null, Request.Scheme),
                    cancel_url = Url.Action("CancelPayment", "Order", null, Request.Scheme)
                }
            };
            
            var createdPayment = payment.Create(apiContext);
        
            var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;

            if (approvalUrl != null)
            {
                return Redirect(approvalUrl);

            }
            else
            {
                //failure
                order.Status = "Failed";
                OrderRepo.InsertOrder(order);
                return RedirectToAction("Index", "Home");

            }
        }

        public IActionResult ExecutePayment(string paymentId, string PayerID)
        {
            var config = new Dictionary<string, string>


        {
            { "mode", "sandbox" }, // "live" or "sandbox"
            { "clientId", _configuration["PayPal:ClientId"] },
            { "clientSecret", _configuration["PayPal:ClientSecret"] }
        };

            Order order = OrderRepo.GetLastOrder(); 
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var paymentExecution = new PaymentExecution
            {
                payer_id = PayerID
            };
            var payment = new Payment { id = paymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            if (executedPayment.state.ToLower() == "approved")
            {
                //success
                order.Status = "Success";
                TempData["Message"] = "Order successfully placed!";
                OrderRepo.InsertOrder(order);
                cart.DeserializeCart(HttpContext.Session.GetString("cart"));
                cart.RemoveAll();
                HttpContext.Session.SetString("cart", cart.SerializeCart());

            }
            else
            {
                //failure
                order.Status = "Failed";
                TempData["Message"] = "Order placement failed. Please try again.";
                OrderRepo.InsertOrder(order);
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult CancelPayment()
        {
            Order order = OrderRepo.GetLastOrder(); 
            order.Status = "Cancelled";
            OrderRepo.InsertOrder(order);
            TempData["Message"] = "Order Cancelled";
            return RedirectToAction("Index", "Home");
        }
    }
}
