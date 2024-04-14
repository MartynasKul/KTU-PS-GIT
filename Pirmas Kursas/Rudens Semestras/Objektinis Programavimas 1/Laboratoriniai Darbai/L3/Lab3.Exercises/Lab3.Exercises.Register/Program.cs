using System;
using System.Collections.Generic;

namespace Lab3.Exercises.Register
{
    class Program
    {
        static void Main(string[] args)
        {
            DogsContainer allDogs = InOutUtils.ReadDogs(@"Dogs.csv");
            DogsContainer allDogsCopy = new DogsContainer(allDogs);
            DogsRegister register = new DogsRegister(allDogs);

            InOutUtils.PrintDogs("Pradinis", allDogs);

            allDogs.Sort();
            InOutUtils.PrintDogs("Sort", allDogs);

            allDogs.SortGender();
            InOutUtils.PrintDogs("Breed-Gender", allDogs);


            Console.WriteLine("Iš viso šunų: {0}", register.DogsCount());
            Console.WriteLine("Patinų: {0}", register.CountByGender(Gender.Male));
            Console.WriteLine("Patelių: {0}", register.CountByGender(Gender.Female));
            Console.WriteLine();

            Gender gender;
            Enum.TryParse("Male", out gender);
            Dog testDog = new Dog(500, "pupper", "doggo", DateTime.Parse("2020-10-15"), gender); // testinio suniuko objektas

            allDogs.Put(testDog, 5);
            InOutUtils.PrintDogs("Put", allDogs);
            Console.WriteLine("");

            allDogs.Insert(testDog, 1);
            InOutUtils.PrintDogs("Insert", allDogs);
            Console.WriteLine("");

            allDogs.Remove(500);
            InOutUtils.PrintDogs("Remove", allDogs);
            Console.WriteLine("");

            allDogs.RemoveAt(6);
            InOutUtils.PrintDogs("RemoveAt", allDogs);
            Console.WriteLine("");

            allDogs.Add(testDog);
            InOutUtils.PrintDogs("Add", allDogs);
            Console.WriteLine("");

            Dog oldest = register.FindOldestDog();
            Console.WriteLine("Seniausias šuo");
            Console.WriteLine("Vardas: {0}, Veislė: {1}, Amžius: {2}",
             oldest.Name, oldest.Breed, oldest.Age);

            List<string> Breeds = register.FindBreeds();
            Console.WriteLine("Šunų veislės:");
            InOutUtils.PrintBreeds(Breeds);
            Console.WriteLine();

            Console.WriteLine("Kokios veislės šunis atrinkti?");
            string selectedBreed = Console.ReadLine();
            DogsContainer FilteredByBreed = register.FilterByBreed(selectedBreed);
            InOutUtils.PrintDogs("Pagal veislę:" , FilteredByBreed);
            Console.WriteLine();

            string fileName = selectedBreed + ".csv";
            InOutUtils.PrintDogsToCSVFile(fileName, FilteredByBreed);

            Console.WriteLine("Kokios veislės seniausią šunį atrinkti?");
            string oldestSelectedBreed = Console.ReadLine();
            Dog oldestByBreed = register.FindOldestDog(oldestSelectedBreed);
            Console.WriteLine("Seniausias pasirinktos veislės šuo: ");
            Console.WriteLine("Vardas: {0}, Veislė: {1}, Amžius: {2}",
             oldestByBreed.Name, oldestByBreed.Breed, oldestByBreed.Age);
            Console.WriteLine();

            string mostPopular = register.MostPopularBreed(Breeds);
            Console.WriteLine("Populiariausia veislė: {0}", mostPopular);
            Console.WriteLine();

            List<Vaccination> VaccinationsData = InOutUtils.ReadVaccinations(@"Vaccinations.csv");
            register.UpdateVaccinationsInfo(VaccinationsData);

            DogsContainer requiresVaccination = register.FilterByVaccinationExpired();

            InOutUtils.PrintDogs("Reikia skiepyti (" + selectedBreed + ")",
                requiresVaccination.Intersect(FilteredByBreed));
            
            InOutUtils.PrintDogs("Vakcinacija reikalinga:", requiresVaccination);
            Console.WriteLine();

            Console.WriteLine("Kopija");
            InOutUtils.PrintDogs("Kopija", allDogsCopy);

            /*Console.WriteLine("Registras:");
            InOutUtils.PrintDogs("Registras:", register);*/

        }
    }
}
