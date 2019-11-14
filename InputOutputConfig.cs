using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class InputOutputConfig
    {
        [JsonProperty("feature_range")]
        public double[] FeatureRange { get; set; }

        [JsonProperty("fit")]
        public double[][] Fit { get; set; }

        [JsonProperty("inverse_transform")]
        public bool InverseTransform { get; set; }
    }
}
