namespace Laboratorinis1
{
    /// <summary>
    /// Museum class that stores data about the museum object
    /// </summary>
    internal class Museum
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public int Mon { get; set; }
        public int Tues { get; set; }
        public int Wednes { get; set; }
        public int Thurs { get; set; }
        public int Fri { get; set; }
        public int Sat { get; set; }
        public int Sun { get; set; }
        public double Price { get; set; }
        public string Guided { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> name of musem </param>
        /// <param name="city"> city that is the museum in </param>
        /// <param name="type"> type of the museum </param>
        /// <param name="mon"> monday data </param>
        /// <param name="tues"> tuesday data </param>
        /// <param name="wednes"> wednesday data </param>
        /// <param name="thurs"> thursday data </param>
        /// <param name="fri"> friday data </param>      
        /// <param name="sat"> saturday data </param>
        /// <param name="sun"> sunday data </param>
        /// <param name="price"> price of the ticket </param>
        /// <param name="guided"> does the museum have a guide </param>
        /// If the museum works on that day it is written as 1, if it doesnt work - as 0
        public Museum(string name, string city, string type,
        int mon, int tues, int wednes, int thurs,
        int fri, int sat, int sun, double price, string guided)
        {
            this.Name = name;
            this.City = city;
            this.Type = type;
            this.Mon = mon;
            this.Tues = tues;
            this.Wednes = wednes;
            this.Thurs = thurs;
            this.Fri = fri;
            this.Sat = sat;
            this.Sun = sun;
            this.Price = price;
            this.Guided = guided;
         }
    }
}
