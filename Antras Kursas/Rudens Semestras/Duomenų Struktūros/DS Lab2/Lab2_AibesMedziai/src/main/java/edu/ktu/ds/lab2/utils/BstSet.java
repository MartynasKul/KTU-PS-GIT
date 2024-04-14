package edu.ktu.ds.lab2.utils;

import javax.swing.*;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.Iterator;
import java.util.Stack;

/**
 * Rikiuojamos objektų kolekcijos - aibės realizacija dvejetainiu paieškos
 * medžiu.
 *
 * @param <E> Aibės elemento tipas. Turi tenkinti interfeisą Comparable<E>, arba
 *            per klasės konstruktorių turi būti paduodamas Comparator<E> interfeisą
 *            tenkinantis objektas.
 * @author darius.matulis@ktu.lt
 * @užduotis Peržiūrėkite ir išsiaiškinkite pateiktus metodus.
 */
public class BstSet<E extends Comparable<E>> implements SortedSet<E>, Cloneable {

    // Medžio šaknies mazgas
    protected BstNode<E> root = null;
    // Medžio dydis
    protected int size = 0;
    // Rodyklė į komparatorių
    protected Comparator<? super E> c;

    /**
     * Sukuriamas aibės objektas DP-medžio raktams naudojant Comparable<E>
     */
    public BstSet() {
        this.c = Comparator.naturalOrder();
    }

    /**
     * Sukuriamas aibės objektas DP-medžio raktams naudojant Comparator<E>
     *
     * @param c Komparatorius
     */
    public BstSet(Comparator<? super E> c) {
        this.c = c;
    }

    /**
     * Patikrinama ar aibė tuščia.
     *
     * @return Grąžinama true, jei aibė tuščia.
     */
    @Override
    public boolean isEmpty() {
        return root == null;
    }

    /**
     * @return Grąžinamas aibėje esančių elementų kiekis.
     */
    @Override
    public int size() {
        return size;
    }

    /**
     * Išvaloma aibė.
     */
    @Override
    public void clear() {
        root = null;
        size = 0;
    }

    /**
     * Patikrinama ar aibėje egzistuoja elementas.
     *
     * @param element - Aibės elementas.
     * @return Grąžinama true, jei aibėje egzistuoja elementas.
     */
    @Override
    public boolean contains(E element) {
        if (element == null) {
            throw new IllegalArgumentException("Element is null in contains(E element)");
        }

        return get(element) != null;
    }

    /**
     * Patikrinama ar visi abės set elementai egzistuoja aibėje
     *
     * @param set aibė
     * @return
     */
    @Override
    public boolean containsAll(Set<E> set) {
        for(E element : set)
        {
            if(!this.contains(element)){
                return false;
            }
        }
        return true;
    }

    /**
     * Aibė papildoma nauju elementu.
     *
     * @param element - Aibės elementas.
     */
    @Override
    public void add(E element) {
        if (element == null) {
            throw new IllegalArgumentException("Element is null in add(E element)");
        }

        root = addRecursive(element, root);
    }

    /**
     * Abės set elementai pridedami į esamą aibę, jeigu abi aibės turi tą patį elementą, jis nėra dedamas.
     *
     * @param set pridedamoji aibė
     */
    @Override
    public void addAll(Set<E> set) {
        for(E e : set) {
            if(!contains(e)) {
                this.add(e);
            }
        }
    }

    private BstNode<E> addRecursive(E element, BstNode<E> node) {
        if (node == null) {
            size++;
            return new BstNode<>(element);
        }

        int cmp = c.compare(element, node.element);

        if (cmp < 0) {
            node.left = addRecursive(element, node.left);
        } else if (cmp > 0) {
            node.right = addRecursive(element, node.right);
        }

        return node;
    }


    /**
     * Pašalinamas elementas iš aibės.
     *
     * @param element - Aibės elementas.
     */
    @Override
    public void remove(E element) {

        if (element == null || root == null) {
            System.out.println("kazkas negerai");
            throw new RuntimeException();

        } else {
            removeRecursive(element, root);
        }
        size--;
    }

    public E ceiling(E element)
    {
        E theElement = null;
        int cnt;
        if(root == null)
        {
            throw new RuntimeException();
        }
        Iterator iter = iterator();
        while (iter.hasNext())
        {
            E elementx = (E)iter.next();
            cnt = c.compare(elementx, element);

            if(cnt >= 0)
            {
                if(theElement != null)
                {
                    cnt = c.compare(elementx, theElement);
                    if(cnt > 0)
                    {
                        theElement = elementx;
                    }
                }
                else
                {
                    theElement = elementx;
                }
            }

        }

        return theElement;

    }

   public SortedSet<E> copyOf(BstSet<?extends E> set)
   {
       return null;
   }

    /**
     * Aibėje lieka tik tie elementai, kurie yra aibėje set.
     *
     * @param set aibė
     */
    @Override
    public void retainAll(Set<E> set) {

        Iterator iter = iterator();
        while (iter.hasNext()) {

            if(!set.contains((E)iter.next()))
            {
                iter.remove();
            }
        }

    }

    private BstNode<E> removeRecursive(E element, BstNode<E> node) {
        int numb = c.compare(element, node.element);
        if (numb < 0) {
            node.left = removeRecursive(element, node.left);
        } else if (numb > 0) {
            node.right = removeRecursive(element, node.right);
        } else {

            if (node.right == null) {
                return node.left;
            } else if (node.left == null) {
                return node.right;
            }
            node.element = minValue(node.right);

            node.right = removeRecursive(node.element, node.right);

        }
        return node;
    }

    private E minValue(BstNode<E> node) {
        E min = node.element;
        while (node.left != null) {
            min = node.left.element;
            node = node.left;
        }
        return min;
    }

    private E get(E element) {
        if (element == null) {
            throw new IllegalArgumentException("Element is null in get(E element)");
        }

        BstNode<E> node = root;
        while (node != null) {
            int cmp = c.compare(element, node.element);

            if (cmp < 0) {
                node = node.left;
            } else if (cmp > 0) {
                node = node.right;
            } else {
                return node.element;
            }
        }

        return null;
    }

    /**
     * Grąžinamas aibės elementų masyvas.
     *
     * @return Grąžinamas aibės elementų masyvas.
     */
    @Override
    public Object[] toArray() {
        int i = 0;
        Object[] array = new Object[size];
        for (Object o : this) {
            array[i++] = o;
        }
        return array;
    }

    /**
     * Aibės elementų išvedimas į String eilutę Inorder (Vidine) tvarka. Aibės
     * elementai išvedami surikiuoti didėjimo tvarka pagal raktą.
     *
     * @return elementų eilutė
     */
    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        for (E element : this) {
            sb.append(element.toString()).append(System.lineSeparator());
        }
        return sb.toString();
    }

    /**
     * Medžio vaizdavimas simboliais, žiūr.: unicode.org/charts/PDF/U2500.pdf
     * Tai 4 galimi terminaliniai simboliai medžio šakos gale
     */
    private static final String[] term = {"\u2500", "\u2534", "\u252C", "\u253C"};
    private static final String rightEdge = "\u250C";
    private static final String leftEdge = "\u2514";
    private static final String endEdge = "\u25CF";
    private static final String vertical = "\u2502  ";
    private String horizontal;

    /* Papildomas metodas, išvedantis aibės elementus į vieną String eilutę.
     * String eilutė formuojama atliekant elementų postūmį nuo krašto,
     * priklausomai nuo elemento lygio medyje. Galima panaudoti spausdinimui į
     * ekraną ar failą tyrinėjant medžio algoritmų veikimą.
     *
     * @author E. Karčiauskas
     */
    @Override
    public String toVisualizedString(String dataCodeDelimiter) {
        horizontal = term[0] + term[0];
        return root == null ? ">" + horizontal
                : toTreeDraw(root, ">", "", dataCodeDelimiter);
    }

    private String toTreeDraw(BstNode<E> node, String edge, String indent, String dataCodeDelimiter) {
        if (node == null) {
            return "";
        }
        String step = (edge.equals(leftEdge)) ? vertical : "   ";
        StringBuilder sb = new StringBuilder();
        sb.append(toTreeDraw(node.right, rightEdge, indent + step, dataCodeDelimiter));
        int t = (node.right != null) ? 1 : 0;
        t = (node.left != null) ? t + 2 : t;
        sb.append(indent).append(edge).append(horizontal).append(term[t]).append(endEdge).append(
                split(node.element.toString(), dataCodeDelimiter)).append(System.lineSeparator());
        step = (edge.equals(rightEdge)) ? vertical : "   ";
        sb.append(toTreeDraw(node.left, leftEdge, indent + step, dataCodeDelimiter));
        return sb.toString();
    }

    private String split(String s, String dataCodeDelimiter) {
        int k = s.indexOf(dataCodeDelimiter);
        if (k <= 0) {
            return s;
        }
        return s.substring(0, k);
    }

    /**
     * Sukuria ir grąžina aibės kopiją.
     *
     * @return Aibės kopija.
     * @throws java.lang.CloneNotSupportedException
     */
    @Override
    public Object clone() throws CloneNotSupportedException {
        BstSet<E> cl = (BstSet<E>) super.clone();
        if (root == null) {
            return cl;
        }
        cl.root = cloneRecursive(root);
        cl.size = this.size;
        return cl;
    }

    private BstNode<E> cloneRecursive(BstNode<E> node) {
        if (node == null) {
            return null;
        }

        BstNode<E> clone = new BstNode<>(node.element);
        clone.left = cloneRecursive(node.left);
        clone.right = cloneRecursive(node.right);
        return clone;
    }

    /**
     * Grąžinamas aibės poaibis iki elemento.
     *
     * @param element - Aibės elementas.
     * @return Grąžinamas aibės poaibis iki elemento.
     */
    @Override
    public Set<E> headSet(E element) {
        if (root == null) {
            return null;
        }

        Iterator iter = iterator();
        Set<E> newSet = new BstSet<E>();

        while (iter.hasNext()) {

            E temp = (E) iter.next();
            if(element.compareTo((E)temp) > 0)
            {
                newSet.add(temp);
            }
            else if(element.compareTo((E)temp) <= 0)
            {
                break;
            }
        }
        if(newSet.isEmpty())
        {
            return null;
        }
        return newSet;
    }




    public Set<E> headSet2(E element) {
        if (root == null) {
            return null;
        }
        Set<E> set = new BstSet<E>();
        set = headSetRecursive(element, root, set);
        if(set == null)
        {
            return null;
        }
        return set;
    }

    public Set<E> headSet3(E element) {
        if (root == null) {
            return null;
        }
        Set<E> set = new BstSet<E>();
        set = headSetAround(element, root, set);
        if(set == null)
        {
            return null;
        }
        return set;
    }

    private Set<E> headSetAround(E element, BstNode<E> node, Set<E> set)
    {
        if(node != null)
        {
            if(c.compare(element, node.element) > 0)
            {
                set.add(node.element);
            }
            headSetAround(element,node.left, set);
            headSetAround(element,node.right, set);
        }
        return set;
    }


    private Set<E> headSetRecursive(E element, BstNode<E> node, Set<E> set)
    {
        int cnt = c.compare(element, node.element);
        if(cnt < 0)
        {
            if(node.left == null)
            {
                return null;
            }
            headSetRecursive(element, node.left, set);
        }
        else if(cnt > 0)
        {
            set.add(node.element);
            if(node.left != null)
            {
                headSetRecursive(element, node.left, set);
            }
            if(node.right != null)
            {
                headSetRecursive(element, node.right, set);
            }
            else
            {
                return set;
            }

        }
        if(cnt == 0)
        {
            if(node.left != null)
            {
                headSetRecursive(element, node.left, set);
            }
            if(node.right != null)
            {
                headSetRecursive(element, node.right, set);
            }
        }

        return set;
    }





    /**
     * Grąžinamas aibės poaibis nuo elemento element1 iki element2.
     *
     * @param element1 - pradinis aibės poaibio elementas.
     * @param element2 - galinis aibės poaibio elementas.
     * @return Grąžinamas aibės poaibis nuo elemento element1 iki element2.
     */
    @Override
    public Set<E> subSet(E element1, E element2) {
        if (root == null) {
            return null;
        }
        Iterator iter = iterator();
        Set<E> newSet = new BstSet<E>();
        while (iter.hasNext()) {

            E temp = (E) iter.next();
            if(element1.compareTo((E)temp) < 0 && element2.compareTo(temp) > 0)
            {
                newSet.add(temp);
            }
            else if(element2.compareTo(temp) <= 0) {
            break;
            }
        }
        return newSet;
    }

    /**
     * Grąžinamas aibės poaibis nuo elemento.
     *
     * @param element - Aibės elementas.
     * @return Grąžinamas aibės poaibis nuo elemento.
     */
    @Override
    public Set<E> tailSet(E element) {
        if (root == null) {
            return null;
        }
        Iterator iter = iterator();
        Set<E> newSet = new BstSet<E>();
        while (iter.hasNext()) {

            E temp = (E) iter.next();
            if(element.compareTo((E)temp) < 0)
            {
                newSet.add(temp);
            }
        }
        return newSet;
    }

    /**
     * Grąžinamas tiesioginis iteratorius.
     *
     * @return Grąžinamas tiesioginis iteratorius.
     */
    @Override
    public Iterator<E> iterator() {
        return new IteratorBst(true);
    }

    /**
     * Grąžinamas atvirkštinis iteratorius.
     *
     * @return Grąžinamas atvirkštinis iteratorius.
     */
    @Override
    public Iterator<E> descendingIterator() {
        return new IteratorBst(false);
    }

    /**
     * Vidinė objektų kolekcijos iteratoriaus klasė. Iteratoriai: didėjantis ir
     * mažėjantis. Kolekcija iteruojama kiekvieną elementą aplankant vieną kartą
     * vidine (angl. inorder) tvarka. Visi aplankyti elementai saugomi steke.
     * Stekas panaudotas iš java.util paketo, bet galima susikurti nuosavą.
     */
    private class IteratorBst implements Iterator<E> {

        private final Stack<BstNode<E>> stack = new Stack<>();
        // Nurodo iteravimo kolekcija kryptį, true - didėjimo tvarka, false - mažėjimo
        private final boolean ascending;
        // Reikalinga remove() metodui.
        private BstNode<E> lastInStack;
        private BstNode<E> last;

        IteratorBst(boolean ascendingOrder) {
            this.ascending = ascendingOrder;
            this.toStack(root);
        }

        @Override
        public boolean hasNext() {
            return !stack.empty();
        }

        @Override
        public E next() {// Jei stekas tuščias
            if (stack.empty()) {
                lastInStack = root;
                last = null;
                return null;
            } else {
                // Grąžinamas paskutinis į steką patalpintas elementas
                BstNode<E> n = stack.pop();
                // Atsimenamas paskutinis grąžintas elementas, o taip pat paskutinis steke esantis elementas.
                // Reikia remove() metodui
                lastInStack = stack.isEmpty() ? root : stack.peek();
                last = n;
                BstNode<E> node = (ascending) ? n.right : n.left;
                // Dešiniajame n pomedyje ieškoma minimalaus elemento,
                // o visi paieškos kelyje esantys elementai talpinami į steką
                toStack(node);
                return n.element;
            }
        }

        @Override
        public void remove() {
            BstNode<E> n = (ascending) ? root.left : root.right;
            toStack(n);
            stack.pop();
        }

        private void toStack(BstNode<E> n) {
            while (n != null) {
                stack.push(n);
                n = (ascending) ? n.left : n.right;
            }
        }
    }

    /**
     * Vidinė kolekcijos mazgo klasė
     *
     * @param <N> mazgo elemento duomenų tipas
     */
    protected static class BstNode<N> {

        // Elementas
        protected N element;
        // Rodyklė į kairįjį pomedį
        protected BstNode<N> left;
        // Rodyklė į dešinįjį pomedį
        protected BstNode<N> right;

        protected BstNode() {
        }

        protected BstNode(N element) {
            this.element = element;
            this.left = null;
            this.right = null;
        }
    }
}
