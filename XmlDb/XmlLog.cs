using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace XmlDataBase
{
    public class XmlLog
    {
        private string PathFile;
        public DataSet Ds { get; set; }
        internal XmlDataTable LogDataObject { get; set; }

        public XmlLog(string pathFile,string tableName, Dictionary<string, Type> Fields)
        {
            PathFile = pathFile;
            Ds = new DataSet();
            LogDataObject = new XmlDataTable();
            LogDataObject.CreateTable(tableName, Fields);
        }

        //public void Init()
        //{
        //    //DtLog.Columns.Add("id", typeof(int));
        //    //DtLog.Columns.Add("date", typeof(DateTime));
        //    //DtLog.Columns.Add("log", typeof(string));
        //    //DtLog.PrimaryKey = new DataColumn[] { DtLog.Columns[0] };
        //    //DtLog.Columns[0].AutoIncrement = true;

        //    if (File.Exists(PathFile))
        //    {
        //        Ds.ReadXml(PathFile);
        //    }
        //    else
        //    {
        //        Ds.WriteXml(PathFile);
        //    }
        //}


        public void Add(Dictionary<string, string> myData)
        {
            DataRow drAdd = LogDataObject.Dt.NewRow();

            foreach (var item in myData)
            {
                drAdd[item.Key] = item.Value;
            }
            LogDataObject.Dt.Rows.Add(drAdd);
            LogDataObject.Dt.WriteXml(PathFile);
        }
    }
}
