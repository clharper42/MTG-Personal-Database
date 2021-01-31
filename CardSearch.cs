using Newtonsoft.Json;
namespace MTGRares {
    class CardSearch {
        [JsonProperty(PropertyName = "set")]
        public string Set {get; set;}

        [JsonProperty(PropertyName = "collector_number")]
        public string Collector_number {get; set;}
        
    }
}