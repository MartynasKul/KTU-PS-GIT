/** @author Eimutis Karčiauskas, KTU IF Programų inžinerijos katedra, 2014 09 23
 *
 * Tai yra demonstracinė automobilio klasė (jos objektai dedami į LinkedList),
 * kuri realizuoja interfeisą Parsable<T>.
 ****************************************************************************/
package com.ktu;

import util.*;

import java.text.Format;
import java.time.LocalDate;
import java.util.Comparator;
import java.util.InputMismatchException;
import java.util.Locale;
import java.util.NoSuchElementException;
import java.util.Scanner;

public class Car implements Parsable<Car> {

    // bendri duomenys visiems automobiliams (visai klasei)
    final static private int minYear = 1994;

    final static private double minPrice = 200.0;
    final static private double maxPrice = 120_000.0;

    // kiekvieno automobilio individualūs duomenys
    private String make;
    private String model;
    private int year;
    private int mileage;
    private double price;

    public Car() {
    }

    public Car(String make, String model,
            int year, int mileage, double price) {
        this.make = make;
        this.model = model;
        this.year = year;
        this.mileage = mileage;
        this.price = price;
    }

    public Car(String data) {
        parse(data);
    }

    @Override
    public final void parse(String data) {
        try {   // ed - tai elementarūs duomenys, atskirti tarpais
            Scanner ed = new Scanner(data);
            make = ed.next();
            model = ed.next();
            year = ed.nextInt();
            mileage = ed.nextInt();
            setPrice(ed.nextDouble());
        } catch (InputMismatchException e) {
            Ks.ern("Blogas duomenų formatas apie auto -> " + data);
        } catch (NoSuchElementException e) {
            Ks.ern("Trūksta duomenų apie auto -> " + data);
        }
    }

    public String validate() {
        String error = "";
        int currentYear = LocalDate.now().getYear();
        if (year < minYear || year > currentYear) {
            error = "Netinkami gamybos metai, turi būti ["
                    + minYear + ":" + currentYear + "]";
        }
        if (price < minPrice || price > maxPrice) {
            error += " Kaina už leistinų ribų [" + minPrice
                    + ":" + maxPrice + "]";
        }
        return error;
    }

    @Override
    public String toString() {  // surenkama visa reikalinga informacija
        return String.format("%-8s %-8s %4d %7d %8.1f %s",
                make, model, year, mileage, price, validate());
    }

    public String getMake() {
        return make;
    }

    public String getModel() {
        return model;
    }

    public int getYear() {
        return year;
    }

    public int getMileage() {
        return mileage;
    }

    public double getPrice() {
        return price;
    }

    // keisti bus galima tik kainą - kiti parametrai pastovūs
    public void setPrice(double price) {
        this.price = price;
    }

    @Override
    public int compareTo(Car otherCar) {
        // lyginame pagal svarbiausią požymį - kainą
        double otherPrice = otherCar.getPrice();
        if (price < otherPrice) {
            return -1;
        }
        if (price > otherPrice) {
            return +1;
        }
        return 0;
    }

    // pradžioje pagal markes, o po to pagal modelius
    public final static Comparator<Car> byMakeAndModel
            = Comparator.comparing(Car::getMake).thenComparing(Car::getModel);

    public final static Comparator<Car> byPrice = (car1, car2) -> {
        double price1 = car1.getPrice();
        double price2 = car2.getPrice();
        // didėjanti tvarka, pradedant nuo mažiausios
        if (price1 < price2) {
            return -1;
        }
        if (price1 > price2) {
            return 1;
        }
        return 0;
    };

    public final static Comparator<Car> byYearAndPrice = (car1, car2) -> {
        // metai mažėjančia tvarka, esant vienodiems lyginama price
        if (car1.getYear() < car2.getYear()) {
            return 1;
        }
        if (car1.getYear() > car2.getYear()) {
            return -1;
        }
        if (car1.getPrice() < car2.getPrice()) {
            return 1;
        }
        if (car1.getPrice() > car2.getPrice()) {
            return -1;
        }
        return 0;
    };

    // metodas main = tiesiog paprastas pirminis automobilių išbandymas
    public static void main(String... args) {
        // suvienodiname skaičių formatus pagal LT lokalę (10-ainis kablelis)
        Ks.giveFileName();


        Locale.setDefault(new Locale("LT"));
        Car a1 = new Car("Renault", "Laguna", 1997, 50000, 599);
        Car a2 = new Car("Renault", "Megane", 2015, 20000, 3500);
        Car a3 = new Car();
        Car a4 = new Car();
        a3.parse("Toyota Corolla 2001 20000 8500,8");
        a4.parse("Toyota Avensis 1990 20000  500,8");
        Ks.oun(a1);
        Ks.oun(a2);
        Ks.oun(a3);
        Ks.oun(a4);

        //LinkedStack tests
        LinkedListStack<Car> LinkedStack = new LinkedListStack<Car>();
        System.out.println("--------------------------------");
        System.out.println("LinkedStack test: ");
        System.out.println(LinkedStack.isEmpty());


        LinkedStack.push(a1);
        LinkedStack.push(a2);
        LinkedStack.push(a3);
        LinkedStack.push(a4);
        Ks.oun(LinkedStack.peak());
        LinkedStack.pop();
        Ks.oun(LinkedStack.peak());
        LinkedStack.pop();
        Ks.oun(LinkedStack.peak());
        System.out.println("--------------------------------");



        //LinkedQueue tests
        System.out.println("--------------------------------");
        System.out.println("LinkedQueue test:");
        LinkedListQueue<Car> LinkedQueue = new LinkedListQueue<Car>();
        System.out.println(LinkedQueue.isEmpty());


        LinkedQueue.enqueue(a1);
        LinkedQueue.enqueue(a3);
        Ks.oun(LinkedQueue.peak());
        LinkedQueue.enqueue(a2);
        LinkedQueue.dequeue();
        Ks.oun(LinkedQueue.peak());
        LinkedQueue.dequeue();
        LinkedQueue.dequeue();
        Ks.oun(LinkedQueue.peak());
        System.out.println("--------------------------------");

        //ArrasyStack tests
        ArrayStack<Car> ArrayStacks = new ArrayStack<Car>();
        System.out.println("--------------------------------");
        System.out.println("ArrayStack test:");
        System.out.println(ArrayStacks.isEmpty());

        ArrayStacks.push(a1);
        ArrayStacks.push(a2);
        ArrayStacks.push(a3);
        ArrayStacks.push(a4);
        Ks.oun(ArrayStacks.peak());
        ArrayStacks.pop();
        Ks.oun(ArrayStacks.peak());
        ArrayStacks.pop();
        Ks.oun(ArrayStacks.peak());
        System.out.println("--------------------------------");

        //ArrayQueue test
        ArrayQueue<Car> ArrayQueue = new ArrayQueue<Car>();
        System.out.println("--------------------------------");
        System.out.println("ArrayQueue test");

        ArrayQueue.enqueue(a1);
        ArrayQueue.enqueue(a2);
        ArrayQueue.enqueue(a3);
        ArrayQueue.dequeue();
        System.out.println(ArrayQueue.isEmpty());
        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a1);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a1);
        ArrayQueue.enqueue(a1);
        ArrayQueue.enqueue(a1);
        ArrayQueue.enqueue(a4);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a1);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a1);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.dequeue();
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a4);
        ArrayQueue.dequeue();
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        ArrayQueue.enqueue(a4);
        Ks.oun(ArrayQueue.peak());

        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        ArrayQueue.dequeue();
        Ks.oun(ArrayQueue.peak());

        System.out.println("--------------------------------");







    }
}
