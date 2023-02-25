using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskParallelLibrary.DTO
{
    public partial class FiveDayWeatherList
    {
        [JsonProperty("dt")]
        public int Dt { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("pop")]
        public double Pop { get; set; }

        [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
        public FiveDayRain Rain { get; set; }

        [JsonProperty("sys")]
        public FiveDaySys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public DateTimeOffset DtTxt { get; set; }
    }
}
