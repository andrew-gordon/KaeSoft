using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace KaeSoft.Core.Classes
{
    /// <summary>
    /// Serializes or deserializes a json result.
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// Converts an object to a JSON format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>Serialized object.</returns>
        public static string Serialize<T>(T obj)
        {
            var stream = new MemoryStream();

            var serializer = new DataContractJsonSerializer(typeof(T));

            serializer.WriteObject(stream, obj);

            string result = Encoding.UTF8.GetString(stream.ToArray());

            return result;
        }

        /// <summary>
        /// Convert json format to an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            var stream = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var deserializer = new DataContractJsonSerializer(typeof(T));
            var result = (T)deserializer.ReadObject(stream);

            return result;
        }
    }
}