using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// Interface for a database
  /// </summary>
  public interface IDatabase : IDisposable
  {
    /// <summary>
    /// Creates a sql query that returns something
    /// </summary>
    /// <param name="commandText">Query statement</param>
    /// <returns>A sql query that can be executed.</returns>
    IQueryCommand CreateQueryCommand(string commandText);

    /// <summary>
    /// Creates a sql query that returns nothing
    /// </summary>
    /// <param name="commandText"></param>
    /// <returns></returns>
    INonQueryCommand CreateNonQueryCommand(string commandText);

  }

  

  

 

  
}
