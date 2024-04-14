using Microsoft.AspNetCore.Mvc;
using WebApplication4.Repositories;
using WebApplication4.Models;
using Newtonsoft.Json;

namespace WebApplication4.Controllers
{
    public class ItemController : Controller
    {
         private readonly List<Item> items = new List<Item>();
         private readonly IEmailSender _emailSender;
         
        public ItemController()
        {
            
            // Initialize items (you might load this from a database in a real application)
            ReadItems();
        }
        public async Task<IActionResult> Index()
        {
            var receiver = "tomass.sakalauskas@gmail.com";
            var subject = "Test";
            var message = "Heelo niggaaa";
            await _emailSender.SendEmailAsync(receiver, subject, message);

            return View("~/Views/Home/Index.cshtml");
        }
        private void ReadItems()
        {
                    items.AddRange(ItemRepo.GetAllItems());

        }  
        public IActionResult EditItem(int id)
        {  
            var item =items.Find(item => item.Id == id);
            return View("~/Views/Item/Edit.cshtml", item);
        }
        public Item GetItemById(int id)
        {
            var item = items.Find(item => item.Id == id);
            return item;
        }
        public IActionResult Item()
        {
            return View();

        }
        public IActionResult Add(Item item)
        {
            // Basic validation: Check if mandatory properties are provided
            if (string.IsNullOrEmpty(item.Name) ||
                string.IsNullOrEmpty(item.Picture) ||
                item.Price <= 0 ||
                string.IsNullOrEmpty(item.Svoris) ||
                string.IsNullOrEmpty(item.Pagaminimo_data))
            {
                ViewBag.Message = "Please fill in all mandatory fields.";
                return View("~/Views/Item/Add.cshtml", item);
            }
    

            try
            {
                // Here you might validate and process the item data, and then insert into the database
                // You may also handle the optional properties that were not filled by the user
                ItemRepo.InsertItem(
                    item.Name,
                    item.Picture,
                    (double)item.Price,
                    item.Svoris,
                    item.Pagaminimo_data,
                    item.FK_CategoryID,
                    item.FK_ManufacturerID,
                    item.Serija,
                    item.Spalva,
                    item.Plotis,
                    item.Aukstis,
                    item.Ilgis,
                    item.Garantija,
                    item.Ekrano_dydis,
                    item.Operacine_sistema,
                    item.Procesorius,
                    item.Kietasis_diskas,
                    item.Vaizdo_plokste,
                    item.Jungtys,
                    item.Baterija,
                    item.Papildoma_info,
                    item.Procesoriaus_karta
                );

                    ViewBag.Message = "Item successfully added!";
            }
            catch (Exception ex)
            {
                    ViewBag.Message = $"Error adding the item: {ex.Message}";
                    
            }

            // Return to the same view with the success or error message
            return View("~/Views/Item/Add.cshtml", item);

        }
        
        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            // Perform deletion action
            ItemRepo.DeleteItem(id);

            // You can provide any feedback to the user if needed
            return Ok(); // Indicates a successful operation
        }
        [HttpPost]
        public IActionResult Update(Item item)
{
    // Basic validation: Check if mandatory properties are provided
    if (item.Id <= 0 ||
        string.IsNullOrEmpty(item.Name) ||
        string.IsNullOrEmpty(item.Picture) ||
        item.Price <= 0 ||
        string.IsNullOrEmpty(item.Svoris) ||
        string.IsNullOrEmpty(item.Pagaminimo_data))
    {
        TempData["ErrorMessage"] = "Please provide a valid item ID and fill in all mandatory fields.";
        return View("~/Views/Item/Edit.cshtml", item);
    }

    try
    {
        // Here you might validate and process the item data, and then update the database
        ItemRepo.UpdateItem(item);

        TempData["SuccessMessage"] = "Item successfully updated!";
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"Error updating the item: {ex.Message}";
    }

    // Return to the same view with the success or error message
    return View("~/Views/Item/Edit.cshtml", item);
}
    













    }
}