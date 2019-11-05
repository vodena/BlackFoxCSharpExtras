using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class Data
    {

        /// <summary>
        /// Prepare the input for prediction with the following steps
        ///   1. removing insignificant columns
        ///   2. packing data for series
        ///   3. scaling(normalizing) values
        /// </summary>
        /// <param name="inputData">Input data</param>
        /// <param name="metadata">Model metadata</param>
        /// <returns>Prepared values</returns>
        public static IEnumerable<IEnumerable<double>> PrepareInputData(IEnumerable<IEnumerable<double>> inputData, BlackFoxMetadata metadata)
        {
            var usedInputs = Input.RemoveNotUsedInputs(inputData, metadata);
            if (metadata.HasRolling)
            {
                usedInputs = Series.PackInputDataForSeries(usedInputs, metadata);
            }
            return Scaler.ScaleInputData(usedInputs, metadata);
        }
    }
}
