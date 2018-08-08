using UniVerseDotNetCore.Domain.CssServiceLayer;

namespace UniVerseDotNetCore
{


    public static class CssAppConfig
    {
        public static bool RunInTestMode { get; set; }

        public static string CssUserName { get; set; }
        public static string CssUserPassword { get; set; }
        public static string CssHostname { get; set; }
        public static string CssAccount { get; set; }

        public static bool SetCssEnvironmentValues()
        {
            return FilterCapability.SetEnvironmentValues(CssUserName,CssUserPassword,CssHostname,CssAccount);
        }
    }


}
