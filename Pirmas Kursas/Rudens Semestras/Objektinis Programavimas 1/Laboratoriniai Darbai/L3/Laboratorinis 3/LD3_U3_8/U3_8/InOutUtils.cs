using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace U3_8
{
    class InOutUtils
    {
        /// <summary>
        /// Read function, reads from specified file
        /// </summary>
        public static MuseumsRegister ReadMuseums(string fileName)
        {
            MuseumsRegister Museums = new MuseumsRegister();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);

            string line;
            Museums.City = Lines[0];
            Museums.Manager = Lines[1];

            for (int i = 2; i < Lines.Length; i++)
            {
                line = Lines[i];
                string[] Bits = line.Split(';'); // splits read line at ; and assigns to string array for separation
                string name = Bits[0];
                string type = Bits[1];
                int mon = int.Parse(Bits[2]);
                int tues = int.Parse(Bits[3]);
                int wednes = int.Parse(Bits[4]);
                int thurs = int.Parse(Bits[5]);
                int fri = int.Parse(Bits[6]);
                int sat = int.Parse(Bits[7]);
                int sun = int.Parse(Bits[8]);
                double price = double.Parse(Bits[9]);
                string guided = Bits[10];

                Museum museum = new Museum(name, type, mon, tues,
                    wednes, thurs, fri, sat, sun, price, guided); // new museum  creation

                museum.CalculateWorkingDays(); // calculates how many days of the week certain museum works
                museum.CalculateWeekenders(); // checks if the museum only works on weekends
                Museums.Add(museum); // adds new museum to museum register
            }
            return Museums; // returns museum register
        }

        /// <summary>
        /// Prints museum information of container to screen
        /// </summary>
        public static void PrintMuseumsToScreen(MuseumsRegister register, string header)
        {
            Console.WriteLine(header);
            Console.WriteLine();
            Console.WriteLine(register.City);
            Console.WriteLine(register.Manager);
            Console.WriteLine(new string('-', 128));
            Console.WriteLine(String.Format("| {0,-30} | {1,-10} | {2,6} | {3,6} | {4,7} |" +
            " {5,7}| {6,5} | {7,4} | {8,6} | {9,5}| {10,-11} |"
           , "Pavadinimas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
            "Sest", "Sekmad", "Kaina", "Turi gidą"));
            Console.WriteLine(new string('-', 128));
            for (int i = 0; i < register.MuseumsCount(); i++)
            {
                Museum museum = register.Get(i);
                Console.WriteLine(museum.ToString());
            }
            Console.WriteLine(new string('-', 128));

            Console.WriteLine();
        }

        /// <summary>
        /// Prints museum information of container to a specified .txt file
        /// </summary>
        public static void PrintMuseumsToTxt(string fileName, MuseumsRegister register, string header)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine();
                fn.WriteLine(register.City);
                fn.WriteLine(register.Manager);
                fn.WriteLine(new string('-', 128));
                fn.WriteLine(String.Format("| {0,-30} | {1,-10} | {2,6} | {3,6} | {4,7} |" +
                " {5,7}| {6,5} | {7,4} | {8,6} | {9,5}| {10,-11} |"
               , "Pavadinimas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą"));
                fn.WriteLine(new string('-', 128));
                for (int i = 0; i < register.MuseumsCount(); i++)
                {
                    Museum museum = register.Get(i);
                    fn.WriteLine(museum.ToString());
                }
                fn.WriteLine(new string('-', 128));

                fn.WriteLine();
            }
        }

        /// <summary>
        /// Prints list of museums to console register method
        /// </summary>
        public static void PrintMostWorkingMuseums(MuseumsRegister register, MuseumsContainer container) // prints museums in a table in console
        {
            //foreach (Museum museum in container)
            for (int i = 0; i < container.Count; i++)            
            {
                Museum museum = container.Get(i);
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |", register.Manager, museum.Name, register.City, museum.Type, museum.Mon, museum.Tues,
                 museum.Wednes, museum.Thurs, museum.Fri, museum.Sat, museum.Sun, museum.Price, museum.Guided);
            }

            Console.WriteLine(new string('-', 169));
        }

        /// <summary>
        /// Prints types of museums of a list
        /// </summary>
        public static void PrintMuseumTypes(List<string> CityTypes, MuseumsRegister register) // prints museums in a table in console
        {
            Console.WriteLine("Mieste {0} trečiadieniais galima aplankyti šiuos muziejų tipus:", register.City);
            foreach (string line in CityTypes)
            {
                Console.WriteLine("{0,-10} ", line);
            }

            Console.WriteLine(new string('-', 70));
        }

        /// <summary>
        /// Prints information of museums that have the same name to .csv file
        /// </summary>
        public static void PrintSameNamesToCSV(string fileName, MuseumsContainer SameNames, string header)
        {
            File.WriteAllText(fileName, header+"\n");

            for (int i = 0; i < SameNames.Count; i++)
            {
                Museum output = SameNames.Get(i);

                string csv = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};\n", output.Name, output.Type, output.Mon,
                    output.Tues, output.Wednes, output.Thurs, output.Fri, output.Sat, output.Sun, output.Price, output.Guided);
                File.AppendAllText(fileName, csv);
            }
        }

        /// <summary>
        /// Prints free to enter museum list to a specified .txt file
        /// </summary>
        public static void PrintFreebies(string fileName, MuseumsContainer Freebies, MuseumsRegister register) // prints free to enter museums to .txt file
        {
            Console.WriteLine("Muziejų, kurie yra nemokami sąrašas yra faile {0}", fileName);

            string[] lines = new string[Freebies.Count + 3];

            lines[0] = String.Format("Nemokami muziejai mieste {0}", register.City); ;
            lines[1] = String.Format("Tipas, Pavadinimas");

            for (int i = 0; i < Freebies.Count; i++)
            {
                lines[i + 2] = string.Join(", ",
                Freebies.Get(i).Type, Freebies.Get(i).Name, Freebies.Get(i).WeekEnder);
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}