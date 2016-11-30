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
                
                var startDate = new Property("start_date", "Start Date", PropertyType.Date, 0, new DateTime(DateTime.UtcNow.Year, 12, 1, 0, 0, 0).ToString());
                startDate.DescriptionText = "The date the plugin will become active, no decorations will be shown before this date";
                generalProperties.Properties.Add(startDate);

                var endDate = new Property("end_date", "End Date", PropertyType.Date, 1, new DateTime(DateTime.UtcNow.Year, 12, 25, 0, 0, 0).AddDays(12).ToString());
                endDate.DescriptionText = "The date the plugin will become inactive, decorations will be hidden after this date";
                generalProperties.Properties.Add(endDate);

                var enabled = new Property("enabled", "Enabled for all users", PropertyType.Bool, 2, false.ToString());
                enabled.DescriptionText = "If ticked decorations will appear for all users";
                generalProperties.Properties.Add(enabled);

                var queryStringKey = new Property("query_string_key", "Enable/Disable query string key", PropertyType.String, 3, string.Empty);
                queryStringKey.DescriptionText = "Query string key to allow users to turn the decorations on or off. Users will be able to turn the decorations on by going to a link containing ?<em>&lt;query string key&gt;</em>=true, and turn off by going to a link containing ?<em>&lt;query string key&gt;</em>=false";
                generalProperties.Properties.Add(queryStringKey);

                var snowProperties = new PropertyGroup("snow_configuration", "Snow", 1);

                var snowEnabled = new Property("snow_enabled", "Enabled", PropertyType.Bool, 0, true.ToString());
                snowEnabled.DescriptionText = "If ticked the snow effect will be enabled";
                snowProperties.Properties.Add(snowEnabled);

                var snowEnabledForMobile = new Property("snow_enabled_mobile", "Enabled for Mobile", PropertyType.Bool, 1, true.ToString());
                snowEnabledForMobile.DescriptionText = "If ticked (and snow is enabled), the snow effect will be shown to single column layout users";
                snowProperties.Properties.Add(snowEnabledForMobile);

                var snowColor = new Property("snow_color", "Color", PropertyType.Color, 2, "#cccccc");
                snowColor.DescriptionText = "The color of the snow flakes";
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
                yield return typeof(ChristmasQueryStringProcessor);
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
            ChristmasConfiguration.StartDate = configuration.GetDateTime("start_date");
            ChristmasConfiguration.EndDate = configuration.GetDateTime("end_date");
            ChristmasConfiguration.EnabledForAll = configuration.GetBool("enabled");
            ChristmasConfiguration.SnowEnabled = configuration.GetBool("snow_enabled");
            ChristmasConfiguration.SnowEnabledForMobile = configuration.GetBool("snow_enabled_mobile");
            ChristmasConfiguration.SnowColor = configuration.GetString("snow_color");
            ChristmasConfiguration.SnowmanEnabled = configuration.GetBool("snowman_enabled");
            ChristmasConfiguration.SnowmanEnabledForMobile = configuration.GetBool("snowman_enabled_mobile");
            ChristmasConfiguration.QueryStringKey = configuration.GetString("query_string_key");
        }
    }
}
