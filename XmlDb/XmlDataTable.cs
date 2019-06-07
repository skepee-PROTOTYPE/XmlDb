﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace XmlDataBase
{
    public class XmlDataTable
    {
        internal DataTable Dt { get; set; }

        public string DataTableName { get; set; }

        internal Dictionary<string, Type> Properties { get; set; }

        public XmlDataTable()
        {
            Properties = new Dictionary<string, Type>();
        }

        public void CreateTable(string name, Dictionary<string, Type> Fields)
        {
            DataTableName = name;
            Properties = Fields;

            Dt = new DataTable(DataTableName);

            foreach (var item in Properties)
            {
                Dt.Columns.Add(item.Key, item.Value);

                if (item.Key.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                {
                    Dt.PrimaryKey = new DataColumn[] { Dt.Columns[item.Key] };
                    Dt.Columns[item.Key].AutoIncrement = true;
                }
            }
        }

        private string SearchString(Dictionary<string, string> myCurrentData)
        {
            string strSearch = string.Empty;
            foreach (var item in myCurrentData)
            {
                if (this.Properties[item.Key].ToString().Equals("System.String", StringComparison.InvariantCultureIgnoreCase))
                    strSearch += item.Key + " = '" + item.Value + "'";
                else
                    strSearch += item.Key + " = " + item.Value;

                strSearch += " AND ";
            }
            strSearch = strSearch.Substring(0, strSearch.Length - 5);

            return strSearch;
        }

        public void Update(Dictionary<string, string> myCurrentData, Dictionary<string, string> myNewData)
        {
            string strSearch = SearchString(myCurrentData);

            DataRow[] drUpdate = Dt.Select(strSearch);

            if (drUpdate != null)
            {
                foreach (DataRow dr in drUpdate)
                {
                    foreach (var item in myNewData)
                    {
                        dr[item.Key] = item.Value;
                    }
                }
            }
        }

        public void Remove(Dictionary<string, string> myData)
        {
            string strSearch = SearchString(myData);

            DataRow[] drUpdate = Dt.Select(strSearch);

            if (drUpdate != null)
            {
                foreach (DataRow dr in drUpdate)
                {
                    dr.Delete();
                }
            }
        }

        public void Add(Dictionary<string, string> myData)
        {
            DataRow drAdd = Dt.NewRow();

            foreach (var item in myData)
            {
                drAdd[item.Key] = item.Value;
            }
            Dt.Rows.Add(drAdd);
        }



        public List<Dictionary<string, string>> List()
        {
            var l = new List<Dictionary<string, string>>();

            int i= 0;
            foreach (DataRow dr in Dt.Rows)
            {
                var m = new Dictionary<string, string>();

                foreach (var item in dr.ItemArray)
                {
                    m.Add(Properties.ElementAt(i).Key, item.ToString());
                    i++;
                }
                l.Add(m);
                i = 0;
            }

            return l;
        }


    }
}
