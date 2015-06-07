using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

namespace SimpleMetrics.Util
{
    public static class Serializer<T>
    {
        public static byte[] BinarySerialize(T input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, input);
                return memoryStream.ToArray();
            }
        }

        public static T BinaryDeserialize(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Position = 0;
                return (T) binaryFormatter.Deserialize(memoryStream);
            }
        }

        public static byte[] JsonSerialize(T input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof (T));
                serializer.WriteObject(memoryStream, input);
                return memoryStream.ToArray();
            }
        }

        public static T JsonDeserialize(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof (T));
                memoryStream.Position = 0;
                return (T) deserializer.ReadObject(memoryStream);
            }
        }

        public static T JsonDeserialize(string data)
        {
            byte[] bytes = new byte[data.Length * sizeof (char)];
            Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);
            return JsonDeserialize(bytes);
        }
    }
}