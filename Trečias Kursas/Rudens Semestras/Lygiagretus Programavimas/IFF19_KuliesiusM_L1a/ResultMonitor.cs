using System;
using System.Threading;

namespace Lab1A
{
    internal class ResultMonitor
    {
        private ProcessedCar[] cars;
        private int size; // monitoriaus nustatytas dydis
        private int count; // monitoriaus elementu kiekis
        private object Lock = new object();
        public bool finished { get; private set; }

        public ResultMonitor(int size) 
        {
            this.size = size;
            cars = new ProcessedCar[size];
            count = 0;
        }

        // patikrina ar monitorius yra tuscias
        public bool IsEmpty() 
        {
            return count == 0;        
        }

        // patikrina ar monitorius yra pilnas
        public bool IsFull()
        {
            return count == size;
        }

        public void Add(ProcessedCar car) 
        {
            Monitor.Enter(Lock);
            try
            {
                while (IsFull()) 
                {
                    Monitor.Wait(Lock);
                }
                AddInSortedOrder(car);
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

        public void AddInSortedOrder(ProcessedCar car) 
        {
            int position = 0;

            while (position < count && cars[position].Make.CompareTo(car.Make) < 0) 
            {
                position++;
            }

            for (int i = count - 1; i >= position && i < size; i--) 
            {
                cars[i + 1] = cars[i];
            }

            cars[position] = car;
            count++;
        }

        public int getCount() 
        {
            return count;
        }

        public ProcessedCar GetCar(int i) 
        {
            if (i >= count || i < 0) 
            {
                throw new IndexOutOfRangeException();
            }
            return cars[i];

        }
    }
}
