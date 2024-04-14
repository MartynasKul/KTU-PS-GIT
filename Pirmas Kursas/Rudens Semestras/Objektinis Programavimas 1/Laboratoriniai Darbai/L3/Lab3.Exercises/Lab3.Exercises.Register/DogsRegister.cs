using System;
using System.Collections.Generic;

namespace Lab3.Exercises.Register
{
    class DogsRegister
    {
        private DogsContainer AllDogs;
        public DogsRegister()
        {
            AllDogs = new DogsContainer();
        }
        public DogsRegister(DogsContainer Dogs)
        {
            AllDogs = new DogsContainer();
            for(int i = 0; i < Dogs.Count; i++)
            {
                Dog dog = Dogs.Get(i);
                this.AllDogs.Add(dog);
            }
        }
        public void Add(Dog dog)
        {
            AllDogs.Add(dog);
        }

        public int DogsCount()
        {
            return this.AllDogs.Count;
        }

        public Dog FindByIndex(int index)
        {
            return AllDogs.Get(index);
        }

        public int CountByGender(Gender gender)
        {
            int count = 0;
            for(int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.Gender.Equals(gender))
                {
                    count++;
                }
            }
            return count;
        }        public Dog FindOldestDog()
        {
            return this.FindOldestDog(this.AllDogs);
        }        public Dog FindOldestDog(string breed)
        {
            DogsContainer Filtered = this.FilterByBreed(breed);
            return this.FindOldestDog(Filtered);
        }

        private Dog FindOldestDog(DogsContainer Dogs)
        {
            Dog oldest = Dogs.Get(0);
            for (int i = 1; i < Dogs.Count; i++) //starts on index value 1
            {
                Dog temp = Dogs.Get(i);
                if (DateTime.Compare(oldest.BirthDate, temp.BirthDate) > 0)
                {
                    oldest = temp;
                }
            }
            return oldest;
        }        public List<string> FindBreeds()
        {
            List<string> Breeds = new List<string>();
            for(int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                string breed = dog.Breed;
                if (!Breeds.Contains(breed)) // uses List method Contains()
                {
                    Breeds.Add(breed);
                }
            }
            return Breeds;
        }        public DogsContainer FilterByBreed(string breed)
        {
            DogsContainer Filtered = new DogsContainer();
            for(int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.Breed.Equals(breed)) // uses string method Equals()
                {
                    Filtered.Add(dog);
                }
            }
            return Filtered;
        }
        public string MostPopularBreed(List<string> Breeds)
        {
            string mostPopular = this.AllDogs.Get(0).Breed;
            int counta = 0;
            int countb = 0;
            foreach (string breed in Breeds)
            {
                counta = 0;
                countb = 0;
                for(int i = 0; i < AllDogs.Count; i++)
                {
                    Dog dog = AllDogs.Get(i);
                    if (dog.Breed == mostPopular)
                        counta++;
                    else if (dog.Breed == breed)
                        countb++;
                }

                if (countb > counta)
                    mostPopular = breed;
            }
            return mostPopular;
        }

        private Dog FindDogByID(int ID)
        {
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.ID == ID)
                {
                    return dog;
                }
            }
            return null;
        }

        public void UpdateVaccinationsInfo(List<Vaccination> Vaccinations)
        {
            foreach (Vaccination vaccination in Vaccinations)
            {
                Dog dog = this.FindDogByID(vaccination.DogID);
                
                if (dog != null)
                {
                    if (vaccination > dog.LastVaccinationDate)
                    {
                        dog.LastVaccinationDate = vaccination.Date;
                    }
                }
            }
        }

        public DogsContainer FilterByVaccinationExpired()
        {
            DogsContainer Filtered = new DogsContainer();

            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.RequiresVaccination)
                {
                    Filtered.Add(dog);
                }
            }
            return Filtered;
        }

        public bool Contains(Dog dog)
        {
            return AllDogs.Contains(dog);
        }
    }
}
