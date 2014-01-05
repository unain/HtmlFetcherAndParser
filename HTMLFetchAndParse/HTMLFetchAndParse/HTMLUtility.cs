using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLFetchAndParse
{
    public static class HTMLUtility
    {
        public static Random r = new Random();
        public static double GetRandom()
        {
            return r.NextDouble();
        }
    }
}
