using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlackFoxExtras
{
    public class ScalerConfig
    {
        [JsonPropertyName("input")]
        public InputOutputConfig Input { get; set; }

        [JsonPropertyName("output")]
        public InputOutputConfig Output { get; set; }
    }
}
