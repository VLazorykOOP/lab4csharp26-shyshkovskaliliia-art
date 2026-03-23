using System;
using System.Text;

#nullable disable

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== ЛАБОРАТОРНА РОБОТА №4 =====\n");
                Console.WriteLine("1. Завдання 1.2 - Клас Triangle");
                Console.WriteLine("2. Завдання 2.2 - Клас VectorUInt");
                Console.WriteLine("3. Завдання 3.2 - Співробітник");
                Console.WriteLine("0. Вихід\n");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        task1.Run();
                        break;
                    case "2":
                        task2.Run();
                        break;
                    case "3":
                        task3.Run();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНатисніть Enter для продовження...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("До побачення!");
        }
    }
}
