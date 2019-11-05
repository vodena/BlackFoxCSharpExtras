using System;
using System.Collections.Generic;
using System.Text;

namespace BlackFoxExtras
{
    public class DataPadding
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public DataPadding(int left, int right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
