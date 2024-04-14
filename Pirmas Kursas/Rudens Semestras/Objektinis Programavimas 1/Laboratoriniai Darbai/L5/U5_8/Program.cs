using System;
using System.IO;
using System.Text;

namespace U5_8
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
            if (File.Exists(@"Po1990.csv"))
            {
                File.Delete(@"Po1990.csv");
            }
            if (File.Exists(@"VisosVietos.csv")) 
            {
                File.Delete(@"VisosVietos.csv");
            }


            Console.OutputEncoding = Encoding.UTF8;

            Register register1 = InOut.ReadPlaces(@"City1.csv"); // reads information from .csv file and adds to register
            InOut.PrintPlacesToScreen(register1, "Pirmojo miesto pradiniai duomenys:"); // prints out starting data to screen
            InOut.PrintMuseumsToTxt("startingData.txt", register1, "Pirmojo miesto pradiniai duomenys:"); // prints starting data to csv file

            Register register2 = InOut.ReadPlaces(@"City2.csv"); // reads information from .csv file and adds to register
            InOut.PrintPlacesToScreen(register2, "Antrojo miesto pradiniai duomenys"); // prints out starting data
            InOut.PrintMuseumsToTxt("startingData.txt", register2, "Antrojo miesto pradiniai duomenys:"); // prints starting data to csv file

            Register register3 = InOut.ReadPlaces(@"City3.csv"); // reads information from .csv file and adds to register
            InOut.PrintPlacesToScreen(register3, "Treciojo miesto pradiniai duomenys"); // prints out starting data
            InOut.PrintMuseumsToTxt("startingData.txt", register3, "Treciojo miesto pradiniai duomenys:"); // prints starting data to csv file


            Console.WriteLine("");

            // first task 
            // Museum count task

            int GuideCount = 0;

            register1.MuseumGuideCount(ref GuideCount);
            register2.MuseumGuideCount(ref GuideCount);
            register3.MuseumGuideCount(ref GuideCount);

            Console.WriteLine("Bendras gidų kiekis miestų muziejuose: {0}", GuideCount);


            // Second task
            // Oldest place

            int oldDate = 3000;
            Place oldestPlace= new Place();

            register1.OldestPlace(ref oldDate, ref oldestPlace);
            register2.OldestPlace(ref oldDate, ref oldestPlace);
            register3.OldestPlace(ref oldDate, ref oldestPlace);

            Console.WriteLine("");
            Console.WriteLine("Seniausios lankytinos vietos informacija: ");
            if (oldestPlace is Museum)
            {
                Console.WriteLine(new string('-', 171));
                Console.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-10} | {4,2} | {5,2} | {6,2} |" +
                    " {7,2}| {8,2} | {9,2} | {10,6} | {11,5}| {12,-11} |"
               , "Pavadinimas", "Adresas", "Įkurta", "Tipas", "Pirmad", "Antrad", "Treciad", "Ketvirt", "Penkt",
                "Sest", "Sekmad", "Kaina", "Turi gidą"));
                Console.WriteLine(new string('-', 171));
                Console.WriteLine((oldestPlace as Museum).ToString());
                Console.WriteLine(new string('-', 171));
            }
            else 
            {
                Console.WriteLine(new string('-', 112));
                Console.WriteLine(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-15} | {4,-15} |",
                    "Pavadinimas", "Adresas", "Įkurta", "Autorius", "Kam skirta"));
                Console.WriteLine(new string('-', 112));
                Console.WriteLine((oldestPlace as Statue).ToString());
                Console.WriteLine(new string('-', 112));
            }

            Console.WriteLine();

            // Third task

            PlaceContainer AllPlaces = new PlaceContainer();

            AllPlaces.CombineAll(ref AllPlaces, register1.GetContainer());
            AllPlaces.CombineAll(ref AllPlaces, register2.GetContainer());
            AllPlaces.CombineAll(ref AllPlaces, register3.GetContainer());

            AllPlaces.Sort();

            InOut.PrintAll("VisosVietos.csv", AllPlaces, "Visos lankomos vietos surikiuotos pagal įkūrimo datą ir pavadinimą: ");
            Console.WriteLine("Visų lankytinų vietų surikiuotas sąrašas yra faile 'VisosVietos.csv' ");
            Console.WriteLine();


            // Fourth task
            // Places after 1990s

            Register After90s = new Register();

            After90s.After1990s(ref After90s, register1);
            After90s.After1990s(ref After90s, register2);
            After90s.After1990s(ref After90s, register3);

            Console.WriteLine("Lankytinų vietų, įkurtų po 1990-ųjų metų sąrašas yra faile 'Po1990.csv' ");

            InOut.PrintAfter1990s("Po1990.csv", After90s, "Lankytinų vietų, kurios buvo sukurtos po 1990-ųjų metų sąrašas:");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}



/*
  ---- Pirmas testas ----

 City1.csv:
Kaunas
Petras Petrauskas
Kauno Karo Muziejus;Gatvė 312;1918;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Kauno Meno galerija;Gatvė 332;1938;Galerija;0;0;0;0;0;1;1;0;Neturi gido
Kauno Mokslo muziejus;Gatvė 22;1928;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Kauno Karo Muziejus;Gatvė 32;1998;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido
Žmogus;gatvė 25;2020;Petras Mazūras;Žmonėm
Pegasas;gatvė 55;1011;Petras Mazūras;Augintiniam
Arklys;gatvė 45;2010;Petras Mazūras;Arkliam

 City2.csv:
Vilnius
Vilniaus Meras
Vilniaus Karo Muziejus;Gatvė 312;1918;Muziejus;1;1;1;1;1;0;1;0;Turi gidą
Vilniaus Meno galerija;Gatvė 332;1938;Galerija;0;0;0;0;0;1;1;0;Turi gidą
Vilniaus Mokslo muziejus;Gatvė 652;1928;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Vilniaus Karo Muziejus;Gatvė 442;1998;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido
Gyvūnas;gatvė 235;2020;Petras Mazūras;Žmonėm
Vienaragis;gatvė 15;2011;Rikas Sarbievijus;Augintiniam
Mašina;gatvė 325;2010;Antanas Kulka;Arkliam

 City3.csv:
Jonava
Jonavos Meras
Jonavos Karo Muziejus;Gatvė 312;1928;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Jonavos Meno galerija;Gatvė 332;1968;Galerija;0;1;1;1;0;1;1;0;Neturi gido
Jonavos Mokslo muziejus;Gatvė 232;1928;Muziejus;1;0;1;0;1;0;1;0;Turi gidą
Jonavos Karo Muziejus;Gatvė 352;1928;Muziejus;0;0;0;0;0;0;1;3.3;Neturi gido
Vanduo;gatvė 245;2020;Antans Mazūras;Žmonėm
Medis;gatvė 525;2011;Petras Mazūras;Augintiniam
Vežimas;gatvė 415;2010;Jonas Mazūras;Arkliam
 
 
 
 
 ---- Antras testas ----

 City1.csv:

 City2.csv:

 City3.csv:

 
 
 
 
 
 
 
 
 
 
 
 */
