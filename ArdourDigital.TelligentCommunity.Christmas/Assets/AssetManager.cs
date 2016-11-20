using System.Collections.Generic;

namespace ArdourDigital.TelligentCommunity.Christmas.Assets
{
    public static class AssetManager
    {
        private static IDictionary<string, byte[]> _assets;
        private static object _resourceLoaderLock = new object();

        static AssetManager()
        {
            _assets = new Dictionary<string, byte[]>();
            _resourceLoaderLock = new object();
        }

        public static byte[] GetAsset(string assetName)
        {
            if (!_assets.ContainsKey(assetName))
            {
                lock(_resourceLoaderLock)
                {
                    if(!_assets.ContainsKey(assetName))
                    {
                        var resourceName = string.Format("ArdourDigital.TelligentCommunity.Christmas.Assets.{0}", assetName);

                        using (var stream = typeof(AssetManager).Assembly.GetManifestResourceStream(resourceName))
                        {
                            if (stream == null)
                            {
                                return new byte[0];
                            }

                            var results = new byte[stream.Length];
                            stream.Read(results, 0, results.Length);
                            _assets.Add(assetName, results);
                        }
                    }
                }
            }

            if (!_assets.ContainsKey(assetName))
            {
                return new byte[0];
            }

            return _assets[assetName];
        }
    }
}
