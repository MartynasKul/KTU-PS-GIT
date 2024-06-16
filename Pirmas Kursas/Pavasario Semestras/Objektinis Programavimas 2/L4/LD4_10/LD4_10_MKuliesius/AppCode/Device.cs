using System;

namespace LD4_10_MKuliesius.AppCode
{
    public abstract class Device: IComparable<Device>, IEquatable<Device>
    {
        public string Maker {  get; set; }
        public string Model { get; set; }
        public string EnergyClass { get; set; }
        public string Colour { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } 

        public Device() { }
        public Device(string maker, string model, string energyClass, string colour, decimal price, string type)
        { 
            Maker = maker;
            Model = model;
            EnergyClass = energyClass;
            Colour = colour;
            Price = price;
            Type = type;
        }

        public abstract int CompareTo(Device other);
        public abstract bool Equals(Device other);
        public override int GetHashCode()
        {
            return base.GetHashCode();  
        }
    }
}