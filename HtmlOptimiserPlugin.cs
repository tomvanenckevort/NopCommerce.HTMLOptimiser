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
                RemoveWhitespace = true,
                RemoveHtmlComments = true,
                RemoveCDATASections = true,
                RemoveQuotes = true,
                RemoveRedundantAttributes = true,
                RemoveOptionalEndTags = true,
                RemoveScriptComments = true,
                MinifyInlineScripts = true,
                MinifyInlineStyles = true,
                UseShortDocType = true,
                RemoveHeaders = null,
                AddHeaders = null
            };

            _settingService.SaveSetting(settings);

            // locales
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinificationEnabled, "Minification Enabled");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinificationEnabledHint, "Check to enable the minification of the HTML output.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveWhitespace, "Remove Whitespace");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveWhitespaceHint, "Check to remove all leading and trailing whitespace characters.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHtmlComments, "Remove HTML Comments");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHtmlCommentsHint, "Check to remove all HTML comments, except conditional comments.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveScriptComments, "Remove Script Comments");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveScriptCommentsHint, "Check to remove all comments from inline JavaScript code and CSS styles.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveCDATASections, "Remove CDATA Sections");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveCDATASectionsHint, "Check to remove all CDATA sections from inline JavaScript code and CSS styles.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsUseShortDocType, "Use Short Document Type");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsUseShortDocTypeHint, "Check to replace existing docment type with short type: <!DOCTYPE html>.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveQuotes, "Remove Quotes");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveQuotesHint, "Check to remove quotes from HTML attributes (only for values without spaces).");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveRedundantAttributes, "Remove Redundant Attributes");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveRedundantAttributesHint, "Check to remove redundant HTML attributes, like script language, form method, etc.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveOptionalEndTags, "Remove Optional End Tags");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveOptionalEndTagsHint, "Check to remove the end tags of HTML elements that are optional, like head, body, tr, td, p, li, etc.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinifyInlineScripts, "Minify Inline Scripts");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinifyInlineScriptsHint, "Check to minify all inline JavaScript code.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinifyInlineStyles, "Minify Inline Styles");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsMinifyInlineStylesHint, "Check to minify all inline CSS styles.");            
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHeaders, "Remove Headers");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsRemoveHeadersHint, "List of HTTP header names to be removed from the HTTP response.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsAddHeaders, "Add Headers");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceFieldsAddHeadersHint, "List of HTTP headers to be added to the HTTP response.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart1, "The HTML Optimiser plugin allows you to do the following things:");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart2, "Minify the HTML output by removing unnecessary whitespace and line breaks.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart3, "Remove unwanted HTTP reponse headers (for example: X-Powered-By, X-AspNet-Version).");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart4, "Add extra HTTP response headers (for example: X-Frame-Options).");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart5, "All this is possible without having to configure or have access to IIS.");
            this.AddOrUpdatePluginLocaleResource(Constants.ResourceIntroPart6, "The minification can be tweaked by switching the following settings:");

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
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveWhitespace);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveWhitespaceHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHtmlComments);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHtmlCommentsHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveScriptComments);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveScriptCommentsHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveCDATASections);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveCDATASectionsHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsUseShortDocType);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsUseShortDocTypeHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveQuotes);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveQuotesHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveRedundantAttributes);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveRedundantAttributesHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveOptionalEndTags);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveOptionalEndTagsHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinifyInlineScripts);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinifyInlineScriptsHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinifyInlineStyles);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsMinifyInlineStylesHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHeaders);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsRemoveHeadersHint);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsAddHeaders);
            this.DeletePluginLocaleResource(Constants.ResourceFieldsAddHeadersHint);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart1);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart2);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart3);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart4);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart5);
            this.DeletePluginLocaleResource(Constants.ResourceIntroPart6);

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
