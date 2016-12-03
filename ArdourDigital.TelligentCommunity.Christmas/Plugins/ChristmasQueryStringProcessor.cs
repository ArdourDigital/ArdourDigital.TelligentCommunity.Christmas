using System;
using System.Linq;
using System.Web;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.Christmas.Plugins
{
#if !Pre9
    public class ChristmasQueryStringProcessor : IHttpRequestFilter, IPlugin
    {
        private const string _cookieName = "christmas_decorations";

        public string Name
        {
            get
            {
                return "Ardour Digital - Christmas Decorations - Query String Processor";
            }
        }

        public string Description
        {
            get
            {
                return "Checks requests for the specified query string and sets a cookie if found";
            }
        }

        public void Initialize()
        {
        }

        public void FilterRequest(IHttpRequest request)
        {
            if (!ChristmasConfiguration.WithinEnabledDates)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(ChristmasConfiguration.QueryStringKey))
            {
                return;
            }

            if (request == null || request.HttpContext == null || request.HttpContext.Request == null)
            {
                return;
            }

            var queryString = request.HttpContext.Request.QueryString;

            if (queryString.AllKeys.Contains(ChristmasConfiguration.QueryStringKey))
            {
                var enable = false;

                bool.TryParse(queryString[ChristmasConfiguration.QueryStringKey], out enable);

                if (enable)
                {
                    SetCookie(request, "true", ChristmasConfiguration.EndDate);
                }
                else
                {
                    SetCookie(request, "false", DateTime.UtcNow.AddMonths(-1));
                }
            }
        }

        private void SetCookie(IHttpRequest request, string value, DateTime expiryDate)
        {
            request.HttpContext.Response.SetCookie(new HttpCookie(ChristmasConfiguration.CookieName, value)
            {
                Expires = expiryDate
            });
        }
    }
#endif
}
