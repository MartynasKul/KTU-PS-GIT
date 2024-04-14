package edu.ktu.ds.lab2.demo;

import edu.ktu.ds.lab2.utils.AvlSet;
import edu.ktu.ds.lab2.utils.BstSet;
import edu.ktu.ds.lab2.utils.BstSetIterative;
import edu.ktu.ds.lab2.utils.SortedSet;
import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.infra.BenchmarkParams;
import org.openjdk.jmh.runner.Runner;
import org.openjdk.jmh.runner.RunnerException;
import org.openjdk.jmh.runner.options.Options;
import org.openjdk.jmh.runner.options.OptionsBuilder;

import java.util.Random;
import java.util.TreeSet;
import java.util.concurrent.TimeUnit;

@BenchmarkMode(Mode.AverageTime)
@State(Scope.Benchmark)
@OutputTimeUnit(TimeUnit.MICROSECONDS)
@Warmup(time = 1, timeUnit = TimeUnit.SECONDS)
@Measurement(time = 1, timeUnit = TimeUnit.SECONDS)
public class Benchmark {

    @State(Scope.Benchmark)
    public static class FullSet {

        Car[] cars;
        BstSet<Car> carSet;
        AvlSet<Car> carSetAvl;
        TreeSet<Car> Javaset;

        Car carp;

        Car element;

        Car element2;

        @Setup(Level.Iteration)
        public void generateElements(BenchmarkParams params) {
            cars = Benchmark.generateElements(Integer.parseInt(params.getParam("elementCount")));
            element = Benchmark.generateElements(1)[0];
            Random rand = new Random();
            element2 = cars[rand.nextInt(cars.length)];
        }



        @Setup(Level.Invocation)
        public void fillCarSet(BenchmarkParams params) {
            carSet = new BstSet<>();
            carSetAvl = new AvlSet<>();
            Javaset = new TreeSet<>();
            addElements(cars, carSet);
            addElements(cars, carSetAvl);
            addElementsJava(cars, Javaset);

        }
    }



    @Param({"10000", "10000", "20000", "30000", "40000", "50000", "60000", "70000", "80000"})
    public int elementCount;

    Car[] cars;

    @Setup(Level.Iteration)
    public void generateElements() {
        cars = generateElements(elementCount);
    }

    static Car[] generateElements(int count) {
        return new CarsGenerator().generateShuffle(count, 1.0);
    }
    /*
    @org.openjdk.jmh.annotations.Benchmark
    public BstSet<Car> addBstRecursive() {
        BstSet<Car> carSet = new BstSet<>(Car.byPrice);
        addElements(cars, carSet);
        return carSet;
    }

    @org.openjdk.jmh.annotations.Benchmark
    public BstSetIterative<Car> addBstIterative() {
        BstSetIterative<Car> carSet = new BstSetIterative<>(Car.byPrice);
        addElements(cars, carSet);
        return carSet;
    }

    @org.openjdk.jmh.annotations.Benchmark
    public AvlSet<Car> addAvlRecursive() {
        AvlSet<Car> carSet = new AvlSet<>(Car.byPrice);
        addElements(cars, carSet);
        return carSet;
    }

    @org.openjdk.jmh.annotations.Benchmark
    public void removeBst(FullSet carSet) {
        for (Car car : carSet.cars) {

            carSet.carSet.remove(car);
        }
    }



    @org.openjdk.jmh.annotations.Benchmark
    public boolean Contains(FullSet carSert)
    {
        return carSert.carSetAvl.contains(carSert.element);
    }

    @org.openjdk.jmh.annotations.Benchmark
    public boolean ContainsJava(FullSet carSert)
    {
        return carSert.Javaset.contains(carSert.element);
    }

    @org.openjdk.jmh.annotations.Benchmark
    public BstSet<Car> HeadSetOneLine(FullSet carSert)
    {
        return (BstSet<Car>) carSert.carSet.headSet3(carSert.element2);
    }

    @org.openjdk.jmh.annotations.Benchmark
    public BstSet<Car> HeadSetSameLook(FullSet carSert)
    {
        return (BstSet<Car>) carSert.carSet.headSet2(carSert.element2);
    }
    */

    @org.openjdk.jmh.annotations.Benchmark
    public void removeTree(FullSet set) {
        for (Car car : set.Javaset) {
            set.Javaset.remove(car);
        }
    }
   // @org.openjdk.jmh.annotations.Benchmark
   // public void removeAvl(FullSet set) {
   //     for (Car car : set.carSetAvl) {
   //         set.carSetAvl.remove(car);
   //     }
   // }
//



    public static void addElements(Car[] carArray, SortedSet<Car> carSet) {
        for (Car car : carArray) {
            carSet.add(car);
        }
    }

    public static void addElementsJava(Car[] carArray, TreeSet<Car> carSet) {
        for (Car car : carArray) {
            carSet.add(car);
        }
    }
    public static void main(String[] args) throws RunnerException {
        Options opt = new OptionsBuilder()
                .include(Benchmark.class.getSimpleName())
                .forks(1)
                .build();
        new Runner(opt).run();


    }
}
