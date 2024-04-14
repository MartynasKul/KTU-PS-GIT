package edu.ktu.ds.lab3.demo;

import edu.ktu.ds.lab3.utils.*;

import java.util.Locale;

import static edu.ktu.ds.lab3.utils.HashMap.DEFAULT_INITIAL_CAPACITY;
import static edu.ktu.ds.lab3.utils.HashMap.DEFAULT_LOAD_FACTOR;

public class ManualTest {

    public static void main(String[] args) {
        Locale.setDefault(Locale.US); // suvienodiname skaičių formatus
        executeTest();
    }

    public static void executeTest() {
        Car car1 = new Car("Renault", "Laguna", 1997, 50000, 1700);
        Car car2 = new Car("Renault", "Megane", 2001, 20000, 3500);
        Car car3 = new Car("Toyota", "Corolla", 2001, 20000, 8500.8);
        Car car4 = new Car("Renault Laguna 2001 115900 7500");
        Car car5 = new Car.Builder().buildRandom();
        Car car6 = new Car("Honda   Civic  2007  36400 8500.3");
        Car car7 = new Car("Renault Laguna 2001 115900 7500");

        // Atvaizdžio raktų masyvas
        String[] carsIds = {"TA156", "TA102", "TA178", "TA126", "TA105", "TA106", "TA107", "TA108"};
        // Atvaizdžio reikšmių masyvas
        Car[] cars = {car1, car2, car3, car4, car5, car6, car7};

        executeCarMapTests(carsIds, cars);
        executeCarMapOaTests(carsIds, cars);
    }

    private static void executeCarMapTests(String[] carsIds, Car[] cars) {
        ParsableMap<String, Car> carsMap = new ParsableHashMap<>(
                String::new,
                Car::new,
                DEFAULT_INITIAL_CAPACITY,
                DEFAULT_LOAD_FACTOR,
                HashManager.HashType.DIVISION
        );

        for (int id = 0; id < cars.length; id++) {
            carsMap.put(carsIds[id], cars[id]);
        }

        Ks.oun("Porų išsidėstymas atvaizdyje pagal raktus:");
        carsMap.println("");
        Ks.oun("Ar egzistuoja pora atvaizdyje?");
        Ks.oun(carsMap.contains(carsIds[6]));
        Ks.oun(carsMap.contains(carsIds[7]));
        Ks.oun("Porų išsidėstymas atvaizdyje pagal raktus. Vaizduojami tik raktai:");
        carsMap.println("=");

        Ks.oun("Atliekame porų paiešką atvaizdyje:");
        Ks.oun(carsMap.get(carsIds[2]));
        Ks.oun(carsMap.get(carsIds[7]));
        Ks.oun("Išspausdiname atvaizdžio poras String eilute:");
        Ks.ounn(carsMap);
    }

    private static void executeCarMapOaTests(String[] carsIds, Car[] cars) {
        ParsableMap<String, Car> carsMapOa = new ParsableHashMapOa<>(
                String::new,
                Car::new,
                DEFAULT_INITIAL_CAPACITY,
                DEFAULT_LOAD_FACTOR,
                HashManager.HashType.DIVISION,
                HashMapOa.OpenAddressingType.LINEAR
        );

        for (int id = 0; id < cars.length; id++) {
            carsMapOa.put(carsIds[id], cars[id]);
        }

        Ks.oun("Porų išsidėstymas atviros adresacijos atvaizdyje pagal raktus:");
        carsMapOa.println("");
        Ks.oun("Ar egzistuoja pora atviros adresacijos atvaizdyje?");
        Ks.oun(carsMapOa.contains(carsIds[6]));
        Ks.oun(carsMapOa.contains(carsIds[7]));
        Ks.oun("Porų išsidėstymas atviros adresacijos atvaizdyje pagal raktus. Vaizduojami tik raktai:");
        carsMapOa.println("=");

        Ks.oun("Atliekame porų paiešką atviros adresacijos atvaizdyje:");
        Ks.oun(carsMapOa.get(carsIds[2]));
        Ks.oun(carsMapOa.get(carsIds[7]));
        Ks.oun("Išspausdiname atviros adresacijos atvaizdžio poras String eilute:");
        Ks.ounn(carsMapOa);
    }
}
