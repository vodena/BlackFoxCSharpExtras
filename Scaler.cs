using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class Scaler
    {
        /// <summary>
        /// Scale input data from real values to normalized.
        /// </summary>
        /// <param name="inputData">Input data</param>
        /// <param name="metadata">Model metadata</param>
        /// <param name="ignoreIntegratedScaler">If False(default), only scale data if model does not contain integrated scaler</param>
        /// <returns>Scaled data</returns>
        public static IEnumerable<IEnumerable<double>> ScaleInputData(IEnumerable<IEnumerable<double>> inputData, BlackFoxMetadata metadata, bool ignoreIntegratedScaler = false)
        {
            if (ignoreIntegratedScaler || !metadata.IsScalerIntegrated)
            {
                return ScaleDataFromConfig(inputData, metadata.ScalerConfig.Input, metadata.ScalerName);
            }
            else
            {
                return inputData;
            }
        }

        /// <summary>
        /// Scale data from normalized values to real values.Use after prediction.
        /// </summary>
        /// <param name="outputData">Output data</param>
        /// <param name="metadata">Model metadata</param>
        /// <param name="ignoreIntegratedScaler">If False(default), only scale data if model does not contain integrated scaler</param>
        /// <returns>Scaled data</returns>
        public static IEnumerable<IEnumerable<double>> ScaleOutputData(IEnumerable<IEnumerable<double>> outputData, BlackFoxMetadata metadata, bool ignoreIntegratedScaler = false)
        {
            if (ignoreIntegratedScaler || !metadata.IsScalerIntegrated)
            {
                return ScaleDataFromConfig(
                    outputData,
                    metadata.ScalerConfig.Output,
                    metadata.ScalerName
                );
            }
            else
            {
                return outputData;
            }

        }

        private static IEnumerable<IEnumerable<double>> ScaleDataFromConfig(IEnumerable<IEnumerable<double>> data, InputOutputConfig config, string scaler_name = "MinMaxScaler")
        {
            if (scaler_name == "MinMaxScaler")
            {
                return MinMaxScaleData(data, config.Fit, config.FeatureRange, config.InverseTransform);
            }
            else
            {
                throw new Exception("Unknown scaler " + scaler_name);
            }

        }
        private static IEnumerable<IEnumerable<double>> MinMaxScaleData(IEnumerable<IEnumerable<double>> data, double[][] fit, double[] featureRange, bool inverse = false)
        {
            List<List<double>> scaledData = new List<List<double>>();
            int j = 0;
            if (inverse)
            {
                foreach (var row in data)
                {
                    j = 0;
                    List<double> scaledRow = new List<double>();
                    foreach (var d in row)
                    {
                        double std = (d - featureRange[0]) / (featureRange[1] - featureRange[0]);
                        double scaled = std * (fit[1][j] - fit[0][j]) + fit[0][j];
                        scaledRow.Add(scaled);
                        j++;
                    }
                    scaledData.Add(scaledRow);
                }
            }
            else
            {
                foreach (var row in data)
                {
                    j = 0;
                    List<double> scaledRow = new List<double>();
                    foreach (var d in row)
                    {
                        double std = (d - fit[0][j]) / (fit[1][j] - fit[0][j]);
                        double scaled = std * (featureRange[1] - featureRange[0]) + featureRange[0];
                        scaledRow.Add(scaled);
                        j++;
                    }
                    scaledData.Add(scaledRow);
                }
            }

            return scaledData;
        }

    }
}
