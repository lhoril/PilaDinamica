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
            Console.WriteLine("Contains --> "+myPila.Contains(5));
            Console.WriteLine("Count --> " +myPila.Count);
            Console.WriteLine("Index --> " +myPila.IndexOf(1));
            myPila.RemoveAt(0);
            //myPila.Insert(3, 1);

            foreach (int i in myPila)
            {
                Console.Write(i +", ");
            }
        }
    }
}