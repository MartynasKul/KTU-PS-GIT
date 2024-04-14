package utils;

import java.util.Iterator;

/*
Realizuokite visus interfeiso metodus susietojo sarašo pagrindu.
Nesinaudokite java klase LinkedList<E>, visa logika meginkite parasyti patys.
Jeigu reikia, galima kurti papildomus metodus ir kintamuosius.
*/
public class LinkedList<T> implements List<T> {

    private class Node{
             public T Data;
             public Node Link;

             public Node(){

             }

             public Node(T value, Node adress)
             {
                 this.Data = value;
                 this.Link = adress;
             }
    }

    private Node head;
    private Node tail;


    public LinkedList()
    {
        this.head = null;
        this.tail = null;
    }

    @Override
    public void add(T item) {

      Node NewNode = new Node(item, null);

      if(this.head == null)
      {
          this.head = NewNode;

      }
      else {
          for(Node d = this.head; d != null; d = d.Link)
          {
              if(d.Link == null)
              {
                  d.Link = NewNode;
                  return;
              }
          }
      }

    }

    @Override
    public T get(int index) {
         int indexnumber = 0;

         for(Node d = this.head; d != null; d = d.Link )
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
    public boolean remove(T item)
    {
        Node s;
        s = head;

        if(this.head == null)
        {
            return true;
        }
        else {
            if (this.head.Data.equals(item)) {
                this.head = this.head.Link;
                return true;
            }
            else {
                for (Node p = this.head.Link; p != null; p = p.Link) {
                    if (p.Data.equals(item)) {
                        s.Link = p.Link;
                        return true;
                    }
                }
            }
        }
        return false;
    }


    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {

            Node current;{
                this.current = LinkedList.this.head;
            }

            @Override
            public boolean hasNext() {
              return this.current !=null;
            }

            @Override
            public T next() {
                 if(this.hasNext()){
                     T data = this.current.Data;
                     this.current = this.current.Link;
                     return data;
                 }
                 else{
                     return null;
                 }
            }
        };
    }
}
