namespace PilaDinamica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pila<int> myPila = new Pila<int>(55);
            myPila.Push(1);
            myPila.Push(2);
            myPila.Push(3);
            myPila.Push(4);
            Console.WriteLine("Contains --> "+myPila.Contains(2));
            Console.WriteLine("Count --> " +myPila.Count);
            Console.WriteLine("Index --> " +myPila.IndexOf(1));
            //myPila.RemoveAt(1);
            myPila.Insert(2, 1);
            Console.WriteLine("Count Despres de Insert--> " + myPila.Count);

            foreach (int i in myPila)
            {
                Console.Write(i +", ");
            }
            Console.WriteLine();
            //myPila.Clear();
            Console.WriteLine(myPila.Remove(2));
            Console.WriteLine("Count --> " + myPila.Count);
        }
    }
}