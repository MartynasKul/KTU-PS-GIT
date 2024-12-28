using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

public class Store
{
    public double X { get; set; }
    public double Y { get; set; }
}

class Program
{
    static Random random = new Random();
    static int parallelThreads = 8;


    // Atstumo tarp dvieju parduotuviu kainos apskaiciavimo metodas
    static double DistanceCost(Store store1, Store store2)
    {
        return Math.Exp(-0.3 * ((store1.X - store2.X) * (store1.X - store2.X) + (store1.Y - store2.Y) * (store1.Y - store2.Y)));
    }

    // Parduotuves vietos kainos skaiciavimo metodas
    static double PlacementCost(Store newStore)
    {
        return (Math.Pow(newStore.X, 4) + Math.Pow(newStore.Y, 4)) / 1000 + (Math.Sin(newStore.X) + Math.Cos(newStore.Y)) / 5 + 0.4;
    }

    // Apskaiciuojama visa kaina sudejus ir naujas ir senas parduotuves. Panaudojamas AsParallel() lygiagretinimui.
    static double TotalCost(List<Store> existingStores, List<Store> newStores)
    {
        double totalCost = 0;

        // Apskaiciuojama kaina pridedant naujas parduotuves kartu su senomis parduotuvemis
        totalCost += newStores.AsParallel().WithDegreeOfParallelism(parallelThreads).SelectMany(newStore => existingStores, (newStore, existingStore) => DistanceCost(newStore, existingStore)).Sum();

        // Pridedama nauju parduotuviu kaina
        totalCost += newStores.AsParallel().WithDegreeOfParallelism(parallelThreads).Select(newStore => PlacementCost(newStore)).Sum();

        return totalCost;
    }

    static void Main()
    {
        
       // Console.Write("Iveskite kiek parduotuviu norite tureti: "); 
       // int n = Convert.ToInt32(Console.ReadLine());
       // Console.Write("Iveskite kiek parduotuviu norite prideti: ");
       // int m = Convert.ToInt32(Console.ReadLine());

        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine("-----------------------------------------------------------------------------------");
        // Sugeneruojamos atitiktines pradines parduotuviu lokacijos
        int n = 400; // parduotuviu kiekis
        List<Store> existingStores = Enumerable.Range(1, n).Select(_ => new Store { X = random.NextDouble() * 20 - 10, Y = random.NextDouble() * 20 - 10 }).ToList();

        // Pradines koordinates nauju parduotuviu.
        int m = 400; // parduotuviu kiekis
        List<Store> newStores = Enumerable.Range(1, m).Select(_ => new Store { X = random.NextDouble() * 20 - 10, Y = random.NextDouble() * 20 - 10 }).ToList();

        int maxIterations = 100;
        double learningRate = 0.1;

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            // Atnaujina nauju parduotuviu lokacijas naudojant gradiento nusileidima
            newStores = newStores.AsParallel().WithDegreeOfParallelism(parallelThreads).Select(newStore =>
            {
                //Apskaiciuoja gradientus
                (double gradientX, double gradientY) = CalculateGradient(existingStores, newStores, newStore);

                //Atnaujina koordinates pagal gradiento nusileidima
                newStore.X -= learningRate * gradientX;
                newStore.Y -= learningRate * gradientY;

                // apriboja koordinates gautas ir gradientu tarp -10 ir 10
                newStore.X = Math.Max(-10, Math.Min(10, newStore.X));
                newStore.Y = Math.Max(-10, Math.Min(10, newStore.Y));

                return newStore;
            }).ToList();

            // Isveda iteracija ir visa kaina
            Console.WriteLine($"Iteration {iteration + 1}, Total Cost: {TotalCost(existingStores, newStores)}");
        }
        Console.WriteLine("-----------------------------------------------------------------------------------");

        timer.Stop();
        
        Console.WriteLine("Pradiniu parduotuviu lokacijos:");
        foreach (var store in existingStores)
        {
            Console.WriteLine($"X: {store.X}, Y: {store.Y}");
            Console.WriteLine("Parduotuves kaina: " + PlacementCost(store));
        }
        Console.WriteLine("-----------------------------------------------------------------------------------");
        Console.WriteLine("Nauju parduotuviu lokacijos:");
        foreach (var store in newStores)
        {
            Console.WriteLine($"X: {store.X}, Y: {store.Y}");
            Console.WriteLine("Parduotuves kaina: " + PlacementCost(store));
        }
        Console.WriteLine("-----------------------------------------------------------------------------------");
        Console.WriteLine("Uztruko laiko " + timer.ElapsedMilliseconds + "ms");

        
    }
    static (double, double) CalculateGradient(List<Store> existingStores, List<Store> newStores, Store currentStore, double epsilon = 1e-6)
    {
        //issisaugau pradines koordinates
        double originalX = currentStore.X;
        double originalY = currentStore.Y;

        //apsiskaiciuoju pradine kaina parduotuves
        double originalCost = TotalCost(existingStores, newStores);

        //Pakeiciu koordinates reiksme sudejus su epsilon
        currentStore.X = originalX + epsilon;
        double perturbedXCost = TotalCost(existingStores, newStores);

        // Atstatau x koordinate
        currentStore.X = originalX;

        //Pakeiciu koordinates reiksme sudejus su epsilon
        currentStore.Y = originalY + epsilon;
        double perturbedYCost = TotalCost(existingStores, newStores);

        // Atstatau y koordinate
        currentStore.Y = originalY;

        // Apskaiciuojama gradientu skaitine aproksimacija
        double gradientX = (perturbedXCost - originalCost) / epsilon;
        double gradientY = (perturbedYCost - originalCost) / epsilon;

        return (gradientX, gradientY);
    }
}
