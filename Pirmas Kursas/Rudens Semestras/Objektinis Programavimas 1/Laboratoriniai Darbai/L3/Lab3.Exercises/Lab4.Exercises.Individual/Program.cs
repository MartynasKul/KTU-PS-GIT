using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab4.Exercises.Individual
{
    class Program
    {
        static void Main(string[] args)
        {
            string CFr = @"Rezultatai.txt";
            if (File.Exists(CFr))
            {
                File.Delete(CFr);
            }
            StudentContainer allStudents = InOutUtils.ReadStudents(@"Students.csv");
            allStudents.SortStudents();
            InOutUtils.PrintStudents(CFr, allStudents);



            StudentRegister register = new StudentRegister(allStudents);
            List<string> Groups = register.Groups();
            List<double> Averages = register.GroupAverage(Groups);
            register.Sort(Groups, Averages);
            InOutUtils.PrintGroupAndAverage(CFr, Groups, Averages);

            Console.ReadKey();
        }
    }
}
