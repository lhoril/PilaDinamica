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

        public bool Contains(T info)
        {
            if (info == null) throw new Exception("No pot comparar amb valors nulls");
            bool trobat = false;
            int num = Convert.ToInt32(top.Info), num2 = Convert.ToInt32(info);
            Node<T> cursor = top;
            while (cursor != null && !trobat)
            {
                if(num == num2)
                {
                    trobat = true;
                }
                num = Convert.ToInt32(cursor.Info);
                cursor = cursor.Seg;
            }
            return trobat;
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
