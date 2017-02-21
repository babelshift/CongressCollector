using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CongressCollector
{
    internal static class XmlHelper
    {
        public static T DeserializeXML<T>(string xml)
        {
            if (String.IsNullOrWhiteSpace(xml))
            {
                return default(T);
            }

            byte[] data = Encoding.UTF8.GetBytes(xml);
            return DeserializeXML<T>(data);
        }

        public static T DeserializeXML<T>(byte[] xml)
        {
            if(xml == null || xml.Length == 0)
            {
                return default(T);
            }

            using (Stream stream = new MemoryStream(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}