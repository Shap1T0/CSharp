using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskParallelLibrary.DTO
{
    public class CuurentRain
    {
        [JsonProperty("1h")]
        public double The1H { get; set; }
    }
}
