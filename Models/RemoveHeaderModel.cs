using Newtonsoft.Json;

namespace Nop.Plugin.Misc.HtmlOptimiser.Models
{
    public class RemoveHeaderModel
    {
        [JsonIgnore]
        public int Index { get; set; }

        public string Name { get; set; }
    }
}
