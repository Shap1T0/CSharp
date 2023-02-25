using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskParallelLibrary.DTO
{
    public partial class FiveDayRain
    {
        [JsonProperty("3h")]
        public double The3H { get; set; }
    }
}
