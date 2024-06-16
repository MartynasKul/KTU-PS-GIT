using System;

namespace LD3_10_MKuliesius
{
    public class Detail : IComparable<Detail>, IEquatable<Detail>
    {
        public string Code { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Detail(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public int CompareTo(Detail other)
        {
            return Code.CompareTo( other.Code);
        }

        // Override ToString method to provide meaningful string representation
        public override string ToString()
        {
            return $"| {Name, 15} | {Code, 12} | {Price, 8} |";
        }

        public bool Equals(Detail other)
        {
            return this.Code.Equals(other.Code);
        }
        public bool Equals(string detailName) 
        {
            return this.Code.Equals(detailName);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}