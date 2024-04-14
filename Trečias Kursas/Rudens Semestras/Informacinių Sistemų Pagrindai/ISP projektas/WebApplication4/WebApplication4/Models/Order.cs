using System;
using System.ComponentModel;
using WebApplication4.Models;

[Serializable]
public class Order
{
    [DisplayName("Order ID")]
    public int ID { get; set; }
    public User User { get; set; }

    public int User1 { get; set; }
    public string Adress { get; set; }

    [DisplayName("Price")]
    public decimal Price { get; set; }

    [DisplayName("Status")]
    public string Status { get; set; }
    public Dictionary<Item, int> items { get; set; }

    public Order()
    {
        this.items = new Dictionary<Item, int>();

    }

    public Dictionary<Item, int> get() { return this.items; }
    public void Add(Item item)
    {
        if (!items.ContainsKey(item))
        {
            items.Add(item, 1);
        }
        else
        {
            items[item]++;
        }
    }

    public void Add(Item item, int amount)
    {
        if (!items.ContainsKey(item))
        {
            items.Add(item, amount);
        }
        else
        {
            items[item] = items[item] + amount;
        }
    }

    public void Remove(Item item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;
            if (items[item] == 0)
            {
                items.Remove(item);
            }
        }
    }

    public bool Contains(Item item)
    {
        return items.ContainsKey(item);
    }

    public double TotalPrice()
    {
        double price = 0;
        Item[] keys = items.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            Item item = keys[i];
            price += (double)item.Price * items[item];
        }
        return price;
    }
    public double ItemPrice(Item item)
    {
        return (double)item.Price * items[item];
    }
    public int Count()
    {
        return items.Count;
    }
    public Item Get(int i)
    {
        return items.ElementAt(i).Key;
    }

    public int GetAmount(int i)
    {
        return items.ElementAt(i).Value;
    }
    public override string ToString()
    {
        return $"{ID}, {User.ID},  {Adress}, {Price.ToString("F2")}, {Status}";
    }

}


