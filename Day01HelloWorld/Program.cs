using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                Console.Write("What is your name? ");
                string name = Console.ReadLine();
                Console.Write("How old are you? ");
                string ageStr = Console.ReadLine();

                int age = int.Parse(ageStr); //ex Format, Overflow

                int ageTwo;
                if (!int.TryParse(ageStr, out ageTwo))
                {
                    Console.WriteLine("Error: you must enter an integer.");
                } else
                {
                    Console.WriteLine("Name : {0}, Age: {1}", name, ageTwo);
                }

                string greeting = $"Hello {name}, You are {age} years old!";
                Console.WriteLine(greeting);
                //Console.WriteLine("Hello {0}, you are {1} years old!", name, age);
                int nameLen = name.Length;

                if (name.ToLower() == "santa")
                {
                    Console.WriteLine("Santa, WEE WOO WOO WHOO");
                }
            } catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: You must enter an integer. \n" +ex.ToString());
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }


        }
    }
}
