package util;

public interface Stack<E>
{
    E pop();

    void push (E item);

    E peak();

    boolean isEmpty();

}
