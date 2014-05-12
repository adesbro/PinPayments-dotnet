using System.Configuration;

namespace PinPayments.Config
{
    public class ServerElement : ConfigurationElement
    {
        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
            set { this["baseUrl"] = value; }
        }
    }
}