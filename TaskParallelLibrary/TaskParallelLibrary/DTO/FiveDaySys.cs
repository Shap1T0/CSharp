using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskParallelLibrary.DTO
{
    public partial class FiveDaySys
    {
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }
}
