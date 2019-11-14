using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace BlackFoxExtras
{
    public class BlackFoxMetadata
    {
        [JsonProperty("__version")]
        public int Version { get; set; }

        [JsonProperty("scaler_name")]
        public string ScalerName { get; set; }

        [JsonProperty("scaler_config")]
        public ScalerConfig ScalerConfig { get; set; }

        [JsonProperty("is_scaler_integrated")]
        public bool IsScalerIntegrated { get; set; }

        [JsonProperty("has_rolling")]
        public bool HasRolling { get; set; }

        [JsonProperty("input_windows")]
        public int[] InputWindows { get; set; }

        [JsonProperty("input_shifts")]
        public int[] InputShifts { get; set; }

        [JsonProperty("input_steps")]
        public int[] InputSteps { get; set; }

        [JsonProperty("input_aggregation_types")]
        public string[] InputAggregationTypes { get; set; }

        [JsonProperty("output_windows")]
        public int[] OutputWindows { get; set; }

        [JsonProperty("output_shifts")]
        public int[] OutputShifts { get; set; }

        [JsonProperty("output_sample_step")]
        public int OutputSampleStep { get; set; }

        [JsonProperty("recurrent_input_count")]
        public int RecurrentInputCount { get; set; }

        [JsonProperty("recurrent_output_count")]
        public int RecurrentOutputCount { get; set; }

        [JsonProperty("is_input_used")]
        public bool[] IsInputUsed { get; set; }


        public static BlackFoxMetadata FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BlackFoxMetadata>(json);
        }

    }
}
