using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlackFoxExtras
{
    public class InputOutputConfig
    {
        [JsonPropertyName("feature_range")]
        public double[] FeatureRange { get; set; }

        [JsonPropertyName("fit")]
        public double[][] Fit { get; set; }

        [JsonPropertyName("inverse_transform")]
        public bool InverseTransform { get; set; }
    }
}
