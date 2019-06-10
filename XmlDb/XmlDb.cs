using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace XmlDataBase
{
    public class XmlDb
    {
        private string DataBasePathFile { get; set; }
        private DataSet Ds { get; set; }
        private List<XmlTable> ListDataObjects { get; set; }
        private XmlLog LogDataObject { get; set; }

        private void AddLogTable(string logFile)
        {
            Dictionary<string, Type> mylogfields = new Dictionary<string, Type>
            {
                { "Id", typeof(int) },
                { "Date", typeof(DateTime) },
                { "Log", typeof(string) }
            };

            LogDataObject = new XmlLog(logFile, "log", mylogfields);
        }

        private void ClearData()
        {
            ListDataObjects.Clear();
            Ds.Tables.Clear();
        }

        private void AddLogItem(Dictionary<string, string> myData, string operation, string tableName)
        {
            LogDataObject.Add(myData, operation, tableName);
        }

        private XmlTable GetItem(string tableName)
        {
            var xmldt = ListDataObjects.ToList().FirstOrDefault(x => x.TableName.Equals(tableName, StringComparison.InvariantCultureIgnoreCase));
            return xmldt;
        }

        public void LoadXmlDb()
        {
            this.ClearData();
            if (File.Exists(DataBasePathFile))
            {
                Ds.ReadXml(DataBasePathFile);

                foreach (DataTable dt in Ds.Tables)
                {
                    XmlTable myXmlData = new XmlTable
                    {
                        TableName = dt.TableName,
                        Dt = dt
                    };

                    foreach (DataColumn dc in dt.Columns)
                    {
                        myXmlData.Properties.Add(dc.ColumnName, dc.DataType);
                    }

                    ListDataObjects.Add(myXmlData);
                }
            }
        }

        public void UpdateItem(string tableName, Dictionary<string, string> myCurrentData, Dictionary<string, string> myNewData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                xmldt.Update(myCurrentData, myNewData);
                Ds.AcceptChanges();
                Ds.WriteXml(DataBasePathFile);
            }

            if (LogDataObject!=null)
                AddLogItem(myNewData, "update",tableName);
        }

        public void RemoveItem(string tableName, Dictionary<string, string> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                xmldt.Remove(myData);
                Ds.AcceptChanges();
                Ds.WriteXml(DataBasePathFile);
            }

            if (LogDataObject != null)
                AddLogItem(myData, "remove", tableName);
        }

        public void AddItem(string tableName,Dictionary<string, string> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                xmldt.Add(myData);
                Ds.AcceptChanges();
                Ds.WriteXml(DataBasePathFile);
            }

            if (LogDataObject != null)
                AddLogItem(myData, "add", tableName);
        }

        public void AddListItem(string tableName, List<Dictionary<string, string>> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                foreach (var item in myData)
                {
                    AddItem(tableName, item);

                    if (LogDataObject != null)
                        AddLogItem(item, "add", tableName);
                }
                Ds.AcceptChanges();
                Ds.WriteXml(DataBasePathFile);
            }
        }

        public void AddTable(string tableName, Dictionary<string, Type> myData)
        {
            XmlTable xmldt = new XmlTable();
            xmldt.CreateTable(tableName, myData);

            if (xmldt != null)
            {
                ListDataObjects.Add(xmldt);
                Ds.Tables.Add(xmldt.Dt);
            }
        }

        public XmlDb(string path, string logPathFile)
        {
            this.DataBasePathFile = path;
            this.ListDataObjects = new List<XmlTable>();
            this.Ds = new DataSet();

            if (!string.IsNullOrEmpty(logPathFile))
                this.AddLogTable(logPathFile);
        }

        public List<Dictionary<string, string>> GetListItems(string tableName)
        {
            var xmldt = GetItem(tableName);
            return xmldt.List();
        }   
    }
}
