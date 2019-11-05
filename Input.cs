using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class Input
    {
        /// <summary>
        /// Remove insignificant columns from data, if model has feature selection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="metadata">Model metadata</param>
        /// <returns>New data</returns>
        public static IEnumerable<IEnumerable<double>> RemoveNotUsedInputs(IEnumerable<IEnumerable<double>> data, BlackFoxMetadata metadata)
        {
            if (metadata.IsInputUsed != null)
            {
                List<List<double>> newData = new List<List<double>>();
                int j = 0;
                foreach (var row in data)
                {
                    j = 0;
                    List<double> newRow = new List<double>();
                    foreach (var d in row)
                    {
                        if (metadata.IsInputUsed[j])
                        {
                            newRow.Add(d);
                        }
                        j++;
                    }
                    newData.Add(newRow);
                }
                return newData;
            }
            else
            {
                return data;
            }
        }

    }
}
