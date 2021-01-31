using System.Collections.Generic;
using Newtonsoft.Json;
namespace MTGRares {
    class Identifier {
        [JsonProperty(PropertyName = "identifiers")]
        public ICollection<CardSearch> IDs {get; set;}
    }
}