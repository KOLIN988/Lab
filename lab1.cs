using System;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rng  = new Random(); 
            int s =rng.Next(100);

            int low = 0, high = 99;

            
            Console.WriteLine("選0 到 99 之間的整數");

            while (true)
            {
                Console.WriteLine("{0},{1}", low, high);
                string input = Console.ReadLine(); 
                
                bool isValid = int.TryParse(input, out int g); 

                if(!isValid) {                   
                   
                    Console.WriteLine(" 請輸入一個有效的整數！");
                    continue; 
                }
               

                if (g > high || g < low)
                {
                    Console.WriteLine("out of range try again");
                    continue;
                }
                if (g > s)
                {
                    Console.WriteLine("too large");
                    high = g - 1;
                }

                else if (g < s)
                {
                    Console.WriteLine("too small");
                    low = g + 1;
                }
                else
                {
                    Console.WriteLine("bongo");
                    break;
                }
                if (low == high)
                {
                    Console.WriteLine($"gameover answer is {s}");
                    break;
                }
            }
        }
    }
}
