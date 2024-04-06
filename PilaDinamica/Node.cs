using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilaDinamica
{
    public class Node<T>
    {
        private T info;
        private Node<T> seg = null;

        public Node(T info)
        {
            this.info = info;
        }

        public T Info { get => info; set => info = value; }
        public Node<T> Seg { get => seg; set => seg = value; }

    }
}
