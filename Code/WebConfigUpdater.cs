using Nop.Core;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Nop.Plugin.Misc.HtmlOptimiser.Code
{
    public static class WebConfigUpdater
    {
        private static readonly IWebHelper _webHelper = EngineContext.Current.Resolve<IWebHelper>();

        public static void UpdateWebConfig(IEnumerable<string> removeHeaders)
        {
            // checks if X-Powered-By and X-AspNet-Version need to be removed.
            // can only be done in the web.config
            bool hasChanged = false;
            bool removeXPoweredBy = false;
            bool removeXAspNetVersion = false;

            if (removeHeaders != null)
            {
                removeXPoweredBy = removeHeaders.Contains("X-Powered-By");
                removeXAspNetVersion = removeHeaders.Contains("X-AspNet-Version");
            }

            // load web.config
            XDocument webConfig = null;

            using (var fs = File.OpenRead(_webHelper.MapPath("~/web.config")))
            {
                webConfig = XDocument.Load(fs);
            }

            if (webConfig != null)
            {
                webConfig.Changed += (o, e) => { hasChanged = true; };

                // set versionheader setting
                var httpRuntimeSettings = webConfig.XPathSelectElement("configuration//system.web//httpRuntime");

                if (httpRuntimeSettings != null)
                {
                    var versionHeader = httpRuntimeSettings.Attribute("enableVersionHeader");

                    if (removeXAspNetVersion && (versionHeader == null || versionHeader.Value == "true"))
                    {
                        httpRuntimeSettings.SetAttributeValue("enableVersionHeader", !removeXAspNetVersion);
                    }

                    if (!removeXAspNetVersion && versionHeader != null)
                    {
                        versionHeader.Remove();
                    }
                }

                // set custom header setting
                var webServerSettings = webConfig.XPathSelectElement("configuration//system.webServer");

                if (webServerSettings != null)
                {
                    var httpProtocol = GetOrCreateElement(webServerSettings, "httpProtocol");

                    var customHeaders = GetOrCreateElement(httpProtocol, "customHeaders");

                    var removeEntryXPoweredBy = GetOrCreateElement(customHeaders, "remove", new XAttribute("name", "X-Powered-By"), removeXPoweredBy);

                    if (!removeXPoweredBy && removeEntryXPoweredBy != null)
                    {
                        removeEntryXPoweredBy.Remove();
                    }
                }

                if (hasChanged)
                {
                    // only save when changes have been made
                    try
                    {
                        webConfig.Save(_webHelper.MapPath("~/web.config"));
                    }
                    catch
                    {
                        throw new NopException("nopCommerce needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                            "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                            "- run the application in a full trust environment, or" + Environment.NewLine +
                            "- give the application write access to the 'web.config' file.");
                    }
                }
            }
        }

        private static XElement GetOrCreateElement(XContainer container, XName name, XAttribute attr = null, bool required = true)
        {
            string xPath = string.Format("//{0}", name);

            if (attr != null)
            {
                xPath += string.Format("[@{0}='{1}']", attr.Name, attr.Value);
            }

            var element = container.XPathSelectElement(xPath);

            if (element == null && required)
            {
                element = new XElement(name);

                if (attr != null)
                {
                    element.SetAttributeValue(attr.Name, attr.Value);
                }

                container.Add(element);
            }

            return element;
        }
    }
}
