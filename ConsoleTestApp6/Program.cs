using System;
using System.Collections.Generic;

namespace ConsoleTestApp6
{
    #region Part 2
    //public delegate void FullNameDelegate(string name, string surname);
    #endregion

    //public delegate bool PromotionDelegate(Employee emp);
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Salary { get; set; }
        public int Experiment { get; set; }
        public string City { get; set; }
        public static void Promotion1(List<Employee> empList/*, PromotionDelegate proDelegate*/ ,Func<Employee,bool> proDelegate)
        {
            foreach (var emp in empList)
            {
                if (proDelegate(emp))
                {
                    Console.WriteLine(emp.Name + " " + emp.Surname);
                }
            }
        }
        public static void Promotion2(List<Employee> empList/*, PromotionDelegate proDelegate*/ , Predicate<Employee> proDelegate)
        {
            foreach (var emp in empList)
            {
                if (proDelegate(emp))
                {
                    Console.WriteLine(emp.Name + " " + emp.Surname);
                }
            }
        }

        //////public static void PromotionWithSalary(List<Employee> empList, int salary)
        //////{
        //////    foreach (var emp in empList)
        //////    {
        //////        if (emp.Salary >= salary)
        //////        {
        //////            Console.WriteLine(emp.Name + " " + emp.Surname);
        //////        }
        //////    }
        //////}
        //////public static void PromotionWithExperiment(List<Employee> empList, int experiment)
        //////{
        //////    foreach (var emp in empList)
        //////    {
        //////        if (emp.Experiment >= experiment)
        //////        {
        //////            Console.WriteLine(emp.Name + " " + emp.Surname);
        //////        }
        //////    }
        //////}
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Part 1
            //SubClass sc = new SubClass("Önder", "Turhan", 31);
            //Console.WriteLine($"{sc.Name} - { sc.Surname } - { sc.Age}");

            //TestClass tc = new TestClass("Ali", 12);
            //tc.ShowData();

            //Developer dev = new Developer("Önder", "Turhan", 31);
            //dev.IntroduceYourself();

            //TestClass c = new TestClass();
            ////c.CallTestVal(); // not accessible, due to be private
            //c.CallFromPublic("Test");
            #endregion

            #region Part 2
            //FullNameDelegate fnd1 = new FullNameDelegate(FullNameM1);
            //FullNameDelegate fnd2 = new FullNameDelegate(FullNameM2);
            //FullNameDelegate fnd3 = new FullNameDelegate(FullNameM3);
            //FullNameDelegate fndChain = fnd1 + fnd2 + fnd3;
            //fndChain("onder", "turhan");
            ////fnd1("onder", "turhan");
            ////fnd2("onder", "turhan");
            ////fnd3("onder", "turhan");
            #endregion

            #region Part 3
            List<Employee> elist = new List<Employee>
            {
                 new Employee { Name = "Önder", Surname = "Turhan" ,City="İstanbul", Experiment = 1, Salary = 2000 },
                 new Employee { Name = "Hasan", Surname = "Turhan" ,City="Ankara", Experiment = 2, Salary = 2300 },
                 new Employee { Name = "Ali", Surname = "Turhan"   ,City="İzmir", Experiment = 3, Salary = 4000 },
                 new Employee { Name = "Mehmet", Surname = "Turhan",City="Bursa", Experiment = 4, Salary = 5000 },
                 new Employee { Name = "Hüsam", Surname = "Turhan" ,City="Aydın", Experiment = 5, Salary = 6000 }
            };
            //////Employee.PromotionWithSalary(elist, 3000);
            //////Employee.PromotionWithExperiment(elist, 3);

            ////Employee.Promotion(elist, new PromotionDelegate(SalaryPromotion3000));
            ////Employee.Promotion(elist, new PromotionDelegate(ExperimentPromotion3));

            //Employee.Promotion(elist, i => i.Salary >= 5000);
            //Employee.Promotion(elist, i => i.Experiment >= 4);
            //Employee.Promotion(elist, i => i.City == "Ankara");

            Employee.Promotion1(elist, i => i.City == "Ankara");
            Employee.Promotion2(elist, i => i.City == "Ankara");

            #endregion

            Console.ReadLine();
        }

        ////public static bool SalaryPromotion3000(Employee emp)
        ////{
        ////    if (emp.Salary >= 3000)
        ////    {
        ////        return true;
        ////    }
        ////    return false;
        ////}
        ////public static bool ExperimentPromotion3(Employee emp)
        ////{
        ////    if (emp.Experiment >= 3)
        ////    {
        ////        return true;
        ////    }
        ////    return false;
        ////}


        #region Part 2
        //public static void FullNameM1(string name,string surname)
        //{
        //    Console.WriteLine(name + " " + surname);
        //}
        //public static void FullNameM2(string name, string surname)
        //{
        //    Console.WriteLine(name.ToUpper() + " " + surname.ToUpper());
        //}
        //public static void FullNameM3(string name, string surname)
        //{
        //    Console.WriteLine(surname.ToUpper() + " " + name.ToUpper());
        //}
        #endregion
    }


    #region Part 1
    //partial class TestClass
    //{
    //    partial void CallTestVal(string val);
    //}
    //partial class TestClass
    //{
    //    public void CallFromPublic(string val)
    //    {
    //        CallTestVal(val);
    //    }
    //    partial void CallTestVal(string val)
    //    {
    //        Console.WriteLine(val);
    //    }
    //}

    //class Developer : IPerson
    //{
    //    public Developer(string name, string surname, int age)
    //    {
    //        Name = name;
    //        Surname= surname;
    //        Age = age;
    //    }
    //    public string Name { get; set; }
    //    public string Surname { get; set; }
    //    public int Age { get; set; }
    //    public void IntroduceYourself()
    //    {
    //        Console.WriteLine($"{Name} - { Surname } - { Age}");
    //    }
    //}
    //partial interface IPerson
    //{
    //    public string Name { get; set; }
    //    public string Surname { get; set; }
    //    public int Age { get; set; }
    //}
    //partial interface IPerson
    //{
    //    void IntroduceYourself();
    //}


    //partial class TestClass :ITest1
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //    public int callVariable1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public void callTest1()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //partial class TestClass : ITest2
    //{
    //    public TestClass()
    //    {
    //    }
    //    public TestClass(string name, int age)
    //    {
    //        Name = name;
    //        Age = age;
    //    }
    //    public int callVariable2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public void callTest2()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //partial class TestClass
    //{
    //    public void ShowData()
    //    {
    //        Console.WriteLine(Name + " - " + Age);
    //    }
    //}
    //interface ITest1
    //{
    //    public int callVariable1 { get; set; }
    //    void callTest1();
    //}
    //interface ITest2
    //{
    //    public int callVariable2 { get; set; }
    //    void callTest2();
    //}


    //internal class BaseClass
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //    //public static int StaticValue { get; set; }
    //    public BaseClass(string name, int age)
    //    {
    //        Console.WriteLine("Base çalışır");
    //        Name = name;
    //        Age = age;
    //    }
    //    //static BaseClass(int value) // paramete gelmez
    //    //{
    //    //}
    //}
    //internal class SubClass: BaseClass
    //{
    //    public string Surname { get; set; }
    //    public SubClass(string name, string surname, int age) : base(name, age)
    //    {
    //        Console.WriteLine("Sub çalışır");
    //        Surname = surname;
    //    }
    //}
    #endregion

}
