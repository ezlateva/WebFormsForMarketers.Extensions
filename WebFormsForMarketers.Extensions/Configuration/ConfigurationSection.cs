using System;
using System.Configuration;

namespace WebFormsForMarketers.Extensions.Configuration
{
    public class ConfigurationSection : System.Configuration.ConfigurationSection
    {
        public static ConfigurationSection Create()
        {
            ConfigurationSection webApiSettingsSection =
                (ConfigurationSection)ConfigurationManager.GetSection("wffm.webapi.settings");

            if (webApiSettingsSection == null)
            {
                throw new ArgumentException("wffm.webapi.settings section in the web.config is not defined");
            }

            return webApiSettingsSection;
        }

        [ConfigurationProperty("origins")]
        [ConfigurationCollection(typeof(OriginsCollection), AddItemName = "origin")]
        public OriginsCollection Origins
        {
            get { return (OriginsCollection) base["origins"]; }
        }

    }
}
