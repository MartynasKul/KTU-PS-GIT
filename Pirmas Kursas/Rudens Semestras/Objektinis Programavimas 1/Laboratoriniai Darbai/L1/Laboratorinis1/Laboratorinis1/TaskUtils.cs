using System;
using System.Collections.Generic;

namespace Laboratorinis1
{
    internal class TaskUtils
    {
        /// <summary>
        /// List filtering with user selected filters
        /// </summary>
        /// <param name="Museums"> List of museums</param>
        /// <param name="isGuided"> user defined parameter for having a guide </param>
        /// <param name="selectedCity"> user defined parameter for a specific city </param>
        /// <returns> returns a List of Museums that are within user selected parameters </returns>
        public static List<Museum> FilterCityGuide(List<Museum> Museums, string isGuided, string selectedCity)
        {
            string guider;
            if (isGuided.ToLower() == "taip")
            {
                guider = "Turi gidą";
            }
            else
            {
                guider = "Neturi gido";
            }
            List<Museum> Filtered = new List<Museum>();
            foreach (Museum museum in Museums)
            {
                if (museum.City.Equals(selectedCity) &&
               museum.Guided.Equals(guider))
                {
                    Filtered.Add(museum);
                }
            }
            return Filtered;
        }

        /// <summary>
        /// Counts the ammount of museums that are within selected parameters
        /// </summary>
        /// <param name="Museums"> List of museums</param>
        /// <param name="selectedCity"> User-selected City parameter </param>
        /// <returns> a number of museums in the selected city </returns>
        public static int CountSelectedCity(List<Museum> Museums, string selectedCity)
        {
            int count = 0;

            foreach (Museum museum in Museums)
                {
                    if (museum.City.Equals(selectedCity))
                    {
                        count++;
                    }
            }
            return count;
        }

        /// <summary>
        /// Filters museums by user selected city
        /// </summary>
        /// <param name="Museums"></param>
        /// <param name="selectedCity"></param>
        /// <returns> A filtered list of museums </returns>
        public static List<Museum> FilterCity(List<Museum> Museums, string
        selectedCity)
        {
            List<Museum> Filtered = new List<Museum>();
            foreach (Museum museum in Museums)
            {
                if (museum.City.Equals(selectedCity))
                {
                    Filtered.Add(museum);
                }
            }
            return Filtered;
        }

        /// <summary>
        /// Finds different types of museums that you can visit on selected day
        /// </summary>
        /// <param name="Museums"> List of Museums </param>
        /// <param name="selectedDay"> Selected day </param>
        /// <returns> Returns a string list of the museum types</returns>
        public static List<string> FindTypes(List<Museum> Museums, string selectedDay)
        {
            List<string> Types = new List<string>(); // string list that contains types of museums        
        
            switch (selectedDay)
               {
                   case "Pirmadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Mon == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   case "Antradienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Tues == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   case "Trečiadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Wednes == 1)
                           {
                                if (!Types.Contains(museum.Type))
                                {
                                    Types.Add(museum.Type);
                                }
                           }
                       }
                       break;
                   case "Ketvirtadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Thurs == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   case "Penktadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Fri == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   case "Šeštadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Sat == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   case "Sekmadienį":
                       foreach (Museum museum in Museums)
                       {
                           if (museum.Sun == 1)
                           {
                               if (!Types.Contains(museum.Type))
                               {
                                   Types.Add(museum.Type);
                               }
                           }
                       }
                       break;
                   default:
                       Console.WriteLine("Neteisingai įvesta diena arba tokios dienos nėra!");
                        break;
               }
               return Types;
        }

        /// <summary>
        /// Creates a list of museums that work a user defined ammount of days in a week.
        /// </summary>
        /// <param name="Museums"> List of museums </param>
        /// <param name="workdays"> Ammount of days the user wants the museum to work </param>
        /// <returns> a List of museums that work more than the user defined ammount of days </returns>
        public static List<Museum> WorkingDays(List<Museum> Museums, int workdays)        
        {
            List<Museum> Working = new List<Museum>();
            int works;

            foreach (Museum museum in Museums)
            {
                works = museum.Mon + museum.Tues + museum.Wednes + museum.Thurs +
                museum.Fri + museum.Sat + museum.Sun;

                if (works >= workdays)
                {
                    Working.Add(museum);
                }
                works = 0;
            }
            return Working;
        }    
    }
}
