package util;

public class ArrayQueue<E> implements Queue<E> {

    int size = 4;
    private E[] elements = (E[]) new Object[size];
    int front = -1;
    int rear = -1;

    //Makes the array bigger and rewrites data in an order,
    // where front is the beginning of the array and rear is
    // the index of the last element in the array.
    private void EnsureCapacity(int min)
    {
        if(min > this.size)
        {
            E[] temp = (E[]) new Object[min];
            for(int i = 0; i < this.size; i++)
            {
                //Sees if the front index is the last element in the array
                if(front == size-1)
                {
                    temp[i] = elements[front];
                    front = 0;

                }
                else
                {
                    //Makes front 0 if the array never used Dequeue
                    // to move the front index, so it starts from 0
                    if(front == -1)
                    {
                        front = 0;
                    }
                    //starts to add elements to the bigger array from the front
                    temp[i] = elements[front];
                    front++;
                }

            }
            //Front gets set to the start rear gets set to the last element
            front = 0;
            rear = size-1;
            this.elements = temp;
            this.size = min;
        }
    }


    // Sees if the array could be minimized size/2, and minimizes if that is the case.
    private void seeSize()
    {
        //Sees if front and rear index are below or above the haf way point of the arrays size
        if(rear < (size)/2 && front < (size)/2 && size > 2 || rear > (size)/2 && front > (size)/2 && size > 2)
        {
            //Makes the arrays size 2 times smaller
            this.size = size/2;
            //Tells how many elements are stored in the array at this point
            int count = rear - front + 1;
            E[] temp = (E[]) new Object[size];
            for(int i = 0; i <= count; i++)
            {

                //front travels true the array and adds all the elements to the smaller one
                temp[i] = elements[front];
                front++;


            }
            front = 0;
            rear = count-1;
            this.elements = temp;
        }
    }

    // Sees if the array is full
     private boolean isFulll()
     {
         //if front index is at the front (means no Dequeue was made) and
         // the rear index is at the back of the array, that it returns true
         if((front == 0 || front == -1) && rear == size-1)
         {
             return  true;
         }
         if(front == rear + 1)
         {
             return true;
         }
         return false;
     }

    @Override
    public void enqueue(Object item) {


        if(isFulll())
        {

            EnsureCapacity(this.size*2);
        }

        //if the rear reached the last index but that is some
        // more space in the front of the array it will add
        if(rear == size - 1 && front != 0)
        {
            rear = 0;
            this.elements[rear] = (E) item;
        }
        else
        {
            rear++;
            elements[rear] = (E) item;

        }

    }

    @Override
    public E dequeue() {
        E removedElement;
        if(isEmpty())
        {
            return null;
        }
        else
        {
            //makes front = 0 if its the first time using dequeue
            if(front == -1)
            {
                front++;
            }
            removedElement = elements[front];
            //if it's the last element
            if(front == rear)
            {
                front = -1;
                rear = -1;
            }
            else
            {
                if(front == size-1)
                {
                    front = 0;
                }
                else
                {
                    front++;
                }

            }

        }
        seeSize();
        return removedElement;
    }

    @Override
    public E peak() {
        System.out.println( "Rear: " + rear);
        System.out.println( "Size: " + size);
        System.out.println("Front: " + front);
        return elements[rear];
    }

    @Override
    public boolean isEmpty() {
        if(rear == -1)
        {
            return true;
        }
        return false;
    }
}
