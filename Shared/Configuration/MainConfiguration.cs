using ConfigurationBase;
using Shared.Models;

namespace Shared.Configuration
{
    public class MainConfiguration : ConfigurationServiceBase<MainConfigurationSettings>
    {
        public MainConfiguration()
        {
            Configure();
        }

        public ServerSettings GetServerSettings()
        {
            return Settings.ServerSettings;
        }
    }
}
