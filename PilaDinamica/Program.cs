namespace PilaDinamica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pila<int> myPila = new Pila<int>(5);
            myPila.Push(1);
            myPila.Push(2);
            myPila.Push(3);
            myPila.Push(4);
            Console.Write("[ ");
            foreach (int i in myPila)
            {
                Console.Write(i + " ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.WriteLine("Count despés de Pop --> " + myPila.Count);
            myPila.Pop();
            Console.WriteLine("indexOf --> "+myPila.IndexOf(5));
            Console.WriteLine("Contains --> "+myPila.Contains(2));
            Console.WriteLine("Contains --> "+myPila.Contains(7));
            Console.WriteLine("Count --> " +myPila.Count);
            Console.WriteLine("Index --> " +myPila.IndexOf(7));
            myPila.Insert(3, 6);
            Console.WriteLine("Count Despres de Insert --> " + myPila.Count);
            Console.Write("[ ");
            foreach (int i in myPila)
            {
                Console.Write(i +" ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.WriteLine(myPila.Remove(2));
            Console.Write("[ ");
            foreach (int i in myPila)
            {
                Console.Write(i + " ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.WriteLine("Count després del Remove --> " + myPila.Count);
            myPila.Clear();
            Console.WriteLine("Count despés del Clear --> " + myPila.Count);
        }
    }
}