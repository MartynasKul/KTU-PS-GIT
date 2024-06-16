using System;

namespace LD4_10_MKuliesius.AppCode
{
    public class Kettle: Device, IComparable<Device>, IEquatable<Kettle>
    {
        // virdulys brangus jei kainuoja virs 50, o virdulius rikiuokite pagal galią
        public int Power { get; set; }
        public double Capacity {  get; set; }

        public Kettle() : base() { }
        public Kettle(string maker, string model, string energyClass, string colour, decimal price, string type, int power, double capacity) : base(maker, model, energyClass, colour,price, type) 
        {
            Power = power;
            Capacity = capacity;
        }

        public override int CompareTo(Device other)
        {
            if (other is Kettle kettle)
            {
                return Power.CompareTo(kettle.Power);
            }
            return 0;
        }

        public override bool Equals(Device other)
        {
            return Price.Equals(other.Price);
        }

        public bool Equals(Kettle other)
        {
            return Capacity.Equals(other.Capacity);
        }
        public override string ToString()
        {
            return Maker + " " + Model + " " + EnergyClass + " " + Colour + " " + Price + "e "+ Power +"W " + Capacity +"L ";
        }
    }
}