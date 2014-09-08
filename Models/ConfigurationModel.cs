using Nop.Plugin.Misc.HtmlOptimiser.Code;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.HtmlOptimiser.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName(Constants.ResourceFieldsMinificationEnabled)]
        public bool MinificationEnabled { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveWhitespace)]
        public bool RemoveWhitespace { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveHtmlComments)]
        public bool RemoveHtmlComments { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveScriptComments)]
        public bool RemoveScriptComments { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveCDATASections)]
        public bool RemoveCDATASections { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsUseShortDocType)]
        public bool UseShortDocType { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveQuotes)]
        public bool RemoveQuotes { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveRedundantAttributes)]
        public bool RemoveRedundantAttributes { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsMinifyInlineScripts)]
        public bool MinifyInlineScripts { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsMinifyInlineStyles)]
        public bool MinifyInlineStyles { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveHeaders)]
        public string RemoveHeaders { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsAddHeaders)]
        public string AddHeaders { get; set; }
    }
}
