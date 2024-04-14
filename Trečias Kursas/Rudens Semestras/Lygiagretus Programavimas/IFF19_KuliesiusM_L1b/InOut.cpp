#include "InOut.h"

vector<Car> InOut::Read(string fileName) {
	ifstream dataFile(fileName);
	if (dataFile.fail()) {
		throw invalid_argument("File" + fileName + " not found");
	}

	nlohmann::json data = nlohmann::json::parse(dataFile);

	dataFile.close();

	vector<Car> Cars;
	for (auto& elm : data.items()) {
		nlohmann::json item = elm.value();
		Car car(item["Make"], item["Model"], item["Year"].get<int64_t>(), item["Plate"], item["Power"].get<int64_t>());
		Cars.push_back(car);
	}
	return Cars;
}

void InOut::Print(string fileName, vector<ProcessedCar> Cars, int YearSum, int PowerSum, string header) {
	int wide = 130;
	ofstream res;


	if (Cars.size() == 0) {
		res << header << endl;
	
		res << "Rezultatu duotiems duomenims nera" << endl;
		res.close();
	}
	else {
		res.open(fileName);
		res << header << endl;
		res << "Aptvarkyta " << Cars.size() << " duomenu" << endl;
		res << string(wide, '-') << endl;
		res << "| Make                                | Model                               | Year  | Plate  | Powerkw    | Result               | " << endl;
		res << string(wide, '-') << endl;
		for (int i = 0; i < Cars.size(); i++) {
			res << Cars[i].ToString() << endl;
		}
		res << string(wide, '-') << endl;
		res << "Sum of Years: " << YearSum << endl;
		res << "Sum of Power: " << PowerSum << endl;
		res.close();
	}
}
