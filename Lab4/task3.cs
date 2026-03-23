using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace Lab4
{
    using EmployeeTuple = (string surname, string name, string patronymic,
        string position, int birthYear, decimal salary);

    internal class task3
    {
        //ВАРІАНТ 1: СТРУКТУРИ

        public struct EmployeeStruct
        {
            public EmployeeStruct(string surname, string name, string patronymic,
                string position, int birthYear, decimal salary)
            {
                this.surname = surname;
                this.name = name;
                this.patronymic = patronymic;
                this.position = position;
                this.birthYear = birthYear;
                this.salary = salary;
            }

            public void Print()
            {
                Console.WriteLine("   {0} {1} {2}, Посада: {3}, Рік нар.: {4}, ЗП: {5:F2}",
                    surname, name, patronymic, position, birthYear, salary);
            }

            public override string ToString()
            {
                return string.Format("{0} {1} {2} ({3}, {4}, {5:F2})",
                    surname, name, patronymic, position, birthYear, salary);
            }

            public string surname;
            public string name;
            public string patronymic;
            public string position;
            public int birthYear;
            public decimal salary;
        }

        //ВАРІАНТ 3

        public record EmployeeRecord(string Surname, string Name, string Patronymic,
            string Position, int BirthYear, decimal Salary);

        public class EmployeeManager
        {
            public static List<T> DeleteBySurname<T>(List<T> employees, string surname)
            {
                List<T> result = new List<T>();
                bool found = false;

                foreach (var emp in employees)
                {
                    string empSurname = GetSurname(emp);
                    if (empSurname == surname)
                    {
                        found = true;
                        Console.WriteLine("Видалено співробітника: {0}", empSurname);
                    }
                    else
                    {
                        result.Add(emp);
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Співробітника з прізвищем {0} не знайдено", surname);
                }

                return result;
            }

            public static List<T> AddAfterIndex<T>(List<T> employees, int index, T newEmployee)
            {
                if (index < 0 || index >= employees.Count)
                {
                    Console.WriteLine("Помилка: невірний індекс {0}", index);
                    return employees;
                }

                employees.Insert(index + 1, newEmployee);
                Console.WriteLine("Додано співробітника після індексу {0}", index);
                return employees;
            }

            private static string GetSurname<T>(T emp)
            {
                if (emp is EmployeeStruct s) return s.surname;
                if (emp is ValueTuple<string, string, string, string, int, decimal> t) return t.Item1;
                if (emp is EmployeeRecord r) return r.Surname;
                return "";
            }

            public static void PrintList<T>(List<T> employees, string title)
            {
                Console.WriteLine("\n{0}:", title);
                Console.WriteLine(new string('-', 60));
                if (employees.Count == 0)
                {
                    Console.WriteLine("   Список порожній");
                }
                else
                {
                    for (int i = 0; i < employees.Count; i++)
                    {
                        Console.Write("   [{0}] ", i);
                        if (employees[i] is EmployeeStruct s)
                        {
                            s.Print();
                        }
                        else if (employees[i] is EmployeeTuple t)
                        {
                            Console.WriteLine("{0} {1} {2}, Посада: {3}, Рік нар.: {4}, ЗП: {5:F2}",
                                t.surname, t.name, t.patronymic, t.position, t.birthYear, t.salary);
                        }
                        else if (employees[i] is EmployeeRecord r)
                        {
                            Console.WriteLine("{0} {1} {2}, Посада: {3}, Рік нар.: {4}, ЗП: {5:F2}",
                                r.Surname, r.Name, r.Patronymic, r.Position, r.BirthYear, r.Salary);
                        }
                    }
                }
                Console.WriteLine(new string('-', 60));
            }
        }

        public static void Run()
        {
            Console.WriteLine("ЗАВДАННЯ 3.2 - Співробітник (Структури, Кортежі, Записи)");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine("\n1. ВАРІАНТ ЗІ СТРУКТУРАМИ:");
            List<EmployeeStruct> structList = new List<EmployeeStruct>
            {
                new EmployeeStruct("Шевченко", "Тарас", "Григорович", "Поет", 1814, 50000),
                new EmployeeStruct("Франко", "Іван", "Якович", "Письменник", 1856, 45000),
                new EmployeeStruct("Леся", "Українка", "Петрівна", "Поетеса", 1871, 48000),
                new EmployeeStruct("Коцюбинський", "Михайло", "Михайлович", "Письменник", 1864, 47000)
            };

            EmployeeManager.PrintList(structList, "Початковий список (Структури)");

            Console.WriteLine("\nВидалення співробітника з прізвищем 'Франко':");
            structList = EmployeeManager.DeleteBySurname(structList, "Франко");
            EmployeeManager.PrintList(structList, "Список після видалення");

            Console.WriteLine("\nДодавання співробітника після індексу 1:");
            EmployeeStruct newEmp = new EmployeeStruct("Грінченко", "Борис", "Дмитрович",
                "Письменник", 1863, 46000);
            structList = EmployeeManager.AddAfterIndex(structList, 1, newEmp);
            EmployeeManager.PrintList(structList, "Список після додавання");

            Console.WriteLine("\n2. ВАРІАНТ З КОРТЕЖАМИ:");
            List<EmployeeTuple> tupleList = new List<EmployeeTuple>
            {
                ("Шевченко", "Тарас", "Григорович", "Поет", 1814, 50000m),
                ("Франко", "Іван", "Якович", "Письменник", 1856, 45000m),
                ("Леся", "Українка", "Петрівна", "Поетеса", 1871, 48000m),
                ("Коцюбинський", "Михайло", "Михайлович", "Письменник", 1864, 47000m)
            };

            EmployeeManager.PrintList(tupleList, "Початковий список (Кортежі)");

            Console.WriteLine("\nВидалення співробітника з прізвищем 'Леся':");
            tupleList = EmployeeManager.DeleteBySurname(tupleList, "Леся");
            EmployeeManager.PrintList(tupleList, "Список після видалення");

            Console.WriteLine("\nДодавання співробітника після індексу 0:");
            EmployeeTuple newTuple = ("Грінченко", "Борис", "Дмитрович", "Письменник", 1863, 46000m);
            tupleList = EmployeeManager.AddAfterIndex(tupleList, 0, newTuple);
            EmployeeManager.PrintList(tupleList, "Список після додавання");

            Console.WriteLine("\n3. ВАРІАНТ ІЗ ЗАПИСАМИ (RECORDS):");
            List<EmployeeRecord> recordList = new List<EmployeeRecord>
            {
                new EmployeeRecord("Шевченко", "Тарас", "Григорович", "Поет", 1814, 50000),
                new EmployeeRecord("Франко", "Іван", "Якович", "Письменник", 1856, 45000),
                new EmployeeRecord("Леся", "Українка", "Петрівна", "Поетеса", 1871, 48000),
                new EmployeeRecord("Коцюбинський", "Михайло", "Михайлович", "Письменник", 1864, 47000)
            };

            EmployeeManager.PrintList(recordList, "Початковий список (Записи)");

            Console.WriteLine("\nВидалення співробітника з прізвищем 'Коцюбинський':");
            recordList = EmployeeManager.DeleteBySurname(recordList, "Коцюбинський");
            EmployeeManager.PrintList(recordList, "Список після видалення");

            Console.WriteLine("\nДодавання співробітника після індексу 2:");
            EmployeeRecord newRecord = new EmployeeRecord("Грінченко", "Борис", "Дмитрович",
                "Письменник", 1863, 46000);
            recordList = EmployeeManager.AddAfterIndex(recordList, 2, newRecord);
            EmployeeManager.PrintList(recordList, "Список після додавання");

            Console.WriteLine("\n4. ПОРІВНЯННЯ ТИПІВ:");
            Console.WriteLine("Структура - тип значення (стек), копіюється при присвоєнні");
            Console.WriteLine("Кортеж - тип значення, зручний для тимчасових груп даних");
            Console.WriteLine("Запис - посилальний тип (купа), порівнюється за значенням");

            EmployeeRecord r1 = new EmployeeRecord("Тест", "Тест", "Тест", "Тест", 2000, 1000);
            EmployeeRecord r2 = new EmployeeRecord("Тест", "Тест", "Тест", "Тест", 2000, 1000);
            Console.WriteLine("Порівняння записів (r1 == r2): {0}", (r1 == r2).ToString().ToLower());

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("Тестування завершено.");
        }
    }
}