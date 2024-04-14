package util;
import com.ktu.Car;
import java.io.*;
import java.util.ArrayList;
import java.util.Scanner;

public class DataReader {

    public static ArrayQueue<Car> readFromFileIntoArray(String filePath)
    {
        FileInputStream fileStream = null;
        Scanner scanner = null;
        ArrayQueue<Car> List = new ArrayQueue<Car>();



        try{
            fileStream = new FileInputStream(filePath);
            scanner = new Scanner(fileStream, "UTF-8");

            while(scanner.hasNextLine())
            {
                String[] Lines = scanner.nextLine().split(" ");
                String make = Lines[0];
                String model = Lines[1];
                int year = Integer.parseInt(Lines[2]);
                int mileage = Integer.parseInt(Lines[3]);
                double price = Integer.parseInt(Lines[4]);

                List.enqueue(new Car (make, model, year, mileage, price));
            }

        }
        catch(Exception e)
        {
            e.printStackTrace();;
        }
        finally {
            if(scanner != null)
            {
                scanner.close();
            }

        }
        return List;
    }

    public static LinkedList<Car> readFromFileIntoLinkedList(String filePath)
    {
        FileInputStream fileStream = null;
        Scanner scanner = null;
        LinkedList<Car> List = new LinkedList<Car>();


        try{
            fileStream = new FileInputStream(filePath);
            scanner = new Scanner(fileStream, "UTF-8");

            while(scanner.hasNextLine())
            {
                String[] Lines = scanner.nextLine().split(" ");
                String make = Lines[0];
                String model = Lines[1];
                int year = Integer.parseInt(Lines[2]);
                int mileage = Integer.parseInt(Lines[3]);
                double price = Integer.parseInt(Lines[4]);

                List.add(new Car (make, model, year, mileage, price));
            }

        }
        catch(Exception e)
        {
            e.printStackTrace();;
        }
        finally {
            if(scanner != null)
            {
                scanner.close();
            }

        }
        return List;
    }

}
