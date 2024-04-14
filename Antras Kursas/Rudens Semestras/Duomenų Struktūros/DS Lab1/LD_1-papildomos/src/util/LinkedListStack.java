package util;

public class LinkedListStack<E> implements Stack<E>{

    java.util.LinkedList<E> linkedList = new java.util.LinkedList<E>();

    @Override
    public E pop() {
        if(linkedList.isEmpty())
            return null;
        return linkedList.removeFirst();
    }

    @Override
    public void push(E item) {
        linkedList.addFirst(item);
    }

    @Override
    public E peak() {
        if(linkedList.isEmpty())
            return null;
        return linkedList.getFirst();
    }

    @Override
    public boolean isEmpty() {
        return linkedList.isEmpty();
    }
}
