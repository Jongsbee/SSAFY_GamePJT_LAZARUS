namespace Test // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action<string> printActions = PrintA;
            printActions += PrintB;
            printActions += PrintC;

            printActions("Hello, World!");
        }

        static void PrintA(string message)
        {
            Console.WriteLine("a" + message);
        }

        static void PrintB(string message)
        {
            Console.WriteLine("b" + message);
        }

        static void PrintC(string message)
        {
            Console.WriteLine("c" + message);
        }
    }
}