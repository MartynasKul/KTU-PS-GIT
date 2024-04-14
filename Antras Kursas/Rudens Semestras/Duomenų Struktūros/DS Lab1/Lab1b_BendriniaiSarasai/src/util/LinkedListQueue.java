package util;

import java.util.LinkedList;
import java.util.List;

public class LinkedListQueue<E> implements Queue<E> {



    private java.util.LinkedList<E> List = new LinkedList<>();


    @Override
    public void enqueue(E item) {
        this.List.addFirst(item);
    }

    @Override
    public E dequeue() {
        if(List.isEmpty())
        {
            return null;
        }
        E temp = this.List.getLast();
        this.List.removeLast();
        return temp;
    }

    @Override
    public E peak() {
        if(List.isEmpty())
        {
            return null;
        }
        return List.getFirst();
    }

    @Override
    public boolean isEmpty() {
        if(List.isEmpty())
        {
            return true;

        }
        return false;
    }
}
