using System.Configuration;

namespace PinPayments.Config
{
    public static class Configuration
    {
        private const string SectionName = "pinPaymentsApi";

        public static PinPaymentsApiSection RootSection
        {
            get
            {
                var section = ConfigurationManager.GetSection(SectionName);
                if (section == null)
                {
                    throw new ConfigurationErrorsException("Unable to locate the 'pinPaymentsApi' section in the application configuration.");
                }
                return section as PinPaymentsApiSection;
            }
        }
    }
}