using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskParallelLibrary.DTO
{
    public partial class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
