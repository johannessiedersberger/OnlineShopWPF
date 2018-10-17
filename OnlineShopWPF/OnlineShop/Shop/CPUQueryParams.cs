using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The CPU Query Params
  /// </summary>
  public class CPUQueryParams
  {
    /// <summary>
    /// The name of the CPU
    /// </summary>
    public string cpuName;
    /// <summary>
    /// The clock Rate of the CPU
    /// </summary>
    public Range? cpuClockRate;
    /// <summary>
    /// The number of cores in the cpu
    /// </summary>
    public Range? cpuCount;
  }
}
