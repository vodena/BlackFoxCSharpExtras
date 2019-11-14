using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class ScalerConfig
    {
        [JsonProperty("input")]
        public InputOutputConfig Input { get; set; }

        [JsonProperty("output")]
        public InputOutputConfig Output { get; set; }
    }
}
