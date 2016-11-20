using System;
using System.Linq;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.Christmas.Plugins
{
    public class ChristmasHtmlIntegrationsPlugin : IHtmlHeaderExtension, IPlugin
    {
        public string Name
        {
            get
            {
                return "Ardour Digital - Christmas Decorations - HTML Integration";
            }
        }

        public string Description
        {
            get
            {
                return "Integrates the christmas decorations into the page HTML";
            }
        }

        public bool IsCacheable
        {
            get
            {
                return false;
            }
        }

        public bool VaryCacheByUser
        {
            get
            {
                return true;
            }
        }

        public void Initialize()
        {
        }

        public string GetHeader(RenderTarget target)
        {
            if (target != RenderTarget.Page)
            {
                return string.Empty;
            }

            var pageContext = PublicApi.Url.CurrentContext;

            if (pageContext == null || pageContext.ThemeTypeId == null)
            {
                // We're on a non-themed page (such as the admin area) - don't run
                return string.Empty;
            }

            if (DateTime.UtcNow < ChristmasConfiguration.StartDate || DateTime.UtcNow > ChristmasConfiguration.EndDate)
            {
                return string.Empty;
            }

            // Check cookie enabled/diabled

            if (!ChristmasConfiguration.EnabledForAll)
            {
                return string.Empty;
            }

            // Add CSS to header

            var assetsHandler = PluginManager.Get<ChristmasAssetsHandlerPlugin>().FirstOrDefault();

            return string.Format(@"<link rel=""stylesheet"" href=""{0}"" media=""screen"" />
<script type=""text/javascript"" src=""{1}""></script>
<script type=""text/javascript"">
    jQuery(document).ready(function() 
        {{ 
            jQuery.ardour.extensions.plugins.christmas.register({{
                snowEnabled: {2},
                snowEnabledForMobile: {3},
                snowColor: '{4}',
                snowmanEnabled: {5},
                snowmanEnabledForMobile: {6}
            }}); 
        }});
</script>",
                assetsHandler.GetAssetUrl("style"),
                assetsHandler.GetAssetUrl("script"),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowEnabled),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowEnabledForMobile),
                ChristmasConfiguration.SnowColor,
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowmanEnabled),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowmanEnabledForMobile));
        }

        private string JavascriptFriendlyBoolean(bool value)
        {
            return value.ToString().ToLower();
        }
    }
}
