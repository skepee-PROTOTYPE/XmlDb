using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlDataBase
{
    internal class XmlLog
    {
        private readonly string PathFile;
        internal XmlTable LogDataObject { get; set; }

        internal XmlLog(string pathFile,string tableName, Dictionary<string, Type> Fields)
        {
            PathFile = pathFile;
            LogDataObject = new XmlTable();
            LogDataObject.CreateTable(tableName, Fields);
        }

        internal void Add(Dictionary<string, string> myData, string operation, string tableName)
        {
            string log=string.Empty;
            foreach (var item in myData)
                log += "Field: '" + item.Key + "' Value: '" + item.Value +"', ";

            log=log.Substring(0, log.Length - 2) +".";
            log= "Operation '" + operation + "' in table: '" + tableName + "' : " + log;

            Dictionary<string, string> myLogData = new Dictionary<string, string>
            {
                { LogDataObject.Properties.ElementAt(1).Key, DateTime.Now.ToString() },
                { LogDataObject.Properties.ElementAt(2).Key, log }
            };

            LogDataObject.Add(myLogData);
            LogDataObject.Dt.AcceptChanges();
            LogDataObject.Dt.WriteXml(PathFile);
        }
    }
}
