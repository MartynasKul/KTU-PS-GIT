using System.Collections.Generic;

namespace U3_8
{
    class MuseumsRegister
    {
        private MuseumsContainer AllMuseums;
        public string Manager { get; set; } // pirmos eilutes saugomi duomenys
        public string City { get; set; } // antros eilutes saugomi duomenys

        /// <summary>
        /// creates new list
        /// </summary>
        public MuseumsRegister()
        {
            AllMuseums = new MuseumsContainer();
        }

        /// <summary>
        /// Adds new objects to container
        /// </summary>
        /// 
        public MuseumsRegister(MuseumsContainer Museums) 
        {
            AllMuseums = new MuseumsContainer();
            for (int i = 0; i < Museums.Count; i++)
            {
                AllMuseums.Add(Museums.Get(i));
            }
        }

        /// <summary>
        /// gives ammount of museums in container
        /// </summary>
        public int MuseumsCount() 
        {
            return AllMuseums.Count;
        }

        /// <summary>
        /// Adds a new object to container
        /// </summary>
        public void Add(Museum museum) 
        {
            AllMuseums.Add(museum);
        }

        /// <summary>
        /// returns museum object that is in the indexed place on the container
        /// </summary>
        public Museum Get(int index)
        {
            return AllMuseums.Get(index);
        }

        /// <summary>
        /// Finds museum that works the most
        /// </summary>
        public int MostWorking()
        {
            int ID = 0;

            for (int i = 0; i < AllMuseums.Count; i++)
            {
                if (AllMuseums.Get(i) > AllMuseums.Get(ID))
                {
                    ID = i; // index of most working museum
                }
            }

            return ID;
        }

        /// <summary>
        /// Creates list of museums that work the most
        /// </summary>
        public MuseumsContainer ComparedByWorking(Museum MostWorkingMuseum, MuseumsContainer Filtered, MuseumsRegister compared)
        {
            for (int i = 0; i < compared.AllMuseums.Count; i++)
            {
                if (MostWorkingMuseum == AllMuseums.Get(i))
                {
                    if (!Filtered.Contains(AllMuseums.Get(i)))
                    {
                        Filtered.Add(AllMuseums.Get(i));
                    }
                }
            }

            return Filtered;
        }

        /// <summary>
        /// Creates list of museums that work on Wednesday
        /// </summary>
        public List<string> FindWorkingOnWednesday(List<string> City, MuseumsRegister compared)
        {
            for (int i = 0; i < compared.AllMuseums.Count; i++)
            {
                if (AllMuseums.Get(i).Wednes == 1)
                {
                    City.Add(AllMuseums.Get(i).Type);
                }
            }
            return City;
        }

        /// <summary>
        /// removes duplicate museum types with the help of HashSet(no duplicate entries are allowed in this type of list)
        /// </summary>
        public void RemoveDuplicateMuseumTypes(ref List<string> CityTypes)
        {
            List<string> NoDuplicates = new List<string>();
            HashSet<string> hash = new HashSet<string>();

            foreach (string line in CityTypes)
            {
                if (hash.Add(line))
                {
                    NoDuplicates.Add(line);
                }
            }

            CityTypes = NoDuplicates;
        }

        /// <summary>
        /// Adds to container museum information that shares the same names
        /// </summary>
        public MuseumsContainer DuplicateNames(MuseumsContainer SameNames, MuseumsRegister register1, MuseumsRegister register2)
        {
            for (int i = 0; i < register1.AllMuseums.Count; i++)
            {
                for (int j = 0; j < register2.AllMuseums.Count; j++)
                {
                    if (register1.AllMuseums.Get(i).Equals(register2.AllMuseums.Get(j)))
                    {
                        SameNames.Add(register1.AllMuseums.Get(i));
                        SameNames.Add(register2.AllMuseums.Get(j));
                    }
                }
            }

            return SameNames;
        }

        /// <summary>
        /// Makes a container of free to enter museums
        /// </summary>
        public MuseumsContainer FreeMuseums(MuseumsContainer Freebies, MuseumsRegister register)
        {
            for (int i = 0; i < register.MuseumsCount(); i++)
            {
                if (register.Get(i) == 0)
                {
                    if (!Freebies.Contains(register.Get(i)))
                    {
                        Freebies.Add(register.Get(i));
                    }
                }
            }

            return Freebies;
        }

    }
}