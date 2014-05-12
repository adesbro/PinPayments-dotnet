using System.Configuration;

namespace PinPayments.Config
{
    public class PinPaymentsApiSection : ConfigurationSection
    {
        [ConfigurationProperty("server", IsRequired = true)]
        public ServerElement Server
        {
            get { return (ServerElement)this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("authentication", IsRequired = false)]
        public AuthenticationElement Authentication
        {
            get { return (AuthenticationElement)this["authentication"]; }
            set { this["authentication"] = value; }
        }
    }
}