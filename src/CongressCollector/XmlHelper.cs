using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CongressCollector
{
    /// <summary>
    /// Provides helper methods to deserialize XML to a C# object.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Deserializes a XML string to a C# object.
        /// </summary>
        /// <param name="xml">XML string to deserialize</param>
        /// <returns>C# object representing the XML contents</returns>
        public static T DeserializeXML<T>(string xml)
        {
            if (String.IsNullOrWhiteSpace(xml))
            {
                return default(T);
            }

            byte[] data = Encoding.UTF8.GetBytes(xml);
            return DeserializeXML<T>(data);
        }

        /// <summary>
        /// Deserializes a XML byte array to a C# object.
        /// </summary>
        /// <param name="xml">XML byte array to deserialize</param>
        /// <returns>C# object representing the XML contents</returns>
        public static T DeserializeXML<T>(byte[] xml)
        {
            if (xml == null || xml.Length == 0)
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