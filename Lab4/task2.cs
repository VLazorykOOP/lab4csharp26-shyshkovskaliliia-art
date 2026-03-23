using System;
using System.Text;

#nullable disable

namespace Lab4
{
    internal class task2
    {
        class VectorUInt
        {

            public VectorUInt() : this(1, 0) { }
            public VectorUInt(uint size) : this(size, 0) { }
            public VectorUInt(uint size, uint initValue)
            {
                this.size = (size > 0) ? size : 1;
                IntArray = new uint[this.size];
                SetAll(initValue);
                codeError = 0;
                num_vec++;
            }

            ~VectorUInt()
            {
                Console.WriteLine("Деструктор: видалено вектор розміром {0}", size);
                num_vec--;
            }

            public uint Size
            {
                get { return size; }
            }

            public int CodeError
            {
                get { return codeError; }
                set { codeError = value; }
            }

            public uint this[uint index]
            {
                get
                {
                    if (index < size)
                    {
                        codeError = 0;
                        return IntArray[index];
                    }
                    else
                    {
                        codeError = 1;
                        return 0;
                    }
                }
                set
                {
                    if (index < size)
                    {
                        codeError = 0;
                        IntArray[index] = value;
                    }
                    else
                    {
                        codeError = 1;

                    }
                }
            }

            public void Input()
            {
                Console.WriteLine("Введення елементів вектора розміром {0}:", size);
                for (uint i = 0; i < size; i++)
                {
                    Console.Write("  [{0}] = ", i);
                    string input = Console.ReadLine();
                    uint value;
                    if (uint.TryParse(input, out value))
                    {
                        IntArray[i] = value;
                    }
                    else
                    {
                        Console.WriteLine("Помилка вводу! Встановлено 0");
                        IntArray[i] = 0;
                        codeError = 2;
                    }
                }
            }
            public void Output()
            {
                Console.Write("[");
                for (uint i = 0; i < size; i++)
                {
                    Console.Write(IntArray[i]);
                    if (i < size - 1) Console.Write(", ");
                }
                Console.WriteLine("]");
            }

            // Присвоїти елементам масиву деяке значення (параметр)
            public void SetAll(uint value)
            {
                for (uint i = 0; i < size; i++)
                {
                    IntArray[i] = value;
                }
                codeError = 0;
            }

            public static uint GetVectorCount()
            {
                return num_vec;
            }

            // Перевизначення ToString (стиль лекції - string.Format)
            public override string ToString()
            {
                StringBuilder buf = new StringBuilder();
                buf.AppendFormat("VectorUInt[{0}]: [", size);
                for (uint i = 0; i < size; i++)
                {
                    buf.Append(IntArray[i]);
                    if (i < size - 1) buf.Append(", ");
                }
                buf.Append("]");
                return buf.ToString();
            }

            // ++ : одночасно збільшує значення елементів масиву на 1
            public static VectorUInt operator ++(VectorUInt v)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] + 1;
                }
                return result;
            }

            // -- : одночасно зменшує значення елементів масиву на 1
            public static VectorUInt operator --(VectorUInt v)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = (v.IntArray[i] > 0) ? v.IntArray[i] - 1 : 0;
                }
                return result;
            }

            // true : true, якщо size != 0 АБО всі елементи != 0
            public static bool operator true(VectorUInt v)
            {
                if (v.size == 0) return false;
                for (uint i = 0; i < v.size; i++)
                {
                    if (v.IntArray[i] != 0) return true;
                }
                return false;
            }

            // false : true, якщо size == 0 І всі елементи == 0
            public static bool operator false(VectorUInt v)
            {
                if (v.size == 0) return true;
                for (uint i = 0; i < v.size; i++)
                {
                    if (v.IntArray[i] != 0) return false;
                }
                return true;
            }

            // ! : повертає true, якщо size == 0
            public static bool operator !(VectorUInt v)
            {
                return (v.size == 0);
            }

            // ~ : побітове заперечення для всіх елементів масиву
            public static VectorUInt operator ~(VectorUInt v)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = ~v.IntArray[i];
                }
                return result;
            }
            private static VectorUInt ApplyBinary(VectorUInt v1, VectorUInt v2,
                Func<uint, uint, uint> operation)
            {
                uint maxSize = (v1.size > v2.size) ? v1.size : v2.size;
                uint minSize = (v1.size < v2.size) ? v1.size : v2.size;

                VectorUInt result = new VectorUInt(maxSize);


                for (uint i = 0; i < minSize; i++)
                {
                    result.IntArray[i] = operation(v1.IntArray[i], v2.IntArray[i]);
                }

                if (v1.size > v2.size)
                {
                    for (uint i = minSize; i < maxSize; i++)
                    {
                        result.IntArray[i] = v1.IntArray[i];
                    }
                }
                else if (v2.size > v1.size)
                {
                    for (uint i = minSize; i < maxSize; i++)
                    {
                        result.IntArray[i] = v2.IntArray[i];
                    }
                }

                return result;
            }

            private static bool CompareVectors(VectorUInt v1, VectorUInt v2,
                Func<uint, uint, bool> comparison)
            {
                uint minSize = (v1.size < v2.size) ? v1.size : v2.size;

                for (uint i = 0; i < minSize; i++)
                {
                    if (!comparison(v1.IntArray[i], v2.IntArray[i]))
                        return false;
                }

                if (v1.size > v2.size)
                {
                    for (uint i = minSize; i < v1.size; i++)
                    {
                        if (!comparison(v1.IntArray[i], 0))
                            return false;
                    }
                }
                else if (v2.size > v1.size)
                {
                    return false;
                }

                return true;
            }

            // + для двох векторів
            public static VectorUInt operator +(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => a + b);

            // + для вектора і скаляра типу int
            public static VectorUInt operator +(VectorUInt v, int scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    long temp = (long)v.IntArray[i] + scalar;
                    result.IntArray[i] = (uint)Math.Max(0, Math.Min(uint.MaxValue, temp));
                }
                return result;
            }

            // + для скаляра і вектора (комутативність)
            public static VectorUInt operator +(int scalar, VectorUInt v) => v + scalar;

            // - для двох векторів
            public static VectorUInt operator -(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => (a >= b) ? a - b : 0);

            // - для вектора і скаляра типу int
            public static VectorUInt operator -(VectorUInt v, int scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    long temp = (long)v.IntArray[i] - scalar;
                    result.IntArray[i] = (uint)Math.Max(0, temp);
                }
                return result;
            }

            // * для двох векторів
            public static VectorUInt operator *(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) =>
                {
                    ulong temp = (ulong)a * b;
                    return (temp > uint.MaxValue) ? uint.MaxValue : (uint)temp;
                });

            // * для вектора і скаляра типу int
            public static VectorUInt operator *(VectorUInt v, int scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                uint absScalar = (uint)Math.Abs(scalar);
                for (uint i = 0; i < v.size; i++)
                {
                    ulong temp = (ulong)v.IntArray[i] * absScalar;
                    result.IntArray[i] = (temp > uint.MaxValue) ? uint.MaxValue : (uint)temp;
                }
                return result;
            }

            // / для двох векторів
            public static VectorUInt operator /(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => (b != 0) ? a / b : 0);

            // / для вектора і скаляра типу short
            public static VectorUInt operator /(VectorUInt v, short scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                if (scalar == 0)
                {
                    result.codeError = 3;
                    return result;
                }
                uint absScalar = (uint)Math.Abs(scalar);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] / absScalar;
                }
                return result;
            }

            // % для двох векторів
            public static VectorUInt operator %(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => (b != 0) ? a % b : 0);

            // % для вектора і скаляра типу short
            public static VectorUInt operator %(VectorUInt v, short scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                if (scalar == 0)
                {
                    result.codeError = 3;
                    return result;
                }
                uint absScalar = (uint)Math.Abs(scalar);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] % absScalar;
                }
                return result;
            }

            // | (побітове АБО) для двох векторів
            public static VectorUInt operator |(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => a | b);

            // | для вектора і скаляра типу uint
            public static VectorUInt operator |(VectorUInt v, uint scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] | scalar;
                }
                return result;
            }

            // ^ (побітове XOR) для двох векторів
            public static VectorUInt operator ^(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => a ^ b);

            // ^ для вектора і скаляра типу uint
            public static VectorUInt operator ^(VectorUInt v, uint scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] ^ scalar;
                }
                return result;
            }

            // & (побітове І) для двох векторів
            public static VectorUInt operator &(VectorUInt v1, VectorUInt v2)
            {
                uint maxSize = (v1.size > v2.size) ? v1.size : v2.size;
                uint minSize = (v1.size < v2.size) ? v1.size : v2.size;

                VectorUInt result = new VectorUInt(maxSize);

                for (uint i = 0; i < minSize; i++)
                {
                    result.IntArray[i] = v1.IntArray[i] & v2.IntArray[i];
                }

                return result;
            }

            // & для вектора і скаляра типу uint
            public static VectorUInt operator &(VectorUInt v, uint scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] & scalar;
                }
                return result;
            }

            // >> (зсув вправо) для двох векторів
            public static VectorUInt operator >>(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => a >> (int)(b % 32));

            // >> для вектора і скаляра типу uint
            public static VectorUInt operator >>(VectorUInt v, uint scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                uint shift = scalar % 32;
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] >> (int)shift;
                }
                return result;
            }

            // << (зсув вліво) для двох векторів
            public static VectorUInt operator <<(VectorUInt v1, VectorUInt v2) =>
                ApplyBinary(v1, v2, (a, b) => a << (int)(b % 32));

            // << для вектора і скаляра типу uint
            public static VectorUInt operator <<(VectorUInt v, uint scalar)
            {
                VectorUInt result = new VectorUInt(v.size);
                uint shift = scalar % 32;
                for (uint i = 0; i < v.size; i++)
                {
                    result.IntArray[i] = v.IntArray[i] << (int)shift;
                }
                return result;
            }


            // == (рівність): true, якщо ВСІ пари елементів рівні
            public static bool operator ==(VectorUInt v1, VectorUInt v2) =>
                CompareVectors(v1, v2, (a, b) => a == b);

            // != (нерівність)
            public static bool operator !=(VectorUInt v1, VectorUInt v2) =>
                !(v1 == v2);

            // > (більше): true, якщо ВСІ пари елементів задовольняють умову
            public static bool operator >(VectorUInt v1, VectorUInt v2) =>
                CompareVectors(v1, v2, (a, b) => a > b);

            // >= (більше або рівне)
            public static bool operator >=(VectorUInt v1, VectorUInt v2) =>
                CompareVectors(v1, v2, (a, b) => a >= b);

            // < (менше)
            public static bool operator <(VectorUInt v1, VectorUInt v2) =>
                CompareVectors(v1, v2, (a, b) => a < b);

            // <= (менше або рівне)
            public static bool operator <=(VectorUInt v1, VectorUInt v2) =>
                CompareVectors(v1, v2, (a, b) => a <= b);
            public override bool Equals(object obj)
            {
                if (obj is VectorUInt v)
                {
                    return this == v;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return size.GetHashCode();
            }

            protected uint[] IntArray;
            protected uint size;
            protected int codeError;
            protected static uint num_vec = 0;
        }

        public static void Run()
        {
            Console.WriteLine("ЗАВДАННЯ 2.2 - Клас VectorUInt");
            Console.WriteLine(new string('-', 60));

            // --- 1. Конструктори ---
            Console.WriteLine("\n1. Конструктори:");
            VectorUInt v1 = new VectorUInt();
            VectorUInt v2 = new VectorUInt(5);
            VectorUInt v3 = new VectorUInt(4, 10);

            Console.WriteLine("v1 (за замовчуванням):"); v1.Output();
            Console.WriteLine("v2 (розмір 5):"); v2.Output();
            Console.WriteLine("v3 (розмір 4, знач. 10):"); v3.Output();

            // --- 2. Властивості та індексатор ---
            Console.WriteLine("\n2. Властивості та індексатор:");
            Console.WriteLine("Розмір v3: {0}", v3.Size);
            Console.WriteLine("Елемент v3[2]: {0}", v3[2]);

            Console.WriteLine("Зміна v3[1] = 25:");
            v3[1] = 25;
            v3.Output();

            Console.WriteLine("Спроба доступу за межами (v3[10]):");
            uint val = v3[10];
            Console.WriteLine("Повернуто: {0}, codeError: {1}", val, v3.CodeError);

            // --- 3. Методи ---
            Console.WriteLine("\n3. Методи:");
            Console.WriteLine("SetAll(7) для v2:");
            v2.SetAll(7);
            v2.Output();
            Console.WriteLine("Кількість векторів: {0}", VectorUInt.GetVectorCount());

            // --- 4. Унарні оператори ---
            Console.WriteLine("\n4. Унарні оператори ++, --, ~:");
            VectorUInt v4 = new VectorUInt(3, 5);
            Console.WriteLine("v4 до ++: {0}", v4);
            v4 = ++v4;
            Console.WriteLine("v4 після ++: {0}", v4);
            v4 = --v4;
            Console.WriteLine("v4 після --: {0}", v4);
            Console.WriteLine("~v4: {0}", ~v4);

            // --- 5. Оператори true/false, ! ---
            Console.WriteLine("\n5. Оператори true/false, !:");
            VectorUInt vt = new VectorUInt(3, 5);
            VectorUInt vf = new VectorUInt(3, 0);
            VectorUInt ve = new VectorUInt(0, 0);

            Console.WriteLine("if(vt {{5,5,5}}): {0}", vt ? "true" : "false");
            Console.WriteLine("if(vf {{0,0,0}}): {0}", vf ? "true" : "false");
            Console.WriteLine("if(ve {{size=0}}): {0}", ve ? "true" : "false");
            Console.WriteLine("!(ve): {0}", !ve ? "true" : "false");  // ✅ ВИПРАВЛЕНО

            // --- 6. Арифметичні оператори ---
            Console.WriteLine("\n6. Арифметичні оператори:");
            VectorUInt va = new VectorUInt(4, 10);
            VectorUInt vb = new VectorUInt(3, 3);
            Console.WriteLine("va: {0}", va);
            Console.WriteLine("vb: {0}", vb);

            Console.WriteLine("va + vb: {0}", va + vb);
            Console.WriteLine("va - vb: {0}", va - vb);
            Console.WriteLine("va * vb: {0}", va * vb);
            Console.WriteLine("va / vb: {0}", va / vb);
            Console.WriteLine("va % vb: {0}", va % vb);

            Console.WriteLine("\nОперації зі скаляром:");
            Console.WriteLine("va + 5: {0}", va + 5);
            Console.WriteLine("va - 3: {0}", va - 3);
            Console.WriteLine("va * 2: {0}", va * 2);
            Console.WriteLine("va / (short)4: {0}", va / (short)4);
            Console.WriteLine("va % (short)3: {0}", va % (short)3);

            // --- 7. Побітові оператори ---
            Console.WriteLine("\n7. Побітові оператори:");
            VectorUInt vx = new VectorUInt(3, 12);
            VectorUInt vy = new VectorUInt(3, 5);
            Console.WriteLine("vx (12): {0}", vx);
            Console.WriteLine("vy (5):  {0}", vy);
            Console.WriteLine("vx | vy: {0}", vx | vy);
            Console.WriteLine("vx ^ vy: {0}", vx ^ vy);
            Console.WriteLine("vx & vy: {0}", vx & vy);
            Console.WriteLine("vx >> 2: {0}", vx >> 2);
            Console.WriteLine("vx << 1: {0}", vx << 1);

            Console.WriteLine("\n8. Оператори порівняння:");
            VectorUInt ve1 = new VectorUInt(3, 10);
            VectorUInt ve2 = new VectorUInt(3, 10);
            VectorUInt vgt = new VectorUInt(3, 15);
            VectorUInt vlt = new VectorUInt(3, 5);

            Console.WriteLine("ve1 == ve2: {0}", (ve1 == ve2).ToString().ToLower());
            Console.WriteLine("ve1 != vlt: {0}", (ve1 != vlt).ToString().ToLower());
            Console.WriteLine("vgt > ve1:  {0}", (vgt > ve1).ToString().ToLower());
            Console.WriteLine("ve1 >= ve2: {0}", (ve1 >= ve2).ToString().ToLower());
            Console.WriteLine("vlt < ve1:  {0}", (vlt < ve1).ToString().ToLower());
            Console.WriteLine("vlt <= ve1: {0}", (vlt <= ve1).ToString().ToLower());

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("Тестування завершено.");
            Console.WriteLine("Кількість векторів: {0}", VectorUInt.GetVectorCount());
        }
    }
}