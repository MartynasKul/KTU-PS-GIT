using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    [DataContract]
    internal class Character
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Age")]
        public int Age { get; set; }
        [DataMember(Name = "Power")]
        public double Power { get; set; }

        public Character(string name, int age, double power)
        {
            Name = name;
            Age = age;
            Power = power;
        }

    }
}
