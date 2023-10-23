using System;
using System.Collections.Generic;

namespace ConsoleDiary
{
    class Program
    {
        static Dictionary<DateTime, List<string>> diary = new Dictionary<DateTime, List<string>>();
        static DateTime currentDate = DateTime.Now.Date;

        static void AddEntry()
        {
            Console.WriteLine("Введите новую запись:");
            string entry = Console.ReadLine();

            if (!diary.ContainsKey(currentDate))
                diary[currentDate] = new List<string>();

            diary[currentDate].Add(entry);
        }

        static void DeleteEntry()
        {
            if (!diary.ContainsKey(currentDate) || diary[currentDate].Count == 0)
            {
                Console.WriteLine("Нет записей для удаления.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Выберите номер записи для удаления:");
            DisplayEntries();

            int entryNumber;
            if (int.TryParse(Console.ReadLine(), out entryNumber) && entryNumber >= 1 && entryNumber <= diary[currentDate].Count)
            {
                diary[currentDate].RemoveAt(entryNumber - 1);
                Console.WriteLine("Запись удалена.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Некорректный номер записи.");
                Console.ReadKey();
            }
        }

        static void DisplayEntries()
        {
            if (diary.ContainsKey(currentDate) && diary[currentDate].Count > 0)
            {
                Console.WriteLine("Записи на " + currentDate.ToShortDateString() + ":");
                for (int i = 0; i < diary[currentDate].Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {diary[currentDate][i]}");
                }
            }
            else
            {
                Console.WriteLine("Нет записей на " + currentDate.ToShortDateString() + ".");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в ежедневник!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Дата: " + currentDate.ToShortDateString());
                Console.WriteLine("+++++++++++++++++++++");
                DisplayEntries();

                Console.WriteLine("\nВыберите действие:" +
                    "\n1. Добавить запись" +
                    "\n2. Удалить запись" +
                    "\n5. Сохранить ежедневник в файл" +
                     "\nСтрелка в право. Перейти к следующей дате" +
                    "\nCтрелка в лево. Перейти к предыдущей дате");

                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        AddEntry();
                        break;
                    case ConsoleKey.D2:
                        DeleteEntry();
                        break;
                    case ConsoleKey.LeftArrow:
                        currentDate = currentDate.AddDays(-1);
                        break;
                    case ConsoleKey.RightArrow:
                        currentDate = currentDate.AddDays(1);
                        break;
                    case ConsoleKey.D3:
                        isRunning = false;
                        break;
                }
            }

            Console.WriteLine("До свидания!");
        }
    }
}
