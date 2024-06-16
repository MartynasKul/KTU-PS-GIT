using System;

namespace LD4_10_MKuliesius.AppCode
{
    public class Oven: Device, IComparable<Device>, IEquatable<Oven>
    {
        // Orkaite brangi jei kainuoja virs 500, mikrobangų krosneles rikiuokite pagal galingumą
        public int Power { get; set; }
        public int ProgramNum { get; set; }

        public Oven() : base() { }
        public Oven(string maker, string model, string energyClass, string colour, decimal price, string type, int power, int programNum) : base(maker, model, energyClass, colour, price, type) 
        {
            Power = power;
            ProgramNum = programNum;
        }

        public override int CompareTo(Device other)
        {
            if (other is Oven oven)
            {
                return Power.CompareTo(oven.Power);
            }
            return 0;
        }

        public override bool Equals(Device other)
        {
            return Maker.Equals(other.Maker);
        }

        public bool Equals(Oven other)
        {
            return ProgramNum.Equals(other.ProgramNum);
        }

        public override string ToString()
        {
            return Maker + " " + Model + " " + EnergyClass + " " + Colour + " " + Price + "e " + Power + "W " + ProgramNum;
        }
    }
}