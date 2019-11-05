using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlackFoxExtras
{
    public class BlackFoxMetadata
    {
        [JsonPropertyName("__version")]
        public int Version { get; set; }

        [JsonPropertyName("scaler_name")]
        public string ScalerName { get; set; }

        [JsonPropertyName("scaler_config")]
        public ScalerConfig ScalerConfig { get; set; }

        [JsonPropertyName("is_scaler_integrated")]
        public bool IsScalerIntegrated { get; set; }

        [JsonPropertyName("has_rolling")]
        public bool HasRolling { get; set; }

        [JsonPropertyName("input_windows")]
        public int[] InputWindows { get; set; }

        [JsonPropertyName("input_shifts")]
        public int[] InputShifts { get; set; }

        [JsonPropertyName("input_steps")]
        public int[] InputSteps { get; set; }

        [JsonPropertyName("input_aggregation_types")]
        public string[] InputAggregationTypes { get; set; }

        [JsonPropertyName("output_windows")]
        public int[] OutputWindows { get; set; }

        [JsonPropertyName("output_shifts")]
        public int[] OutputShifts { get; set; }

        [JsonPropertyName("output_sample_step")]
        public int OutputSampleStep { get; set; }

        [JsonPropertyName("recurrent_input_count")]
        public int RecurrentInputCount { get; set; }

        [JsonPropertyName("recurrent_output_count")]
        public int RecurrentOutputCount { get; set; }

        [JsonPropertyName("is_input_used")]
        public bool[] IsInputUsed { get; set; }


        public static BlackFoxMetadata FromJson(string json)
        {
            return JsonSerializer.Deserialize<BlackFoxMetadata>(json);
        }

    }
}
