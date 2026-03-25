using System;

#nullable disable

namespace Lab4
{
    internal class task1
    {
        class Triangle
        {
            protected int a;        
            protected int b;       
            protected int c;     
            protected int color;    
            protected bool isValid;  

            public Triangle() : this(3, 3, 3, 0)
            {
                
            }

            public Triangle(int a, int b, int c, int color = 0)
            {
                if (IsValidTriangle(a, b, c))
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                    this.color = color;
                    this.isValid = true;
                }
                else
                {
                    Console.WriteLine("Помилка: задані сторони не утворюють трикутник!");
                    this.a = 3;
                    this.b = 3;
                    this.c = 3;
                    this.color = 0;
                    this.isValid = false;
                }
            }

            public int this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.a;
                        case 1: return this.b;
                        case 2: return this.c;
                        case 3: return this.color;
                        default:
                            Console.WriteLine($"Помилка індексатора (get): індекс [{index}] недопустимий! Допустимі: 0, 1, 2, 3");
                            return -1;
                    }
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            if (IsValidTriangle(value, this.b, this.c))
                                this.a = value;
                            else
                                Console.WriteLine($"Помилка індексатора (set): значення {value} не утворює трикутник (a)");
                            break;

                        case 1:
                            if (IsValidTriangle(this.a, value, this.c))
                                this.b = value;
                            else
                                Console.WriteLine($"Помилка індексатора (set): значення {value} не утворює трикутник (b)");
                            break;

                        case 2:
                            if (IsValidTriangle(this.a, this.b, value))
                                this.c = value;
                            else
                                Console.WriteLine($"Помилка індексатора (set): значення {value} не утворює трикутник (c)");
                            break;

                        case 3:
                          
                            this.color = value;
                            break;

                        default:
                            Console.WriteLine($" Помилка індексатора (set): індекс [{index}] недопустимий! Допустимі: 0, 1, 2, 3");
                            break;
                    }
                }
            }

            public static Triangle operator ++(Triangle t)
            {
                if (t != null)
                {
                    t.a++;
                    t.b++;
                    t.c++;
                    t.UpdateValidity(); 
                }
                return t;
            }

            public static Triangle operator --(Triangle t)
            {
                if (t != null)
                {
                    t.a--;
                    t.b--;
                    t.c--;
                    t.UpdateValidity();
                }
                return t;
            }

            public static bool operator true(Triangle t) => t?.isValid == true;
            public static bool operator false(Triangle t) => t?.isValid != true;

            public static Triangle operator *(Triangle t, int scalar)
            {
                if (t == null) return null;
                return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
            }

            public static Triangle operator *(int scalar, Triangle t) => t * scalar;
            public static explicit operator string(Triangle t)
            {
                if (t == null) return "Triangle[null]";
                return t.ToString(); 
            }

            public static explicit operator Triangle(string s)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(s))
                        return new Triangle(); 

                    string[] parts = s.Split(new[] { ',', ';', ' ' },
                                           StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Попередження: недостатньо даних для створення трикутника");
                        return new Triangle();
                    }

                    int a = int.Parse(parts[0].Trim());
                    int b = int.Parse(parts[1].Trim());
                    int c = int.Parse(parts[2].Trim());
                    int color = (parts.Length > 3) ? int.Parse(parts[3].Trim()) : 0;

                    return new Triangle(a, b, c, color);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка формату: не вдалося розібрати рядок");
                    return new Triangle();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Помилка: значення виходять за межі типу int");
                    return new Triangle();
                }
            }

            public int GetPerimeter() => this.a + this.b + this.c;

            public double GetArea()
            {
                if (!this.isValid) return 0.0;

                double p = GetPerimeter() / 2.0; 
                return Math.Sqrt(p * (p - this.a) * (p - this.b) * (p - this.c));
            }

            public override string ToString()
            {
                return string.Format(
                    "Triangle[a={0}, b={1}, c={2}] | P={3}, S={4:F2} | Color={5} | Valid={6}",
                    this.a, this.b, this.c,
                    GetPerimeter(), GetArea(),
                    this.color, this.isValid ? "Так" : "Ні");
            }

            private void UpdateValidity()
            {
                this.isValid = IsValidTriangle(this.a, this.b, this.c);
            }

            private static bool IsValidTriangle(int x, int y, int z)
            {
                bool positive = (x > 0) && (y > 0) && (z > 0);
                bool triangleInequality = (x + y > z) &&
                                         (x + z > y) &&
                                         (y + z > x);

                return positive && triangleInequality;
            }
        }

        public static void Run()
        {
            Console.WriteLine("ЗАВДАННЯ 1.2 - Клас Triangle (Лекція 3: Індексатори та Оператори)");
            Console.WriteLine(new string('-', 70));

            Console.WriteLine("\n1. Тестування індексатора:");
            Triangle t1 = new Triangle(3, 4, 5, 10);
            Console.WriteLine("Початковий стан: {0}", t1);
            Console.WriteLine("Індекс [0] (a): {0}", t1[0]);
            Console.WriteLine("Індекс [3] (color): {0}", t1[3]);

            Console.WriteLine("Спроба читання за індексом [5]:");
            int val = t1[5];
            Console.WriteLine("Повернуто значення: {0}", val);

            Console.WriteLine("Зміна через індексатор [0] = 4:");
            t1[0] = 4;
            Console.WriteLine("Новий стан: {0}", t1);

            Console.WriteLine("Спроба зміни [0] = 100 (має бути помилка валідності):");
            t1[0] = 100;

            Console.WriteLine("Спроба запису за індексом [10]:");
            t1[10] = 99;

            Console.WriteLine("\n2. Тестування операторів ++ та --:");
            Triangle t2 = new Triangle(3, 3, 3, 5);
            Console.WriteLine("До ++: {0}", t2);
            t2++;
            Console.WriteLine("Після ++: {0}", t2);
            t2--;
            Console.WriteLine("Після --: {0}", t2);

            Console.WriteLine("\n3. Тестування операторів true / false:");
            Triangle tValid = new Triangle(3, 4, 5, 1);
            if (tValid)
                Console.WriteLine("tValid є валідним трикутником");
            else
                Console.WriteLine("tValid НЕ є валідним трикутником");

            Triangle tInvalid = new Triangle(1, 1, 10, 0);
            if (tInvalid)
                Console.WriteLine("tInvalid є валідним (помилка!)");
            else
                Console.WriteLine("tInvalid НЕ є валідним трикутником");

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

            Console.WriteLine("Спроба перетворення некоректного рядка:");
            Triangle tBad = (Triangle)"invalid";
            Console.WriteLine("Результат: {0}", tBad);

            Console.WriteLine("\n" + new string('-', 70));
        }
    }
}
