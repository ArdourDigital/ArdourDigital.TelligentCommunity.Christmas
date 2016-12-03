using ArdourDigital.TelligentCommunity.Christmas.Plugins;
using System;
using System.Collections.Generic;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.Christmas
{
    public class ChristmasPlugin : IPlugin, IConfigurablePlugin, IRequiredConfigurationPlugin, IPluginGroup
    {
        // TODO - Allow cookie to be set via QS
        // TODO - Allow cookie to be set via textbox

        public string Name
        {
            get
            {
                return "Ardour Digital - Christmas Decorations";
            }
        }

        public string Description
        {
            get
            {
                return "Add christmas decorations to your community";
            }
        }

        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                var generalProperties = new PropertyGroup("general", "General", 0);

                var startDate = new Property("start_date", "Start Date", PropertyType.Date, 0, DefaultStartDate.ToString());
                startDate.DescriptionText = "The date the plugin will become active, no decorations will be shown before this date";
                generalProperties.Properties.Add(startDate);

                var endDate = new Property("end_date", "End Date", PropertyType.Date, 1, DefaultEndDate.ToString());
                endDate.DescriptionText = "The date the plugin will become inactive, decorations will be hidden after this date";
                generalProperties.Properties.Add(endDate);

                var enabled = new Property("enabled", "Enabled for all users", PropertyType.Bool, 2, false.ToString());
                enabled.DescriptionText = "If ticked decorations will appear for all users";
                generalProperties.Properties.Add(enabled);

#if !TELLIGENT8

                var queryStringKey = new Property("query_string_key", "Enable/Disable query string key", PropertyType.String, 3, string.Empty);
                queryStringKey.DescriptionText = "Query string key to allow users to turn the decorations on or off. Users will be able to turn the decorations on by going to a link containing ?<em>&lt;query string key&gt;</em>=true, and turn off by going to a link containing ?<em>&lt;query string key&gt;</em>=false";
                generalProperties.Properties.Add(queryStringKey);

#endif

                var textBoxSelector = new Property("textbox_selector", "Textbox selector", PropertyType.String, 4, string.Empty);
                textBoxSelector.DescriptionText = "JQuery selector of a textbox to monitor. If either of the values specified below are typed into this textbox the decorations will be enabled or disabled. To use the default theme search box enter <em>.site-banner .search .field-item-input input</em>";
                generalProperties.Properties.Add(textBoxSelector);

                var textBoxEnabledValue = new Property("textbox_enabled_value", "Textbox enabled value", PropertyType.String, 5, string.Empty);
                textBoxEnabledValue.DescriptionText = "When this value is typed in the textbox specified above the decorations will be enabled";
                generalProperties.Properties.Add(textBoxEnabledValue);

                var textBoxDisabledValue = new Property("textbox_disabled_value", "Textbox disabled value", PropertyType.String, 6, string.Empty);
                textBoxDisabledValue.DescriptionText = "When this value is typed in the textbox specified above the decorations will be disabled";
                generalProperties.Properties.Add(textBoxDisabledValue);

                var snowProperties = new PropertyGroup("snow_configuration", "Snow", 1);

                var snowEnabled = new Property("snow_enabled", "Enabled", PropertyType.Bool, 0, true.ToString());
                snowEnabled.DescriptionText = "If ticked the snow effect will be enabled";
                snowProperties.Properties.Add(snowEnabled);

                var snowEnabledForMobile = new Property("snow_enabled_mobile", "Enabled for Mobile", PropertyType.Bool, 1, true.ToString());
                snowEnabledForMobile.DescriptionText = "If ticked (and snow is enabled), the snow effect will be shown to single column layout users";
                snowProperties.Properties.Add(snowEnabledForMobile);

                var snowColor = new Property("snow_color", "Color", PropertyType.String, 2, "#cccccc");
                snowColor.DescriptionText = "The color of the snow flakes (as a hex code e.g. <em>#cccccc</em>)";
                snowProperties.Properties.Add(snowColor);

                var snowmanProperties = new PropertyGroup("snowman_configuration", "Snowman", 2);

                var snowmanEnabled = new Property("snowman_enabled", "Enabled", PropertyType.Bool, 0, true.ToString());
                snowmanEnabled.DescriptionText = "If ticked the snowman will be enabled";
                snowmanProperties.Properties.Add(snowmanEnabled);

                var snowmanEnabledForMobile = new Property("snowman_enabled_mobile", "Enabled for Mobile", PropertyType.Bool, 1, true.ToString());
                snowmanEnabledForMobile.DescriptionText = "If ticked (and snowman is enabled), the snowman will be shown to single column layout users";
                snowmanProperties.Properties.Add(snowmanEnabledForMobile);

                return new[] { generalProperties, snowProperties, snowmanProperties };
            }
        }

        public IEnumerable<Type> Plugins
        {
            get
            {
                yield return typeof(ChristmasHtmlIntegrationsPlugin);
                yield return typeof(ChristmasAssetsHandlerPlugin);
#if !TELLIGENT8
                yield return typeof(ChristmasQueryStringProcessor);
#endif
            }
        }

        public bool IsConfigured
        {
            get
            {
                return ChristmasConfiguration.StartDate != default(DateTime)
                    && ChristmasConfiguration.EndDate != default(DateTime);
            }
        }

        public void Initialize()
        {
        }

        public void Update(IPluginConfiguration configuration)
        {
            var startDate = configuration.GetDateTime("start_date");
            var endDate = configuration.GetDateTime("end_date");

            ChristmasConfiguration.StartDate = startDate == default(DateTime) ? DefaultStartDate : startDate;
            ChristmasConfiguration.EndDate = endDate == default(DateTime) ? DefaultEndDate : endDate;
            ChristmasConfiguration.EnabledForAll = configuration.GetBool("enabled");
            ChristmasConfiguration.SnowEnabled = configuration.GetBool("snow_enabled");
            ChristmasConfiguration.SnowEnabledForMobile = configuration.GetBool("snow_enabled_mobile");
            ChristmasConfiguration.SnowColor = configuration.GetString("snow_color");
            ChristmasConfiguration.SnowmanEnabled = configuration.GetBool("snowman_enabled");
            ChristmasConfiguration.SnowmanEnabledForMobile = configuration.GetBool("snowman_enabled_mobile");
            ChristmasConfiguration.QueryStringKey = configuration.GetString("query_string_key");
            ChristmasConfiguration.TextboxSelector = configuration.GetString("textbox_selector");
            ChristmasConfiguration.TextboxEnableValue = configuration.GetString("textbox_enabled_value");
            ChristmasConfiguration.TextboxDisableValue = configuration.GetString("textbox_disabled_value");
        }

        private DateTime DefaultStartDate
        {
            get { return new DateTime(DateTime.UtcNow.Year, 12, 1, 0, 0, 0); }
        }

        private DateTime DefaultEndDate
        {
            get { return new DateTime(DateTime.UtcNow.Year, 12, 25, 0, 0, 0).AddDays(12); }
        }
    }
}
