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
            Console.WriteLine(myPila.Contains(5));

            foreach (int i in myPila)
            {
                Console.WriteLine(i);
            }
        }
    }
}