// Martynas Kuliešius IFF-1/9

using System;
using System.Linq;
using System.IO;

namespace K2
{
    internal class Program
    {
        static void Main(string[] Args) 
        {
            string duomenys = "Tekstas.txt";
            string rezultatai = "RedTekstas.txt";

            if (File.Exists(rezultatai)) 
            {
                File.Delete(rezultatai);
            }

            TaskUtils.PerformTasksU(duomenys, rezultatai);
        
        }
    }
    class TaskUtils 
    {
        public static bool LowerCaseLettersOnly(string line) 
        {
            foreach(char c in line) 
            {
                string test = c.ToString();
                if (test.ToLower() != test) 
                {
                    return false;
                }
            }
            return true;
        }

        public static int NumberOfDigits(string line) 
        {
            string digits = "0123456789";

            int count = 0;

            foreach (char c in line) 
            {
                if (digits.Contains(c)) 
                {
                    count++;
                }
            }

            return count;
        }

        public static string FindWord1InLine(string line, string punctuation) 
        {
            string returned ="";
            string[] Words = line.Split();
            int currentNum = 0;

            foreach (string word in Words) 
            {
                if (NumberOfDigits(word) > currentNum && NumberOfDigits(word) > 0 && NumberOfDigits(word) < 10)  
                {
                    currentNum = NumberOfDigits(word);
                    returned = word;
                }
            }

            return returned;
        }
        //ilgiausias zodis is mazuju raidziu
        public static string FindWord2InLine(string line, string punctuation)
        {
            string returned = ""; 
            int wLength = -1;
            string[] Words = line.Split();

            foreach (string word in Words) 
            {
                if (LowerCaseLettersOnly(word)) 
                {
                    if (word.Length > wLength) 
                    {
                        wLength=word.Length;
                        returned = word;
                    }                
                }            
            }

            return returned;
        }

        public static string EditLine(string line, string punctuation, string word) 
        {
            string returnLine = "";

            if (line.Contains(word))
            {
                string[] Words = line.Split();

                foreach (string asd in Words)
                {
                    if (!asd.Equals(word))
                    {
                        returnLine = returnLine + asd + " ";
                    }
                }
            }

            else
            {
                Console.WriteLine("Tokio zodzio eiluteje nera! Gautas zodis - " +word);
            }

            return returnLine;
        }

        public static void PerformTasksU(string fd, string fr) 
        {
            string punctuation;
            string[] Lines;
            int numberCount = 0;

            Lines=File.ReadAllLines(fd);
            punctuation = Lines[0];

            using (var Bob = File.AppendText(fr)) 
            {
                for (int i = 1; i < Lines.Length; i++)
                {
                    numberCount += NumberOfDigits(Lines[i]);

                    string removeWord = FindWord2InLine(Lines[i], punctuation);
                    string addWord = FindWord1InLine(Lines[i], punctuation);

                    Console.WriteLine(removeWord);

                    Lines[i] = EditLine(Lines[i], punctuation, removeWord);
                    Lines[i] = Lines[i] + " " + addWord;

                    Bob.WriteLine(Lines[i]);

                }

                Bob.WriteLine("Bendras teksto skaitmenu kiekis: " + numberCount);
            }
        }    
    }
}
