using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class PriceRange
  {
    /// <summary>
    /// Min value
    /// </summary>
    public Money Min { get; private set; }
    /// <summary>
    /// Max Value
    /// </summary>
    public Money Max { get; private set; }

    /// <summary>
    /// sets min and max
    /// </summary>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    public PriceRange(Money min, Money max)
    {
      Min = min;
      Max = max;
    }
  }
}

