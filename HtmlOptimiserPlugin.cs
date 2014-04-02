using Nop.Core.Plugins;
using Nop.Plugin.Misc.HtmlOptimiser.Code;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System.Linq;
using System.Web.Routing;

namespace Nop.Plugin.Misc.HtmlOptimiser
{
    public class HtmlOptimiserPlugin : BasePlugin, IMiscPlugin
    {
        private readonly ISettingService _settingService;

        public HtmlOptimiserPlugin(ISettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// Installs the plugin.
        /// </summary>
        public override void Install()
        {
            // settings
            var settings = new HtmlOptimiserSettings()
            {
                MinificationEnabled = true,
                RemoveHeaders = null,
                AddHeaders = null
            };

            _settingService.SaveSetting(settings);

            // locales
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinificationEnabled, "Minification Enabled");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinificationEnabledHint, "Check to enable the minification of the HTML output.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHeaders, "Remove Headers");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHeadersHint, "List of HTTP header names to be removed from the HTTP response.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsAddHeaders, "Add Headers");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsAddHeadersHint, "List of HTTP headers to be added to the HTTP response.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart1, "The HTML Optimiser plugin allows you to do the following things:");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart2, "Minify the HTML output by removing unnecessary whitespace and line breaks.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart3, "Remove unwanted HTTP reponse headers (for example: X-Powered-By, X-AspNet-Version).");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart4, "Add extra HTTP response headers (for example: X-Frame-Options).");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart5, "All this is possible without having to configure or have access to IIS.");

            base.Install();
        }

        /// <summary>
        /// Uninstalls the plugin.
        /// </summary>
        public override void Uninstall()
        {
            // settings
            _settingService.DeleteSetting<HtmlOptimiserSettings>();

            // locales
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinificationEnabled);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinificationEnabledHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHeaders);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHeadersHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsAddHeaders);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsAddHeadersHint);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart1);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart2);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart3);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart4);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart5);

            // update web.config
            WebConfigUpdater.UpdateWebConfig(Enumerable.Empty<string>());

            base.Uninstall();
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "MiscHtmlOptimiser";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Misc.HtmlOptimiser.Controllers" }, { "area", null } };
        }
    }
}
