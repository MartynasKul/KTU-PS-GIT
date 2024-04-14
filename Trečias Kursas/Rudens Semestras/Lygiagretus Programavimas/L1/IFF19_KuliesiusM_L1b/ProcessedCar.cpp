#include "ProcessedCar.h"

ProcessedCar::ProcessedCar() {} // tuscias konstruktorius
ProcessedCar::ProcessedCar(string make, string model, int year, string plate, int power) : Car(make, model, year, plate, power) {} // paprastas konstruktorius paveldintis Car klases konstruktoriu
ProcessedCar::ProcessedCar(Car car) : Car(car.Make, car.Model, car.Year, car.Plate, car.Power) {} // konstruktorius paveldintis klases Car kintamuosius
ProcessedCar::ProcessedCar(Car car, string calculatedResult) : ProcessedCar(car) {
	result = calculatedResult;
}
string ProcessedCar::ToString() {
	stringstream stream;
	stream << "| " << setfill(' ') 
		<< setw(35) << Make << " | " 
		<< setw(35) << Model << " | " 
		<< setw(5) << Year << " | " 
		<< setw(5) << Plate << " | " 
		<< setw(10) << Power << " | " 
		<< setw(20) << result <<" |";

	return stream.str();
}

