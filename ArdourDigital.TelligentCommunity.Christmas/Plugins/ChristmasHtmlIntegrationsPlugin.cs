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
        
        private bool IsPotentiallyEnabled
        {
            get
            {
                if (ChristmasConfiguration.EnabledForAll)
                {
                    return true;
                }

                if (!string.IsNullOrWhiteSpace(ChristmasConfiguration.QueryStringKey))
                {
                    return true;
                }

                if (!string.IsNullOrWhiteSpace(ChristmasConfiguration.TextboxSelector)
                    && !string.IsNullOrWhiteSpace(ChristmasConfiguration.TextboxEnableValue)
                    && !string.IsNullOrWhiteSpace(ChristmasConfiguration.TextboxDisableValue))
                {
                    return true;
                }

                return false;
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

            if (!ChristmasConfiguration.WithinEnabledDates)
            {
                return string.Empty;
            }
            
            if (!IsPotentiallyEnabled)
            {
                return string.Empty;
            }
            
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
                snowmanEnabledForMobile: {6},
                cookieName: '{7}',
                requireCookie: {8},
                textboxSelector: '{9}',
                textboxEnabledValue: '{10}',
                textboxDisabledValue: '{11}',
                cookieExpiryTime: '{12}'
            }}); 
        }});
</script>",
                assetsHandler.GetAssetUrl("style"),
                assetsHandler.GetAssetUrl("script"),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowEnabled),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowEnabledForMobile),
                ChristmasConfiguration.SnowColor,
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowmanEnabled),
                JavascriptFriendlyBoolean(ChristmasConfiguration.SnowmanEnabledForMobile),
                ChristmasConfiguration.CookieName,
                JavascriptFriendlyBoolean(!ChristmasConfiguration.EnabledForAll),
                ChristmasConfiguration.TextboxSelector,
                JavascriptFriendlyString(ChristmasConfiguration.TextboxEnableValue),
                JavascriptFriendlyString(ChristmasConfiguration.TextboxDisableValue),
                ChristmasConfiguration.EndDate.ToString("ddd, dd MMM yyyy HH:mm:ss UTC"));
        }

        private string JavascriptFriendlyBoolean(bool value)
        {
            return value.ToString().ToLower();
        }

        private string JavascriptFriendlyString(string value)
        {
            return value.Replace("'", "\'");
        }
    }
}
