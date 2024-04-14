using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace U4L_8
{
    class TaskUtils
    {
    //    /// <summary>
    //    /// Runs input method and calls 'Analise' method do all the work
    //    /// </summary>
    //    public static void Process(string fd, string fr)
    //    {
    //        // deletes old result file
    //        if (File.Exists(fr))
    //        {
    //            File.Delete(fr);
    //        }
    //
    //        string[] lines = File.ReadAllLines(fd, Encoding.UTF8);
    //
    //        Analise(lines, fr);
    //    }

        /// <summary>
        /// Removes unnecessary punctuation that repeats itself.
        /// </summary>
        public static void RemoveUnnecessaryPunctuation(ref string[] lines)
        {
            MatchEvaluator evaluator = new MatchEvaluator(ReturnFirstCharacter);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = Regex.Replace(lines[i], @"(\W)\1{1,}", evaluator);
                if (lines[i].Length == 1)
                    lines[i] = "";
            }
        }
        /// <summary>
        /// Returns first character from a regex match.
        /// </summary>
        public static string ReturnFirstCharacter(Match match)
        {
            return Convert.ToString(match.Value[0]);
        }

        /// <summary>
        /// Finds the longest text fragment that 
        /// is made from words where the last 
        /// letter is the same as the next 
        /// word first letter, line numbers.
        /// Counts the number of words that are 
        /// only made from numbers and finds their sum.
        /// </summary>
        public static void Analize(string[] lines, string fr)
        {
            // antra uzduotis
            //----------------------------------------------------------------------
            int numbersCount = 0; // stores the count of number type words
            int numbersSum = 0; // stores the sum of number type words
            ProcessNumbers(lines, ref numbersCount, ref numbersSum);

            //----------------------------------------------------------------------


            string allText = String.Join("\n", lines); // Joins all lines to a single string 

            int largestWordCountInFragment = 0; // Stores word count in fragment
            int longestFragmentLineStart = 0; // Stores fragment starting line index
            int longestFragmentLineEnd = 0; // Stores fragment ending line index

            int longestFragmentStartIndex = 0; // Stores the index where the fragment begins.
            int longestFragmentEndIndex = 0; // Stores the index where the fragment ends.

            int currentLine = 0;
            int endLine = 0;

            string pattern = "\\w+|[\n\\s\\W]+";
            MatchCollection matches = Regex.Matches(allText, pattern);
            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                CheckIfMatchContainsNewLines(match, ref currentLine);
                if (IsAWord(match))
                {
                    string firstWord = match.Value;
                    int secondWordIndex = FindNextWord(matches, i);

                    if (secondWordIndex > 0 &&
                        WordsStartEndMatch(firstWord, matches[secondWordIndex].Value))
                    {
                        string secondWord = matches[secondWordIndex].Value;

                        int wordsInFragment = 2;

                        int lastMatchingIndex = secondWordIndex;
                        FindFragmentEnd(matches, ref lastMatchingIndex, ref wordsInFragment);
                        endLine = currentLine + CountNewLines(matches, i, lastMatchingIndex);

                        if (wordsInFragment > largestWordCountInFragment)
                        {
                            largestWordCountInFragment = wordsInFragment;
                            longestFragmentLineStart = currentLine;
                            longestFragmentLineEnd = endLine;
                            longestFragmentStartIndex = i;
                            longestFragmentEndIndex = lastMatchingIndex;
                        }

                        currentLine = endLine;
                        i = lastMatchingIndex;
                    }
                }
            }

            using (StreamWriter writer = File.CreateText(fr))
            {
                if (largestWordCountInFragment > 0)
                {
                    // Prints the longest fragment.
                    for (int i = longestFragmentStartIndex; i <= longestFragmentEndIndex; i++) 
                    { 
                        writer.Write(matches[i].Value);
                    }

                    writer.WriteLine();

                    if (longestFragmentLineEnd - longestFragmentLineStart > 0) 
                    {
                        writer.WriteLine("Žodžių fragmente: {0}\nEilutės: {1}-{2}",
                            largestWordCountInFragment, longestFragmentLineStart + 1,
                            longestFragmentLineEnd + 1);
                    }
                    else 
                    {
                        writer.WriteLine("Žodžių fragmente: {0}\nEilutė: {1}",
                            largestWordCountInFragment, longestFragmentLineStart + 1);
                    }
                        
                    writer.WriteLine();
                }
                writer.WriteLine("Žodžių, kuriuos sudaro " +
                "tik skaitmenys, kiekis: {0}", numbersCount);
                writer.WriteLine("Žodžių, kuriuos sudaro " +
                    "tik skaitmenys, bendra suma: {0}", numbersSum);
            }
        }
        /// <summary>
        /// Counts the amount of new line characters in a fragment.
        /// </summary>
        public static int CountNewLines(MatchCollection matches, int fragmentStartIndex, int fragmentEndIndex)
        {
            int count = 0;
            for (int i = fragmentStartIndex;
                i < fragmentEndIndex; i++)
            {
                CheckIfMatchContainsNewLines(matches[i], ref count);
            }
            return count;
        }

        /// <summary>
        /// Checks if a match contains new lines.
        /// </summary>
        public static void CheckIfMatchContainsNewLines(Match match, ref int count)
        {
            // a match may contain multiple new Lines
            string m = match.Value;

            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] == '\n')
                    count++;
            }
        }

        /// <summary>
        /// Checks if the two given words in parameters ending and start match.
        /// </summary>
        public static bool WordsStartEndMatch(string word1, string word2)
        {
            int currentLastIndex = word1.Length - 1; // stores last characters index

            return Char.ToLower(Convert.ToChar(word1[currentLastIndex])) == Char.ToLower(Convert.ToChar(word2[0]));
        }

        /// <summary>
        /// Checks if the regex match is a word. not a number
        /// </summary>
        public static bool IsAWord(Match match)
        {
            return Regex.IsMatch(match.Value, @"\w+");
        }

        /// <summary>
        /// Finds the next word in a collection of regex matches.
        /// </summary>
        public static int FindNextWord(MatchCollection matches, int currentIndex)
        {
            int nextIndex = -1;
            for (int i = currentIndex + 1; i < matches.Count; i++)
            {
                if (IsAWord(matches[i]))
                {
                    return i;
                }
            }
            return nextIndex;
        }

        /// <summary>
        /// Finds the text fragment ending index 
        /// and the word count in fragment through references.
        /// </summary>
        public static bool FindFragmentEnd(MatchCollection matches, ref int matchIndex, ref int wordCount)
        {
            Match match = matches[matchIndex];
            string firstWord = match.Value;
            int secondWordIndex = FindNextWord(matches, matchIndex);

            // if there is a words after the first word,
            // and if it matches
            if (secondWordIndex > 0 &&
                WordsStartEndMatch(firstWord, matches[secondWordIndex].Value))
            {
                string secondWord = matches[secondWordIndex].Value;
                wordCount++;
                // recursively find the fragment ending.
                FindFragmentEnd(matches, ref secondWordIndex, ref wordCount);
                matchIndex = secondWordIndex;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Finds the count and sum of number words in book.
        /// </summary>
        /// Skaiciuoja visus skaicius, ne tik atskirus skaitinius zodzius
        public static void ProcessNumbers
            (string[] lines, ref int count, ref int sum)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                MatchCollection numbers = Regex.Matches(line, @"[-]?\d+");
                foreach (Match number in numbers)
                {
                    count++; // counts ammount of number words
                    sum += Convert.ToInt32(number.Value); // adds values together
                }
            }
        }
    }
}