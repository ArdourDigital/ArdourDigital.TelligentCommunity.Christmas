﻿using System;

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

        public static string QueryStringKey { get; set; }

        public static string TextboxSelector { get; set; }

        public static string TextboxEnableValue { get; set; }

        public static string TextboxDisableValue { get; set; }

        public static string CookieName
        {
            get
            {
                return "christmas_decorations";
            }
        }

        public static bool WithinEnabledDates
        {
            get
            {
                return DateTime.UtcNow > StartDate && DateTime.UtcNow < EndDate;
            }
        }
    }
}
