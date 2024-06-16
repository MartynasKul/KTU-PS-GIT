using System;
using System.Collections;
using System.Collections.Generic;

namespace LD3_10_MKuliesius
{
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> Head;
        private int count;

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> current = Head; current != null; current = current.Next)
            {
                yield return current.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LinkedList()
        {
            Head = null;
            count = 0;
        }

        public bool Empty => count == 0;

        public int Count => count;

        public void Add(T obj)
        {
            AddToEnd(obj);
        }

        private void AddToEnd(T obj)
        {
            Node<T> newNode = new Node<T>(obj, null);
            if (Empty)
            {
                Head = newNode;
            }
            else
            {
                GetLastNode().Next = newNode;
            }
            count++;
        }

        /// <summary>
        /// Sorts the list ascending
        /// </summary>
        public void Sort()
        {
            // If the list is empty or contains only one element, it's already sorted
            if (Empty || Head.Next == null)
            {
                return;
            }

            bool swapped;
            do
            {
                swapped = false;
                Node<T> current = Head;
                Node<T> previous = null;

                while (current.Next != null)
                {
                    if (current.Data.CompareTo(current.Next.Data) > 0)
                    {
                        // Swap data of current node and next node
                        T temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        swapped = true;
                    }
                    previous = current;
                    current = current.Next;
                }
            } while (swapped);
        }

        /// <summary>
        /// Returns the indexed information.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T Get(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            Node<T> current = Head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        /// <summary>
        /// Gets the first element that matches the specified condition.
        /// </summary>
        /// <param name="predicate">The condition to match.</param>
        /// <returns>The first element that matches the condition, or null if no such element is found.</returns>
        public T Get(Func<T, bool> predicate)
        {
            for (Node<T> current = Head; current != null; current = current.Next)
            {
                if (predicate(current.Data))
                {
                    return current.Data;
                }
            }
            return default; // Return default value if no element matches the condition
        }

        private Node<T> GetLastNode()
        {
            Node<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            return current;
        }
    }

}