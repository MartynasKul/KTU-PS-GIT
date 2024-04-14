#include "Car.h"

    Car::Car() {
    
    }
    Car::Car(string make, string model, int year, string plate, int power) {
        Make = make;
        Model = model;
        Year = year;
        Plate = plate;
        Power = power;
    }