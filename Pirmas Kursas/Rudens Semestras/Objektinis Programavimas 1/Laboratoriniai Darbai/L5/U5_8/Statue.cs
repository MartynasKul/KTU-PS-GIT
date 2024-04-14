using System;

namespace U5_8
{
    class Statue : Place
    {
        public string Author { get; set; }
        public string Recipient { get; set; }

        public Statue(string name, string adress, int dob, string author,
            string recipient) : base(name, adress, dob)
        {
            this.Author = author;
            this.Recipient = recipient;
        }
        public override string ToString()
        {
            string line;
            line = string.Format(String.Format("| {0,-30} | {1,-30} | {2,6} | {3,-15} | {4,-15} |", Name, Adress, DoB, Author, Recipient));

            return line;
        }
    }
}
