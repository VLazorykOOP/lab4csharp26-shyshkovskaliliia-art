using System;

#nullable disable

namespace Lab4
{
    internal class task1
    {
        class Triangle
        {
            public Triangle() : this(3, 3, 3, 0) { }

            public Triangle(int a, int b, int c, int color = 0)
            {
                if (IsValidTriangle(a, b, c))
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                    this.color = color;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Помилка: Задані сторони не утворюють трикутник!");
                    this.a = 3; this.b = 3; this.c = 3;
                    this.color = 0;
                    isValid = false;
                }
            }

            public int this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return a;
                        case 1: return b;
                        case 2: return c;
                        case 3: return color;
                        default: return -1;
                    }
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            if (IsValidTriangle(value, b, c)) a = value;
                            else Console.WriteLine("Помилка індексації [0]: Сторона недопустима.");
                            break;
                        case 1:
                            if (IsValidTriangle(a, value, c)) b = value;
                            else Console.WriteLine("Помилка індексації [1]: Сторона недопустима.");
                            break;
                        case 2:
                            if (IsValidTriangle(a, b, value)) c = value;
                            else Console.WriteLine("Помилка індексації [2]: Сторона недопустима.");
                            break;
                        case 3:
                            color = value;
                            break;
                    }
                }
            }

            // Оператори ++, --
            public static Triangle operator ++(Triangle t) { t.a++; t.b++; t.c++; t.UpdateValidity(); return t; }
            public static Triangle operator --(Triangle t) { t.a--; t.b--; t.c--; t.UpdateValidity(); return t; }

            // Оператори true, false
            public static bool operator true(Triangle t) => t.isValid;
            public static bool operator false(Triangle t) => !t.isValid;

            // Оператор * (скаляр)
            public static Triangle operator *(Triangle t, int scalar)
            {
                return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
            }

            // Комутативне множення (скаляр * вектор)
            public static Triangle operator *(int scalar, Triangle t) => t * scalar;

            // Перетворення типів
            public static explicit operator string(Triangle t) => t.ToString();

            public static explicit operator Triangle(string s)
            {
                try
                {
                    string[] parts = s.Split(',');
                    if (parts.Length >= 3)
                    {
                        int a = int.Parse(parts[0]);
                        int b = int.Parse(parts[1]);
                        int c = int.Parse(parts[2]);
                        int color = (parts.Length > 3) ? int.Parse(parts[3]) : 0;
                        return new Triangle(a, b, c, color);
                    }
                    return new Triangle();
                }
                catch { return new Triangle(); }
            }

            // Методи
            public int GetPerimeter() => a + b + c;

            public double GetArea()
            {
                if (!isValid) return 0;
                double p = GetPerimeter() / 2.0;
                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }

            public override string ToString()
            {
                return string.Format("Triangle[{0}, {1}, {2}] P={3} S={4:F2} Color={5}", a, b, c, GetPerimeter(), GetArea(), color);
            }

            private void UpdateValidity()
            {
                isValid = IsValidTriangle(a, b, c);
            }

            private static bool IsValidTriangle(int x, int y, int z)
            {
                return (x > 0 && y > 0 && z > 0) && (x + y > z) && (x + z > y) && (y + z > x);
            }

            // Поля
            protected int a, b, c;
            protected int color;
            protected bool isValid;
        }

        public static void Run()
        {
            Console.WriteLine("ЗАВДАННЯ 1.2 - Клас Triangle (Розширений)");
            Console.WriteLine(new string('-', 60));

            // 1. Тестування індексатора
            Console.WriteLine("\n1. Тестування індексатора:");
            Triangle t1 = new Triangle(3, 4, 5, 10);
            Console.WriteLine("Початковий стан: {0}", t1);
            Console.WriteLine("Індекс [0] (a): {0}", t1[0]);
            Console.WriteLine("Індекс [3] (color): {0}", t1[3]);

            Console.WriteLine("Зміна через індексатор [0] = 4:");
            t1[0] = 4;
            Console.WriteLine("Новий стан: {0}", t1);

            Console.WriteLine("Спроба зміни [0] = 100 (має бути помилка):");
            t1[0] = 100;

            // 2. Тестування операторів ++ та --
            Console.WriteLine("\n2. Тестування операторів ++ та --:");
            Triangle t2 = new Triangle(3, 3, 3, 5);
            Console.WriteLine("До ++: {0}", t2);
            t2++;
            Console.WriteLine("Після ++: {0}", t2);
            t2--;
            Console.WriteLine("Після --: {0}", t2);

            // 3. Тестування операторів true / false
            Console.WriteLine("\n3. Тестування операторів true / false:");
            Triangle tValid = new Triangle(3, 4, 5, 1);
            if (tValid)
                Console.WriteLine("✓ t1 є валідним трикутником");
            else
                Console.WriteLine("✗ t1 НЕ є валідним трикутником");

            Triangle tInvalid = new Triangle(1, 1, 1, 0);
            tInvalid--; 
            if (tInvalid)
                Console.WriteLine("✗ tInvalid є валідним (помилка!)");
            else
                Console.WriteLine("✓ tInvalid НЕ є валідним трикутником");

            // 4. Тестування оператора * (скаляр)
            Console.WriteLine("\n4. Тестування оператора * (скаляр):");
            Triangle t3 = new Triangle(3, 4, 5, 1);
            Console.WriteLine("Оригінал: {0}", t3);
            Triangle t3Scaled = t3 * 2;
            Console.WriteLine("Після * 2: {0}", t3Scaled);
            Triangle t3Scaled2 = 3 * t3;
            Console.WriteLine("3 * t3: {0}", t3Scaled2);

            Console.WriteLine("\n5. Тестування перетворення типів:");
            string sTriangle = (string)t1;
            Console.WriteLine("Triangle → string: {0}", sTriangle);

            string input = "5,5,5,7";
            Console.WriteLine("Вхідний рядок: \"{0}\"", input);
            Triangle tFromString = (Triangle)input;
            Console.WriteLine("Результат: {0}", tFromString);

            Console.WriteLine("\n" + new string('-', 60));
        }
    }
}