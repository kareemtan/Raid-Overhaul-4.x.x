using Newtonsoft.Json;

namespace RaidOverhaul.Models
{
    internal struct DebugConfigs
    {
        [JsonProperty("DebugMode")]
        public bool DebugMode;

        [JsonProperty("DumpData")]
        public bool DumpData;
    }
}
