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
        private int nElem = 0;
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

        public int Count => nElem;

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
        #endregion

        #region Metoths
        /// <summary>
        /// GoTo es una funcio que ens permet agafar una quantitat de numeros dins una caixa
        /// </summary>
        /// <param name="posicio"> index al que volem arribar</param>
        /// <returns>Una variable de classe node amb els numeros desde la posicio que nosaltres volem</returns>
        private Node<T> GoTo(int posicio) // Metodo el cual te lleva a una posicion en la cual te mostrara la informacion actual i la de después
        {
            Node<T> caixa = top;
            if (posicio >= 0 && posicio < this.Count)
            {
                for (int i = 0; i < posicio; i++)
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
        /// <summary>
        /// Push es una funcio que crea una caixa que després la posara dins la pila
        /// Al principi de tot canviant la posicio dels altres elements cap endevant 
        /// deixant un espai pels nous elements
        /// </summary>
        /// <param name="info">info es la informació que volem posar dins la nostra pila en una caixa</param>
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
        /// <summary>
        /// Pop es una funcio que elimina el primer element que tenim devant nostre (Top.info)
        /// Baixant en 1 el numero de element en cas d'haver eliminat el numero.
        /// </summary>
        /// <returns>retorna el element que hem eliminat</returns>
        /// <exception cref="Exception">Dona una excepcio en cas de que la pila sigui buida</exception>
        public T Pop() // Elimina el elemento que tienes delante tuyo
        {
            if (Empty) throw new Exception("STACK UNDERFLOW! STACK IS EMPTY!!!");
            T dada = top.Info;
            if(top.Seg == null)
            {
                top = null;
                nElem--;
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
        /// <summary>
        /// Contains es una funcio que permet cercar un element 
        /// en una pila de elements. 
        /// </summary>
        /// <param name="info">Cerca un element dins la pila</param>
        /// <returns>Retorna True or False depenent si lo ha trobat o no</returns>
        /// <exception cref="ArgumentNullException">Donara Exception en cas de que el item que volem cerca sigui null</exception>
        public bool Contains(T info) // Busca un elemnto i retrurna True o false dependiendo si lo has esncontrado
        {
            if (info == null) throw new ArgumentNullException("No pot comparar amb valors nulls");
            bool trobat = false;
            IEnumerator<T> cursor = this.GetEnumerator();
            while (cursor.MoveNext() && !trobat)
            {
                trobat = cursor.Current.Equals(info);
            }
            return trobat;
        }
        /// <summary>
        /// IndexOf es una funcio que cercara un numero i després agafara el num de posicio dins una pila de elements
        /// </summary>
        /// <param name="item">item es el numero que cercarem</param>
        /// <returns>Retornara el numero de popsicio o -1 depenent si s'ha trobat el numero o no</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int IndexOf(T item) // Busca un elemento si trobat es igual a true devolvera el numero de posicion de elemento y si es false devolvera -1
        {
            if (item == null) throw new ArgumentNullException("No pot comparar amb valors nulls");
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
        /// <summary>
        /// Insertara un numero dins la posico que demanem
        /// </summary>
        /// <param name="index">Numero de posicio dins la pila</param>
        /// <param name="item">Numero que voldrem insertar dins la pila</param>
        /// <exception cref="ArgumentOutOfRangeException">Donara exception en cas de que index estigui fora del rang de elements de la pila</exception>
        /// <exception cref="NotSupportedException">Donara exception en cas de que la pila sigui de només lectura</exception>
        public void Insert(int index, T item) // Inserta una informacion a partir de una informacion
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("El index no esta dins el rang de elements");
            if (IsReadOnly) throw new NotSupportedException("No es pot editar la pila es només de lectura");
            Node<T> itNum = new Node<T>(item);
            if (index == 0)
                Add(item);
            else if(index == (Count - 1))
            {
                Node<T> antCaixa = GoTo(index-1);
                Node<T> ultCaixa = antCaixa.Seg;
                antCaixa.Seg = itNum;
                itNum.Seg = ultCaixa;
                nElem++;
            }
            else
            {
                Node<T> antCaixa = GoTo(index-1);
                Node<T> ultCaixa = antCaixa.Seg; // Caja que recuperara los ultimos enlaces
                antCaixa.Seg = itNum;
                itNum.Seg = ultCaixa;
                nElem++;
            }
        }
        /// <summary>
        /// Elimina un numero per posicio
        /// </summary>
        /// <param name="index">posicio dek item que voldrem eliminar</param>
        /// <exception cref="ArgumentOutOfRangeException">Donara exception en cas de que el index sigui major o menor a la cuantitat de elements</exception>
        /// <exception cref="NotSupportedException">Donara exception en cas de que la pila sigui de només lectura</exception>
        public void RemoveAt(int index) // RemoveAt elimina un item por index o posicion
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("El index no esta dins el rang de elements");
            if (IsReadOnly) throw new NotSupportedException("No es pot editar la pila es només de lectura");
            if (index == 0)
            {
                Pop();
            }
            else if (index == (Count - 1))
            {
                Node<T> antCaixa = GoTo(index-1);
                Node<T>ultCaixa= antCaixa.Seg;
                Node<T> node = ultCaixa.Seg;
                antCaixa.Seg = null;
                antCaixa.Seg = node;
                nElem--;
            }
            else
            {
                Node<T> antCaixa = GoTo(index-1);
                Node<T> node = antCaixa.Seg;
                Node<T> newIt = node.Seg;
                antCaixa.Seg = null;
                antCaixa.Seg = newIt;
                nElem--;
            }
        }
        /// <summary>
        /// Elimina un item concret
        /// </summary>
        /// <param name="item">El numero que volem eliminar</param>
        /// <returns>Si s'ha eliminat el numero retorna True i si no False</returns>
        /// <exception cref="NotSupportedException">Donara exception si la pila es només de lectura</exception>
        public bool Remove(T item) // Remove elimina un item
        {
            if (IsReadOnly) throw new NotSupportedException("No es pot editar la pila es només de lectura");
            bool isRemoved = false;
            int index = IndexOf(item);
            RemoveAt(index);
            if(!Contains(item)) isRemoved = true;
            return isRemoved;
        }
        /// <summary>
        /// Elimina tots els elements de la pila
        /// </summary>
        /// <exception cref="NotSupportedException"> Donara exception en cas de que la pila sigui només de lectura</exception>
        public void Clear() // Elimina todos los elementos
        {
            if (IsReadOnly) throw new NotSupportedException("No es pot eliminar cap cosa de la pila es només de lectura");
            for (int i = Count; i > 0; i--)
            {
                Pop();
            }
        }
        /// <summary>
        /// Copia una informacio d'un array a partir d'una posicio i la coloca amb un push a la pila
        /// </summary>
        /// <param name="array">L'array amb la informacio a copiar</param>
        /// <param name="arrayIndex">El numero de elements per posicio</param>
        /// <exception cref="ArgumentNullException">Donara exception en cas de que l'array sigui null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Donara exception en cas de que el numero de posicio sigui menor a la capacitat del array</exception>
        /// <exception cref="ArgumentException">Donara Exception en cas de que el index sigui major a la capacitat del array</exception>
        public void CopyTo(T[] array, int arrayIndex) // Copia una informacion de un array a partir de un index i la coloca con un push a la pila
        {
            if(array == null) throw new ArgumentNullException("El contingut del array no pot ser null");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("El index no pot ser menor a la capacitat del array");
            if (arrayIndex > array.Length) throw new ArgumentException("El index no pot ser mayor a la capacitat del array");
            for (int i = 0; i < arrayIndex; i++)
            {
                Add(array[i]);
            }
        }
        /// <summary>
        /// Afegeix numeros a la primera posicio
        /// </summary>
        /// <param name="item">es el numero que volem afegir</param>
        /// <exception cref="NotSupportedException">Donara exception si la pila es de només lectura</exception>
        public void Add(T item) // Añades un numero
        {
            if (IsReadOnly) throw new NotSupportedException("No es pot afegir cap element a una pila de només lectura");
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
