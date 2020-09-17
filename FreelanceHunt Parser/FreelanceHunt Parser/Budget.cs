using Newtonsoft.Json;

namespace FreelanceHunt_Parser
{
    public class Budget
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
