namespace LD5_10_MKuliesius.AppCode
{
    /// <summary>
    /// Class of publication publisher. Contains the publication code, publisher name, pricePerMoth and profit for month variables.
    /// </summary>
    public class Publication
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string PublisherName { get; set; }
        public decimal PricePerMonth { get; set; }
        public decimal ProfitMonth { get; set; }

        public Publication() { }
        public Publication(string code, string name, string publisherName, decimal price)
        {
            this.Code = code;
            this.Name = name;
            this.PublisherName = publisherName;
            this.PricePerMonth = price;
            ProfitMonth = 0;
        }
    }
}
