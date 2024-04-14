using System;
using System.IO;
using System.Text;

namespace U5_8
{
    class InOut
    {
        /// <summary>
        /// Read function, reads from specified file
        /// </summary>
        public static Register ReadPlaces(string fileName)
        {
            Register Places = new Register();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);

            string line;
            Places.City = Lines[0];
            Places.Manager = Lines[1];

            for (int i = 2; i < Lines.Length; i++)
            {
                line = Lines[i];
                string[] Bits = line.Split(';'); // splits read line at ; and assigns to string array for separation
                string name = Bits[0];
                string adress = Bits[1];
                int dob = int.Parse(Bits[2]);

                int range = Bits.Length / 13; // 1-muziejus 0-statula

                switch (range)
                {
                    case 0:
                        {
                            string author = Bits[3];
                            string recipient = Bits[4];

                            Statue statue = new Statue(name, adress, dob, author, recipient);
                            Places.Add(statue);

                            break;
                        }
                    case 1:
                        {
                            string type = Bits[3];
                            int mon = int.Parse(Bits[4]);
                            int tues = int.Parse(Bits[5]);
                            int wednes = int.Parse(Bits[6]);
                            int thurs = int.Parse(Bits[7]);
                            int fri = int.Parse(Bits[8]);
                            int sat = int.Parse(Bits[9]);
                            int sun = int.Parse(Bits[10]);
                            double price = double.Parse(Bits[11]);
                            string guided = Bits[12];

                            Museum museum = new Museum(name, adress, dob, type, mon, tues,
                                wednes, thurs, fri, sat, sun, price, guided);

                            museum.CalculateWorkingDays(); // calculates how many days of the week certain museum works
                            museum.CalculateWeekenders(); // checks if the museum only works on weekends

                            Places.Add(museum);

                            break;
                        }
                }
            }
            return Places; // returns museum register
        }

        /// <summary>
        /// Prints museum information of container to screen
        /// </summary>
        public static void PrintPlacesToScreen(Register register, string header)
        {
            Console.WriteLine(header);
            Console.WriteLine();
            Console.WriteLine(register.City);
            Console.WriteLine(register.Manager);
            Console.WriteLine(new string('-', 171));
            Console.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-10} | {4,2} | {5,2} | {6,2} |" +
                " {7,2}| {8,2} | {9,2} | {10,6} | {11,5}| {12,-11} |"
           , "Pavadinimas", "Adresas", "Įkurta", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
            "Sest", "Sekmad", "Kaina", "Turi gidą"));
            Console.WriteLine(new string('-', 171));
            for (int i = 0; i < register.PlacesCount(); i++)
            {
                Place place = register.Get(i);

                if(place is Museum)
                {
                    Console.WriteLine((place as Museum).ToString());
                }
                else
                {
                    Console.WriteLine((place as Statue).ToString() + "   * Kūrėjas/Autorius | Kam skirta statula" + new string(' ', 16)+"|");
                }

            }
            Console.WriteLine(new string('-', 171));

            Console.WriteLine();
        }

        /// <summary>
        /// Prints place information of container to a specified .txt file
        /// </summary>
        public static void PrintMuseumsToTxt(string fileName, Register register, string header)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine();
                fn.WriteLine(register.City);
                fn.WriteLine(register.Manager);
                fn.WriteLine(new string('-', 171));
                fn.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-10} | {4,2} | {5,2} | {6,2} |" +
                    " {7,2}| {8,2} | {9,2} | {10,6} | {11,5}| {12,-11} |"
               , "Pavadinimas", "Adresas", "Įkurta", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą"));
                fn.WriteLine(new string('-', 171));

                for (int i = 0; i < register.PlacesCount(); i++)
                {
                    Place place = register.Get(i);
                    if (place is Museum)
                    {
                        fn.WriteLine((place as Museum).ToString());
                    }
                    else
                    {
                        fn.WriteLine((place as Statue).ToString() + "   * Kūrėjas/Autorius | Kam skirta statula" + new string(' ', 16) + "|");
                    }
                }
                fn.WriteLine(new string('-', 171));

                fn.WriteLine();
            }
        }

        /// <summary>
        /// Prints places info that were created after 1990s
        /// </summary>
        public static void PrintAfter1990s(string fileName, Register After90s, string header)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine();

                fn.WriteLine(new string('-', 171));
                fn.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-10} | {4,2} | {5,2} | {6,2} |" +
                    " {7,2}| {8,2} | {9,2} | {10,6} | {11,5}| {12,-11} |"
               , "Pavadinimas", "Adresas", "Įkurta", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą"));
                fn.WriteLine(new string('-', 171));

                for (int i = 0; i < After90s.PlacesCount(); i++)
                {
                    Place place = After90s.Get(i);
                    if (place is Museum)
                    {
                        fn.WriteLine((place as Museum).ToString());
                    }
                    else
                    {
                        fn.WriteLine((place as Statue).ToString() + "   * Kūrėjas/Autorius | Kam skirta statula" + new string(' ', 16) + "|");
                    }
                }
                fn.WriteLine(new string('-', 171));

                fn.WriteLine();
            }
        }

        /// <summary>
        /// Prints container information
        /// </summary>
        public static void PrintAll(string fileName, PlaceContainer AllPlace, string header)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine();

                fn.WriteLine(new string('-', 171));
                fn.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-10} | {4,2} | {5,2} | {6,2} |" +
                    " {7,2}| {8,2} | {9,2} | {10,6} | {11,5}| {12,-11} |"
               , "Pavadinimas", "Adresas", "Įkurta", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą"));
                fn.WriteLine(new string('-', 171));

                for (int i = 0; i < AllPlace.Count; i++)
                {
                    Place place = AllPlace.Get(i);
                    if (place is Museum)
                    {
                        fn.WriteLine((place as Museum).ToString());
                    }
                    else
                    {
                        fn.WriteLine((place as Statue).ToString() + "   * Kūrėjas/Autorius | Kam skirta statula" + new string(' ', 16) + "|");
                    }
                }
                fn.WriteLine(new string('-', 171));

                fn.WriteLine();
            }
        }
    }
}
