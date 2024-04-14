using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laboratorinis1
{
    internal class InOutUtils
    {
        /// <summary>
        /// Read from file method, reads from file Museums.csv
        /// </summary>
        /// <param name="fileName"> the file name of the file that has the museum data </param>
        /// <returns></returns>
        public static List<Museum> ReadMuseums(string fileName)
        {
            List<Museum> Museums = new List<Museum>();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in Lines)
            {
                string[] Bits = line.Split(';'); // splits read line at ; and assigns to string array for separation
                
                string name = Bits[0];
                string city = Bits[1];
                string type = Bits[2];
                int mon = int.Parse(Bits[3]);
                int tues = int.Parse(Bits[4]);
                int wednes = int.Parse(Bits[5]);
                int thurs = int.Parse(Bits[6]);
                int fri = int.Parse(Bits[7]);
                int sat = int.Parse(Bits[8]);
                int sun = int.Parse(Bits[9]);
                double price = double.Parse(Bits[10]);
                string guided = Bits[11];

                Museum museum = new Museum(name, city, type, mon, tues,
                wednes, thurs, fri, sat, sun, price, guided); // new museum object creation

                Museums.Add(museum); // adds a new museum object  to list Museums
            }
            return Museums; // returns museum list
        }
        
        /// <summary>
        /// Method that prints museum lsit to file
        /// </summary>
        /// <param name="Museums"> List of museums</param>
        public static void PrintMuseums(List<Museum> Museums)
        {
            
             Console.WriteLine(new string ('-', 142));
             Console.WriteLine("| {0,-30} | {1,-10} | {2,-10} | {3,-6} | {4,-6} | {5,-7} |" +
             " {6,-7}| {7,-5} | {8,-4} | {9,-6} | {10,-5}| {11,-11} |",
             "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", 
            "Ketvirt", "Penkt",
             "Sest", "Sekmad", "Kaina", "Turi gidą");
             Console.WriteLine(new string ('-', 142));
             foreach (Museum museum in Museums)
             {
                 Console.WriteLine("| {0,-30} | {1,-10} | {2,-10} | {3,6} | {4,6} | {5,7} |" +
                 " {6,7}| {7,5} | {8,4} | {9,6} | {10,5}| {11,-11} |",
                 museum.Name, museum.City, museum.Type, museum.Mon, museum.Tues, museum.Wednes, museum.Thurs, museum.Fri,
                 museum.Sat, museum.Sun, museum.Price, museum.Guided);
             }
                Console.WriteLine(new string ('-', 142));
        }

        /// <summary>
        /// Prints out explanation on whether museum working meaning. Extra function, not neccessary
        /// </summary>
        public static void PrintHelp()
        {
            Console.WriteLine("");
            Console.WriteLine("Muziejų darbo paaiškinimas:");
            Console.WriteLine("1 - dirba, 0 - nedirba");
            Console.WriteLine("");
        }

        /// <summary>
        /// Method that outputs a count of filtered museums and museum information to console
        /// </summary>
        /// <param name="Museums"> List of museums</param>
        /// <param name="countedMuseums"> Ammount of museums that are within parameters </param>
        public static void PrintFilteredAndCounted(List<Museum> Museums, int
        countedMuseums)
        {
            string dashes = new string('-', 142);

            Console.WriteLine("Visas muziejų skaičius, kurie atitinka filtrą: {0}", countedMuseums);

            Console.WriteLine(dashes);
            Console.WriteLine("| {0,-30} | {1,-10} | {2,-10} | {3,-6} | {4,-6} | {5,-7} |" +
                 " {6,-7}| {7,-5} | {8,-4} | {9,-6} | {10,-5}| {11,-11} |",
                 "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                 "Sest", "Sekmad", "Kaina", "Turi gidą");

            Console.WriteLine(dashes);

            foreach (Museum museum in Museums)
            {
                Console.WriteLine("| {0,-30} | {1,-10} | {2,-10} | {3,6} | {4,6} | {5,7} |" +
                " {6,7}| {7,5} | {8,4} | {9,6} | {10,5}| {11,-11} |",
                museum.Name, museum.City, museum.Type, museum.Mon, museum.Tues, museum.Wednes, museum.Thurs, museum.Fri,
                museum.Sat, museum.Sun, museum.Price, museum.Guided);
            }
            Console.WriteLine(dashes);
        }

        /// <summary>
        /// A method that prints the starting data to a .txt file
        /// </summary>
        /// <param name="fileName"> FileName to where to print the starting data</param>
        /// <param name="Museums"> A list of museums </param>
        public static void PrintStartingResources(string fileName, List<Museum> Museums)
        {
            string dashes = new string('-', 142); // a string for table borders 

            string[] lines = new string[Museums.Count + 5];
            lines[0] = String.Format("Pradiniai duomenys:");
            lines[1] = String.Format(dashes); // top border of table
            lines[2] = String.Format("| {0,-30} | {1,-10} | {2,-10} | {3,-6} | {4,-6} | {5,-7} |" +
               " {6,-7}| {7,-5} | {8,-4} | {9,-6} | {10,-5}| {11,-11} |",
               "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
               "Sest", "Sekmad", "Kaina", "Turi gidą"); // information explanations

            lines[3] = String.Format(dashes); // lower border of the table
            for (int i = 0; i < Museums.Count; i++)
            {
                lines[i + 4] = String.Format("| {0,-30} | {1,-10} | {2,-10} | {3,6} | {4,6} | {5,7} |" +
                " {6,7}| {7,5} | {8,4} | {9,6} | {10,5}| {11,-11} |",
                Museums[i].Name, Museums[i].City, Museums[i].Type, Museums[i].Mon, Museums[i].Tues, Museums[i].Wednes, Museums[i].Thurs, Museums[i].Fri,
                Museums[i].Sat, Museums[i].Sun, Museums[i].Price, Museums[i].Guided); // assings an element of list to string array
            }
            lines[Museums.Count + 4] = String.Format(dashes); // bottom border of table
             File.WriteAllLines(fileName, lines, Encoding.UTF8); // prints string array to.txt file
        }

        /// <summary>
        /// A method that prints out the types of museums that were found working in that day
        /// </summary>
        /// <param name="Types"> list of types</param>
        public static void PrintTypes(List<string> Types)
        {
            if (Types != null) // checks if list is not empty
            {
                Console.WriteLine("Šiuos muziejų tipus galite aplankyti savo pasirinktame mieste, pasirinktą dieną: ");
            foreach (string type in Types) // takes one part of the list and outputs to console
            {
                    Console.WriteLine(type);
                }
            }
            else
            {
                Console.WriteLine("Jūsų pasirinktą dieną, muziejai nedirbo");
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Method that prints task information to a .csv file
        /// </summary>
        /// <param name="fileName"> file name</param>
        /// <param name="Museums"> list of museums that work</param>
        /// <param name="workdays"> the ammount of days the museums work</param>
        public static void PrintWorkingMuseums(string fileName, List<Museum>
        Museums, int workdays)
        {
            Console.WriteLine("Pagal parametrus atrinktų muziejų informacija yra faile {0}", fileName);
            string[] lines = new string[Museums.Count + 5];
            lines[0] = String.Format("Muziejų, kurie dirba bent {0} dienas/ų pasirinktame mieste informacija: ", workdays);
           
            lines[1] = String.Format("{0},{1},{2},{3},{4},{5}," +
                "{6},{7},{8},{9},{10},{11}",
                "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad",
                "Ketvirt", "Penkt", "Sest", "Sekmad", "Kaina", "Turi gidą"); // information explanations
            for (int i = 0; i < Museums.Count; i++)
                {
                    lines[i + 2] = String.Format("{0},{1},{2},{3},{4},{5}," +
                        "{6},{7},{8},{9},{10},{11}",
                        Museums[i].Name, Museums[i].City, Museums[i].Type, Museums[i].Mon,
                        Museums[i].Tues, Museums[i].Wednes, Museums[i].Thurs, Museums[i].Fri,
                        Museums[i].Sat, Museums[i].Sun, Museums[i].Price,
                        Museums[i].Guided); // assings an element of list to string array
                }
            File.WriteAllLines(fileName, lines, Encoding.UTF8); // prints string array to .csv file
        }
    }
}
