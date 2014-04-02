using Newtonsoft.Json;

namespace Nop.Plugin.Misc.HtmlOptimiser.Models
{
    public class AddHeaderModel
    {
        [JsonIgnore]
        public int Index { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
