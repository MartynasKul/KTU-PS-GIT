using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Http;
using WebApplication4.Repositories;

namespace WebApplication4.Controllers
{


    public class CartController : Controller
    {
        List<Item> items = new List<Item>();
        Cart cart = new Cart();

        public void ReadItems()
        {

         items.AddRange(ItemRepo.GetAllItems());

        }

        public Item GetItemByName(string name)
        {
            ReadItems();
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.Equals(name))
                {
                    return items[i];
                }

            }
            return null;
        }

        public Item GetItem(int id, string name)
        {
            ReadItems();
            if (name == null || id <= 0)
            {
                throw new ArgumentNullException(nameof(name));
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.Equals(name) && items[i].Id == id)
                {
                    return items[i];
                }
            }
            return null;
        }

        public IActionResult AddToCart(int id, string name)
        {
            if (name == null || id < 0)
            {
                return BadRequest("Item not found");
            }
            Item item = GetItem(id, name);
            if (item == null)
            {
                return BadRequest("Item not found");
            }

            UpdateCart(item);

            return Ok();
        }

        public void InitializeSession()
        {
            if (!HttpContext.Session.Keys.Contains("cart"))
            {
                HttpContext.Session.SetString("cart", cart.SerializeCart());
                return;
            }
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
        }

        public void UpdateCart(Item item)
        {
            ReadItems();
            InitializeSession();
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            cart.Add(item, 1);
            HttpContext.Session.SetString("cart", cart.SerializeCart());
        }

        public IActionResult Cart()
        {
            InitializeSession();
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            return View(cart);
        }


        public IActionResult ChangeCartItemDetails(string productName, int amount)
        {
            if (amount <= 0)
            {
                amount = 1;
            }
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            Item item = GetItemByName(productName);       
            cart.Update(item, amount);
            HttpContext.Session.SetString("cart", cart.SerializeCart());
            return RedirectToAction("Cart");
        }

        public IActionResult DeleteFromCart(int id)
        {
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            Item item = cart.Items.Keys.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetString("cart", cart.SerializeCart());
            }
            else
            {
                // Log an error message to help diagnose the issue
                Console.Error.WriteLine("Failed to remove item from cart: " + id);
            }
            return RedirectToAction("Cart");
        }

        public IActionResult DeleteFromCartAll(int id)
        {
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));
            cart.RemoveAll();
            HttpContext.Session.SetString("cart", cart.SerializeCart());
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> DownloadCartAsPdf()
        {
            InitializeSession();
            cart.DeserializeCart(HttpContext.Session.GetString("cart"));

            MemoryStream memoryStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfPTable table = new PdfPTable(5);
            table.AddCell("Picture");
            table.AddCell("Name");
            table.AddCell("Price");
            table.AddCell("Amount");
            table.AddCell("Total");

            decimal totalSum = 0;

            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < cart.Size(); i++)
                {
                    byte[] imageData = await client.GetByteArrayAsync(cart.Get(i).Picture);
                    Image img = Image.GetInstance(imageData);
                    PdfPCell cell = new PdfPCell(img, true);
                    table.AddCell(cell);
               
                    table.AddCell(new PdfPCell(new Phrase(cart.Get(i).Name)));
                    table.AddCell(new PdfPCell(new Phrase(cart.Get(i).Price.ToString("0.00"))));
                    table.AddCell(new PdfPCell(new Phrase(cart.GetAmount(i).ToString())));
                    decimal itemTotal = cart.Get(i).Price * cart.GetAmount(i);
                    totalSum += itemTotal;
                    table.AddCell(new PdfPCell(new Phrase(itemTotal.ToString("0.00"))));
                }
            }

            PdfPCell totalCell = new PdfPCell(new Phrase("Total Sum", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
            totalCell.Colspan = 4;
            table.AddCell(totalCell);
            PdfPCell totalSumCell = new PdfPCell(new Phrase(totalSum.ToString("0.00")));
            table.AddCell(totalSumCell);

            document.Add(table);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            return File(bytes, "application/pdf", "cart.pdf");
        }
    }
}
