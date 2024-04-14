using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace U2_8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clearing old test files
            if (File.Exists("startingData.txt"))
            {
                File.Delete("startingData.txt");
            }
            if (File.Exists("Nemokami_Kaunas.txt"))
            {
                File.Delete("Nemokami_Kaunas.txt");
            }
            if (File.Exists("Nemokami_Vilnius.txt"))
            {
                File.Delete("Nemokami_Vilnius.txt");
            }


            Console.OutputEncoding = Encoding.UTF8;

            MuseumsRegister register1= InOutUtils.ReadMuseums(@"City1.csv"); // reads information from .csv file and adds to register
            InOutUtils.AppendMuseumsToTxt("startingData.txt", register1); // prints out starting data

            MuseumsRegister register2 = InOutUtils.ReadMuseums(@"City2.csv"); // reads information from .csv file and adds to register
            InOutUtils.AppendMuseumsToTxt("startingData.txt", register2); // prints out starting data

            Console.WriteLine("");

            // First task 

            // These lists are going to be used later
            List<Museum> City1 = new List<Museum>();
            List<Museum> City2 = new List<Museum>();

           // Museum MostWorkingCity1 = register1.GetList()[register1.MostWorking()]; // pertvarkyta su GetList() funkcija || Finds the most working museum from first city
           // Museum MostWorkingCity2 = register2.GetList()[register2.MostWorking()]; // pertvarkyta su GetList() funkcija || Finds the most working museum from second city

            Museum MostWorkingCity1 = register1.GetByIndex(register1.MostWorking());
            Museum MostWorkingCity2 = register2.GetByIndex(register2.MostWorking());

            City1 = register1.ComparedByWorking(MostWorkingCity1, City1, register1);
            City2 = register2.ComparedByWorking(MostWorkingCity2, City2, register2);

            if (MostWorkingCity1 > MostWorkingCity2)
            {
                Console.WriteLine("Muziejų, kurie dirba daugiausiai per savaitę buvo mieste {0}, jų sąrašas:", register1.City);

                InOutUtils.TableHeader();
                InOutUtils.PMWM(City1, register1);
                Console.WriteLine("");
            }
            else if (MostWorkingCity1 < MostWorkingCity2)
            {
                Console.WriteLine("Muziejų, kurie dirba daugiausiai per savaitę buvo mieste {0}, jų sąrašas:", register2.City);
                InOutUtils.TableHeader();
                InOutUtils.PMWM(City2, register2);
                Console.WriteLine("");
            }
            else 
            {
                Console.WriteLine("Abiejuose miestuose daugiausiai dirbančių muziejų skaičius buvo vienodas, jų sąrašas:");
                InOutUtils.TableHeader();
                InOutUtils.PMWM(City1, register1);
                InOutUtils.PMWM(City2, register2);
                Console.WriteLine("");

            }

            // second task

            int guidedCity1 = register1.CalculateGuides(); // calculates the ammount of museums that have guides
            int guidedCity2 = register2.CalculateGuides(); // calculates the ammount of museums that have guides

            if (guidedCity1 > guidedCity2)
            {
                Console.WriteLine("Mieste {0} yra daugiau muziejų su gidais nei mieste {1}. Muziejų su gidais kiekis: {2}", register1.City,register2.City, guidedCity1);
                Console.WriteLine("");
            }
            else if (guidedCity1 < guidedCity2)
            {
                Console.WriteLine("Mieste {0} yra daugiau muziejų su gidais nei mieste {1}. Muziejų su gidais kiekis: {2}", register2.City, register1.City, guidedCity2);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Abiejuose miestuose yra vienodas kiekis muziejų su gidais. Muziejų su gidais skaičius: {0}", guidedCity1);
                Console.WriteLine("");
            }

            // third task

            City1.Clear();
            City2.Clear();

            City1 = register1.FreeMuseums(City1, register1); // Adds museums that are free to enter to list
            City2 = register2.FreeMuseums(City2, register2); // Adds museums that are free to enter to list

            string FreebieFileName; // Printing file for free to enter museums

            if (City1.Count > 0)
            {
                FreebieFileName = "Nemokami_" + register1.City + ".txt";
                InOutUtils.PrintFreebies(FreebieFileName, City1, register1);
            }
            else 
            {
                Console.WriteLine("Mieste {0} nėra muziejų, kuriuos galima aplankyti nemokamai", register1.City);
            }

            if (City2.Count > 0)
            {
                FreebieFileName = "Nemokami_" + register2.City + ".txt";
                InOutUtils.PrintFreebies(FreebieFileName, City2, register2);
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
Kauno Karo Muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą;
Kauno Meno galerija;Galerija;0;0;0;0;0;1;1;0;Neturi gido;
Kauno Mokslo muziejus;Muziejus;1;0;1;0;1;0;1;0;Turi gidą;
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido;
Kauno Zoologijos sodas;Parkas;1;1;1;1;1;1;1;5;Neturi gido;
Kauno Karo Muziejus;Muziejus;0;0;0;0;0;1;1;3.3;Turi gidą;


City2.csv failo duomenys:
 
Vilnius
Antanas Antanauskas
Vilniaus Meno galerija;Galerija;1;0;1;0;1;0;1;3.3;Neturi gido;
Vilniaus Muzikos Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Turi gidą;
Vilniaus Karo Muziejus;Muziejus;0;0;1;0;1;0;1;3.3;Turi gidą;
Vilniaus Antano Muziejus;Muziejus;1;0;1;0;1;0;1;3.3;Turi gidą;
Vilniaus Memorialinis parkas;Parkas;1;0;0;0;1;0;1;3.3;Neturi gido;
Vilniaus Uno Muziejus;Muziejus;1;1;1;1;1;1;1;3.3;Turi gidą;
Vilniaus Petro Muziejus;Muziejus;1;0;1;0;1;0;0;3.3;Neturi gido;
Vilniaus Gedimino Muziejus;Muziejus;0;0;0;0;1;0;1;3.3;Turi gidą;
 
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