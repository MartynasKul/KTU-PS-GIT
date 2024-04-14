package utils;

import java.util.Iterator;

/*
Realizuokite visus interfeiso metodus susietojo sarašo pagrindu.
Nesinaudokite java klase LinkedList<E>, visa logika meginkite parasyti patys.
Jeigu reikia, galima kurti papildomus metodus ir kintamuosius.
*/
public class LinkedList<T> implements List<T> {

    private class Node<T>{
        public T Data;
        public Node<T> Link;

        public Node(){

        }

        public Node(T value, Node<T> adress)
        {
            this.Data = value;
            this.Link = adress;
        }
    }
    private Node<T> first;
    private Node<T> last;
    private Node<T> current;
    private int listLength;

    public LinkedList(){


    }

    @Override
    public void add(T item) {

        Node NewNode = new Node(item, null);

        if(this.first == null)
        {
            this.first = NewNode;

        }
        else {
            for(Node d = this.first; d != null; d = d.Link)
            {
                if(d.Link == null)
                {
                    d.Link = NewNode;
                    return;
                }
            }
        }

      //  Entry<T> e;
      //       if (n < size / 2)
      //             {
      //               e = first;
      //               // n less than size/2, iterate from start
      //               while (n-- > 0)
      //                     e = e.next;
      //             }
      //       else
      //         {
      //               e = last;
      //               // n greater than size/2, iterate from end
      //               while (++n < size)
      //                     e = e.previous;
      //             }
      //       return e;
    }


    @Override
    public T get(int index) {

        int indexnumber = 0;

        for(Node<T> d = this.first; d != null; d = d.Link )
        {
            if(indexnumber == index)
            {
                return d.Data;
            }
            indexnumber++;
        }
        return null;

    }

    @Override
    public boolean remove(T item) {
        Node s;
        s = first;

        if(this.first == null)
        {
            return true;
        }
        else {
            if (this.first.Data.equals(item)) {
                this.first = this.first.Link;
                return true;
            } else {
                for (Node p = this.first.Link; p != null; p = p.Link) {
                    if (p.Data.equals(item)) {
                        s.Link = p.Link;
                        return true;
                    }
                }
            }
        }
        return false;


       // throw new UnsupportedOperationException("Metoda reikia realiztuoti");
    }

    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {

            Node current;
            {
                this.current = LinkedList.this.first;
            }

            @Override
            public boolean hasNext() {
                return this.current !=null;
            }

            @Override
            public T next() {
                if(this.hasNext())
                {
                    T data = (T) this.current.Data;
                    this.current = this.current.Link;
                    return data;
                }
                else
                {
                    return null;
                }
            }
        };
    }
}
