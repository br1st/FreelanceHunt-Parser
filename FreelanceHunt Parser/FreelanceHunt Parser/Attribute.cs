using Newtonsoft.Json;

namespace FreelanceHunt_Parser
{
    public class Attribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("budget")]
        public Budget Budget { get; set; }
    }
}
