using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class PackedData
    {
        public List<List<double>> Input { get; private set; }
        public List<List<double>> Output { get; private set; }

        public PackedData(List<List<double>> input, List<List<double>> ouput)
        {
            this.Input = input;
            this.Output = Output;
        }
    }
}
