package edu.ktu.ds.lab3.demo;

import edu.ktu.ds.lab3.gui.ValidationException;

import java.util.Collections;
import java.util.LinkedList;
import java.util.Queue;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class CarsGenerator {

    private static final String ID_CODE = "TA";      //  ***** Nauja
    private static int serNr = 100;                  //  ***** Nauja

    private Queue<String> keys;
    private Queue<Car> cars;

    /**
     * Generuojama Automobilių ir raktų eilė. Šis metodas naudojamas grafinėje sąsajoje
     *
     * @param setSize Sugeneruotos automobilių ir raktų eilių dydžiai
     * @throws ValidationException
     */
    public void generateShuffleIdsAndCars(int setSize) throws ValidationException {
        keys = generateShuffleIds(setSize);
        cars = generateShuffleCars(setSize);
    }

    /**
     * Gražinamas vienas elementas (Automobilis) iš sugeneruotos Automobilių eilės.
     * Kai elementai baigiasi sugeneruojama nuosava situacija ir išmetamas pranešimas.
     * Šis metodas naudojamas grafinėje sąsajoje
     *
     * @return Automobilis
     */
    public Car getCar() {
        if (cars == null) {
            throw new ValidationException("carsNotGenerated");
        }
        if (cars.isEmpty()) {
            throw new ValidationException("allSetStoredToMap");
        }

        return cars.poll();
    }

    /**
     * Grąžinamas vienas raktas (ID) iš sugeneruotos raktų eilės.
     * Kai raktai baigiasi sugeneruojama nuosava situacija (exception) ir išmetamas pranešimas.
     * Šis metodas naudojamas grafinėje sąsajoje
     *
     * @return raktas
     */
    public String getCarId() {
        if (keys == null) {
            throw new ValidationException("carsIdsNotGenerated");
        }
        if (keys.isEmpty()) {
            throw new ValidationException("allKeysStoredToMap");
        }

        return keys.poll();
    }

    public static Queue<Car> generateShuffleCars(int size) {
        LinkedList<Car> cars = IntStream.range(0, size)
                .mapToObj(i -> new Car.Builder().buildRandom())
                .collect(Collectors.toCollection(LinkedList::new));
        Collections.shuffle(cars);
        return cars;
    }

    public static Queue<String> generateShuffleIds(int size) {
        LinkedList<String> keys = IntStream.range(0, size)
                .mapToObj(i -> generateId())
                .collect(Collectors.toCollection(LinkedList::new));
        Collections.shuffle(keys);
        return keys;
    }

    public static String generateId() {
        return ID_CODE + (serNr++);
    }
}
