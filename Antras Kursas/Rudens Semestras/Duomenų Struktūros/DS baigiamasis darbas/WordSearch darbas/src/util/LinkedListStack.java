package util;

import java.util.LinkedList;
import java.util.List;

public class LinkedListStack<E> implements Stack<E> {

private LinkedList<E> List = new LinkedList<>();


    @Override
    public E pop() {
        if(List.isEmpty())
        {
            return null;
        }
       E temp = List.getFirst();
       List.removeFirst();
       return temp;
    }

    @Override
    public void push(E item) {
        List.addFirst(item);
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
