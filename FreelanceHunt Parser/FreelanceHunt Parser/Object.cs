using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceHunt_Parser
{
    public class Object
    {
        [JsonProperty("data")]
        public Project[] Data { get; set; }
    }
}
