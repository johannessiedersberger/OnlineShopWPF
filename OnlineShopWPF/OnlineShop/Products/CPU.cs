using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
  /// <summary>
  /// A CPU from a Notebook
  /// </summary>
  public class CPU
  {
    /// <summary>
    /// The number of kernels
    /// </summary>
    public int Count { get; private set; }
    /// <summary>
    /// The Clock rate in GHZ
    /// </summary>
    public double ClockRate { get; private set; }
    /// <summary>
    /// The name of the CPU
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Saves the Parameters as members
    /// </summary>
    /// <param name="count">The number of kernels</param>
    /// <param name="clockRate">The clock rate in ghz </param>
    /// <param name="name">The name of the cpu </param>
    public CPU(int count, double clockRate, string name)
    {
      Count = count;
      ClockRate = clockRate;
      Name = name;
    }

    
  }
}
