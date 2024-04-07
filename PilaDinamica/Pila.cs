using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PilaDinamica
{
    public class Pila<T> : IEnumerable<T>, IEnumerable, IList<T>
    {
        private Node<T> top = null;
        private int nElem = -1;
        #region Constructors
        public Pila()
        {

        }

        public Pila(T info)
        {
           Node<T> node = new Node<T>(info);
           this.top = node;
            nElem++;
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
                Node<T> target = GoTo(index);
                target.Info = value;
            }
        }

        private Node<T> GoTo(int posicio) // Metodo el cual te lleva a una posicion en la cual te mostrara la informacion actual i la de después
        {
            Node<T> caixa = top;
            if(posicio >= 0 && posicio < this.Count)
            {
                for (int i = 0; i <= posicio; i++)
                {
                    caixa = caixa.Seg;
                }
            }
            else
            {
                caixa = null;
            }


            return caixa;
        }
        #endregion
        #region Constructors
        public void Push(T info) // Mete un elemento delante tuyo
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

        public T Pop() // Elimina el elemento que tienes delante tuyo
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
                nElem--;
            }
            return dada;
        }

        public bool Contains(T info) // Busca un elemnto i retrurna True o false dependiendo si lo has esncontrado
        {
            if (info == null) throw new Exception("No pot comparar amb valors nulls");
            bool trobat = false;
            int num = Convert.ToInt32(top.Info), num2 = Convert.ToInt32(info);
            Node<T> cursor = top;
            while (cursor != null && !trobat)
            {
                trobat = num.Equals(num2);
                num = Convert.ToInt32(cursor.Info);
                cursor = cursor.Seg;
            }
            return trobat;
        }

        public int IndexOf(T item) // Busca un elemento si trobat es igual a true devolvera el numero de posicion de elemento y si es false devolvera -1
        {
            if (item == null) throw new Exception("No pot comparar amb valors nulls");
            bool trobat = false;
            int index = -1;
            IEnumerator<T> cursor = this.GetEnumerator();
            while ( cursor.MoveNext() && !trobat)
            {
                trobat = cursor.Current.Equals(item);
                index++;
            }
            if(!trobat) index = -1;
            return index;
        }

        public void Insert(int index, T item) // Inserta una informacion a partir de una informacion
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index");
            if (IsReadOnly) throw new NotSupportedException();
            Node<T> itNum = new Node<T>(item);
            if (index == 0)
                Add(item);
            else if(index == (Count - 1))
            {
                GoTo(index).Seg = itNum;
                nElem++;
            }
            else
            {
                Node<T> antCaixa = GoTo(index - 1);
                Node<T> ultCaixa = antCaixa.Seg; // Caja que recuperara los ultimos enlaces
                antCaixa.Seg = itNum;
                itNum.Seg = ultCaixa;
                nElem++;
            }
        }
        public void RemoveAt(int index) // RemoveAt elimina un item por index o posicion
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index");
            if (IsReadOnly) throw new NotSupportedException();
            if (index == 0)
            {
                Pop();
            }
            else if (index == (Count - 1))
            {
                GoTo(index).Seg = null;
            }
            else
            {
                Node<T> antCaixa = GoTo(index-1);
                Node<T> node = antCaixa.Seg;
                antCaixa.Seg = node;
                nElem--;
            }
        }

        public bool Remove(T item) // Remove elimina un item
        {
            int index = IndexOf(item);
            RemoveAt(index);
            return Contains(item);
        }

        public void Clear() // Elimina todos los elementos
        {
            if (IsReadOnly) throw new NotSupportedException("No es pot eliminar cap cosa de la pila es només de lectura");
            for (int i = Count; i > 0; i--)
            {
                Pop();
            }
        }

        public void CopyTo(T[] array, int arrayIndex) // Copia una informacion de un array a partir de un index i la coloca con un push a la pila
        {
            for (int i = 0; i < arrayIndex; i++)
            {
                Add(array[i]);
            }
        }

        public void Add(T item) // Añades un numero
        {
            if (item == null) throw new ArgumentNullException("ITEM NO POT SER NULL");
            Push(item);
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

        #endregion
    }
}
