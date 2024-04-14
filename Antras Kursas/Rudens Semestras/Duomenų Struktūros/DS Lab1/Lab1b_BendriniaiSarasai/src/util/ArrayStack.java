package util;

import java.util.ArrayList;

public class ArrayStack<E> implements Stack<E> {

private final ArrayList<E> elements = new ArrayList<E>();

    @Override
    public E pop() {
        if(elements.isEmpty())
        {
            return null;
        }
        E temp = elements.get(elements.size() - 1);
        elements.remove(elements.size() - 1);
        return temp;
    }

    @Override
    public void push(E item) {
        elements.add(item);
    }

    @Override
    public E peak() {
        if(elements.isEmpty())
        {
            return null;
        }
        return elements.get(elements.size() - 1);
    }

    @Override
    public boolean isEmpty() {
        return elements.isEmpty();
    }
}
