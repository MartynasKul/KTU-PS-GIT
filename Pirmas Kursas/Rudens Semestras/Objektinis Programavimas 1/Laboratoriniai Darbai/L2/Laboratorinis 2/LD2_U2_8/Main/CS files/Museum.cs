using System;

namespace U2_8
{
    class Museum
    {
        public string Name { get; set; }
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
        public int WorkingDays { get; set; }
        public string WeekEnder { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> name of musem</param>
        /// <param name="type"> type of museum </param>
        /// <param name="mon"> monday </param>
        /// <param name="tues"> tuesday </param>
        /// <param name="wednes"> wednesday </param>
        /// <param name="thurs"> thursday </param>
        /// <param name="fri"> friday </param>
        /// <param name="sat"> saturday </param>
        /// <param name="sun"> sunday </param>
        /// <param name="price"> price of ticket</param>
        /// <param name="guided"> does museum have a guide </param>
        /// <param name="WorkingDays"> how many days does museum work </param>
        /// <param name="WeekEnder"> string for checking if museum only works on weekends </param>

        public Museum(string name, string type,
            int mon, int tues, int wednes, int thurs,
            int fri, int sat, int sun, double price, string guided)
        {
            this.Name = name;
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

        /// <summary>
        /// Calculates how many days a week does a museum work
        /// </summary>
        public void CalculateWorkingDays()
        {
            int works = 0;
            works = Mon + Tues + Thurs + Wednes + Fri + Sat + Sun;

            WorkingDays = works;
        }

        /// <summary>
        /// Assigns value to weekend string if museum only works 
        /// </summary>
        public void CalculateWeekenders()
        {
            string weekend;

            if ((Mon + Tues + Thurs + Wednes + Fri) == 0 & (Sat + Sun > 0))
            {
                weekend = "Tik Savaitgaliais!";
            }
            else
            {
                weekend = "";
            }

            WeekEnder = weekend;
        }

        /// <summary>
        /// Overrides the method ToString() to return a string in a required format
        /// </summary>
        /// <returns>A string of information in a required format</returns>
        public override string ToString()
        {
            string line;
            line = string.Format(String.Format("| {0,-30} | {1,-10} | {2,6} | {3,6} | {4,7} |" +
                " {5,7}| {6,5} | {7,4} | {8,6} | {9,5}| {10,-11} |", Name, Type, Mon, Tues,
                Wednes, Thurs, Fri, Sat, Sun, Price, Guided));

            return line;
        }

        public static bool operator <(Museum museum1, Museum museum2)
        {
            return museum1.WorkingDays < museum2.WorkingDays;
        }
        public static bool operator >(Museum museum1, Museum museum2)
        {
            return museum1.WorkingDays > museum2.WorkingDays;
        }

        public static bool operator ==(Museum museum1, Museum museum2)
        {
            return museum1.WorkingDays == museum2.WorkingDays;
        }

        public static bool operator !=(Museum museum1, Museum museum2)
        {
            return museum1.WorkingDays != museum2.WorkingDays;
        }

        public static bool operator ==(Museum museum, int num)
        {
            return museum.Price == num;
        }

        public static bool operator !=(Museum museum, int num)
        {
            return museum.WorkingDays != num;
        }

        /// <summary>
        /// Overrides the method Equals() to compare 
        /// </summary>
        public override bool Equals(object obj)
        {

            return this.Name == ((Museum)obj).Name;
        }

        /// <summary>
        /// Security function
        /// </summary>
        public override int GetHashCode() 
        {
            return this.Name.GetHashCode();
        }
    }
}
