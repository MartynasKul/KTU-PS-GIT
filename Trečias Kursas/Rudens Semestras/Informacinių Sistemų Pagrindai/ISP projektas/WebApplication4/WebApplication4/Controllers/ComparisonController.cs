// ComparisonController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Controllers
{
    public class ComparisonController : Controller
    {
        private readonly List<Item> items = ItemRepo.GetAllItems();
        private readonly Comparison comparison = new Comparison();

        public IActionResult AddToComparison(int id)
        {
            Item item = GetItemById(id);

            // Check if the comparison already has two items
            if (comparison.Size() < 2)
            {
                UpdateComparison(item);
            }
            else
            {
                // Log an error message or handle the case where more than two items are being added
                Console.Error.WriteLine("Cannot add more than two items to the comparison.");
            }

            return RedirectToAction("Index", "Home");
        }

        public void InitializeSession()
        {
            if (!HttpContext.Session.Keys.Contains("comparison"))
            {
                HttpContext.Session.SetString("comparison", comparison.SerializeComparison());
               // Console.WriteLine("Initialized session with Comparison: " + HttpContext.Session.GetString("comparison"));
                return;
            }

           // Console.WriteLine("Session already contains Comparison: " + HttpContext.Session.GetString("comparison"));
            comparison.DeserializeComparison(HttpContext.Session.GetString("comparison"));
        }

        public void UpdateComparison(Item item)
        {
            ReadItems();
            InitializeSession();
            comparison.DeserializeComparison(HttpContext.Session.GetString("comparison"));
            comparison.Add(item);
            HttpContext.Session.SetString("comparison", comparison.SerializeComparison());
           // Console.WriteLine("Updated Comparison Size: " + comparison.Size());
        }

        public IActionResult Comparison()
        {
            InitializeSession();
            comparison.DeserializeComparison(HttpContext.Session.GetString("comparison"));
           // Console.WriteLine("Comparison Size: " + comparison.Size());

            return View(comparison);
        }

        public IActionResult DeleteFromComparison(int itemId)
{
    comparison.DeserializeComparison(HttpContext.Session.GetString("comparison"));
    // Assuming you want to remove all items from the comparison
    comparison.Items.Clear();
    HttpContext.Session.SetString("comparison", comparison.SerializeComparison());
    return RedirectToAction("Comparison");
}

        private Item GetItemById(int itemId)
        {
            

            return items.FirstOrDefault(item => item.Id == itemId);
        }

        private void ReadItems()
        {
            // You might need to fetch items from a repository or database
            // For simplicity, I'm assuming you have a GetAllItems method in ItemRepo
            // Replace this with your actual logic
            items.Clear();
            items.AddRange(ItemRepo.GetAllItems());
        }
    }
}
