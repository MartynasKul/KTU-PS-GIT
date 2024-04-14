#pragma once

#include <string>
using namespace std;

class Car{
public:
    string Make;
    string Model;
    int Year;
    string Plate;
    int Power;
    Car();
    Car(string make, string model, int year, string plate, int power);

};
