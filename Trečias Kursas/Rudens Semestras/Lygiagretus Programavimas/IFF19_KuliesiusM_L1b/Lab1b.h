#pragma once
#include <iostream>
#include <string>
#include "ProcessedCar.h"
#include "InOut.h"
#include <list>
#include <vector>
#include <omp.h>
#include <math.h>
#include "Car.h"
class Program
{
public:

    static vector<Car> cars;
    static vector<ProcessedCar> processedCars;
    static int main();

};