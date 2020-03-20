using System;
using System.Collections;
using System.Collections.Generic;

namespace ai_lab2
{
    //реалізація черги з пріорітетами
    public class PriorityQueue<T> : IEnumerable<T> 
    {
        private readonly List<T> list;
        private readonly IComparer<T> comparer;

        public PriorityQueue(int capacity = 0)
            : this(Comparer<T>.Default, capacity)
        {
        }

        public PriorityQueue(IComparer<T> comparer, int capacity = 0)
        {
            this.list = new List<T>(capacity);
            this.comparer = comparer;
        }

        public PriorityQueue<T> Clone()
        {
            var result = new PriorityQueue<T>(this.comparer, this.Count);

            this.list.ForEach(element => result.Enqueue(element));
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T t)
        {
            return list.Contains(t);
        }

        public bool IsEmpty
        {
            get
            {
                return list.Count == 0;
            }
        }

        public T Peek()
        {

            if (IsEmpty)
            {
                throw new Exception();
            }

            return list[0];
        }

        public bool TryPeek(out T t)
        {

            try
            {
                t = Peek();
                return true;
            }
            catch
            {
            }

            t = default(T);
            return false;
        }

        private void UpHeap(int nIndex)
        {

            while (nIndex > 0)
            {
                int nParentIndex = (nIndex - 1) / 2;
                                
                if (comparer.Compare(list[nParentIndex], list[nIndex]) >= 0)   
                {
                    break;
                }

                T temp = list[nParentIndex];

                list[nParentIndex] = list[nIndex];
                list[nIndex] = temp;
                nIndex = nParentIndex;
            }
        }

        public void Enqueue(T t)
        {
            list.Add(t);
            UpHeap(list.Count - 1);
        }

        private void DownHeap(int nIndex)
        {

            for (; ; )
            {
                int nLeftChildIndex = 2 * nIndex + 1;

                if (nLeftChildIndex >= list.Count)
                {
                    break;
                }

                int nRightChildIndex = nLeftChildIndex + 1;
                int nChildIndex = (
                    nRightChildIndex == list.Count ||       
                    comparer.Compare(list[nLeftChildIndex], list[nRightChildIndex]) > 0 
                    ) ? nLeftChildIndex : nRightChildIndex;

                if (comparer.Compare(list[nIndex], list[nChildIndex]) >= 0) 
                {
                    break;
                }

                T temp = list[nChildIndex];

                list[nChildIndex] = list[nIndex];
                list[nIndex] = temp;
                nIndex = nChildIndex;
            }
        }

        public T Dequeue()
        {

            if (IsEmpty)
            {
                throw new Exception();
            }

            var result = list[0];

            list[0] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            DownHeap(0);
            return result;
        }

        public bool TryDequeue(out T t)
        {

            try
            {
                t = Dequeue();
                return true;
            }
            catch
            {
            }

            t = default(T);
            return false;
        }

        public List<T> DequeueAllToList()
        {
            var result = new List<T>(this.Count);

            while (!this.IsEmpty)
            {
                result.Add(this.Dequeue());
            }

            return result;
        }

        public void FindAndUpHeap(T t)
        {
            UpHeap(this.list.FindIndex(t2 => t2.Equals(t)));
        }

        public T Find(T t)
        {
            return this.list.Find(t2 => t2.Equals(t));
        }
    }
}
