using Newtonsoft.Json;

namespace BlockchainApi.Net.Models
{
    public class XpubGap
    {
        [JsonProperty("gap")]
        public int Gap { get; private set; }
    }
}