using System;
using System.Collections.Generic;
using System.Linq;

namespace LD1_10_MKuliesius.AppClasses
{
    public class TaskUtils
    {
        /// <summary>
        /// Iškviečiama funkcija darbo atlikimui
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="moleList"></param>
        public static void Work(string [] lines, List<Moles> moleList) 
        {

            string[] dimensions = lines[0].Split(' ');
            int n = int.Parse(dimensions[0]);
            int m = int.Parse(dimensions[1]);

            char[,] map = new char[n, m];

            for (int i = 0; i < n; i++) 
            {
                string row = lines[i + 1];
                for(int j = 0; j < m; j++)
                {
                    map[i, j] = row[j];
                }
            }

            int[] sizes=CountMoleHoles(map, n, m);

            for (int i = 0; i < sizes.Count(); i++) 
            {
                Moles moleHole = new Moles();
                moleHole.CaveSize= sizes[i];
                moleList.Add(moleHole);

            }
            
        }

        /// <summary>
        /// Apskaičiuojamas kurmių skylių kiekis
        /// </summary>
        /// <returns></returns>
        static int[] CountMoleHoles(char[,] map, int n, int m)
        {
            bool[,] visited = new bool[n, m]; // Masyvas, skirtas žymėti lankytas vietas
            int[] sizes = new int[0]; // Masyvas, kuriame saugosime urvų dydžius

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (map[i, j] == 'u' && !visited[i, j])
                    { // Jei esame urve ir dar nebuvo lankyta ši vieta
                        int size = CountMoleHoleSize(map, visited, i, j, n, m); // Skaičiuojame urvo dydį
                        Array.Resize(ref sizes, sizes.Length + 1); // Išplečiame masyvą
                        sizes[sizes.Length - 1] = size; // Pridedame naują urvo dydį į masyvą
                    }
                }
            }
         
            Array.Sort(sizes); // Surikiuojame dydžius mažėjimo tvarka
            Array.Reverse(sizes); // Apverčiame masyvą, kad būtų mažėjimo tvarka

            return sizes;
        }
        /// <summary>
        /// Apskaičiuojamas Kurmio urvo skylės dydis.
        /// </summary>
        /// <returns></returns>
        static int CountMoleHoleSize(char[,] map, bool[,] visited, int i, int j, int n, int m)
        {
            if (i < 0 || i >= n || j < 0 || j >= m || map[i, j] != 'u' || visited[i, j])
            {
                return 0; // Jei esame už masyvo ribų, arba ne urve, arba jau lankėme šią vietą, grąžiname 0
            }

            visited[i, j] = true; // Žymime šią vietą kaip lankytą

            // Rekursyviai ieškome kaimyninių vietų
            return 1 + CountMoleHoleSize(map, visited, i - 1, j, n, m) + // viršus
                       CountMoleHoleSize(map, visited, i + 1, j, n, m) + // apačia
                       CountMoleHoleSize(map, visited, i, j - 1, n, m) + // kairė
                       CountMoleHoleSize(map, visited, i, j + 1, n, m); // dešinė
        }

    }
}