using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class ReflectionEx
    {
        //static void Main(string[] args)
        //{
        //    Employee e = new Employee();
        //    var type = e.GetType();
        //    var properties = type.GetProperties().ToList();
        //    properties.ForEach(p =>
        //    {
        //        Console.WriteLine(p.Name);
        //    });

        //    Employee e = new Employee();
        //    var type = e.GetType();
        //    var properties = type.GetProperties().ToList();
        //    properties.ForEach(p =>
        //    {
        //        if (p.Name == "EmployeeNo")
        //            p.SetValue(e, 123456);
        //        if (p.Name == "Name")
        //            p.SetValue(e, "onder");
        //        if (p.Name == "Surname")
        //            p.SetValue(e, "turhan");
        //        var value = p.GetValue(e).ToString();
        //        Console.WriteLine($"{p.Name} : {value}");
        //    });

        //    Employee e = new Employee();
        //    var type = e.GetType();
        //    var methods = type.GetMethods().ToList();
        //    methods.ForEach(m =>
        //    {
        //        Console.WriteLine(m.Name);
        //    });

        //    Employee e = new Employee();
        //    var type = e.GetType();
        //    var method = type.GetMethod("CreateEmail");
        //    var result = method.Invoke(e, new object[] { "onder", "turhan" });
        //    Console.WriteLine($"Email : {result}");

        //    Type type = Type.GetType("ConsoleTestApp.Employee");
        //    var e = Activator.CreateInstance(type);
        //    var method = type.GetMethod("CreateEmail");
        //    var result = method.Invoke(e, new object[] { "onder", "turhan" });
        //    Console.WriteLine($"Email : {result}");

        //    List<Employee> el = new List<Employee>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        el.Add(new Employee
        //        {
        //            EmployeeNo = i + 1,
        //            Name = "Name" + (i + 1),
        //            Surname = "Surname" + (i + 1)
        //        });
        //    }
        //    var table = ListToDT(el);
        //    foreach (DataRow row in table.Rows)
        //    {
        //        Console.WriteLine($"{row[0].ToString()} - {row[1].ToString()} - {row[2].ToString()}");
        //    }


        //    Console.ReadLine();

        //}

        //public static DataTable ListToDT<T>(List<T> data)
        //{
        //    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}
    }
    //class Employee
    //{
    //    public int EmployeeNo { get; set; }
    //    public string Name { get; set; }
    //    public string Surname { get; set; }

    //    public string CreateEmail(string name, string surname)
    //    {
    //        return $"{name}.{surname}@advanco.com";
    //    }
    //}
}
