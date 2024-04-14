namespace U5_8
{
    class Register
    {
        private PlaceContainer AllPlaces;
        public string Manager { get; set; } // pirmos eilutes saugomi duomenys
        public string City { get; set; } // antros eilutes saugomi duomenys

        /// <summary>
        /// creates new list
        /// </summary>
        public Register()
        {
            AllPlaces = new PlaceContainer();
        }

        /// <summary>
        /// Adds new objects to container
        /// </summary>
        /// 
        public Register(PlaceContainer Places)
        {
            AllPlaces = new PlaceContainer();
            for (int i = 0; i < Places.Count; i++)
            {
                AllPlaces.Add(Places.Get(i));
            }
        }

        /// <summary>
        /// gives ammount of museums in container
        /// </summary>
        public int PlacesCount()
        {
            return AllPlaces.Count;
        }

        /// <summary>
        /// gives ammount of museums in container
        /// </summary>
        public PlaceContainer GetContainer()
        {
            return AllPlaces;
        }

        /// <summary>
        /// Adds a new object to container
        /// </summary>
        public void Add(Place place)
        {
            AllPlaces.Add(place);
        }

        /// <summary>
        /// returns museum object that is in the indexed place on the container
        /// </summary>
        public Place Get(int index)
        {
            return AllPlaces.Get(index);
        }

        /// <summary>
        /// Finds museum that works the most
        /// </summary>
        public int MostWorking()
        {
            int ID = 0;

            for (int i = 0; i < AllPlaces.Count; i++)
            {
                Place max = AllPlaces.Get(ID);
                Place checker = AllPlaces.Get(i);
                if (max is Museum && checker is Museum)
                {
                    if ((max as Museum).WorkingDays > (checker as Museum).WorkingDays)
                    {
                        ID = i; // index of most working museum
                    }
                }
                else 
                {
                    ID++;
                }

               
            }

            return ID;
        }

        /// <summary>
        /// counts number of museum guides in register
        /// </summary>
        public void MuseumGuideCount(ref int count) 
        {
            for (int i = 0; i < PlacesCount(); i++)
            {
                Place place = Get(i);
                if (place is Museum) 
                {
                    if ((place as Museum).Guided == "Turi gidą") 
                    {
                        count++;
                    }
                }
            }
        }

        /// <summary>
        /// finds oldest place in register
        /// </summary>
        public void OldestPlace(ref int OldDate, ref Place oldestPlace) 
        {
            for (int i = 0; i < PlacesCount(); i++)
            {
                if (AllPlaces.Get(i).DoB < OldDate) 
                {
                    OldDate = AllPlaces.Get(i).DoB;
                    oldestPlace = AllPlaces.Get(i);
                }
            }
        }

        /// <summary>
        /// Adds places that were created after 1990s to a register
        /// </summary>
        public void After1990s(ref Register After90s, Register register)
        {
            for (int i = 0; i < register.PlacesCount(); i++)
            {
                if (register.Get(i).DoB > 1990) 
                {
                    After90s.Add(register.Get(i));
                }
            }
        }
    }
}
