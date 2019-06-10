using System;
using System.Collections.Generic;
using XmlDataBase;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDb mydb = new XmlDb(@"D:\xmldb.xml", string.Empty);

            Console.WriteLine("Reading existing database");
            Console.WriteLine(" ");
            Console.WriteLine("**************************************");
            mydb.LoadXmlDb();
            Console.WriteLine("Printing table data:");
            PrintList("Security", mydb.GetListItems("Security"));
            Console.WriteLine(" ");
            Console.WriteLine("Printing table data:");
            PrintList("Order", mydb.GetListItems("Order"));
            Console.WriteLine(" ");
            Console.WriteLine("Printing table data:");
            PrintList("Contact", mydb.GetListItems("Contact"));

            Console.ReadLine();
        }

        static void PrintList(string tableName, List<Dictionary<string, string>> mylist)
        {
            Console.WriteLine("**************************************");
            Console.WriteLine(tableName);
            Console.WriteLine("**************************************");

            foreach (Dictionary<string, string> item in mylist)
            {
                foreach (var x in item)
                {
                    Console.WriteLine(x.Key + ": " + x.Value);
                }
                Console.WriteLine("-------------------------------------");
            }
            Console.WriteLine(" ");
        }

    }
}
