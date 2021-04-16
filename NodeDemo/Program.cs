using System;

namespace NodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var nm = new NodeManager();
            Console.Write("VAL\t{0}\t", nm.CurrentVal);
            nm.Draw();
            nm.CurrentVal = 650;
            Console.Write("VAL\t{0}\t", nm.CurrentVal);
            nm.Draw();
            nm.CurrentVal = 649;
            Console.Write("VAL\t{0}\t", nm.CurrentVal);
            nm.Draw();
            
        }
    }
}