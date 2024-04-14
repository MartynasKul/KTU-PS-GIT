package com.ktu;

import util.ParsableList;

public class CarList extends ParsableList<Car> {

    @Override
    protected Car createElement(String data) {
        return new Car(data);
    }
}
