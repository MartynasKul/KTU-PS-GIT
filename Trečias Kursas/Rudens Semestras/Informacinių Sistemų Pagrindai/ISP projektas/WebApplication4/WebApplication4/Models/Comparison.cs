// Comparison.cs
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WebApplication4.Models
{
    public class Comparison
    {
        public List<Item> Items { get; set; }

        public Comparison()
        {
            Items = new List<Item>();
        }

        public void Add(Item item)
        {
            if (Items.Count < 2 && !Items.Contains(item))
            {
                Items.Add(item);
            }
        }

        public void Remove(Item item)
        {
            Items.Remove(item);
        }

        public Item Get(int index)
        {
            return Items.ElementAt(index);
        }

        public int Size()
        {
            return Items.Count;
        }

        public string SerializeComparison()
        {
            int[] itemIds = Items.Select(item => item.Id).ToArray();
            return JsonSerializer.Serialize(itemIds);
        }

        public void DeserializeComparison(string serializedValue)
        {
            Items.Clear();

            int[] itemIds = JsonSerializer.Deserialize<int[]>(serializedValue);

            foreach (int itemId in itemIds)
            {
                Item item = GetItemById(itemId);

                if (item != null)
                {
                    Add(item);
                }
            }
        }

        private Item GetItemById(int itemId)
        {
            // Replace this with your logic to get an item by ID
            // For example, you might fetch the item from a database
            // or from a predefined list of items
            return new Item { Id = itemId, Name = "Sample Item" };
        }
    }
}
