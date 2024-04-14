package utils;

public interface List<T> extends Iterable<T>{
    public void add(T item); // add item to the end
    public T get(int index); // get element by its place in list
    public boolean remove(T item); // remove item, if item removed return true, else return false
}
