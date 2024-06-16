using System;

namespace LD4_10_MKuliesius.AppCode
{
    public class Fridge : Device, IComparable<Device>, IEquatable<Fridge>
    {
        //Saldytuvas brangus jei kainuoja virs 1000 . Šaldytuvus rikiuokite pagal aukštį
        public int Capacity { get; set; }
        public string MountType { get; set; }
        public bool HasFreezer { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }

        public Fridge() :base() { }
        public Fridge(string maker, string model, string energyClass, string colour, decimal price, string type, int capacity, string mountType, bool hasFreezer, double height, double width, double depth) : base(maker, model,energyClass,colour,price, type)
        { 
            Capacity = capacity;
            MountType = mountType; 
            HasFreezer = hasFreezer;
            Height = height;
            Width = width;
            Depth = depth;
        }

        public override int CompareTo(Device other)
        {
            if(other is Fridge fridge) 
            {
                return Height.CompareTo(fridge.Height);
            }
            return 0;
        }

       public override bool Equals(Device other)
       {
           return Maker.Equals(other.Maker);
       }

        public bool Equals(Fridge other)
        {
            return MountType.Equals(other.MountType);
        }
        public override string ToString()
        {
            return Maker + " " + Model + " " + EnergyClass + " " + Colour + " " + Price + "e " + Capacity + "L " + MountType + " " + HasFreezer + " " + Height + "m " + Width + "m " + Depth + "m ";
        }
    }
}