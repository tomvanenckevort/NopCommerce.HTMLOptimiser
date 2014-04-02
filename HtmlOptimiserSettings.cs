using Nop.Core.Configuration;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.HtmlOptimiser
{
    public class HtmlOptimiserSettings :ISettings
    {
        public bool MinificationEnabled { get; set; }

        public List<string> RemoveHeaders { get; set; }

        public List<AddHeader> AddHeaders { get; set; }
    }

    public class AddHeader
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
