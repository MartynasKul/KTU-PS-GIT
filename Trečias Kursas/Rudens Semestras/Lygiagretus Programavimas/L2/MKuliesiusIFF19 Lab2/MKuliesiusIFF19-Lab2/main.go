package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"sort"
)

type Car struct {
	Make  string
	Model string
	Year  int
	Plate string
	Power int
}
type CarRez struct {
	Make  string
	Model string
	Year  int
	Plate string
	Power int
	CarID string
}

func main() {

	//komunikaciju tarp rutinu kanalai
	startDataIn := make(chan Car, 1)
	startDataOut := make(chan Car, 1)
	resultDataIn := make(chan CarRez, 1)
	resultDataOut := make(chan CarRez, 1)
	resultDone := make(chan bool)
	workerCount := 50

	//startData, err := ioutil.ReadFile("IFF19_KuliesiusM_L2_dat_1.json") //Dalis duomenu atitinka kriterijus
	//startData, err := ioutil.ReadFile("IFF19_KuliesiusM_L2_dat_2.json") //Visi duomenys atitinka kriterijus
	startData, err := ioutil.ReadFile("IFF19_KuliesiusM_L2_dat_3.json") // Nei vieni duomenys atitinka kriterijus

	//patikra ar atidaro ir nuskaito duomenu faila
	if err != nil {
		log.Fatal("Error when opening file: ", err)
	}

	var load []Car

	err = json.Unmarshal(startData, &load) // isveda JSON duomenis i masyva
	if err != nil {                        //Isveda jeigu isvedimo metu atsitinka klaida
		log.Fatal("Error during Unmarshal(): ", err)
	}

	for i := 0; i < workerCount; i++ {
		go func() {
			workerRoutine(startDataOut, resultDataIn) //uzduodama uzduotis worker threadui
		}()
	}
	go dataRoutine(startDataIn, startDataOut, len(load)/2)
	go resultRoutine(resultDataIn, resultDataOut, resultDone)

	// Isvedimas
	for i := 0; i < len(load); i++ {
		startDataIn <- load[i]
	}
	close(startDataIn)

	resultDone <- true

	resultsFile, err := os.Create("LD2Result.txt")
	errcheck(err)

	resultsFile.WriteString("Gauti rezultatai\n" +
		"---------------------------------------------------------------------------------------------------------------\n" +
		"| Make                              | Model                             | Year | Power |  Plate | CarID       |\n" +
		"---------------------------------------------------------------------------------------------------------------\n")

	for {
		CarRez, more := <-resultDataOut
		if more {

			fmt.Fprintf(resultsFile, "| %-34s| %-34s| %-5d| %-4dkw| %-7s| %-12s|\n", CarRez.Make, CarRez.Model, CarRez.Year, CarRez.Power, CarRez.Plate, CarRez.CarID)
		} else {

			break
		}
	}
	resultsFile.WriteString("---------------------------------------------------------------------------------------------------------------\n")
	defer resultsFile.Close()
}

// duomenu ivedimo metodas
func dataRoutine(dataIn <-chan Car, dataOut chan<- Car, maxSize int) {
	var data []Car
	received := false
	sent := false

	for {
		//tikrinama ar uztenka vietos masyve ir ar dar duomenu gavimas nera pasibaiges
		if len(data) < maxSize && !received {
			select {
			//bando gauti duomenis is dataIn kanalo
			case car, more := <-dataIn:
				if more {
					//jei yra vietos masyve, prideda nauja masina

					data = append(data, car)

				} else {
					// dataIn kanalas uzsidares, todel receiving done padaromas true
					received = true
				}
			default:
				//fmt.Println("Pasiekta default stadija ivedime")
				//tam kad select neuzsiblokuotu select blokas

			}
		}
		//papildomas sortas nebutinas jis

		//tikrinama ar yra duomenu, kuriuos galima butu issiusti
		//ideti select
		if len(data) > 0 {
			dataOut <- data[len(data)-1] //issiuncia paskutini elementa ir ji pasalina
			data = data[:len(data)-1]
		} else if received {
			sent = true
		}
		if received && sent {
			break
		}

	}
	// uzdaro dataOut kanala kai visi procesai nustoja veikti
	close(dataOut)
}

// CarID skaiciavimo metodas
func workerRoutine(dataIn <-chan Car, dataOut chan<- CarRez) {
	for {
		car, more := <-dataIn
		if more {

			makeSubstring := ""
			if len(car.Make) >= 2 {
				makeSubstring = car.Make[:2]
			}

			modelSubstring := ""
			if len(car.Model) >= 2 {
				modelSubstring = car.Model[:2]
			}

			plateSubstring := ""
			if len(car.Plate) >= 2 {
				plateSubstring = car.Plate[:2]
			}

			processedData := fmt.Sprintf("%s%s%02d%s%d", makeSubstring, modelSubstring, car.Year%100, plateSubstring, car.Power)

			if 2023-car.Year < 30 {
				dataOut <- CarRez{
					Make:  car.Make,
					Model: car.Model,
					Year:  car.Year,
					Power: car.Power,
					Plate: car.Plate,
					CarID: processedData,
				}
			}
		} else {
			return
		}
	}
}

func resultRoutine(dataIn <-chan CarRez, dataOut chan<- CarRez, done <-chan bool) {
	var data []CarRez
	received := false
	for {
		select {
		case <-done:
			received = true
		case carRez := <-dataIn:
			data = append(data, carRez) //appendina ieinancius rezultatus i data masyva,
		}
		if received {
			break
		}
	}
	sort.Slice(data, func(i, j int) bool {
		return data[i].Year < data[j].Year

	})
	//issiuncia visus data elementus i dataOut kanala
	for i := 0; i < len(data); i++ {
		dataOut <- data[i]
	}
	//uzdaro dataOut kanala kai baigia dirbti
	close(dataOut)
}

// exceptionu tikrinimas, jei yra klaidos pranesimas, ismeta exception.
func errcheck(e error) {
	if e != nil {
		panic(e)
	}
}
