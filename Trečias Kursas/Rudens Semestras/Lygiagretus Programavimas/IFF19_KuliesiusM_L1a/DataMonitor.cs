using System;
using System.Threading;

namespace Lab1A
{
    internal class DataMonitor
    {
        private Car[] cars;
        private int size;
        private int count;
        private object Lock = new object();
        public bool finished { get; private set; }
        public DataMonitor(int size) 
        {
            this.size = size;
            cars= new Car[size];
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

        public void Add(Car car)
        {
            Monitor.Enter(Lock);

            try
            {
                while (IsFull())
                {
                    Monitor.Wait(Lock); 
                }

                cars[count++] = car;
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

        public Car Remove()
        {
            Car car;
            car = null;
            Monitor.Enter(Lock);
            try
            {
                while (IsEmpty()) 
                {
                    if (finished) 
                    {
                        break;
                    }
                    Monitor.Wait(Lock) ;
                }
                if (!IsEmpty()) 
                {
                    car = cars[--count];
                }
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
            return car;
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
