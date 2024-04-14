using System;

namespace U5_8
{
    class PlaceContainer
    {
        private Place[] places;
        private int Capacity;
        public int Count { get; private set; }

        public PlaceContainer(int capacity = 16)
        {
            this.Capacity = capacity; // int capacity = 16 is the default capacity of container
            this.places = new Place[capacity];
        }

        /// <summary>
        /// makes room for extra places if current container capacity inst enough
        /// </summary>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Place[] temp = new Place[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.places[i];
                }
                this.Capacity = minimumCapacity;
                this.places = temp;
            }
        }

        /// <summary>
        /// adds a new instance of place to container
        /// </summary>
        public void Add(Place place)
        {
            if (this.Count == this.Capacity) // container is full
            {
                EnsureCapacity(this.Capacity + 2);
            }
            this.places[this.Count++] = place;
        }

        /// <summary>
        /// returns a certain places information according to index
        /// </summary>
        public Place Get(int index)
        {
            return this.places[index];
        }

        /// <summary>
        /// checks if place in question exists in container
        /// </summary>
        public bool Contains(Place place)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.places[i].Equals(place))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds all register information to one register
        /// </summary>
        public void CombineAll(ref PlaceContainer EveryPlace, PlaceContainer register)
        {
            for (int i = 0; i < register.Count; i++)
            {
                EveryPlace.Add(register.Get(i));
            }
        }

        /// <summary>
        /// Sorts the container by DoB(date of birth/ creation date) and then by name
        /// </summary>
        public void Sort()
        {
            int n = this.Count;

            for (int i = 0; i < n-1; i++)
            {
                for (int j = 0; j < n-i-1; j++)
                {
                    if (places[j].CompareTo(places[j + 1]) > 0)
                    {
                        Place temp = places[j];
                        places[j] = places[j+1];
                        places[j+1] = temp;
                    }
                    if (places[j].CompareTo(places[j + 1]) == 0)
                    {
                        Place temp = places[j];
                        places[j] = places[j + 1];
                        places[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Puts new place object in index location
        /// </summary>
        public void Put(Place newPlace, int index)
        {
            if (index >= 0 && index < Count)
            {
                this.places[index] = newPlace;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// inserts new place object in index location
        /// </summary>
        public void Insert(Place newPlace, int index)
        {
            if (index >= 0 && index <= Count)
            {
                Count++;
                Place temp = newPlace;
                for (int i = index; i < Count; i++)
                {
                    Place removed = places[i];
                    this.places[i] = temp;
                    temp = removed;
                }
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes place object at index location
        /// </summary>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count; i++)
                {
                    this.places[i] = places[i + 1];
                }
                Count--;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes place object by name while using RemoveAt() method
        /// </summary>
        public void Remove(string name)
        {
            bool flag = false;
            for (int i = 0; i < Count; i++)
            {
                if (this.places[i].Name.Equals(name))
                {
                    flag = true;
                    RemoveAt(i);
                }
            }
            if (!flag)
            {
                Console.WriteLine("Vietos nurodytu pavadinimu ({0}) sąraše nėra", name);
            }
        }
    }
}
