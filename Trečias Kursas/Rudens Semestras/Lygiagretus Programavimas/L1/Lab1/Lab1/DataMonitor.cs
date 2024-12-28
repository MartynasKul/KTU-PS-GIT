using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class DataMonitor
    {
        private Character[] characters;
        private int size;
        private int count;
        private object Lock = new object();
        public bool finished { get; private set; }
        public DataMonitor(int size)
        {
            this.size = size;
            characters = new Character[size];
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
        public void AddItem(Character character)
        {
            Monitor.Enter(Lock);
            
            try
            {
                while (IsFull())
                {
                    
                    Monitor.Wait(Lock);
                }
                
                characters[count++] = character;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            finally
            {
                
                Monitor.Pulse(Lock); 
                Monitor.Exit(Lock); 
            }
        }
        public Character RemoveItem()
        {
            Character character;
            character = null;
            Monitor.Enter(Lock);
            try
            {
                while (IsEmpty())
                {
                    if (finished)
                    {
                        break;
                    }
                    Monitor.Wait(Lock);
                }
                if (!IsEmpty())
                {
                    character = characters[--count];
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally 
            {
                Monitor.PulseAll(Lock); 
                Monitor.Exit(Lock); 
            }
            return character;
        }

 
        public void SetFinished()
        {
            Monitor.Enter(Lock);
            try
            {
                finished = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Monitor.PulseAll(Lock);
                Monitor.Exit(Lock);
            }
        }
    }
}
