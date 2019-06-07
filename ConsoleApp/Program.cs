using System;
using System.Collections.Generic;
using System.Linq;
using XmlDataBase;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDb mydb = new XmlDb("mydb", @"D:\xmldb2.xml", "myDataset", "myDatasetnamespace");

            //read existing file
            //mydb.LoadXmlDb();
            //PrintList("Security", mydb.GetListItems("Security"));
            //PrintList("Order", mydb.GetListItems("Order"));
            //PrintList("Contact", mydb.GetListItems("Contact"));




            // add log table
            Dictionary<string, Type> mylogfields = new Dictionary<string, Type>
            {
                { "Id", typeof(int) },
                { "Date", typeof(DateTime) },
                { "Log", typeof(string) }
            };
            mydb.AddLogTable(@"D:\xmldb2.log", "log", mylogfields);


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
                {"Name", "Pippo" },
                {"Surname", "Paperino"}
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

           
            PrintList("Security", mydb.GetListItems("Security"));
            PrintList("Order", mydb.GetListItems("Order"));
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
