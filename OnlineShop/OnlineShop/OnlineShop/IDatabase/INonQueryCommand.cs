using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// A SqlQueryCommand that returns nothing
  /// </summary>
  public interface INonQueryCommand : IDatabaseCommand
  {
    /// <summary>
    /// Executes this command
    /// </summary>
    int Execute();
  }
}
