using System.Runtime.Serialization;

namespace Lab1A
{
    [DataContract]
    internal class Car
    {

        [DataMember(Name = "Make")]
        public string Make { get;set; }
        [DataMember(Name = "Model")]
        public string Model { get;set; }
        [DataMember(Name = "Year")]
        public int Year { get;set; }
        [DataMember(Name = "Plate")]
        public string Plate { get;set; }
        [DataMember(Name = "Power")]
        public int Power { get;set; }  

        public Car(string make, string model, int year, string plate, int power) 
        {
            Make = make;
            Model = model;
            Year = year;
            Plate = plate;
            Power = power;      
        
        }
        public override int GetHashCode()
        {
            
            string combinedProperties = $"{Make}{Model}{Year}{Plate}{Power}";
            return combinedProperties.GetHashCode();
        }
    }
}
