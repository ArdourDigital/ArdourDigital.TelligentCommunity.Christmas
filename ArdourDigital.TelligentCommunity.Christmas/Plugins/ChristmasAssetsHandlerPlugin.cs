using ArdourDigital.TelligentCommunity.Christmas.Assets;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.Christmas.Plugins
{
    public class ChristmasAssetsHandlerPlugin : IHttpCallback, IPlugin
    {
        private const string _fileNameQueryString = "file";
        
        private IHttpCallbackController _callbackController;

        public string Name
        {
            get
            {
                return "Ardour Digital - Christmas Decorations - Asset Handler";
            }
        }

        public string Description
        {
            get
            {
                return "Handles requests for assets used by the christmas decorations plugin";
            }
        }

        public void Initialize()
        {
        }

        public void ProcessRequest(HttpContextBase httpContext)
        {
            // TO DO

            // Ensure caching set appropriately

            if (!httpContext.Request.QueryString.AllKeys.Contains(_fileNameQueryString))
            {
                NotFoundResponse(httpContext);
                return;
            }

            // Handle each file type based on QS
            switch(httpContext.Request.QueryString[_fileNameQueryString].ToLowerInvariant())
            {
                case "script":
                    ScriptResponse(httpContext);
                    return;
                case "style":
                    StyleResponse(httpContext);
                    return;
            }

            NotFoundResponse(httpContext);
        }

        public string GetAssetUrl(string file)
        {
            return _callbackController.GetUrl(new NameValueCollection { { _fileNameQueryString, file } });
        }

        private void ScriptResponse(HttpContextBase httpContext)
        {
            httpContext.Response.ContentType = "text/javascript";
            httpContext.Response.BinaryWrite(AssetManager.GetAsset("Scripts.bundle.min.js"));
        }

        private void StyleResponse(HttpContextBase httpContext)
        {
            httpContext.Response.ContentType = "text/css";
            httpContext.Response.BinaryWrite(AssetManager.GetAsset("Styles.snowman.min.css"));
        }

        private void NotFoundResponse(HttpContextBase httpContext)
        {
            httpContext.Response.StatusCode = 404;
            httpContext.Response.StatusDescription = "File not found";
        }

        public void SetController(IHttpCallbackController controller)
        {
            _callbackController = controller;
        }
    }
}
