using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The Range 
  /// </summary>
  public class Range
  {
    /// <summary>
    /// Min value
    /// </summary>
    public double Min { get; private set; }
    /// <summary>
    /// Max Value
    /// </summary>
    public double Max { get; private set; }

    /// <summary>
    /// sets min and max
    /// </summary>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    public Range(double min, double max)
    {
      Min = min;
      Max = max;
    }
  }
}
