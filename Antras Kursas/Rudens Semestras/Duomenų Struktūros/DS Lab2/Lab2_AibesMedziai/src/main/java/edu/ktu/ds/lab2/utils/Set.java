package edu.ktu.ds.lab2.utils;

/**
 * Interfeisu aprašomas Aibės ADT.
 *
 * @param <E> Aibės elemento duomenų tipas
 */
public interface Set<E> extends Iterable<E> {

    //Patikrinama ar aibė tuščia.
    boolean isEmpty();

    // Grąžinamas aibėje esančių elementų kiekis.
    int size();

    // Išvaloma aibė.
    void clear();

    // Aibė papildoma nauju elementu.
    void add(E element);

    // Abės set elementai pridedami į esamą aibę, jeigu abi aibės turi tą patį elementą, jis nėra dedamas.
    void addAll(Set<E> set);

    // Pašalinamas elementas iš aibės.
    void remove(E element);

    // Aibėje lieka tie elementai, kurie yra esamoje abėje ir aibėje set.
    void retainAll(Set<E> set);

    // Patikrinama ar elementas egzistuoja aibėje.
    boolean contains(E element);

    // Patikrinama ar visi abės set elementai egzistuoja aibėje.
    boolean containsAll(Set<E> set);

    E ceiling(E element);

    // Grąžinamas aibės elementų masyvas.
    Object[] toArray();

    // Gražinamas vizualiai išdėstytas aibės elementų turinys
    String toVisualizedString(String dataCodeDelimiter);
}
