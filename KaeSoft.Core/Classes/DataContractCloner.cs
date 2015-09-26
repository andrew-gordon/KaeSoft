using System;
using System.IO;
using System.Runtime.Serialization;

namespace Andy.Lib.Classes
{
    public static class DataContractCloner
    {
        public static T Clone<T>(T source)
        {
            var sourceMarkedWithDataContractAttribute = typeof(T).IsDefined(typeof(DataContractAttribute), false);

            if (!sourceMarkedWithDataContractAttribute)
            {
                var msg = string.Format("DataContractCloner must only be used on types marked with the DataContract attribute.  Type {0} does not have this attribute.",
                        typeof (T).FullName);
                throw new ArgumentException(msg, "source");
            }

            var serializer = new DataContractSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, source);
                ms.Seek(0, SeekOrigin.Begin);

                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
