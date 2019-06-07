using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace XmlDataBase
{
    public class XmlDb
    {
        private string PathFile { get; set; }
        public string DataBaseName { get; set; }
        private DataSet Ds { get; set; }
        public string DataSetName { get; set; }
        public string DataSetNameSpace { get; set; }
        private List<XmlDataTable> ListDataObjects { get; set; }
        private XmlLog LogDataObject { get; set; }

        public void AddLogTable(string logFile, string tableName, Dictionary<string, Type> Fields)
        {
            LogDataObject = new XmlLog(logFile, tableName, Fields);
        }


        private void AddLogItem(Dictionary<string, string> myData)
        {
            LogDataObject.Add(myData);
            Ds.AcceptChanges();
            Ds.WriteXml(PathFile);
        }


        private XmlDataTable GetItem(string tableName)
        {
            var xmldt = ListDataObjects.ToList().FirstOrDefault(x => x.DataTableName.Equals(tableName, StringComparison.InvariantCultureIgnoreCase));

            return xmldt;
        }

        public void LoadXmlDb()
        {
            if (File.Exists(PathFile))
            {
                Ds.ReadXml(PathFile);

                foreach (DataTable dt in Ds.Tables)
                {

                    XmlDataTable myXmlData = new XmlDataTable
                    {
                        DataTableName = dt.TableName,
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
                Ds.WriteXml(PathFile);
            }
        }

        public void RemoveItem(string tableName, Dictionary<string, string> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                xmldt.Remove(myData);
                Ds.AcceptChanges();
                Ds.WriteXml(PathFile);
            }
        }

        public void AddItem(string tableName,Dictionary<string, string> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                xmldt.Add(myData);
                Ds.AcceptChanges();
                Ds.WriteXml(PathFile);
            }

            // TODO
            Dictionary<string, string> dicLog = new Dictionary<string, string>();
            foreach (var x in LogDataObject.LogDataObject.Properties)
            {


            }


            AddLogItem(new Dictionary<string, string>
            {
                    {"Name", "Name1" },
                    {"Type", "1000" }
            });

        }

        public void AddListItem(string tableName, List<Dictionary<string, string>> myData)
        {
            var xmldt = GetItem(tableName);

            if (xmldt != null)
            {
                foreach (var item in myData)
                {
                    AddItem(tableName, item);
                }
                Ds.AcceptChanges();
                Ds.WriteXml(PathFile);
            }
        }


        public void AddTable(string tableName, Dictionary<string, Type> myData)
        {
            XmlDataTable xmldt = new XmlDataTable();
            xmldt.CreateTable(tableName, myData);

            if (xmldt != null)
            {
                ListDataObjects.Add(xmldt);
                Ds.Tables.Add(xmldt.Dt);
            }
        }

        public XmlDb(string name, string path, string dataSetName, string dataSetNameSpace)
        {
            this.DataSetName = dataSetName;
            this.DataSetNameSpace = dataSetNameSpace;
            this.PathFile = path;
            this.DataBaseName = name;
            this.ListDataObjects = new List<XmlDataTable>();
            this.Ds = new DataSet();
        }

        public List<Dictionary<string, string>> GetListItems(string tableName)
        {
            var xmldt = GetItem(tableName);
            return xmldt.List();
        }   
    }
}
