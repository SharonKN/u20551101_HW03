using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u20551101_HW03.Models
{
    public class ChartData
    {
        public int Year { get; set; }
        public string[] Labels { get; set; }
        public int[] Counts { get; set; }
    }

}