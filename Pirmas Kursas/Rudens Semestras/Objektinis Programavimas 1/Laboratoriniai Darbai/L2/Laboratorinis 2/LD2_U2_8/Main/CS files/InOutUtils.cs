using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace U2_8
{
    class InOutUtils
    {
        /// <summary>
        /// Read function, reads from file Cityn.csv
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
        /// Extra method to print out a header for table
        /// </summary>
        public static void TableHeader() 
        {
            string dashes = new string('-', 169);

            Console.WriteLine(dashes);
            Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |",
                "Atsakingas asmuo", "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą");
            Console.WriteLine(dashes);
        }
        /// <summary>
        /// Fixed PrintMostWorkingMuseums method.
        /// </summary>
        /// <param name="City">list of the museums in the city</param>
        public static void PMWM(List<Museum> City, MuseumsRegister Register) 
        {
            string dashes = new string('-', 169);

            foreach (Museum museum in City)
            {
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |", Register.Manager, museum.Name, Register.City, museum.Type, museum.Mon, museum.Tues,
                 museum.Wednes, museum.Thurs, museum.Fri, museum.Sat, museum.Sun, museum.Price, museum.Guided);
            }
            Console.WriteLine(dashes);
        }

        /*
        /// <summary>
        /// Prints list of museums to console  || first task
        /// </summary>
        public static void PrintMostWorkingMuseums(Museum mostWorkingMuseum, List<Museum> City1, List<Museum> City2, MuseumsRegister register1, MuseumsRegister register2) // prints museums in a table in console
        {
            string dashes = new string('-', 169);
            City1 = register1.ComparedByWorking(mostWorkingMuseum, City1, register1);

            Console.WriteLine(dashes);
            Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |",
                "Atsakingas asmuo", "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą");
            Console.WriteLine(dashes);

            foreach (Museum museum in City1)
            {
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |", register1.Manager, museum.Name, register1.City, museum.Type, museum.Mon, museum.Tues,
                 museum.Wednes, museum.Thurs, museum.Fri, museum.Sat, museum.Sun, museum.Price, museum.Guided);
            }
            Console.WriteLine(dashes); Console.WriteLine(dashes);
            InOutUtils.PrintMostWorkingMuseums(mostWorkingMuseum, City2, register2);

            Console.WriteLine(dashes);
        }

        /// <summary>
        /// Prints list of museums to console  || first task second part
        /// </summary>
        public static void PrintMostWorkingMuseums(Museum mostWorkingMuseum, List<Museum> City2, MuseumsRegister register2) // prints museums in a table in console
        {
            City2 = register2.ComparedByWorking(mostWorkingMuseum, City2, register2);

            foreach (Museum museum in City2)
            {
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |", register2.Manager, museum.Name, register2.City, museum.Type, museum.Mon, museum.Tues,
                 museum.Wednes, museum.Thurs, museum.Fri, museum.Sat, museum.Sun, museum.Price, museum.Guided);
            }
        }
        */

        /// <summary>
        /// Appends a register to an already existing non-empty file
        /// </summary>
        public static void AppendMuseumsToTxt(string fileName, MuseumsRegister register)
        {
            string[] lines = new string[register.MuseumsCount() + 7];
            lines[0] = register.City;
            lines[1] = register.Manager;
            lines[2] = String.Format(new string('-', 128));
            lines[3] = String.Format("| {0,-30} | {1,-10} | {2,6} | {3,6} | {4,7} |" +
                " {5,7}| {6,5} | {7,4} | {8,6} | {9,5}| {10,-11} |"
               , "Pavadinimas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą");
            lines[4] = String.Format(new string('-', 128));
            for (int i = 0; i < register.MuseumsCount(); i++)
            {
                Museum museum = register.GetByIndex(i);
                lines[i + 6] = museum.ToString();
            }
            lines[register.MuseumsCount() + 6] = String.Format(new String('-', 128));

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Prints free to enter museum list to a specified .txt file
        /// </summary>
        /// <param name="txt"></param> .txt file name
        /// <param name="register"></param> free to enter museum list
        public static void PrintFreebies(string fileName, List<Museum> Freebies,MuseumsRegister register) // prints free to enter museums to .txt file
        {
            Console.WriteLine("Muziejų, kurie yra nemokami sąrašas yra faile {0}", fileName);

            string[] lines = new string[Freebies.Count + 3];

            lines[0] = String.Format("Nemokami muziejai mieste {0}",register.City); ;
            lines[1] = String.Format("Tipas, Pavadinimas");

            for (int i = 0; i < Freebies.Count; i++)
            {
                lines[i + 2] = string.Join(", ",
                Freebies[i].Type, Freebies[i].Name, Freebies[i].WeekEnder);
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}