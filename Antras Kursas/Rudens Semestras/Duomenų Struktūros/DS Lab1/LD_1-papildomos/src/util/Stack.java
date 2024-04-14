package util;

public interface Stack<E> {

    /**
     *    Deletes the first element and returns it
     */
    E pop();

    /**
     *    Adds a new element at the start of the stack
     */
    void push(E item);
    /**
     *   Returns the first element in the stack
     */
    E peak();

    /**
     * Checks if the stack is empty
     */
    boolean isEmpty();
}
