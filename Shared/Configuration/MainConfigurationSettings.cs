using ConfigurationBase;
using Shared.Models;

namespace Shared.Configuration
{
    public class MainConfigurationSettings : IConfigurationSettings
    {
        public ServerSettings ServerSettings  { get; set; }
    }
}
