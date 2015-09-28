using System.Configuration;

namespace WebFormsForMarketers.Extensions.Configuration
{
    public class OriginsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new OriginElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OriginElement) element).Name;
        }
    }
}