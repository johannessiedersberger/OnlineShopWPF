using System;
using System.Collections.Generic;
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
    /// Information of connecting to a database.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Creates a sql query.
    /// </summary>
    /// <param name="commandText">Query statement</param>
    /// <returns>A sql query that can be executed.</returns>
    IDbCommand CreateCommand(string commandText);

    /// <summary>
    /// Creates parameters for a sql query.
    /// </summary>
    /// <param name="parameterName">The parameter to change</param>
    /// <param name="value">The value for the parameter</param>
    /// <returns></returns>
    IDataParameter CreateParameter(string parameterName, string value);

    /// <summary>
    /// Opens the database.
    /// </summary>
    void Open();

    /// <summary>
    /// Closes the database.
    /// </summary>
    void Close();
  }
}
