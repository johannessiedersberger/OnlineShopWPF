using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// Reads the Data of a Query 
  /// </summary>
  public interface IReader : IDisposable
  {
    /// <summary>
    /// Returns true if the query returns something and sets am object array with the result of the query
    /// </summary>
    /// <param name="row"></param>
    /// <returns>returns true if the query returns something</returns>
    bool TryReadNextRow(out object[] row);
    /// <summary>
    /// Returns An Array with the Column names
    /// </summary>
    string[] ColumnNames { get; }
  }
}
