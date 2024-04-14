///
/// Martynas Kuliešius IFF-1/9 L1_U8
///

using System;
using System.Collections.Generic;

namespace Laboratorinis1
{
    internal class Program
    {
        /// <summary>
        /// Main function of program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Test 1
            List<Museum> allMuseums = InOutUtils.ReadMuseums(@"Data1.csv"); // reads information from .csv file and adds to list

            //Test 2
            //List<Museum> allMuseums = InOutUtils.ReadMuseums(@"Data2.csv"); // reads information from .csv file and adds to list


            Console.WriteLine("Muziejų informacija"); // information of the table
            InOutUtils.PrintMuseums(allMuseums); // prints to console table of information
            InOutUtils.PrintHelp(); // prints to console a helper for museum work

            InOutUtils.PrintStartingResources("Museums.txt", allMuseums); // prints starting information to Museums.txt file. 

            // First objective of individual task
            Console.WriteLine("Pasirinkite miestą, kurio muziejuose norite apsilankyti: ");
            string selectedCity = Console.ReadLine();
            Console.WriteLine("Ar reikia gido? ");
            string sGuided = Console.ReadLine();

            Console.WriteLine("");

            List<Museum> FilteredByCityGuide = TaskUtils.FilterCityGuide(allMuseums, sGuided, selectedCity);
            int countedMuseums = TaskUtils.CountSelectedCity(FilteredByCityGuide, selectedCity);
            InOutUtils.PrintFilteredAndCounted(FilteredByCityGuide, countedMuseums);
            Console.WriteLine("");

            // Second objective of individual task

            Console.WriteLine("Pasirinkite miestą, kurio muziejuose norite apsilankyti: ");
            selectedCity = Console.ReadLine();
            List<Museum> SelectedMuseums = TaskUtils.FilterCity(allMuseums, selectedCity);
            Console.WriteLine("Kelintadienį norite apsilankyti?");
            string selectedDay = Console.ReadLine();
            Console.WriteLine("");
            List<string> Types = TaskUtils.FindTypes(SelectedMuseums, selectedDay);
            InOutUtils.PrintTypes(Types);
            Console.WriteLine("");

            // Third objective of individual task

            Console.WriteLine("Pasirinkite miestą, kurio muziejus norite matyti.");
            selectedCity = Console.ReadLine();
            List<Museum> SelectedMuseum = TaskUtils.FilterCity(allMuseums, selectedCity);
            Console.WriteLine("Bent kiek dienų turi dirbti šie muziejai?");
            int workdays = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            List<Museum> WorkingMuseums = TaskUtils.WorkingDays(SelectedMuseum, workdays);
            InOutUtils.PrintWorkingMuseums(selectedCity + ".csv", WorkingMuseums, workdays);

            Console.ReadKey();
        }
    }
}

        /*
         Testas nr.1
        
        Kauno Karo Muziejus;Kaunas;Muziejus;0;0;1;0;0;1;0;3.3;Turi gidą;
        Vilniaus Karo Muziejus;Vilnius;Muziejus;1;1;0;0;1;1;0;3.2;Neturi gido;
        Kauno Paveikslų galerija;Kaunas;Galerija;0;1;1;0;0;0;1;3.4;Neturi gido;
        Kauno Sakralinis Muziejus;Kaunas;Muziejus;1;1;1;0;0;1;1;5.09;Turi gidą;
        Kauno Miesto Muziejus;Kaunas;Muziejus;1;1;0;0;0;1;1;1.7;Turi gidą;
        Vilniaus Muziejus;Vilnius;Muziejus;0;0;1;0;0;1;0;8.62;Turi gidą;
        Vilniaus MO muziejus;Vilnius;Muziejus;0;1;1;1;0;0;1;2.22;Neturi gido;
             
             */
        
        /*
         Testas nr.2
        
        Vilniaus MO muziejus;Vilnius;Muziejus;1;1;0;1;0;0;1;2.22;Neturi gido;
        Kauno Paveikslų galerija;Kaunas;Galerija;0;1;0;0;0;0;0;3.4;Neturi gido;
        Vilniaus Muziejus;Vilnius;Muziejus;0;0;1;0;0;1;1;8.62;Turi gidą;
        Vilniaus Karo Muziejus;Vilnius;Muziejus;1;1;0;0;1;1;0;3.2;Turi gidą;
        Kauno Karo Muziejus;Kaunas;Muziejus;1;0;1;0;1;0;0;3.7;Neturi gido;
        Kauno Miesto Muziejus;Kaunas;Muziejus;1;1;0;0;1;0;0;1.7;Turi gidą;
        Kauno Sakralinis Muziejus;Kaunas;Muziejus;1;1;1;0;1;1;1;5.09;Neturi gido;
             
             */
