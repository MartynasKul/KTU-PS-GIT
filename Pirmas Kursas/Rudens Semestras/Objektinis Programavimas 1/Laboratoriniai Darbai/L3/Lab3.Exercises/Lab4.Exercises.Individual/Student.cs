using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual
{
    class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int Amount { get; set; }
        private int[] Grades;

        public Student(string surname, string name, string group, int amount, int[] grades)
        {
            this.Surname = surname;
            this.Name = name;
            this.Group = group;
            this.Amount = amount;

            Grades=new int[amount];
            for (int i = 0; i < amount; i++)
            {
                this.Grades[i] = grades[i];
            }
        }

        public double Average
        {
            get
            {
                double average = 0;
                for(int i = 0; i < Amount; i++)
                {
                    average += Grades[i];
                }
                average /= Amount;
                return average;
            }
        }

        public int GetGrade(int index) 
        {
            return Grades[index]; 
        }


        public override string ToString()
        {
            return string.Format("| {0,-20} | {1,-20} | {2,10} | {3,15} |", 
                Surname, Name, Group, Amount);
        }

        public static bool operator ==(string group, Student student)
        {
            return group == student.Group;
        }

        public static bool operator !=(string group, Student student)
        {
            return group != student.Group;
        }


       // public static bool operator <(Student lhs, Student rhs) 
       // {
       //     if (lhs.Name.CompareTo(rhs.Name) == 1 && lhs.Average < rhs.Average) 
       //     {
       //         return true; 
       //     }
       //     else { return false; }
       // }
       // public static bool operator >(Student lhs, Student rhs) 
       // {
       //     if (lhs.Name.CompareTo(rhs.Name) < 1 && lhs.Average > rhs.Average)
       //     {
       //         return true;
       //     }
       //     else { return false; }
       // }


        public override bool Equals(object obj)
        {
            return this.Surname == ((Student)obj).Surname;
        }

        public override int GetHashCode()
        {
            return this.Surname.GetHashCode();
        }
    }
}
