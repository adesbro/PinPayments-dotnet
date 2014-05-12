using System.Configuration;

namespace PinPayments.Config
{
    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("secretKey", IsRequired = true)]
        public string SecretKey
        {
            get { return (string)this["secretKey"]; }
            set { this["secretKey"] = value; }
        }
    }
}