using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;

namespace AimBusiness.Customers.Boletus.Website.Common
{
    /// <summary>
    /// IO handling for xml backup data
    /// </summary>
    public class XmlHandler
    {        
        public string FilePath { get; set; }        

        /// <summary>
        /// Save object as serialized xml
        /// </summary>
        /// <param name="objToSave"></param>
        public void WriteDataItems(object objToSave)
        {
            XmlSerializer serializer = new XmlSerializer(objToSave.GetType());
            TextWriter tw = new StreamWriter(FilePath);
            serializer.Serialize(tw, objToSave);
            tw.Close();
        }

        /// <summary>
        /// Get object from serialized xml
        /// </summary>
        /// <param name="objToRead"></param>
        public object ReadDataItems(Type objectType)
        {
            object objToRead = new object();

            XmlSerializer serializer = new XmlSerializer(objectType);
            TextReader tr = new StreamReader(FilePath);
            objToRead = serializer.Deserialize(tr);
            tr.Close();

            return objToRead;
        }

    }
}