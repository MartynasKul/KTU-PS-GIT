package com.company;

import java.util.Arrays;
import java.util.Collections;
import java.util.Iterator;
import java.util.Locale;
import java.util.Random;
import java.util.ArrayList;
import java.util.List;


public class Main {

    public static void main(String[] args) {

        // isrinkti didziausia vienodu skaiciu grupes dydi is saraso

        int Masyvas[]={1,2,4,4,4,4,3,6,1,22,41};
        //int Masyvas[]={11,22,11,2,3,2,2,2,24,2,4};
        //int Masyvas[]={1,22,11,2,3,3,3,3,3,2,4};
        //int Masyvas[]={11,22,11,2,3,2,2,2,24,2,4};
/*

// didziausio skaiciaus atrinkimas
        int biggest=0;
        for(int i=0;i<Masyvas.length;i++){
            if(Masyvas[i]>biggest) {
                biggest = Masyvas[i];

            }
        }

        System.out.println("Didziausias skaicius masyve: " + biggest);
*/


        int sizeMaxSerieFound = 0;
        int numberMaxFound = Masyvas[0];

        int sizeMaxCurrentSerie = 0;
        int numberCurrentSerie = Masyvas[0];

        boolean isMaxSerieIsTheCurrent = true;

        for (int i = 0; i < Masyvas.length; i++) {
            int currentNumber = Masyvas[i];

            if (currentNumber == numberCurrentSerie) {
                sizeMaxCurrentSerie++;
            }
            else {
                sizeMaxCurrentSerie = 1;
                numberCurrentSerie = currentNumber;
                isMaxSerieIsTheCurrent = false;
            }

            // SECOND STEP : set the state of the max serie

            // we increment the current number of the actual max serie
            if (currentNumber == numberMaxFound && isMaxSerieIsTheCurrent) {
                sizeMaxSerieFound++;
            }

            // we reinit the serie because we have found a new greater serie
            else if (currentNumber != numberMaxFound && sizeMaxCurrentSerie > sizeMaxSerieFound) {
                sizeMaxSerieFound = sizeMaxCurrentSerie;
                numberMaxFound = currentNumber;
                isMaxSerieIsTheCurrent = true;
            }

        }


        System.out.println("Didžiausia grupė skaičiaus " + numberMaxFound +" buvo " + sizeMaxSerieFound+" ilgio.");
        List<String> identical = new ArrayList<>();
        for (int i = 0; i < sizeMaxSerieFound; i++) {
            identical.add(String.valueOf(numberMaxFound));
        }


    }
}
