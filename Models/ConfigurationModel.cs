using Nop.Plugin.Misc.HtmlOptimiser.Code;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.HtmlOptimiser.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName(Constants.ResourceFieldsMinificationEnabled)]
        public bool MinificationEnabled { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsRemoveHeaders)]
        public string RemoveHeaders { get; set; }

        [NopResourceDisplayName(Constants.ResourceFieldsAddHeaders)]
        public string AddHeaders { get; set; }
    }
}
