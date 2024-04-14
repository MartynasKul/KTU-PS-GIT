//Martynas Kuliešius IFF-1/9

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KD1Perlaikymas
{
    class Travel 
    {
        public string TouristName { get; set; }
        public string TravelName { get; set; }
        public int Duration { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }

        public Travel(string travelName, int duration, int number, decimal price)
        {
            this.TravelName = travelName;
            this.Duration = duration;
            this.Number = number;
            this.Price = price;
        }

        public Travel(string touristName, int duration, int number) 
        {
            this.TouristName = touristName;
            this.Duration = duration;
            this.Number = number;
        }

        public static bool operator <(Travel lhs, Travel rhs) 
        {
            if (lhs.Duration == rhs.Duration)
            {
                if (rhs.Number <= lhs.Number)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        
        }
        public static bool operator >(Travel rhs, Travel lhs) 
        {
            if (lhs.Duration == rhs.Duration)
            {
                if (rhs.Number >= lhs.Number)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string ToString() 
        {
            string line = String.Format("| {0,-15} | {1,-15} | {2, 3} | {3, 3} | {4, 8} |",
                this.TouristName, this.TravelName, this.Duration, this.Number, this.Price);

            return line;
        }

    }

    class TravelAgency 
    {
        List<Travel> AllTours = new List<Travel>();
        public TravelAgency() 
        {
            List<Travel> AllTours=new List<Travel>();
        }
        public TravelAgency(List<Travel> allTours)
        {
            List<Travel> AllTours = new List<Travel>();

            foreach (Travel tour in allTours) 
            {
                AllTours.Add(tour);
            }
        }

        public int GetCount() 
        {
            return AllTours.Count;
        }

        public Travel GetEntry(int index) 
        {
            return AllTours[index]; 
        }

        public void AddEntry(Travel travel) 
        {
            AllTours.Add(travel);
        }

        public decimal Sum(int duration) 
        {
            decimal sum = 0;
            foreach (Travel travel in AllTours) 
            {
                if (travel.Duration == duration) 
                {
                    sum += travel.Number * travel.Price;
                }
            }

            return sum;
        }

        public int MinPriceIndex(Travel travel) 
        {
            // metode kazka ne taip parasiau ir jis neveikia

            int ID = 0;

            for (int i = 0; i < GetCount(); i++)
            {
                if (travel.TravelName.Equals(GetEntry(i).TravelName)) 
                {
                    if (travel < GetEntry(i)) 
                    {
                        ID = i;
                    }
                }
            }

            return ID;
        
        }

        public void AddTouristOrder(List<Travel> touristOrder) 
        {
            for (int i = 0; i < GetCount(); i++)
            {
                for (int j = 0; j < touristOrder.Count; j++)
                {
                    if (touristOrder[j].TravelName == null) 
                    {
                        if (touristOrder[j].Duration == GetEntry(i).Duration && touristOrder[j].Number <= GetEntry(i).Number)
                        {
                            touristOrder[j].TravelName = GetEntry(i).TravelName;
                            touristOrder[j].Price = GetEntry(i).Price;
                            GetEntry(i).Number -= touristOrder[j].Number;
                        }
                    }                    
                }
            }
        }
    }

    class InOut 
    {
        public static TravelAgency ReadTravelData(string fileName) 
        {
            TravelAgency Tours = new TravelAgency();

            string[] Lines = File.ReadAllLines(fileName);
            foreach (string line in Lines) 
            {
                string[] Bits = line.Split(';');
               
                string travelName = Bits[0];
                int duration = Convert.ToInt32(Bits[1]);
                int number = Convert.ToInt32(Bits[2]);
                decimal price = Convert.ToDecimal(Bits[3]);

                Travel tour = new Travel(travelName, duration, number, price);
                Tours.AddEntry(tour);        
            }

            return Tours;
        }

        public static List<Travel> ReadTouristData(string fileName) 
        {
            List<Travel> AllTourists = new List<Travel>();

            string[] Lines = File.ReadAllLines(fileName);

            foreach (string line in Lines) 
            {
                string[] Bits = line.Split(';');

                string touristName = Bits[0];
                int duration = Convert.ToInt32(Bits[1]);
                int number = Convert.ToInt32(Bits[2]);

                Travel NewTourist = new Travel(touristName, duration, number);
                AllTourists.Add(NewTourist);
            }

            return AllTourists;            
        }
        public static void Print(TravelAgency allTravels, string fileName, string header) 
        {
            string dashes = "------------------------------------------------------------";

            using (var fn = File.AppendText(fileName)) 
            {
                fn.WriteLine(header);
                fn.WriteLine("\n");
                fn.WriteLine(dashes);

                for (int i = 0; i < allTravels.GetCount(); i++)
                {
                    fn.WriteLine(allTravels.GetEntry(i).ToString());
                    fn.WriteLine(dashes);
                }
            }
        }
        public static void Print(List<Travel> orderTravels, string fileName, string header)
        {
            string dashes = "------------------------------------------------------------";

            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine("\n");
                fn.WriteLine(dashes);

                foreach (Travel travel in orderTravels) 
                {
                    fn.WriteLine(travel.ToString());
                    fn.WriteLine(dashes);
                }
            }
        }

        public static void PrintLeftovers(decimal sum, string fileName) 
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine("Agentūroje liko neužsakytų 4 dienų trukmės kelionių už {0} eur.", sum);            
            }
        }

    }


    internal class Program
    {
        static void Main(string[] args) 
        {
            if (File.Exists(@"Result.txt")) 
            {
                File.Delete(@"Result.txt");
            }


            TravelAgency Agency = InOut.ReadTravelData("Travel.txt");
            List<Travel> Tourists = InOut.ReadTouristData("TouristOrder.txt");

            InOut.Print(Agency, "Result.txt", "Pradiniai Kelionų agentūros duomenys");
            InOut.Print(Tourists, "Result.txt", "Pradiniai kelionių užsakymo duomenys");

            Agency.AddTouristOrder(Tourists);

            InOut.Print(Agency, "Result.txt", "Atnaujintas sąrašas");
            InOut.Print(Tourists, "Result.txt", "Užpildyti kelionių užsakymų duomenys");
            InOut.PrintLeftovers(Agency.Sum(4), "Result.txt");

            Console.ReadKey();
        }
    }
}
