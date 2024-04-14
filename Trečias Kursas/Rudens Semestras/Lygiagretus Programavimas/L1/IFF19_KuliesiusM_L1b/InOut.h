#pragma once
#include <string>
#include <iomanip>
#include <list>
#include <fstream>
#include "libraries\nlohmann\json.hpp"
#include "Car.h"
#include "ProcessedCar.h"
#include <vector>
#include <stdexcept>

using namespace std;

class InOut{
public:
	static vector<Car> Read(string fileName);
	static void Print(string fileName, vector<ProcessedCar> cars, int yearSum, int powerSum, string header);
};

