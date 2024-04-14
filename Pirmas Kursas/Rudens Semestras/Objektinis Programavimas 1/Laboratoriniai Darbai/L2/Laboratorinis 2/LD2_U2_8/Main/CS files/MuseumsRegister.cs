using System;
using System.Collections.Generic;


namespace U2_8
{
    class MuseumsRegister
    {
        private List<Museum> Museums;
        public string Manager { get; set; } // pirmos eilutes saugomi duomenys
        public string City { get; set; } // antros eilutes saugomi duomenys

        /// <summary>
        /// creates new list
        /// </summary>
        public MuseumsRegister()
        {
            Museums = new List<Museum>();
        }

        /// <summary>
        /// Adds new objects to list
        /// </summary>
        /// <param name="Museums"></param>
        public MuseumsRegister(List<Museum> Museums)
        {
            Museums = new List<Museum>();

            foreach (Museum museum in Museums)
            {
                this.Museums.Add(museum);
            }
        }

        public List<Museum> GetList()
        {
            return Museums;
        }

        /// <summary>
        /// Add a museum to register
        /// </summary>
        /// <param name="museum"> Add museum object to register</param>
        public void Add(Museum museum)
        {
            Museums.Add(museum);
        }

        /// <summary>
        /// Finds number of museums
        /// </summary>
        /// <returns></returns>
        public int MuseumsCount()
        {
            return this.Museums.Count;
        }

        /// <summary>
        /// Returns a museum in the register by its index
        /// </summary>
        public Museum GetByIndex(int index)
        {
            return Museums[index];
        }

        /// <summary>
        /// Finds museum that works the most
        /// </summary>
        /// <returns> index of museum that works the most days of the weak </returns>
        public int MostWorking() 
        {
            int ID=0;

            for (int i = 0; i < Museums.Count; i++)
            {
                if (Museums[i] > Museums[ID])
                {
                    ID = i; // max day integer
                }
            }

            return ID;
        }

        /// <summary>
        /// Bool to check if there's a duplicate in the register
        /// </summary>
        /// <param name="museum"></param>
        /// <returns></returns>
        public bool Contains(Museum museum)
        {
            return Museums.Contains(museum);
        }

        /// <summary>
        /// Calculates how many museums have guided tours in one city
        /// </summary>
        /// <returns> Returns ammount of museums that have a guide </returns>
        public int CalculateGuides()
        {
            int guideNum = 0;

            foreach (Museum museum in Museums)
            {
                if (museum.Guided.Equals("Turi gidą"))
                {
                    guideNum++;
                }
            }

            return guideNum;
        }

        /// <summary>
        /// Creates list of museums that work the most
        /// </summary>
        /// <param name="MostWorkingMuseum"> Museum object</param>
        /// <param name="Filtered"> a list for filtered museums to be stored in</param>
        /// <param name="compared"> museum comparison register</param>
        /// <returns></returns>
        public List<Museum> ComparedByWorking(Museum MostWorkingMuseum, List<Museum> Filtered, MuseumsRegister compared) 
        {
            for (int i = 0; i < compared.Museums.Count; i++)            
            {
                if (MostWorkingMuseum == compared.Museums[i]) 
                {
                    if (!Filtered.Contains(compared.Museums[i]))
                    {
                        Filtered.Add(compared.Museums[i]);
                    }
                }
            }
            return Filtered;
        }

        /// <summary>
        /// Makes a list of free to enter museums
        /// </summary>
        /// <param name="Freebies"> List of museums that are free to enter </param>
        /// <param name="register"> the main storage register </param>
        /// <returns></returns>
        public List<Museum> FreeMuseums(List<Museum> Freebies, MuseumsRegister register) 
        {
            for (int i = 0; i < register.Museums.Count; i++)
            {
                if (register.Museums[i] == 0) 
                {
                   if (!Freebies.Contains(register.Museums[i])) 
                   {
                       Freebies.Add(register.Museums[i]);
                   }
                }
            }
            return Freebies;
        }
    }
}