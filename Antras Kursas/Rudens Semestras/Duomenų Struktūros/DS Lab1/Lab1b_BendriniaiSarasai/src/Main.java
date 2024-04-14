import com.ktu.Car;
import util.ArrayQueue;
import util.DataReader;
import util.LinkedList;

public class Main {
    private static final String filepath  = "Cars.txt";
    public static void main(String[] arg)
    {
        ArrayQueue<Car> QueueArray = DataReader.readFromFileIntoArray(filepath);
        LinkedList<Car> LinkedList = DataReader.readFromFileIntoLinkedList(filepath);
    }

}
