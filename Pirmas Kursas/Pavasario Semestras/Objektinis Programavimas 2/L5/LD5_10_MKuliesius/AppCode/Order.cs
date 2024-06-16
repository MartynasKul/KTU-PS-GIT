namespace LD5_10_MKuliesius.AppCode
{
    /// <summary>
    /// Class of each order(persons order information)
    /// </summary>
    public class Order
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int StartMonth { get; set; }
        public int Duration { get; set; }
        public string PublicationCode { get; set; }

        public Order() { }
        public Order(string name, string address, int startMonth, int duration, string publicationCode) 
        {
            this.Address = address;
            this.Name = name;
            this.StartMonth = startMonth;
            this.Duration = duration;
            this.PublicationCode = publicationCode;
        }

        public override string ToString() 
        {
            return Name;
        }
    }
}