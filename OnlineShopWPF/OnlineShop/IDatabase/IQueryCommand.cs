using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// A Sql Query Command that returns something
  /// </summary>
  public interface IQueryCommand : IDatabaseCommand
  {
    /// <summary>
    /// Executes the Reader 
    /// </summary>
    /// <returns></returns>
    IReader ExecuteReader();
  }
}
