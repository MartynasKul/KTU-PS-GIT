using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace U3_8
{
    class Program
    {
        static void Main(string[] args)
        {

            // deletes old result files to reset results
            if (File.Exists(@"startingData.txt")) 
            {
                File.Delete(@"startingData.txt");
            }

            if (File.Exists(@"Sutampa.csv"))
            {
                File.Delete(@"Sutampa.csv");
            }

            Console.OutputEncoding = Encoding.UTF8;

            MuseumsRegister register1= InOutUtils.ReadMuseums(@"City1.csv"); // reads information from .csv file and adds to register
            //MuseumsRegister register1 = InOutUtils.ReadMuseums(@"City3.csv"); // reads information from .csv file and adds to register
            InOutUtils.PrintMuseumsToTxt("startingData.txt", register1, "Pirmojo miesto pradiniai duomenys:"); // prints out starting data
            // InOutUtils.PrintMuseumsToScreen(register1,"Pirmojo miesto pradiniai duomenys:"); // prints out starting data
            
            MuseumsRegister register2 = InOutUtils.ReadMuseums(@"City2.csv"); // reads information from .csv file and adds to register
            //MuseumsRegister register2 = InOutUtils.ReadMuseums(@"City4.csv"); // reads information from .csv file and adds to register
            InOutUtils.PrintMuseumsToTxt("startingData.txt", register2, "Antrojo miesto pradiniai duomenys"); // prints out starting data
            // InOutUtils.PrintMuseumsToScreen(register2, "Antrojo miesto pradiniai duomenys:"); // prints out starting data

            Console.WriteLine("");


            // First task 

            Museum MostWorkingCity1 = register1.Get(register1.MostWorking()); // pertvarkyta su GetList() funkcija
            Museum MostWorkingCity2 = register2.Get(register2.MostWorking()); // pertvarkyta su GetList() funkcija

            MuseumsContainer City1 = new MuseumsContainer();
            MuseumsContainer City2 = new MuseumsContainer();

            register1.ComparedByWorking(MostWorkingCity1, City1, register1);
            register2.ComparedByWorking(MostWorkingCity2, City2, register2);

            if (MostWorkingCity1 > MostWorkingCity2) 
            {
                Console.WriteLine("Mieste {0} yra darbščiausi muziejai, jų sarašas:", register1.City);
                Console.WriteLine(new string('-', 169));
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                    " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |",
                    "Atsakingas asmuo", "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                    "Sest", "Sekmad", "Kaina", "Turi gidą");
                Console.WriteLine(new string('-', 169));
                InOutUtils.PrintMostWorkingMuseums(register1, City1);
                Console.WriteLine("");
            }
            else if (MostWorkingCity1 < MostWorkingCity2)
            {
                Console.WriteLine("Mieste {0} yra darbščiausi muziejai, jų sarašas:", register2.City);
                Console.WriteLine(new string('-', 169));
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                    " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |",
                    "Atsakingas asmuo", "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                    "Sest", "Sekmad", "Kaina", "Turi gidą");
                Console.WriteLine(new string('-', 169));
                InOutUtils.PrintMostWorkingMuseums(register2, City2);
                Console.WriteLine("");
            }
            else 
            {
                Console.WriteLine("Abiejuose miestuose darbščiausi muziejai dirbo vienodą dienų skaičių:");
                Console.WriteLine(new string('-', 169));
                Console.WriteLine("|{0,-25} | {1,-30} | {2,-10} | {3,-10} | {4,6} | {5,6} | {6,7} |" +
                    " {7,7}| {8,5} | {9,4} | {10,6} | {11,5}| {12,-11} |",
                    "Atsakingas asmuo", "Pavadinimas", "Miestas", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                    "Sest", "Sekmad", "Kaina", "Turi gidą");
                Console.WriteLine(new string('-', 169));
                InOutUtils.PrintMostWorkingMuseums(register1, City1);
                InOutUtils.PrintMostWorkingMuseums(register2, City2);
                Console.WriteLine("");
            }

            // second task

            /*
            City1.Clear(); // clears list
            City2.Clear(); // clears list
            */

            List<string> City1Types = new List<string>();
            List<string> City2Types = new List<string>();

            register1.FindWorkingOnWednesday(City1Types, register1); // adds museums that work on wednesdays to the list
            register2.FindWorkingOnWednesday(City2Types, register2); // adds museums that work on wednesdays to the list

            register1.RemoveDuplicateMuseumTypes(ref City1Types); // removes duplicate museum types
            register2.RemoveDuplicateMuseumTypes(ref City2Types); // removes duplicate museum types

            InOutUtils.PrintMuseumTypes(City1Types, register1); // prints out museum types of that city
            InOutUtils.PrintMuseumTypes(City2Types, register2); // prints out museum types of that city
            Console.WriteLine("");

            // third task

            MuseumsContainer SameNames = new MuseumsContainer();

            register1.DuplicateNames(SameNames, register1, register2); // finds and adds museums with the same names to container

            if (SameNames.Count != 0)
            {
                Console.WriteLine("Muziejų, kurių pavadinimai sutampa, sąrašas yra faile Sutampa.csv.");
                InOutUtils.PrintSameNamesToCSV("Sutampa.csv", SameNames, "Muziejų, kurių vardai sutampa sąrašas: ");
                Console.WriteLine("");
            }
            else 
            {
                Console.WriteLine("Muziejų, kurių pavadinimai sutampa nėra.");
                Console.WriteLine("");
            }

            // fourth task

            MuseumsContainer FreebiesCity1 = new MuseumsContainer(); // List containing museums that are free to enter from first city
            MuseumsContainer FreebiesCity2 = new MuseumsContainer(); // List containing museums that are free to enter from second city

            FreebiesCity1 = register1.FreeMuseums(FreebiesCity1, register1); // Adds museums that are free to enter to list
            FreebiesCity2 = register1.FreeMuseums(FreebiesCity2, register2); // Adds museums that are free to enter to list

            FreebiesCity1.Sort();
            FreebiesCity2.Sort();

            string FreebieFileName; // file name for free to enter museums

            if (FreebiesCity1.Count > 0)
            {
                FreebieFileName = "Nemokami_" + register1.City + ".txt";
                InOutUtils.PrintFreebies(FreebieFileName, FreebiesCity1, register1);
            }
            else
            {
                Console.WriteLine("Mieste {0} nėra muziejų, kuriuos galima aplankyti nemokamai", register1.City);
            }

            if (FreebiesCity2.Count > 0)
            {
                FreebieFileName = "Nemokami_" + register2.City + ".txt";
                InOutUtils.PrintFreebies(FreebieFileName, FreebiesCity2, register2);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Mieste {0} nėra muziejų, kuriuos galima aplankyti nemokamai", register2.City);
                Console.WriteLine("");
            }

            Console.ReadKey();
        }
    }
}

/*
---Pirmas testas:
City1.csv failo duomenys:

Kaunas
Petras Petrauskas
Kauno Karo Muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Kauno Meno galerija;Galerija;0;0;0;0;0;1;1;0;Neturi gido
Kauno Mokslo muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido
Kauno Zoologijos sodas;Parkas;1;1;1;1;1;1;1;5;Neturi gido
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;1;1;3.3;Turi gidą
Vienybės Parkas;Parkas;0;1;1;1;1;1;0;0.69;Neturi gido


City2.csv failo duomenys:
 
Vilnius
Antanas Antanauskas
Vilniaus Meno galerija;Galerija;1;0;1;0;1;0;1;3.3;Neturi gido
Vilniaus Muzikos Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Turi gidą
Vilniaus Karo Muziejus;Muziejus;0;0;1;0;1;0;1;3.3;Turi gidą
Vilniaus Antano Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Turi gidą
Vilniaus Memorialinis parkas;Parkas;1;1;1;1;1;1;1;3.3;Neturi gido
Vilniaus Uno Muziejus;Muziejus;1;1;1;1;1;1;1;3.3;Turi gidą
Vilniaus Petro Muziejus;Muziejus;1;0;1;0;1;0;0;3.3;Neturi gido
Vilniaus Gedimino Muziejus;Muziejus;0;0;0;0;1;0;1;3.3;Turi gidą
Vienybės Parkas;Parkas;0;1;1;1;1;0;0;0.69;Neturi gido
 
---Antras testas:

City1.csv failo duomenys:

Kaunas
Petras Petrauskas
Kauno Karo Muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą;
Kauno Meno galerija;Galerija;0;0;0;0;0;1;1;0;Turi gidą;
Kauno Mokslo muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą;
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido;
Kauno Zoologijos sodas;Parkas;1;1;1;1;1;1;1;5;Turi gidą;
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;1;1;0;Turi gidą;
Kauno Šventas sodas;Parkas;1;1;1;1;1;1;1;0;Turi gidą;
Kauno Miesto Muziejus;Muziejus;0;0;0;1;0;1;1;2.5;Turi gidą;



City2.csv failo duomenys:

Vilnius
Antanas Antanauskas
Vilniaus Meno galerija;Galerija;1;0;1;0;1;0;1;3.3;Neturi gido;
Vilniaus Muzikos Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Turi gidą;
Vilniaus Karo Muziejus;Muziejus;0;0;1;0;1;0;1;0;Turi gidą;
Vilniaus Antano Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Neturi gido;
Vilniaus Memorialinis parkas;Parkas;1;0;0;0;1;0;1;0;Neturi gido;
Vilniaus Uno Muziejus;Muziejus;1;1;1;1;1;1;1;3.3;Neturi gido;
Vilniaus Petro Muziejus;Muziejus;1;0;1;0;1;0;0;3.3;Neturi gido;
Vilniaus Gedimino Muziejus;Muziejus;0;0;0;0;1;0;1;0;Neturi gido;
Vilniaus MO Muziejus;Muziejus;1;1;1;1;1;1;1;6.9;Turi gidą;
Vilniaus Žirgų Muziejus;Muziejus;1;1;1;1;1;1;1;6.9;Neturi gido;
Vilniaus Miesto Muziejus;Muziejus;0;0;0;0;0;1;1;0;Neturi gido;
 
 
 */