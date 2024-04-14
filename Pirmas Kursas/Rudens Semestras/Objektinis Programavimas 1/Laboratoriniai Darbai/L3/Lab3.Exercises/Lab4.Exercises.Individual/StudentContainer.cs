using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual
{
    class StudentContainer
    {
        public string Faculty { get; set; }
        private Student[] students;
        public int Count { get; private set; }
        private int Capacity;

        public StudentContainer(int capacity = 16)
        {
            this.Capacity = capacity;
            this.students = new Student[capacity];
        }

        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Student[] temp = new Student[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.students[i];
                }
                this.Capacity = minimumCapacity;
                this.students = temp;
            }
        }

        public void Add(Student student)
        {
            if (this.Count == this.Capacity)
            {
                EnsureCapacity(this.Capacity * 2);
            }
            this.students[this.Count++] = student;
        }

        public Student Get(int index)
        {
            return this.students[index];
        }

        public bool Contains(Student student)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.students[i].Equals(student))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetCount() 
        {
            int count = 0;

            for (int i = 0; i < students.Length; i++)
            {
                if (students[i] != null) 
                {
                    count++;
                }
            }

            return count;
        }

        public void SortStudents()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < GetCount() - 1; i++)
                {
                    for (int j = i; j < GetCount(); j++)
                    {
                        if (students[i].Surname.CompareTo(students[j].Surname) < 0 && students[i].Average < students[j].Average) 
                        {
                            Student temp = students[i];
                            students[i] = students[j];
                            students[j] = temp;
                        }
                    }
                }                
            }
        }
    }
}
