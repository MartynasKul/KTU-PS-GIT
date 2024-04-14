using System.Collections.Generic;

namespace Lab4.Exercises.Individual
{
    class StudentRegister
    {
        StudentContainer AllStudents;

        public StudentRegister()
        {
            AllStudents = new StudentContainer();
        }

        public StudentRegister(StudentContainer Students)
        {
            AllStudents = new StudentContainer();
            for (int i = 0; i < Students.Count; i++)
            {
                AllStudents.Add(Students.Get(i));
            }
        }

        public int StudentsCount()
        {
            return AllStudents.Count;
        }

        public void Add(Student student)
        {
            AllStudents.Add(student);
        }

        public List<string> Groups()
        {
            List<string> groups = new List<string>();

            for(int i = 0; i < AllStudents.Count; i++)
            {
                Student temp = AllStudents.Get(i);
                if (!groups.Contains(temp.Group))
                {
                    groups.Add(temp.Group);
                }
            }

            return groups;
        }

        private double Sum(Student student)
        {
            double sum = 0;
            for(int i = 0; i < student.Amount; i++)
            {
                sum += student.GetGrade(i);
            }
        
            return sum;
        }

        public List<double> GroupAverage(List<string> Groups)
        {
            List<double> Average = new List<double>();
            double average;
            int count;
            
            for(int i = 0; i < Groups.Count; i++)
            {
                average = 0;
                count = 0;
                Average.Add(0);
                for (int j = 0; j < AllStudents.Count; j++)
                {
                    Student temp = AllStudents.Get(j);
                    if(Groups[i] == temp)
                    {
                        average += Sum(temp);
                        count += temp.Amount;
                    }
                }
                Average[i] += average;
                Average[i] /= count;
            }
            return Average;
        }


        public void Sort(List<string> Groups, List<double> Averages)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < Averages.Count - 1; i++)
                {
                    double a = Averages[i];
                    string groupA = Groups[i];
                    
                    for (int j = i; j < Averages.Count; j++)
                    {
                        double b = Averages[j];
                        string groupB = Groups[j];
                        if (a.CompareTo(b) < 0)
                        {
                            Averages[i] = b;
                            Averages[j] = a;
                            string temp = Groups[j];
                            Groups[j] = Groups[i];
                            Groups[i] = temp;
                        }
                        if (groupA.CompareTo(groupB) > 0 && Averages[i] == Averages[j])
                        {
                            Groups[j] = groupA;
                            Groups[i] = groupB;
                        }
                    }
                }
            }
        }
    }
}
