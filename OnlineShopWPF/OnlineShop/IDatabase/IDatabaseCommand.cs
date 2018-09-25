using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// A Database command
  /// </summary>
  public interface IDatabaseCommand : IDisposable
  {
    /// <summary>
    /// Adds a single parameter to this command.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    void AddParameter(string name, object value);

    /// <summary>
    /// Gets the read only collection of parameters.
    /// </summary>
    IReadOnlyDictionary<string, object> Parameters { get; }
  }
}
