using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr.Core;

namespace Max.Writer.Rest
{
    public static class ConfigurationService
    {
        private static IConfigurationProvider s_configurationProvider;

        public static void Initialize(IConfigurationProvider configurationProvider)
        {
            s_configurationProvider = configurationProvider;
        }

        public static RestWriterSettings Settings
        {
            get
            {
                return s_configurationProvider != null
                    ? s_configurationProvider.GetConfiguration<RestWriterSettings>()
                    : new RestWriterSettings();
            }
        }
    }
}
