using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using RestSharp.Serializers;

namespace PinPayments.Serialization
{
    public class RestSharpDataContractJsonSerializer : ISerializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }

        public RestSharpDataContractJsonSerializer()
        {
            ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                var byteArray = stream.ToArray();
                stream.Close();
                return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
        }
    }
}