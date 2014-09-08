using Nop.Core.Configuration;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.HtmlOptimiser
{
    public class HtmlOptimiserSettings :ISettings
    {
        public bool MinificationEnabled { get; set; }

        public bool RemoveWhitespace { get; set; }

        public bool RemoveHtmlComments { get; set; }

        public bool RemoveScriptComments { get; set; }

        public bool RemoveCDATASections { get; set; }

        public bool UseShortDocType { get; set; }

        public bool RemoveQuotes { get; set; }

        public bool RemoveRedundantAttributes { get; set; }

        public bool RemoveOptionalEndTags { get; set; }

        public bool MinifyInlineScripts { get; set; }

        public bool MinifyInlineStyles { get; set; }

        public List<string> RemoveHeaders { get; set; }

        public List<AddHeader> AddHeaders { get; set; }
    }

    public class AddHeader
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
