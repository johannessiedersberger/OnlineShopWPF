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

  public interface INonQueryCommand : IDatabaseCommand
  {
    /// <summary>
    /// Executes this command
    /// </summary>
    int Execute();
  }

  public interface IQueryCommand : IDatabaseCommand
  {
    IReader ExecuteReader();
  }

  public interface IReader : IDisposable
  {
    IReadOnlyDictionary<string, object> Values { get; }

    bool Read();
    bool HasRows { get; }
    object this[int i] { get; }
  }
}
