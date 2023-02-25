using Newtonsoft.Json;

namespace TaskParallelLibrary.DTO
{
    public class FiveDayWeather
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public int Message { get; set; }

        [JsonProperty("cnt")]
        public int Cnt { get; set; }

        [JsonProperty("list")]
        public FiveDayWeatherList[] FiveDayWeatherList { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }
}
