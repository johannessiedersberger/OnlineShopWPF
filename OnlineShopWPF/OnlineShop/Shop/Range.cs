using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class Range
  {
    public double Min { get; private set; }
    public double Max { get; private set; }
    public Range(double min, double max)
    {
      Min = min;
      Max = max;
    }
  }
}
