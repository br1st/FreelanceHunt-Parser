using Newtonsoft.Json;

namespace FreelanceHunt_Parser
{
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("attributes")]
        public Attribute Params { get; set; }
    }
}
