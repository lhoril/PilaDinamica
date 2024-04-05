using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilaDinamica
{
    public class Pila<T> : IEnumerable<T>, IEnumerable
    {
        private Node<T> top = null;

        public Pila()
        {

        }

        public Pila(T info)
        {
           Node<T> node = new Node<T>(info);
           this.top = node;
        }

        public bool Empty
        {
            get { return top == null; }
        }

        public void Push(T info)
        {
            Node<T> node = new Node<T>(info);
            if (!Empty)
            {
                node.Seg = top;
                top = node;
            }
            else
            {
                this.top = node;
            }
        }

        public T Pop()
        {
            if (Empty) throw new Exception("STACK UNDERFLOW! STACK IS EMPTY!!!");
            T dada = top.Info;
            if(top.Seg == null)
            {
                top = null;
            }
            else
            {
                Node<T> tmp = top;
                top = top.Seg;
                tmp.Seg = null;
            }
            return dada;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> cursor = top;
            while (cursor != null)
            {
                yield return cursor.Info;
                cursor = cursor.Seg;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
