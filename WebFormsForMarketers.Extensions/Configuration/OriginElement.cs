using System.Configuration;

namespace WebFormsForMarketers.Extensions.Configuration
{
    public class OriginElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string) base["name"]; }
            set { base["name"] = value; }
        }
    }
}
