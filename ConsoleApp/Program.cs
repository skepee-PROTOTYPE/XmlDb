using System;
using System.Collections.Generic;
using XmlDataBase;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating new database");
            Console.WriteLine(" ");
            Console.WriteLine("**************************************");

            XmlDb mydb = new XmlDb(@"D:\xmldbtest.xml", @"D:\xmldbtestlog.xml");

            //create tables from scratch
            Dictionary<string, Type> myfields = new Dictionary<string, Type>
            {
                { "Id", typeof(int) },
                { "Name", typeof(string) },
                { "Type", typeof(string) }
            };

            mydb.AddTable("Security", myfields);
            mydb.AddTable("Order", myfields);

            mydb.AddTable("Contact", new Dictionary<string, Type>
            {
                { "Id", typeof(int) },
                { "Name", typeof(string) },
                { "Surname", typeof(string) }
            });

            mydb.AddListItem("Order", new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
            {
                    {"Name", "Name1" },
                    {"Type", "1000" }
            },
                new Dictionary<string, string>
            {
                    {"Name", "Name2" },
                    {"Type", "2000" }
            },
                new Dictionary<string, string>
            {
                    {"Name", "Name3" },
                    {"Type", "3000" }
            }
            });

            //crud operations on tables 
            mydb.AddItem("Contact", new Dictionary<string, string>
            {
                {"Name", "Name1" },
                {"Surname", "Surname1"}
            });

            mydb.AddItem("Security", new Dictionary<string, string>
            {
                {"Name", "NameSecurity0" },
                {"Type", "TypeSecurity0"}
            });

            mydb.AddItem("Security", new Dictionary<string, string>
            {
                {"Name", "NameSecurity1" },
                {"Type", "TypeSecurity1"}
            });

            mydb.AddItem("Security", new Dictionary<string, string>
            {
                {"Name", "NameSecurity1" },
                {"Type", "TypeSecurity1"}
            });

            mydb.UpdateItem("Security", new Dictionary<string, string>
            {
                {"Name", "NameSecurity1"},
                {"Type", "TypeSecurity1"}
            },
             new Dictionary<string, string>
            {
                {"Name", "NameSecurity2"},
                {"Type", "TypeSecurity2"}
            });


            mydb.RemoveItem("Security", new Dictionary<string, string>
            {
                {"Name", "NameSecurity0"},
                {"Type", "TypeSecurity0"}
            });


            Console.WriteLine("Printing table data:");
            PrintList("Security", mydb.GetListItems("Security"));
            Console.WriteLine(" ");
            Console.WriteLine("Printing table data:");
            PrintList("Order", mydb.GetListItems("Order"));
            Console.WriteLine(" ");
            Console.WriteLine("Printing table data:");
            PrintList("Contact", mydb.GetListItems("Contact"));
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("completed. ");

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
