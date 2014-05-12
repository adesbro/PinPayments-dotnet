using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace PinPayments.Serialization
{
    public class RestSharpDataContractJsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var result = (T)serializer.ReadObject(stream);
                stream.Close();
                return result;
            }
        }
    }
}