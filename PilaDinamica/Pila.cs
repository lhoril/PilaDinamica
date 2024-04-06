using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PilaDinamica
{
    public class Pila<T> : IEnumerable<T>, IEnumerable, IList<T>
    {
        private Node<T> top = null;
        private int nElem = 0;
        #region Constructors
        public Pila()
        {

        }

        public Pila(T info)
        {
           Node<T> node = new Node<T>(info);
           this.top = node;
        }
        #endregion
        #region Propietats
        public bool Empty
        {
            get { return top == null; }
        }

        public int Count => nElem+1;

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException("FORA DE RANG");

                Node<T> target = GoTo(index);
                return target.Info;
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException("FORA DE RANG");
                if (IsReadOnly) throw new NotSupportedException("L'ESTRUCTURA NO PERMET MODIFICACIONS");
            }
        }

        private Node<T> GoTo(int posicio)
        {
            Node<T> target = top;
            if(posicio >= 0 && posicio < this.Count)
            {
                for (int i = 0; i <= posicio; i++)
                {
                    target = target.Seg;
                }
            }
            else
            {
                target = null;
            }


            return target;
        }
        #endregion
        #region Constructors
        public void Push(T info)
        {
            Node<T> node = new Node<T>(info);
            if (!Empty)
            {
                node.Seg = top;
                top = node;
                nElem++;
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

        public int IndexOf(T item)
        {
            if (item == null) throw new Exception("No pot comparar amb valors nulls");
            bool trobat = false;
            int num = 0, num2 = Convert.ToInt32(item), index = -1;
            IEnumerator<T> cursor = this.GetEnumerator();
            while ( cursor.MoveNext() && !trobat)
            {
                num = Convert.ToInt32(cursor.Current);
                trobat = num.Equals(num2);
                index++;
            }
            return index;
        }

        public void Insert(int index, T item)
        {
            
        }

        public void RemoveAt(int index)
        {
            
        }

        public void Clear()
        {
            top = null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Interficies

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

        public void Add(T item)
        {
            if(item == null) throw new ArgumentNullException("ITEM NO POT SER NULL");
            Push(item);
        }

        #endregion
    }
}
