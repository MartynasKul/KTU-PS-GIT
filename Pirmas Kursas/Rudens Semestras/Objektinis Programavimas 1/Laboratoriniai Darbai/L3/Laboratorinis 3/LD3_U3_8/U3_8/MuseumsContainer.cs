using System;

namespace U3_8
{
    class MuseumsContainer
    {
        private Museum[] museums;
        private int Capacity;
        public int Count { get; private set; }

        public MuseumsContainer( int capacity=16) 
        {
            this.Capacity = capacity; // int capacity = 16 is the default capacity of container
            this.museums = new Museum[capacity];
        }

        /// <summary>
        /// makes room for extra museums if current container capacity inst enough
        /// </summary>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Museum[] temp = new Museum[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.museums[i];
                }
                this.Capacity = minimumCapacity;
                this.museums = temp;
            }
        }

        /// <summary>
        /// adds a new instance of museum to container
        /// </summary>
        public void Add(Museum museum) 
        {
            if (this.Count == this.Capacity) // container is full
            {
                EnsureCapacity(this.Capacity + 2);
            }
            this.museums[this.Count++] = museum;
        }

        /// <summary>
        /// returns a certain museums information according to index
        /// </summary>
        public Museum Get(int index) 
        {
            return this.museums[index];
        }

        /// <summary>
        /// checks if museum in question exists in container
        /// </summary>
        public bool Contains(Museum museum) 
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.museums[i].Equals(museum)) 
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sorts the container by height (lowest to highest), surname and name
        /// </summary>
        /// 
        public void Sort()
        {
            int n = this.Count;
            if (n > 0)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    int min = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (museums[j].CompareTo(museums[min]) < 0)
                        {
                            min = j;
                        }
                    }

                    Museum temp = museums[i];
                    museums[i] = museums[min];
                    museums[min] = temp;
                }
            }
        }

        /// <summary>
        /// Puts new museum object in index location
        /// </summary>
        public void Put(Museum newMuseum, int index)
        {
            if (index >= 0 && index < Count)
            {
                this.museums[index] = newMuseum;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// inserts new museum object in index location
        /// </summary>
        public void Insert(Museum newMuseum, int index)
        {
            if (index >= 0 && index <= Count)
            {
                Count++;
                Museum temp = newMuseum;
                for (int i = index; i < Count; i++)
                {
                    EnsureCapacity(Count);
                    Museum removed = museums[i];
                    this.museums[i] = temp;
                    temp = removed;
                }
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes museum object at index location
        /// </summary>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count-1; i++)
                {
                    this.museums[i] = museums[i + 1];
                }
                Count--;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes museum object by name while using RemoveAt() method
        /// </summary>
        /// 
        public void Remove(string name)
        {
            bool flag = false;
            for (int i = 0; i < Count; i++)
            {
                if (this.museums[i].Name.Equals(name))
                {
                    flag = true;
                    RemoveAt(i);
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("Muziejaus nurodytu pavadinimu ({0}) sąraše nėra", name);
            }
        }

    }
}
