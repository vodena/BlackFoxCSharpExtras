using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackFoxExtras
{
    public class Series
    {

        /// <summary>
        /// Get left and right data padding
        /// </summary>
        /// <param name="metadata">Model metadata</param>
        /// <returns>Left padding, right padding</returns>
        public static DataPadding GetSeriesPadding(BlackFoxMetadata metadata)
        {
            int leftInput = int.MinValue;
            int rightInput = 0;
            int leftОutput = int.MinValue;
            int rightOutput = 0;
            for (int i = 0; i < metadata.InputShifts.Length; i++)
            {
                leftInput = Math.Max(leftInput, -metadata.InputShifts[i] + metadata.InputWindows[i]);
                rightInput = Math.Max(rightInput, metadata.InputShifts[i]);
            }
            for (int i = 0; i < metadata.OutputShifts.Length; i++)
            {
                leftОutput = Math.Max(leftОutput, -metadata.OutputShifts[i] + metadata.OutputWindows[i]);
                rightOutput = Math.Max(rightOutput, metadata.OutputShifts[i]);
            }

            int left_padding = Math.Max(Math.Max(0, leftInput - 1), leftОutput - 1);
            left_padding = metadata.OutputSampleStep * (int)Math.Ceiling((decimal)left_padding / metadata.OutputSampleStep);
            int right_padding = Math.Max(Math.Max(rightInput, rightOutput), 0);

            return new DataPadding(left_padding, right_padding);

        }

        /// <summary>
        /// Packing input data for series
        /// </summary>
        /// <param name="inputData">Data as numpy array</param>
        /// <param name="metadata">Model metadata</param>
        /// <returns>Packed data</returns>
        public static List<List<double>> PackInputDataForSeries(IEnumerable<IEnumerable<double>> inputData, BlackFoxMetadata metadata)
        {
            DataPadding padding = GetSeriesPadding(metadata);
            
            List<List<double>> xData = new List<List<double>>(); ;
            int rowsCount = inputData.Count() - padding.Right;

            for (int i = padding.Left; i < rowsCount; i += metadata.OutputSampleStep)
            {
                List<double> xRow = GetInputRow(inputData, metadata, i, metadata.InputShifts.Length);
                xData.Add(xRow);
            }

            return xData;
        }

        /// <summary>
        /// Packing output data for series
        /// </summary>
        /// <param name="output_data">Data as numpy array</param>
        /// <param name="metadata">Model metadata</param>
        /// <returns>Packed data</returns>
        public static List<List<double>> PackOutputDataForSeries(double[][] output_data, BlackFoxMetadata metadata)
        {
            int outputCount = metadata.OutputShifts.Length;
            int sampleStep = metadata.OutputSampleStep;

            DataPadding padding = GetSeriesPadding(metadata);

            List<List<double>> yData = new List<List<double>>();
            int rowsCount = output_data.Length - padding.Right;
            for (int i = padding.Left; i < rowsCount; i += sampleStep)
            {
                List<double> yRow = GetOutputRow(output_data, metadata, i, outputCount, 0);
                yData.Add(yRow);
            }
            return yData;
        }

        /// <summary>
        /// Packing input and output data for series
        /// </summary>
        /// <param name="data"></param>
        /// <param name="metadata"></param>
        /// <returns>Packed data</returns>
        public static PackedData PackDataForSeries(double[][] data, BlackFoxMetadata metadata)
        {
            int inputCount = metadata.InputShifts.Length;
            int outputCount = metadata.OutputShifts.Length;
            int sample_step = metadata.OutputSampleStep;

            DataPadding padding = GetSeriesPadding(metadata);

            List<List<double>> xData = new List<List<double>>();
            List<List<double>> yData = new List<List<double>>();
            int rows_count = data.Length - padding.Right;
            for (int i = padding.Left; i < rows_count; i += sample_step)
            {

                List<double> xRow = GetInputRow(data, metadata, i, inputCount);
                xData.Add(xRow);

                List<double> yRow = GetOutputRow(data, metadata, i, outputCount, inputCount);
                yData.Add(yRow);
            }

            return new PackedData(xData, yData);
        }


        private static double[] GetColumnData(IEnumerable<IEnumerable<double>> data, int fromRow, int toRow, int columnIndex)
        {
            double[] d = new double[toRow - fromRow];
            for (int i = fromRow; i < toRow; i++)
            {
                d[i - fromRow] = data.ElementAt(i).ElementAt(columnIndex);
            }
            return d;
        }

        private static double[] GetColumnData(double[] data, int fromRow, int toRow)
        {
            double[] d = new double[toRow - fromRow];
            for (int i = fromRow; i < toRow; i++)
            {
                d[i - fromRow] = data[i];
            }
            return d;
        }

        private static List<double> GetInputRow(IEnumerable<IEnumerable<double>> data, BlackFoxMetadata metadata, int i, int input_count)
        {
            List<double> xRow = new List<double>();
            for (int j = 0; j < input_count; j++)
            {
                int shift = metadata.InputShifts[j];
                int window = metadata.InputWindows[j];
                int step = metadata.InputSteps[j];
                string aggregationType = metadata.InputAggregationTypes[j];
                int offset = i + 1 + shift;
                double[] d = GetColumnData(data, offset - window, offset, j);
                int n = d.Length;
                List<double> newD = new List<double>();
                if (aggregationType == "sum")
                {
                    for (int s = (n) % step; s < n - 1; s += step)
                    {
                        newD.Add(GetColumnData(d, s, s + step).Sum());
                    }
                }
                else if (aggregationType == "avg")
                {
                    for (int s = (n) % step; s < n - 1; s += step)
                    {
                        newD.Add(GetColumnData(d, s, s + step).Average());
                    }
                }
                else
                {
                    for (int s = (n - 1) % step; s < n; s += step)
                    {
                        newD.Add(d[s]);
                    }
                }

                xRow.AddRange(newD);
            }
            return xRow;
        }

        private static List<double> GetOutputRow(double[][] data, BlackFoxMetadata metadata, int i, int outputCount, int inputOffset = 0)
        {
            List<double> yRow = new List<double>();
            for (int j = 0; j < outputCount; j++)
            {
                int c = inputOffset + j;
                int shift = metadata.OutputShifts[j];
                int window = metadata.OutputWindows[j];
                int offset = i + 1 + shift;
                double[] d = GetColumnData(data, offset - window, offset, c);
                yRow.AddRange(d);
            }

            return yRow;
        }
    }
}
