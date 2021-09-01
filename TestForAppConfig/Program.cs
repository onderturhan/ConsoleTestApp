using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestForAppConfig
{
    class Program
    {

        static void Main(string[] args)
        {

            //string ConfigFilePath =
            //                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            //XmlReaderSettings _configsettings = new XmlReaderSettings();
            //_configsettings.IgnoreComments = true;

            //XmlReader _configreader = XmlReader.Create(ConfigFilePath, _configsettings);
            //XmlDocument doc_config = new XmlDocument();
            //doc_config.Load(_configreader);
            //_configreader.Close();

            //foreach (XmlNode RootName in doc_config.DocumentElement.ChildNodes)
            //{
            //    if (RootName.LocalName == "applicationSettings") //ADVANCO.ARC.AdminPL.Properties.Settings
            //    {
            //        if (RootName.HasChildNodes)
            //        {
            //            foreach (XmlNode _childs in RootName.ChildNodes)
            //            {
            //                if (_childs.HasChildNodes)
            //                {
            //                    foreach (XmlNode _child in _childs.ChildNodes)
            //                    {
            //                        if (_child.Attributes["name"].Value == "CockpitThemeSetting")
            //                        {
            //                            _child.ChildNodes[0].InnerText = "Theme2";
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //doc_config.Save(ConfigFilePath);


            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<book>" +
            //            "  <title>Oberon's Legacy</title>" +
            //            "  <price>5.95</price>" +
            //            "</book>");
            //// Create a new element node.
            //XmlNode newElem = doc.CreateNode("element", "pages", "");
            //newElem.InnerText = "290";
            //Console.WriteLine("Add the new element to the document...");
            //XmlElement root = doc.DocumentElement;
            //root.AppendChild(newElem);
            //Console.WriteLine("Display the modified XML document...");
            //Console.WriteLine(doc.OuterXml);


            //DataSet ds = new DataSet();
            //DataTable customerTable = GetCustomers();
            //DataTable orderTable = GetOrders();

            //ds.Tables.Add(customerTable);
            //ds.Tables.Add(orderTable);
            //ds.Relations.Add("CustomerOrder",
            //    new DataColumn[] { customerTable.Columns[0] },
            //    new DataColumn[] { orderTable.Columns[1] }, true);

            //System.IO.StringWriter writer = new System.IO.StringWriter();
            //customerTable.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            //PrintOutput(writer, "Customer table, without hierarchy");

            //writer = new System.IO.StringWriter();
            //customerTable.WriteXml(writer, XmlWriteMode.WriteSchema, true);
            //PrintOutput(writer, "Customer table, with hierarchy");


            ExportResourceFromDB();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void ExportResourceFromDB()
        {
            string ExportResourceAsTextBoxText = "TestResxFile.resx"; 
            using (ResXResourceWriter resx = new ResXResourceWriter(ExportResourceAsTextBoxText))
            {
                resx.AddResource("TestKey1", "Test Value1");
                resx.AddResource("TestKey2", "Test Value2");
                resx.AddResource("TestKey3", "Test Value3");
                resx.AddResource("TestKey4", "Test Value4");
            }
        }

        private static DataTable GetCustomers()
        {
            // Create sample Customers table, in order
            // to demonstrate the behavior of the DataTableReader.
            DataTable table = new DataTable();

            // Create two columns, ID and Name.
            DataColumn idColumn = table.Columns.Add("ID", typeof(System.Int32));
            table.Columns.Add("Name", typeof(System.String));

            // Set the ID column as the primary key column.
            table.PrimaryKey = new DataColumn[] { idColumn };

            table.Rows.Add(new object[] { 1, "Mary" });
            table.Rows.Add(new object[] { 2, "Andy" });
            table.Rows.Add(new object[] { 3, "Peter" });
            table.Rows.Add(new object[] { 4, "Russ" });
            table.AcceptChanges();
            return table;
        }

        private static DataTable GetOrders()
        {
            // Create sample Customers table, in order
            // to demonstrate the behavior of the DataTableReader.
            DataTable table = new DataTable();

            // Create three columns; OrderID, CustomerID, and OrderDate.
            table.Columns.Add(new DataColumn("OrderID", typeof(System.Int32)));
            table.Columns.Add(new DataColumn("CustomerID", typeof(System.Int32)));
            table.Columns.Add(new DataColumn("OrderDate", typeof(System.DateTime)));

            // Set the OrderID column as the primary key column.
            table.PrimaryKey = new DataColumn[] { table.Columns[0] };

            table.Rows.Add(new object[] { 1, 1, "12/2/2003" });
            table.Rows.Add(new object[] { 2, 1, "1/3/2004" });
            table.Rows.Add(new object[] { 3, 2, "11/13/2004" });
            table.Rows.Add(new object[] { 4, 3, "5/16/2004" });
            table.Rows.Add(new object[] { 5, 3, "5/22/2004" });
            table.Rows.Add(new object[] { 6, 4, "6/15/2004" });
            table.AcceptChanges();
            return table;
        }

        private static void PrintOutput(System.IO.TextWriter writer, string caption)
        {
            Console.WriteLine("==============================");
            Console.WriteLine(caption);
            Console.WriteLine("==============================");
            Console.WriteLine(writer.ToString());
        }

        //static void Main(string[] args)
        //{
        //    SetSetting("HelloOnder", "Hello Önder");
        //    Console.WriteLine(GetSetting("HelloOnder"));
        //    Console.ReadLine();
        //}
        //private static string GetSetting(string key)
        //{
        //    return ConfigurationManager.AppSettings[key];
        //}
        //private static void SetSetting(string key, string value)
        //{
        //    Configuration configuration =
        //        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    configuration.AppSettings.Settings[key].Value = value;
        //    configuration.Save(ConfigurationSaveMode.Full, true);
        //    ConfigurationManager.RefreshSection("applicationSettings");
        //}
    }
}
