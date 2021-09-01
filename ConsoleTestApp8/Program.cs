using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleTestApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Task 1
            //Console.WriteLine(Operate(2, 4, out int carpSonuc, out int cikarSonuc));
            //Console.WriteLine(carpSonuc);
            //Console.WriteLine(cikarSonuc);

            //int a = 10;
            //Console.WriteLine("a değeri: " + a);
            //GetData(ref a);
            //Console.WriteLine("sonraki a değeri: " + a);
            #endregion

            #region Task 2
            //string[] names = new string[5];
            //names[0] = "Test1";
            //names[1] = "Test2";
            //names[2] = "Test3";
            ////sayı sabit

            //ArrayList numbers = new ArrayList(); // default 4 tanedir, 4 ü aşınca 8 lik ,8 i aşınca 16 lık yer ayırır
            //numbers.Add("Önder");
            //numbers.Add(31);
            //numbers.Add(true);
            ////performans kötü

            //List<string> list = new List<string>();
            //list.Add("sad");
            //list.Add("sad");
            //list.Add("sad");
            ////en uygun

            #endregion

            #region Task 3
            //Console.WriteLine(Add(1, 2, 3, 4, 5));
            #endregion

            #region Task 4
            //int sayi = 20;
            //if (sayi.IsEven())
            //{
            //    Console.WriteLine("Number is even");
            //}
            #endregion

            #region Task 5
            //Rectangle r1 = new Rectangle { Width = 10, Heigth = 20 };
            //Rectangle r2 = new Rectangle { Width = 30, Heigth = 40 };

            //Rectangle r3 = r1 + r2;
            //Console.WriteLine(r3.ToString());

            //Rectangle r4 = r2 - r1;
            //Console.WriteLine(r4.ToString());
            #endregion
        }

        #region Task 3
        //public static int Add(params int[] values)
        //{
        //    int sum = 0;
        //    foreach (var item in values)
        //    {
        //        sum += item;
        //    }
        //    return sum;
        //}
        #endregion

        #region Task 1
        //public static void GetData(ref int b)
        //{
        //    b = b + 10;
        //}

        //public static int Operate(int a, int b, out int multiply, out int substract)
        //{
        //    multiply = a * b;
        //    substract = a - b;
        //    return a + b;
        //}
        #endregion
    }
    public class Shape
    {
        public void Connect()
        {
            Console.WriteLine("connected");// static polymorphizm // ana metodu çeşitlendirdik
        }
        public void Connect(string db)
        {
            Console.WriteLine("connected by db");// static polymorphizm // ana metodu çeşitlendirdik
        }

        public virtual void Write()
        {
            Console.WriteLine("shape is created");
        }
    }
    public class Triangle : Shape
    {
        public override void Write()
        {
            Console.WriteLine("triangle is created");//dynamic polymorphizm // ana metodu çeşitlendirdik
        }
    }
    public class Square : Shape
    {
        public override void Write()
        {
            Console.WriteLine("square is created");//dynamic polymorphizm // ana metodu çeşitlendirdik
        }
    }

    #region Task 5
    //public class Rectangle
    //{
    //    public int Width { get; set; }
    //    public int Heigth { get; set; }
    //    public override string ToString()
    //    {
    //        return Width + "," + Heigth;
    //    }
    //    public static Rectangle operator +(Rectangle r1, Rectangle r2)
    //    {
    //        return new Rectangle { Width = r1.Width + r2.Width, Heigth = r1.Heigth + r2.Heigth };
    //    }
    //    public static Rectangle operator -(Rectangle r1, Rectangle r2)
    //    {
    //        return new Rectangle { Width = r1.Width - r2.Width, Heigth = r1.Heigth - r2.Heigth };
    //    }
    //}
    #endregion

    #region Task 4

    //public static class ExtensionMethods
    //{
    //    public static bool IsEven(this int i)
    //    {
    //        return i % 2 == 0;
    //    }
    //    public static bool IsOdd(this int i)
    //    {
    //        return i % 2 == 1;
    //    }
    //}
    #endregion
}
