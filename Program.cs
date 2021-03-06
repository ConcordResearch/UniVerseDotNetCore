﻿using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace UniVerseDotNetCore
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine($"FrameworkDescription:   {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"OSDescription:          {RuntimeInformation.OSDescription}");
            Console.WriteLine($"OSArchitecture:         {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"ProcessArchitecture:    {RuntimeInformation.ProcessArchitecture}");
            
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var inTestMode = configuration.GetSection("RUN_IN_TEST_MODE").Value;

            var testModeCheck =
                string.Compare(inTestMode, "Yes", CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);

             if (testModeCheck == 0)
            {
                CssAppConfig.RunInTestMode = true;
                Console.WriteLine($" Using demo data? ({CssAppConfig.RunInTestMode})");
            }

            Console.Write($"In test mode? ({CssAppConfig.RunInTestMode})");
            
            CssAppConfig.CssUserName = configuration.GetSection("CSS_USERNAME").Value;
            CssAppConfig.CssUserPassword = configuration.GetSection("CSS_USERPASSWORD").Value;
            CssAppConfig.CssAccount = configuration.GetSection("CSS_ACCOUNT").Value;
            CssAppConfig.CssHostname = configuration.GetSection("CSS_HOSTNAME").Value;

            var setValues = CssAppConfig.SetCssEnvironmentValues();

            if (!setValues)
                throw new Exception("Error setting environment values for CSS.");

            var passwordSet = string.IsNullOrEmpty(CssAppConfig.CssUserPassword) ? "Not Set" : "Set";

            Console.WriteLine($"CssUserName     = {CssAppConfig.CssUserName}");
            Console.WriteLine($"CssUserPassword = {passwordSet}");
            Console.WriteLine($"CssAccount      = {CssAppConfig.CssAccount}");
            Console.WriteLine($"CssHostname     = {CssAppConfig.CssHostname}");

            CreateWebHostBuilder(args).Build().Run();

        }
       
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                .UseStartup<Startup>();
    }
}
