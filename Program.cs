using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace UniVerseDotNetCore
{
    public partial class Program
    {
        public static void Main(string[] args)
        {


            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //}
            //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
            //         RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            //{
            //}
            Console.WriteLine($"FrameworkDescription:   {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"OSDescription:          {RuntimeInformation.OSDescription}");
            Console.WriteLine($"OSArchitecture:         {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"ProcessArchitecture:    {RuntimeInformation.ProcessArchitecture}");
            
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var inTestMode = configuration.GetSection("RUN_IN_TEST_MODE").Value;
            Console.Write($"In test mode? ({inTestMode})");
            if (string.Compare(inTestMode, "Yes", CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0)
            {
                CssAppConfig.RunInTestMode = true;
                Console.WriteLine($" Using demo data ({CssAppConfig.RunInTestMode})");
            }


            CssAppConfig.CssUserName = configuration.GetSection("CSS_USERNAME").Value;
            CssAppConfig.CssUserPassword = configuration.GetSection("CSS_USERPASSWORD").Value;
            CssAppConfig.CssAccount = configuration.GetSection("CSS_ACCOUNT").Value;
            CssAppConfig.CssHostname = configuration.GetSection("CSS_HOSTNAME").Value;
            var setValues = CssAppConfig.SetCssEnvironmentValues();
            if (!setValues)
            {
                Console.WriteLine($"Error setting environment values for CSS.");
                return;
            }

            CreateWebHostBuilder(args).Build().Run();

        }
       
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                .UseStartup<Startup>();
    }
}
