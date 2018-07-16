using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;

namespace AimBusiness.Customers.Runo.EventsWebsite.Logic
{
    public class EventHandler
    {
        private string _filepath;

        public List<Containers.Event> Events { get; private set; }

        public EventHandler(string customerpath)
        {            
            _filepath = HttpContext.Current.Server.MapPath(customerpath + "/articles/events.xml");
            Events = new List<Containers.Event>();
            Events = (List<Containers.Event>)ReadDataItems(Events.GetType());
        }

        public void Save()
        {
            WriteDataItems(Events);
        }

        /// <summary>
        /// Save object as serialized xml
        /// </summary>
        /// <param name="objToSave"></param>
        private void WriteDataItems(object objToSave)
        {
            XmlSerializer serializer = new XmlSerializer(objToSave.GetType());
            TextWriter tw = new StreamWriter(_filepath);
            serializer.Serialize(tw, objToSave);
            tw.Close();
        }

        /// <summary>
        /// Get object from serialized xml
        /// </summary>
        /// <param name="objToRead"></param>
        private object ReadDataItems(Type objectType)
        {
            object objToRead = new object();

            XmlSerializer serializer = new XmlSerializer(objectType);
            TextReader tr = new StreamReader(_filepath);
            objToRead = serializer.Deserialize(tr);
            tr.Close();

            return objToRead;
        }
    }
}