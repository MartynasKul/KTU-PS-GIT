using System;

namespace Lab1A
{
    internal class ProcessedCar : Car
    {
        public string computedData;

        public ProcessedCar(Car car, string processedData) : base(car.Make, car.Model, car.Year, car.Plate, car.Power)
        {
            computedData = processedData;
        }

        public override string ToString()
        {
            return String.Format(" | {0,-35} | {1,-35} | {2,-5} | {3,-5}kw | {4,-10} | {5, -20} | ", Make, Model, Year, Power, Plate, computedData);
        }
    }
}
