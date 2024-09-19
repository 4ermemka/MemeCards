using Microsoft.Extensions.Configuration;

namespace ConfigurationBase
{
    /// <summary>
    /// Абстракция, подтягивающая appsettings-файл и конвертирующая в модель IConfigurationSettings
    /// </summary>
    public abstract class ConfigurationServiceBase<T> : IConfigurationService where T : IConfigurationSettings
    {
        protected T Settings { get; set; }
        protected IConfiguration Configuration;

        public void Configure()
        {
            Configuration = GetConfiguration(); //подлючение конфига
            //var configRoot = GetConfigurationRoot();
            Settings = Configuration.Get<T>();
        }

        #region Configuration

        protected IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.dev.json", true)
                .Build();
        }

        protected IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.dev.json", true)
                .Build();
        }

        #endregion
    }
}
