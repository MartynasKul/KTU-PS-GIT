using System;
using System.Data;
using System.IO;
using System.Linq;

using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
//using static Algor.GeneticAlgorithm;
//using static Algor.Route;
using System.ComponentModel;

namespace ASAIP
{
    class Program
    {
        static void Main(string[] args)
        {

            List<City> cities = new List<City>();
            List<Road> roads = new List<Road>();
            cities = InOutUtils.ReadFile("IP_data_2023.xlsx");
           //int genOrNorm;
           //Console.WriteLine("Genetic ar paprastus? 1|2");
           //genOrNorm=Convert.ToInt32(Console.ReadLine());

            Stopwatch timer = new Stopwatch();

            bool secondMethod = false; // needed for activation of the second method.


            Dictionary<City, bool> visitedC = new Dictionary<City, bool>();
            foreach (City c in cities)
            {
                visitedC[c] = false;
            }

            List<Path> Paths = new List<Path>();
            timer.Start();
            while (timer.Elapsed.Seconds < 10)
            {

                Dictionary<City, bool> visited = new Dictionary<City, bool>();
                foreach (City c in cities)
                {
                    visited[c] = false;
                }

                int Ratings = 0;
                int Timerr = 0;
                Random rnd = new Random();
                int number = rnd.Next(0, cities.Count);
                if (visitedC[cities[number]] == true)
                {
                    continue;
                }
                else
                {
                    visitedC[cities[number]] = true;
                }
                City StartingCity = cities[number];
                Path startpath = new Path();
                Stopwatch st = new Stopwatch();
                st.Start();
                secondMethod = true ;
                
                FindAllPathsForced(StartingCity, Paths, Ratings, StartingCity, visited, startpath, secondMethod);

            }
            timer.Stop();


            Console.WriteLine(timer.Elapsed.TotalSeconds);
            Paths.Sort((p1, p2) => p1.Rating.CompareTo(p2.Rating));
            Console.WriteLine("------------------------------------------------------------------------- DISTINCT ROADS ----------------------------");
            Console.WriteLine(Paths.Count);

            List<Path> distinctPaths = Paths.Distinct(new PathComparer()).ToList();
            InOutUtils.PrintPaths(distinctPaths);

            Console.WriteLine("------------------------------------------------------------------------- BEST ROADS ----------------------------");
            Console.WriteLine(distinctPaths.Count);
            distinctPaths = Utils.BestPaths(distinctPaths);
            InOutUtils.PrintPaths(distinctPaths);
            InOutUtils.PrintPathsToFile(distinctPaths, "GeriausiKeliai.txt", "Geriausi atrasti keliai");

            Console.WriteLine("------------------------------------------------------------------------- TOP 10 ROADS ----------------------------");
            List<Path> Top10 = Utils.BestPaths(distinctPaths);
            InOutUtils.PrintPaths(Top10);
            InOutUtils.PrintPathsToFile(Top10, "GeriausiKeliai.txt", "Top10 keliai");
       
            timer.Reset();
            timer.Start();


            int populationSize = 50;
            int numGenerations = 100;

            Population population = new Population(distinctPaths, populationSize, true);
            Console.WriteLine("--------------------------  Genetic Paths  -------------------------------------");
            population.printer();

            Console.WriteLine("--------------------------Genetic Best Path-------------------------------------");

            // Iterate for the desired number of generations
            for (int generation = 0; generation < numGenerations; generation++)
            {
                // Evolve the population
                population = GeneticOptimization.EvolvePopulation(population, roads);
            }

            // Get the best path from the final population
            Path bestPath = population.GetFittest();

            // Output the best path, its rating
            Console.WriteLine("Best Path: " + string.Join(" -> ", bestPath.Roads.Select(city => city.Name)));
            Console.WriteLine("Rating: " + bestPath.Rating);
            
            timer.Stop();


            void FindAllPathsForced(City StartingCity, List<Path> list, double Ratings, City current, Dictionary<City, bool> visited, Path path, bool secondMethod)
            {                                                                                                                                               
                if (timer.Elapsed.TotalSeconds > 10) { return; }                                                                                                 // c1  | 1
                Dictionary<City, bool> dic = new Dictionary<City, bool>(visited);                                                                                // c2  | 1
                Path newPath = new Path(path);                                                                                                                   // c3  | 1
                double Rating = Ratings;                                                                                                                         // c4  | 1
                                                                                                                                                                 //
                newPath.Add(Rating, current);                                                                                                                    // c5  | 1
                foreach (City c in path.Roads)                                                                                                                   // c6  | n
                {                                                                                                                                                //
                    Console.Write(c.Name + "->");                                                                                                                // c7  | n*1
                }                                                                                                                                                //
                                                                                                                                                                 //
                Console.Write("\n");                                                                                                                             // c8  | 1
                                                                                                                                                                 //
                if (current.Name == StartingCity.Name && Rating > 30)                                                                                            // c9  | 1
                {                                                                                                                                                //
                    list.Add(newPath);                                                                                                                           // c10  | 1
                    return;                                                                                                                                      // c11  | 1
                }                                                                                                                                                //
                                                                                                                                                                 //
                for (int i = 0; i < current.Roads.Count(); i++)                                                                                                  // c12  | n+1
                {   //Šis metodas reikalingas paleisti antrą metodą, dėl to čia vyksta jo kvietimas                                                              //
                    if (secondMethod == false)                                                                                                                   // c13  | n*1
                    {                                                                                                                                            //
                        if (newPath.Repeat(current, current.Roads[i].To))                                                                                        // c14  | n*1
                        {                                                                                                                                        //
                            if (newPath.Roads.Contains(current.Roads[i].To))                                                                                     // c15  | n*1
                                FindAllPathsForced(StartingCity, list, Rating, current.Roads[i].To, dic, newPath, secondMethod);                                 // T(n - 1) | n*1
                            else                                                                                                                                 // c17  | n*1
                                FindAllPathsForced(StartingCity, list, Rating + current.Roads[i].To.Rating, current.Roads[i].To, dic, newPath, secondMethod);    // T(n - 1) | n*1
                        }                                                                                                                                        //
                    }                                                                                                                                            //
                    else                                                                                                                                         // c18  | n*1
                    {                                                                                                                                            //
                        if (newPath.Roads.Contains(current.Roads[i].To))                                                                                         // c19  | n*1
                        {                                                                                                                                        // 
                            FindAllPaths(StartingCity, list, Rating, current.Roads[i].To, dic, newPath, 10000);                                                  // T(n - 1) | n*1
                        }                                                                                                                                        // 
                        else                                                                                                                                     // c20  | n*1
                        {                                                                                                                                        //
                            FindAllPaths(StartingCity, list, Rating + current.Roads[i].To.Rating, current.Roads[i].To, dic, newPath, 10000);                     // T(n - 1) | n*1
                        }                                                                                                                                        
                    }                                                                                                                                            
                }                                                                                                                                                
            }

            void FindAllPaths(City StartingCity, List<Path> list, double Ratings, City current, Dictionary<City, bool> visited, Path path, int Timerr)
            {
                Dictionary<City, bool> dic = new Dictionary<City, bool>(visited);                                                                        // c1     | 1
                Path newPath = new Path(path);                                                                                                           // c2     | 1
                double Rating = Ratings;                                                                                                                 // c3     | 1
                                                                                                                                                         //        
                for (int r = 0; r < current.Roads.Count(); r++)                                                                                          // c4     | n+1
                {                                                                                                                                        //        
                    if (dic[current] == false)                                                                                                           // c5     | n+1*1
                    {                                                                                                                                    //        
                        dic[current] = true;                                                                                                             // c6     | 1 * n+1
                                                                                                                                                                   
                    }                                                                                                                                    //        
                    else                                                                                                                                 // c7     | n+1*1
                    {                                                                                                                                    //        
                        if (current.Name != StartingCity.Name)                                                                                           // c8     | n+1*1
                        {                                                                                                                                //        
                            return;                                                                                                                      // c9     | n+1*1
                        }                                                                                                                                //       
                    }                                                                                                                                    //       
                                                                                                                                                         // c10    | n+1*1
                    int Timer = 10000;                                                                                                                   // c11    | n+1*1
                    City next = current;                                                                                                                 //       
                                                                                                                                                         //       
                    if (StartingCity.Name == current.Name)                                                                                               // c12    | n+1*1
                    {                                                                                                                                    //       
                        if (r == 0)                                                                                                                      // c13    | n+1*1
                        {                                                                                                                                //       
                            newPath.Add(Rating, current);                                                                                                // c14    | n+1*1
                        }                                                                                                                                //       
                    }                                                                                                                                    //       
                    else                                                                                                                                 // c15    | n+1*1
                    {                                                                                                                                    //       
                        newPath.Add(Rating, current);                                                                                                    // c16    | n+1*1
                    }                                                                                                                                    //       
                    if (Rating > 30)                                                                                                                     // c17    | n+1*1
                    {                                                                                                                                    //       
                        if (current.Name == StartingCity.Name)                                                                                           // c18    | n+1*1
                        {                                                                                                                                //       
                            list.Add(newPath);                                                                                                           // c19    | n+1*1
                            return;                                                                                                                      // c20    | n+1*1
                        }                                                                                                                                //       
                        for (int i = 0; i < current.Roads.Count(); i++)                                                                                  // c21    | n+1*1*n+1
                        {                                                                                                                                //       
                            if (StartingCity.Name == current.Roads[i].To.Name)                                                                           // c22    | n+1*1*n+1*1
                            {                                                                                                                            //
                                FindAllPaths(StartingCity, list, Rating + current.Roads[i].To.Rating, current.Roads[i].To, dic, newPath, Timer);         // T(n-1) | n+1*1*n+1*1
                            }                                                                                                                            //
                            if ((dic[current.Roads[i].To] == false || current.Roads[i].To.Name == StartingCity.Name))                                    // c23    | n+1*1*n+1*1
                            {                                                                                                                            //
                                next = current.Roads[i].To;                                                                                              // c24    | n+1*1*n+1*1
                            }                                                                                                                            //
                        }                                                                                                                                //
                        FindAllPaths(StartingCity, list, Rating + next.Rating, next, dic, newPath, Timer);                                               // T(n-1) | n+1*1*n+1*1
                        dic[next] = true;                                                                                                                // c25    | n+1*1*n+1*1
                    }                                                                                                                                    //
                    else                                                                                                                                 // c26    | n+1*1*1
                    {                                                                                                                                    //
                        if (current.Name == StartingCity.Name)                                                                                           // c27    | n+1*1*1
                        {                                                                                                                                //
                            list.Add(newPath);                                                                                                           // c28    | n+1*1*1
                            return;                                                                                                                      // c29    | n+1*1*1
                        }                                                                                                                                //
                                                                                                                                                         //
                        for (int i = 0; i < current.Roads.Count(); i++)                                                                                  // c30    | n+1*1*1*n+1
                        {                                                                                                                                //
                            if (Timer > current.Roads[i].Time && (dic[current.Roads[i].To] == false || current.Roads[i].To.Name == StartingCity.Name))   // c31    | n+1*1*1*n+1
                            {                                                                                                                            //
                                Timer = current.Roads[i].Time;                                                                                           // c32    | n+1*1*1*n+1*1
                                next = current.Roads[i].To;                                                                                              // c33    | n+1*1*1*n+1*1
                            }                                                                                                                            //
                        }                                                                                                                                // 
                        FindAllPaths(StartingCity, list, Rating + next.Rating, next, dic, newPath, Timer);                                               // T(n-1) | n+1*1*1
                        dic[next] = true;                                                                                                                // c35    | n+1*1*1
                    }                                                                                                                                    
                }                                                                                                                                        
            }                                                                                                                                            
    }

    public class PathComparer : IEqualityComparer<Path>
    {
        public bool Equals(Path x, Path y)
        {
            return x.Roads.SequenceEqual(y.Roads);
        }

        public int GetHashCode(Path obj)
        {
            int hash = 17;
            foreach (var road in obj.Roads)
            {
                hash = hash * 23 + road.GetHashCode();
            }
            return hash;
        }
    }

    public static class Utils
    {
        public static List<Path> BestPaths(List<Path> paths)
        {
            int count = 0;
            List<Path> result = new List<Path>();
            for (int i = paths.Count - 1; i > 0; i--)
            {
                if (count == 10)
                {
                    break;
                }
                Path path = paths[i];
                if (path.Rating > 30)
                {
                    result.Add(path);
                    count++;
                }
            }
            return result;
        }

        public static Path BestPath(List<Path> paths)
        {
            int count = 0;
            Path result = new Path();
            double goodRating = 30;

            for (int i = 0; i < paths.Count; i++) 
            {
                if (paths[i].Rating > goodRating) 
                {
                    goodRating = paths[i].Rating;
                    result = paths[i];
                }
            }

            
            return result;
        }

            public static void RemoveUnder(List<Path> paths, double amm)
        {

                int pc = paths.Count;
                for (int i = 0; i < pc; i++) 
            //foreach (Path p in paths) 
            {
                    if (paths[i].Rating < amm) 
                {
                        paths.Remove(paths[i]);
                        pc--;
                }
            }

        }

            public static int CalculateRating(List<City> roads1, List<Road> roads2)
            {
                int result = 0;

                for (int i = 0; i < roads1.Count; i++) 
                {
                    result += Convert.ToInt32(roads1[i].Rating);
                }


                return result;
            }
        }

    public class Path
    {
        public double Rating { get; set; }

        public int Time { get; set; }

        public List<City> Roads { get; set; }

        public Path()
        {
            this.Roads = new List<City>();
            this.Rating = 0;
            this.Time = 0;
        }

        public Path(Path p)
        {

            this.Roads = new List<City>(p.Roads);
            this.Rating = p.Rating;
            this.Time = p.Time;
        }

        public void Add(double rating, City city)
        {
            this.Rating = rating;
            this.Roads.Add(city);
        }

        public bool Repeat(City current, City next)
        {
            for (int i = 0; i < Roads.Count(); i++)
            {
                if (current.Name == Roads[i].Name)
                {
                    if (i + 1 < Roads.Count() && next.Name == Roads[i + 1].Name)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool Contains(City next)
        {
            for (int i = 0; i < Roads.Count(); i++)
            {
                for (int j = 0; j < Roads.Count(); j++)
                {
                    if (Roads[i].Name == Roads[j].Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class City
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public List<Road> Roads { get; set; }

        public City(string name, double rating)
        {
            Name = name;
            Rating = rating;
            Roads = new List<Road>();
        }

        public City()
        {
            Name = "";
            Rating = 0;
            Roads = new List<Road>();
        }

        public void AddRoad(Road r)
        {
            this.Roads.Add((Road)r);
        }
    }

    public class Road
    {
        public City From { get; set; }
        public City To { get; set; }
        public int Time { get; set; }
        public double Cost { get; set; }

        public Road(City from, City to, int time, double cost)
        {
            From = from;
            To = to;
            Time = time;
            Cost = cost;
        }


    }

    public static class InOutUtils
    {

        public static void PrintPaths(List<Path> paths)
        {
            bool thirtyFlag = false;
            foreach (Path p in paths)
            {

                foreach (City y in p.Roads)
                {


                    Console.Write(y.Name + " -> ");
                    if (y.Rating > 30)
                    {
                        thirtyFlag = true;
                    }


                }
                if (thirtyFlag = true)
                {
                    Console.Write(p.Rating);
                    Console.Write("\n\n");
                    thirtyFlag = false;
                }
            }
        }
        public static void PrintPathsToFile(List<Path> paths, string fileName, string header)
        {
            using (var fn = File.AppendText(fileName))
            {
                fn.WriteLine(header);
                fn.WriteLine();
                bool thirtyFlag = false;
                foreach (Path p in paths)
                {

                    foreach (City y in p.Roads)
                    {


                        fn.Write(y.Name + " -> ");
                        if (y.Rating > 30)
                        {
                            thirtyFlag = true;
                        }


                    }
                    if (thirtyFlag = true)
                    {
                        fn.Write(p.Rating);
                        fn.Write("\n\n");
                        thirtyFlag = false;
                    }
                }
            }
        }

        public static List<City> ReadFile(string FilePath)
        {
            List<City> list = new List<City>();
            List<Road> Roads = new List<Road>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(FilePath)))
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                ExcelRange cityrange = worksheet.Cells["H6:I104"];

                ExcelRange roadrange = worksheet.Cells["M6:P2275"];



                object[,] cities = cityrange.Value as object[,];

                string CityName = "";
                double Rating = 0;

                for (int row = 0; row < cities.GetLength(0); row++)
                {

                    for (int col = 0; col < cities.GetLength(1); col++)
                    {
                        if (col == 0)
                        {
                            CityName = cities[row, col].ToString();
                        }
                        if (col == 1)
                        {
                            Rating = (double)cities[row, col];
                        }

                    }
                    list.Add(new City(CityName, Rating));
                }

                object[,] roads = roadrange.Value as object[,];

                City From = new City();
                City To = new City();
                int Time = 0;
                double price = 0;

                for (int row = 0; row < roads.GetLength(0); row++)
                {
                    for (int col = 0; col < roads.GetLength(1); col++)
                    {

                        if (col == 0)
                        {
                            string f = roads[row, col].ToString();
                            foreach (City c in list)
                            {
                                if (f == c.Name)
                                {
                                    From = c;
                                    break;
                                }
                            }
                        }
                        if (col == 1)
                        {
                            string g = roads[row, col].ToString();
                            foreach (City c in list)
                            {
                                if (g == c.Name)
                                {
                                    To = c;
                                    break;
                                }
                            }
                        }
                        if (col == 2)
                        {
                            Time = Convert.ToInt32(roads[row, col]);
                        }
                        if (col == 3)
                        {
                            price = (double)roads[row, col];
                        }
                    }
                    foreach (City c in list)
                    {
                        if (From.Name == c.Name)
                        {
                            c.AddRoad(new Road(From, To, Time, price));
                            break;
                        }
                    }
                }

            }
            return list;
        }
        }
     public class GeneticOptimization
     {
         private const double Mutation = 1.5;
         private const int TournamentSize = 3;
         private static Path SelectViaTournament(Population Population, List<Road> roads)
         {
             Population Tournament = new Population(TournamentSize);

             Random Randomizer = new Random();
             for (int i = 0; i < TournamentSize; i++)
                 Tournament.PathAdd(Population.Paths[Randomizer.Next(0, Population.Paths.Count)]);

             return Tournament.GetFittest();
         }


         private static Path CrossoverAndMutate(Path first, Path second, List<Road> roads)
         {
             Random randomizer = new Random();

             // Create a new path object for the offspring
             Path offspring = new Path();

             // Randomly select a crossover point
             int crossoverPoint = randomizer.Next(1, first.Roads.Count - 1);

             // Add cities from the first parent up to the crossover point
             for (int i = 0; i < crossoverPoint; i++)
             {
                 City city = first.Roads[i];
                 offspring.Roads.Add(city);
                 offspring.Rating += city.Rating;

             }
             // Add cities from the second parent after the crossover point
             for (int i = crossoverPoint; i < second.Roads.Count - 1; i++)
             {
                 City city = second.Roads[i];

                 // Check if the city is already present in the offspring
                 if (!offspring.Roads.Contains(city))
                 {
                     offspring.Roads.Add(city);
                     offspring.Rating += city.Rating;
                 }
             }

             // Add the starting city at the end to represent the round trip
             offspring.Roads.Add(offspring.Roads[0]);
             //offspring.Time += offspring.Roads.Last().Roads.Find(r => r.To == offspring.Roads[0]).Time;

             // Perform mutation
             if (randomizer.NextDouble() < Mutation)
             {
                 int mutationPoint1 = randomizer.Next(1, offspring.Roads.Count - 1);
                 int mutationPoint2 = randomizer.Next(1, offspring.Roads.Count - 1);

                 // Swap the cities at the mutation points
                 City temp = offspring.Roads[mutationPoint1];
                 offspring.Roads[mutationPoint1] = offspring.Roads[mutationPoint2];
                 offspring.Roads[mutationPoint2] = temp;

                 // Update the rating for the mutated path
                 offspring.Time = Utils.CalculateRating(offspring.Roads, roads);
             }
             return offspring;
         }

         public static Population EvolvePopulation(Population population, List<Road> roads)
         {
             Population newPopulation = new Population(population.PopulationSize);

             // Add the fittest individuals from the previous population (elitism)
             Path bestPath = Utils.BestPath(population.Paths);
             newPopulation.PathAdd(bestPath);

             for (int i = 1; i < population.PopulationSize; i++)
             {
                 // Select parents via tournament selection
                 Path parent1 = SelectViaTournament(population, roads);
                 Path parent2 = SelectViaTournament(population, roads);

                 // Perform crossover and mutation to create offspring
                 Path offspring = CrossoverAndMutate(parent1, parent2, roads);

                 // Add the offspring to the new population
                 newPopulation.PathAdd(offspring);
             }

             newPopulation.Paths = newPopulation.Paths.OrderBy(path => path.Rating).ToList();

             return newPopulation;

         }

     }

     public class Population
     {
         public int PopulationSize;
         public List<Path> Paths;
         public Population(List<Path> Paths, int PopulationSize, bool Initialize)
         {
             this.Paths = new List<Path>();
             this.PopulationSize = PopulationSize;
             for (int i = 0; i < PopulationSize; i++)
             {


                 Random rnd = new Random();
                 int number = rnd.Next(0, Paths.Count);
                 if (Paths[number].Time / 3600 < 48)
                 {
                     this.Paths.Add(Paths[number]);

                 }
                 else
                 {
                     i--;
                     continue;
                 }


             }

         }

         public Population(int PopulationSize)
         {
             this.Paths = new List<Path>();
             this.PopulationSize = PopulationSize;
         }

         public void PathAdd(Path path)
         {
             this.Paths.Add((Path)path);
         }

         public Path GetFittest()
         {
             //Console.WriteLine(Utils.BestPath(Paths).Time+ " "+ Utils.BestPath(Paths).Rating);
             return Utils.BestPath(Paths);
         }

         public void printer()
         {
             InOutUtils.PrintPaths(this.Paths);
         }


     }

    }
}