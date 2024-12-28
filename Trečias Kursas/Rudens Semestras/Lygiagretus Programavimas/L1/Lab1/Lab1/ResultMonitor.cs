using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class ResultMonitor
    {
        private ProcessedCharacter[] characters;
        private int size;
        private int count;
        private object Lock = new object();
        public bool finished { get; private set; }
        public ResultMonitor(int size)
        {
            this.size = size;
            characters = new ProcessedCharacter[size];
            count = 0;
        }
        public bool IsEmpty()
        {
            return count == 0;
        }
        public bool IsFull()
        {
            return count == size;
        }
        public void AddItem(ProcessedCharacter character)
        {
            Monitor.Enter(Lock);
            try
            {
                while (IsFull())
                {
                    Monitor.Wait(Lock);
                }
                AddInSortedOrder(character);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Monitor.Pulse(Lock);
                Monitor.Exit(Lock);
            }
        }
        public void AddInSortedOrder(ProcessedCharacter character)
        {
            int position = 0;
            while (position < count && characters[position].Name.CompareTo(character.Name) < 0)
            {
                position++;
            }
            for (int i = count - 1; i >= position && i < size; i--)
            {
                characters[i + 1] = characters[i];
            }
            characters[position] = character;
            count++;
        }

        public int getCount()
        {
            return count;
        }
        public ProcessedCharacter Getcharacter(int i)
        {
            if (i >= count || i < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return characters[i];
        }
    }
}
