using System;

namespace ArdourDigital.TelligentCommunity.Christmas
{
    internal static class ChristmasConfiguration
    {
        public static DateTime StartDate { get; set; }

        public static DateTime EndDate { get; set; }

        public static bool EnabledForAll { get; set; }

        public static bool SnowEnabled { get; set; }

        public static bool SnowEnabledForMobile { get; set; }

        public static string SnowColor { get; set; }

        public static bool SnowmanEnabled { get; set; }

        public static bool SnowmanEnabledForMobile { get; set; }
    }
}
