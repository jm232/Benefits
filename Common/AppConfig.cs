using Microsoft.Extensions.Configuration;
using System;

namespace BenefitsTA.Common
{
    public class AppConfig
    {
        private readonly IConfiguration _configuration;

        // Constructor to initialize the configuration from appsettings.json
        public AppConfig()
        {
            // Load configuration
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)  // Set base path to the application's directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Load the config file
                .Build();
        }

        // Method to get the username from the configuration
        public string GetUsername()
        {
            return _configuration["AppSettings:Username"];
        }

        // Method to get the password from the configuration
        public string GetPassword()
        {
            return _configuration["AppSettings:Password"];
        }

        // Method to get the server name (API URL, for example)
        public string GetServerName()
        {
            return _configuration["AppSettings:ServerName"];
        }

        // Method to get the API token
        public string GetToken()
        {
            return _configuration["AppSettings:Token"];
        }
    }
}