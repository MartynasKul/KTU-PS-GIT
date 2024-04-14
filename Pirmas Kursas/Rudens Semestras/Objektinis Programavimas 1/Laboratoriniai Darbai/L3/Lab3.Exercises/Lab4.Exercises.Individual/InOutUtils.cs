using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab4.Exercises.Individual
{
    static class InOutUtils
    {
        public static StudentContainer ReadStudents(string fileName)
        {
            StudentContainer Students = new StudentContainer();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);
            Students.Faculty = Lines[0];
            for (int i = 1; i < Lines.Length; i++)
            {
                string[] Values = Lines[i].Split(';');
                string surname = Values[0];
                string name = Values[1];
                string group = Values[2];
                int amount = int.Parse(Values[3]);
                int[] marks = new int[amount];
                for (int j = 4; j < Values.Length; j++)
                {
                    marks[j-4] = int.Parse(Values[j]);

                }
                Student student = new Student(surname, name, group, amount, marks);
                Students.Add(student);
            }
            return Students;
        }

        /// <summary>
        /// Prints all stundents info
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Students"></param>
        public static void PrintStudents(string fileName, StudentContainer Students)
        {
            using (var fn = File.AppendText(fileName))
            {
                
                fn.WriteLine(Students.Faculty);
                fn.WriteLine(new string('-', 100));
                fn.WriteLine(String.Format("| {0,-20} | {1,-20} | {2,-10} | {3,15} | {4,20} |",
                "Pavardė", "Vardas", "Grupė", "Paž. Kiekis", "Pažymiai"));
                fn.WriteLine(new string('-', 100));
                for (int i = 0; i < Students.Count; i++)
                {
                    Student student = Students.Get(i);
                    fn.Write(student.ToString());
                    for (int j = 0; j < student.Amount; j++)
                        fn.Write(string.Format(" {0,1} ", student.GetGrade(j)));
                    
                    fn.WriteLine();
                }
                fn.WriteLine(new string('-', 100));
            }
        }

        /// <summary>
        /// writes results to txt file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Groups"></param>
        /// <param name="Averages"></param>
        public static void PrintGroupAndAverage(string fileName, List<string> Groups, List<double> Averages)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine("Grupės ir jų vidurkiai");
                fn.WriteLine(new string('-', 27));
                fn.WriteLine(String.Format("| {0, -10} | {1, 10} |", 
                    "Grupės", "Vidurkiai"));
                fn.WriteLine(new string('-', 27));
                for(int i = 0; i < Groups.Count; i++)
                {
                    fn.WriteLine(String.Format("| {0, -10} | {1, 10:0.00} |", 
                        Groups[i], Averages[i]));
                }
                fn.WriteLine(new string('-', 27));
            }
        }
    }
}
