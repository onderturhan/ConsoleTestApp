using Matematik;
using System;

namespace ConsoleTestApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person.WritePriceStatically();
            //Person.Price = 5;


            //Developer p = new Developer("Önder");
            //p.SayHello();
            //p.WritePrice();
            //p.GoAway();

            //TestClass tc = new TestClass();
            //Console.WriteLine(tc.deger3);

            //TestClass tc1 = new TestClass();
            //TestClass tc2 = new TestClass();
            //TestClass tc3 = new TestClass();
            //TestClass tc4 = new TestClass();

            //TestClass tc  = TestClass.Instance();

            Console.ReadLine();
        }
    }

    public class TestClass
    {
        private TestClass()
        {
        }
        public static TestClass Instance()
        {
            return new TestClass();
        }
    }

    //internal class TestClass
    //{
    //    public int Name { get; set; }
    //    public static string Department { get; set; }
    //    static TestClass()
    //    {
    //        //Department = "Bla";
    //        Console.WriteLine("Static Ctor çalıştı.");
    //    }
    //    public TestClass()
    //    {
    //        Console.WriteLine("Ctor çalıştı.");
    //    }
    //}


    //public class TestClass
    //{
    //    public bool deger1;
    //    public int deger2;
    //    public string deger3;
    //}

    //public abstract class BaseClass
    //{
    //    public string Name { get; set; }
    //    public abstract string GetName();
    //}
    //public class SubClass : BaseClass
    //{
    //    public /*sealed*/ override string GetName()
    //    {
    //        return Name.ToUpper();
    //    }
    //}
    //public class SubSubClass : SubClass
    //{
    //    public override string GetName()
    //    {
    //        return base.GetName();
    //    }
    //}

    //public class BaseClass
    //{
    //    public string Name { get; set; }
    //    public virtual string GetName()
    //    {
    //        return Name;
    //    }
    //}
    //public class SubClass : BaseClass
    //{
    //    public /*sealed*/ override string GetName()
    //    {
    //        return base.GetName().ToUpper();
    //    }
    //}
    //public class SubSubClass : SubClass
    //{
    //    public override string GetName()
    //    {
    //        return base.GetName();
    //    }
    //}


    //public sealed class BaseClass
    //{
    //}
    //public class SubClass : BaseClass
    //{
    //}


    #region Part2

    public abstract class Person
    {
        public Person(string name)
        {
            Console.WriteLine("Base ctor "+ name);
            Name = name;
        }

        public static int Price;
        public string Name { get; set; }
        public void SayHello()
        {
            Console.WriteLine("Hello " + Name);
        }
        public abstract void GoAway();
        public void WritePrice()
        {
            Console.WriteLine("Price :  " + Price);
        }
        public static void WritePriceStatically()
        {
            Console.WriteLine("Price Static:  " + Price);
        }
    }

    public class Developer : Person
    {
        public Developer(string name) : base(name)
        {
            Console.WriteLine("Child ctor " + name);
        }
        public string Level { get; set; }
        public override void GoAway()
        {
            Console.WriteLine("Go away Developer !!" + base.Name);
            WritePriceStatically();
        }
        
    }
    public class Lawyer : Person
    {
        public Lawyer() : base("Turgay")
        {
            Console.WriteLine("Child ctor " + Name);
        }
        public override void GoAway()
        {
            Console.WriteLine("Go away Lawyer !!" + base.Name);
            WritePriceStatically();
        }
    }

    #endregion
}
    //#region Part1
    //PublicClass i = new PublicClass();
    ////Ok i.PublicVeri

    ////Not InternalClass c = new InternalClass()

    //PublicBaseClass bc = new PublicBaseClass();
    ////Not bc.ProtectedVeri
    ////Not bc.ProtectedInternalVeri
    ////Not bc.PrivateProtectedVeri

    //PublicDerrivedClass dc = new PublicDerrivedClass();
    ////Not dc.ProtectedVeri
    ////Not dc.ProtectedInternalVeri
    ////Not dc.PrivateProtectedVeri

    //Test2Class t = new Test2Class();
    //t.durumlar = Test2Class.Durum.A;

    ////is ve as opertatörleri
    //Object[] dizin = { "ali", 5, null, 7, "null", true };
    //foreach (var item in dizin)
    //{
    //    if (item is string)
    //    {
    //        string veri = item as string;
    //        Console.WriteLine(veri);
    //    }
    //    //if (item is null)
    //    //{
    //    //    Console.WriteLine("asd");
    //    //}
    //}


    ////Ok  Daire.PI1
    ////Not Daire.PI2
    ////Not Daire.PI3

    //Daire d = new Daire();
    ////Not d.PI1
    ////Ok  d.PI2
    ////Ok  d.PI3


    //MyClass c1 = new MyClass();
    //MyStruct s1 = new MyStruct();
    //MyStruct s2 = new MyStruct("ada");



    //Universe u = new Universe();
    //Universe.Person p = new Universe.Person();
    //Universe.Person.Event.Name = "asd";
    //#endregion

    //#region Part2

    //I1 ii = new C1();
    //I2 iii = new C1();

    //ii.t(1, 2);
    //iii.c(1, 2);


    //Sekil sekil = new Sekil();
    //sekil.ciz();

    //IKare kare = new Sekil();
    //kare.ciz();

    //IUcgen ucgen = new Sekil();
    //ucgen.ciz();

    //#endregion
    //#region Part2
    //public class C1 : I1, I2
    //{
    //    public int c(int a, int b)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int t(int a, int b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //interface I1
    //{
    //    int t(int a, int b);
    //}
    //interface I2
    //{
    //    int c(int a, int b);
    //}

    //public class Sekil : IKare, IUcgen
    //{
    //    void IKare.ciz()
    //    {
    //        Console.WriteLine("Ikare çalıştı");
    //    }
    //    void IUcgen.ciz()
    //    {
    //        Console.WriteLine("IUcgen çalıştı");
    //    }
    //    public void ciz()
    //    {
    //        Console.WriteLine("Şekil çalıştı");
    //    }
    //}

    //interface IKare
    //{
    //    void ciz();
    //}
    //interface IUcgen
    //{
    //    void ciz();
    //}


    //#endregion

    //#region Part1
    //public class PublicDerrivedClass2 : PublicBaseClass
    //{
    //    void topla()
    //    {
    //        //Ok ProtectedVeri
    //        //Ok ProtectedInternalVeri
    //        //Not PrivateProtectedVeri

    //        PublicBaseClass bc = new PublicBaseClass();
    //        //Not bc.ProtectedVeri
    //        //Not bc.ProtectedInternalVeri
    //        //Not bc.PrivateProtectedVeri
    //    }
    //}
    //public class Daire
    //{
    //    public const double PI1 = 3.14;
    //    public readonly double PI2 = 3.14;
    //    public readonly double PI3;

    //    public Daire()
    //    {
    //        //Not PI1 = 3.15;
    //        //Ok  PI2 = 3.15;
    //        //Ok  PI3 = 3.16;

    //    }
    //}


    //internal class MyClass
    //{
    //    string deger;
    //    public MyClass()
    //    {
    //        deger = "5";
    //    }
    //}
    ////Ok class MyClass2 : MyClass
    ////   {
    ////   }

    //struct MyStruct
    //{

    //    string deger;
    //    public MyStruct(string _deger)
    //    {
    //        deger = _deger;
    //    }
    //}

    ////Not struct MyStruct2 : MyStruct
    ////    {
    ////    }


    //public class Universe
    //{
    //    public string Name { get; set; }
    //    public class Person
    //    {
    //        public string Name { get; set; }

    //        public static class Event
    //        {
    //            public static string Name { get; set; }
    //        }
    //    }
    //}


    //class MyClass : BaseClass1, /*BaseClass2,*/ IPerson, IDog
    //{

    //}

    //interface IPerson
    //{

    //}
    //interface IDog
    //{

    //}
    //class BaseClass1
    //{

    //}
    //class BaseClass2
    //{

    //}
    //#endregion
//#region Part1
////dynamic s;
////s = "1";
////s = 2;
////var d = new object();
////d = 1;
////d = "2";
////Console.WriteLine(typeof(int));
////Console.WriteLine(typeof(Int16));
////Console.WriteLine(typeof(Program));
//#endregion