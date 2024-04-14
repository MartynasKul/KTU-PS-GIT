#include "Lab1b.h"
#include <iostream>
#include <ctime>

vector<Car> Program::cars;
vector<ProcessedCar> Program::processedCars;

int Program::main()
{
    //string fileName = "IFF19_KuliesiusM_L1_dat_1.json"; // dalis duomenu
    string fileName = "IFF19_KuliesiusM_L1_dat_2.json"; // visi duomenys
    //string fileName = "IFF19_KuliesiusM_L1_dat_3.json"; // ne vienas duomuo

    string resultFile = "IFF19_KuliesiusM_L1_rez.txt";

    //trinti resultatu faila
    std::ofstream ofs;
    ofs.open(resultFile, std::ofstream::out | std::ofstream::trunc);
    ofs.close();


    cars = InOut::Read(fileName);

    int YearSum = 0;
    int PowerSum = 0;

    int totalThreads = 2;

    int minElementsPerThread = cars.size() / totalThreads; 
    int threadsWithExtraElement = cars.size() % totalThreads;

    omp_set_num_threads(totalThreads);

    #pragma omp parallel reduction(+:YearSum) reduction(+:PowerSum)
    {
        int threadIndex = omp_get_thread_num();
        int totalElements = minElementsPerThread;

        if (threadIndex < threadsWithExtraElement) {
            totalElements++;
        }

        for (int i = 0; i < totalElements; i++) {
            int indexInVector = totalThreads * i + threadIndex;
            Car car = cars.at(indexInVector);

            string makeSubstring = car.Make.length() >= 2 ? car.Make.substr(0, 2) : "";
            string modelSubstring = car.Model.length() >= 2 ? car.Model.substr(0, 2) : "";
            string plateSubstring = car.Plate.length() >= 2 ? car.Plate.substr(0, 2) : "";
        
            char calculationResult[50]; // Adjust the size as needed
            std::snprintf(calculationResult, sizeof(calculationResult), "%s%s%02d%s%d", makeSubstring.c_str(), modelSubstring.c_str(), car.Year % 100, plateSubstring.c_str(), car.Power);
            string processedData(calculationResult);
        
            ProcessedCar processedCar = ProcessedCar(car, processedData);

            if (2023 - processedCar.Year < 30)
            {
                YearSum += processedCar.Year;
                PowerSum += processedCar.Power;
                #pragma omp critical
                {
                    int position = 0;
                    while (position < processedCars.size() && processedCars[position].Make.compare(processedCar.Make) < 0) {
                        position++;
                    }
                    processedCars.insert(processedCars.begin() + position, processedCar);
                }
            }
        }
    }

    InOut::Print(resultFile, processedCars, YearSum, PowerSum, "Rezultatai");
    return 0;

}
int main() {
    Program program;
    return program.main();
}
