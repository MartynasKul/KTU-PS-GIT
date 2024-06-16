using System.Collections.Generic;
using System.Linq;

namespace LD4_10_MKuliesius.AppCode
{
    public class TaskUtils
    {
        /// <summary>
        /// Task 1, calculate how many siemens devices there are
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public static int SiemensCount(List<DevicesContainer> shops) 
        {
            int count = 0;

            foreach (DevicesContainer shop in shops) 
            {
                for (int i = 0; i < shop.Count(); i++) 
                {
                    Device device = shop.Get(i);
                    if (device.Maker.Equals("Siemens")) 
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Task 2, returns the cheapest standing fridges that are freestadnign and have more than 80l capacity
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public static List<Fridge> StandingCheapestFridges(List<DevicesContainer> shops) 
        {
            return shops
                .SelectMany(shop => shop.GetAll())
                .OfType<Fridge>() // Ensure the device is a Fridge before checking further
                .Where(fridge => fridge.MountType == "Freestanding" && fridge.Capacity > 80) // Filters for Freestanding mount type and capacity > 80
                .OrderBy(fridge => fridge.Price)
                .Take(10)
                .ToList();
        }

        /// <summary>
        /// Finds devices that are only sold in a single store
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public static List<DevicesContainer> OnlyOneShop(List<DevicesContainer> shops) 
        {
            // Dictionary to count devices
            var deviceCounts = new Dictionary<(string Manufacturer, string Model), int>();

            // Fill the dictionary with device counts across all shops
            foreach (var shop in shops)
            {
                foreach (var device in shop.GetAll())
                {
                    var key = (device.Maker, device.Model);
                    if (deviceCounts.ContainsKey(key))
                    {
                        deviceCounts[key]++;
                    }
                    else
                    {
                        deviceCounts[key] = 1;
                    }
                }
            }

            // Now, create a list of containers to store only unique devices
            List<DevicesContainer> uniqueShops = shops.Select(_ => new DevicesContainer()).ToList();

            // Iterate through shops and add only unique items
            for (int i = 0; i < shops.Count; i++)
            {
                foreach (var device in shops[i].GetAll())
                {
                    var key = (device.Maker, device.Model);
                    if (deviceCounts[key] == 1)  // Only add device if it appears exactly once across all shops
                    {
                        uniqueShops[i].Add(device);
                    }
                }
            }

            return uniqueShops;  // This list will include empty containers if no unique items found in corresponding shop

        }

        /// <summary>
        /// Finds Most expensive items that are for sale in each store
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public static List<Device> ExpensiveDevices(List<DevicesContainer> shops)
        {
            List<Device> expensiveDevices = new List<Device>();

            // Aggregate all devices from all shops into a single list for easier processing
            List<Device> allDevices = shops.SelectMany(shop => shop.GetAll()).ToList();

            // Filtering and sorting Fridges
            var expensiveFridges = allDevices.OfType<Fridge>()
                                            .Where(f => f.Price > 1000)
                                            .OrderBy(f => f) // Using IComparable<Fridge>
                                            .ToList();

            // Filtering and sorting Ovens
            var expensiveOvens = allDevices.OfType<Oven>()
                                           .Where(o => o.Price > 500)
                                           .OrderBy(o => o) // Using IComparable<Oven>
                                           .ToList();

            // Filtering and sorting Kettles
            var expensiveKettles = allDevices.OfType<Kettle>()
                                             .Where(k => k.Price > 50)
                                             .OrderBy(k => k) // Using IComparable<Kettle>
                                             .ToList();

            // Add sorted lists to the final list
            expensiveDevices.AddRange(expensiveFridges);
            expensiveDevices.AddRange(expensiveOvens);
            expensiveDevices.AddRange(expensiveKettles);

            return expensiveDevices;
        }
    }
}