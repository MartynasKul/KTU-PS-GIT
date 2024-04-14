using System;

namespace U5_8
{
    class Place
    {
        public string Name { get; set; } // name of place
        public string Adress { get; set; } // adress of place
        public int DoB { get; set; } // date of birth or when was the place created

        public Place(string name, string adress, int dob)
        {
            this.Name = name;
            this.Adress = adress;
            this.DoB = dob;
        }
        public Place() { }

        public override bool Equals(object other)
        {
            return this.DoB == ((Place)other).DoB;
        }
        public override int GetHashCode()
        {
            return this.DoB.GetHashCode();
        }

        public int CompareTo(Place other)
        {
            int result = this.DoB.CompareTo(other.DoB);
            if (result == 0)
            {
                return this.Name.CompareTo(other.Name);
            }
            return result;
        }

        /// <summary>
        /// Overrides the method ToString() to return a string in a required format
        /// </summary>
        /// <returns>A string of information in a required format</returns>
        public override string ToString()
        {
            string line;
            line = string.Format(String.Format("| {0,-30} | {1,-30} | {2,8} |", Name, Adress, DoB));

            return line;
        }
    }
}
