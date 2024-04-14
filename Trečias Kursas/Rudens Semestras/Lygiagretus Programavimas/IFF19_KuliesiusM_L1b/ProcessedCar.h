#pragma once
#include <string>
#include <sstream>
#include <iomanip>
#include "Car.h"

using namespace std;

class ProcessedCar : public Car{
public:
	string result;
	ProcessedCar();
	ProcessedCar(string make, string model, int year, string plate, int power);
	ProcessedCar(Car car);
	ProcessedCar(Car car, string result);
	string ToString();
};