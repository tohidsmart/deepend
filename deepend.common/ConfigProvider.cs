using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace deepend.common
{
    public class ConfigProvider : IConfigProvider
    {
        public  ConnectionStringSettingsCollection ConnectionStringSettings;

        public ConfigProvider(ConnectionStringSettingsCollection connections)
        {
            ConnectionStringSettings = connections;
        }


        public string GetConnectionString()
        {
            string connectionString = ConnectionStringSettings["deependdb"].ConnectionString;
            return connectionString;
        }
    }
}
