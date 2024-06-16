using System;

namespace LD2_10_MKuliesius.AppCode
{
    public class Worker : IComparable<Worker>, IEquatable<Worker>
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string DetailCode { get; set; }
        public int DetailProduced { get; set; }
        public int TotalDaysWorked { get; set; }
        public int TotalDetailsProduced { get; set; }
        public decimal TotalEarnings { get; set; }


        public Worker(string name, string date, string detailCode, int detailProduced)
        {
            Name = name;
            Date = date;
            DetailCode = detailCode;
            DetailProduced = detailProduced;
            TotalDaysWorked = 0;
            TotalDetailsProduced = 0;
            TotalEarnings = 0;

        }
        public Worker() { }


        public int CompareTo(Worker other) 
        {
            return this.Name.CompareTo(other.Name);
        }
        public int CompareTo(decimal sum)
        {
            return this.TotalEarnings.CompareTo(sum);
        }


        // Override ToString method to provide meaningful string representation
        public override string ToString()
        {
            return $"| {Date, 10} | {Name, 20} | {DetailCode, 10} | {DetailProduced, 8} | {TotalDaysWorked,10} | {TotalDetailsProduced,13} | {TotalEarnings,12} |";
        }

        public bool Equals(Worker other)
        {
            return this.Name.Equals(other.Name);
        }
        public bool Equals(string code) 
        {
            return this.DetailCode.Equals(code);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}