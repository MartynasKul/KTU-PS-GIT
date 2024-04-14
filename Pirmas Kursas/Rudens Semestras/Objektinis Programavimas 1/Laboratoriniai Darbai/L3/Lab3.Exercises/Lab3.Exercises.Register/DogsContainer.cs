using System;

namespace Lab3.Exercises.Register
{
    class DogsContainer
    {
        private Dog[] dogs;
        public int Count { get; private set; }
        private int Capacity;

       
        public DogsContainer(int capacity = 16) //parameter with default value
        {
            this.Capacity = capacity;
            this.dogs = new Dog[capacity];
        }        public DogsContainer(DogsContainer container) : this() //calls another constructor
        {
            for (int i = 0; i < container.Count; i++)
            {
                this.Add(container.Get(i));
            }
        }        /// <summary>
        /// adds new dog to dog container
        /// </summary>
        public void Add(Dog dog)
        {
            if (this.Count == this.Capacity) //container is full
            {
                EnsureCapacity(this.Capacity * 2);
            }
            this.dogs[this.Count++] = dog;
        }

        /// <summary>
        /// returns dog information in index location
        /// </summary>
        /// <returns>dog object</returns>
        public Dog Get(int index)
        {
            return this.dogs[index];
        }        /// <summary>
        /// checks if dog is in container
        /// </summary>
        /// <returns></returns>        public bool Contains(Dog dog)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.dogs[i].Equals(dog))
                {
                    return true;
                }
            }
            return false;
        }        /// <summary>
        /// Puts new dog in index location
        /// </summary>
        /// <param name="dog"></param>
        /// <param name="index"></param>        public void Put(Dog dog, int index)
        {
            if (index >= 0 && index < Count)
            {
                this.dogs[index] = dog;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }

        }        /// <summary>
        /// inserts new dog in index location
        /// </summary>
        /// <param name="dog"></param>
        /// <param name="index"></param>        public void Insert(Dog dog, int index)
        {
            if (index >= 0 && index <= Count)
            {
                Count++;
                Dog temp = dog;
                for (int i = index; i < Count; i++)
                {
                    Dog removed = dogs[i];
                    this.dogs[i] = temp;
                    temp = removed;
                }
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes dog at index location
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count; i++)
                {
                    this.dogs[i] = dogs[i + 1];
                }
                Count--;
            }
            else
            {
                Console.WriteLine("Nurodytas netinkamas indeksas");
            }
        }

        /// <summary>
        /// removes dog by id while using RemoveAt() method
        /// </summary>
        /// <param name="ID"></param>
        public void Remove(int ID)
        {
            bool flag = false;
            for(int i = 0; i < Count; i++)
            {
                if (this.dogs[i].ID == ID)
                {
                    flag = true;
                    RemoveAt(i);
                }
            }
            if(!flag)
            {
                Console.WriteLine("Šunio su nurodytu ID ({0}) nerasta", ID);
            }
        }

        /// <summary>
        /// Makes sure theres enough capacity
        /// </summary>
        /// <param name="minimumCapacity"></param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Dog[] temp = new Dog[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.dogs[i];
                }
                this.Capacity = minimumCapacity;
                this.dogs = temp;
            }
        }

        /// <summary>
        /// sorts dogs by breed
        /// </summary>
        public void Sort()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    Dog a = this.dogs[i];
                    Dog b = this.dogs[i + 1];
                    if (a.CompareTo(b) > 0)
                    {
                        this.dogs[i] = b;
                        this.dogs[i + 1] = a;
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// sorts dogs by gender
        /// </summary>
       public void SortGender()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    Dog a = this.dogs[i];
                    Dog b = this.dogs[i + 1];
                    
                    if (a.CompareGender(b) > 0 && a.Breed == b.Breed)
                    {
                        this.dogs[i] = b;
                        this.dogs[i + 1] = a;
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// combines containers
        /// </summary>
        /// <param name="other"></param>
        /// <returns>combined container</returns>
        public DogsContainer Intersect(DogsContainer other)
        {
            DogsContainer result = new DogsContainer();
            for (int i = 0; i < this.Count; i++)
            {
                Dog current = this.dogs[i];
                if (other.Contains(current))
                {
                    result.Add(current);
                }
            }
            return result;
        }
    }
}
