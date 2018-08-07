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
    /// Information of connecting to a database.
    /// </summary>
    string ConnectionString { get; } // TODO: weg

    /// <summary>
    /// Creates a sql query.
    /// </summary>
    /// <param name="commandText">Query statement</param>
    /// <returns>A sql query that can be executed.</returns>
    IDbCommand CreateCommand(string commandText);

    // INonQueryCommand CreateNonQueryCommand(string commandText);

    /// <summary>
    /// Creates parameters for a sql query.
    /// </summary>
    /// <param name="parameterName">The parameter to change</param>
    /// <param name="value">The value for the parameter</param>
    /// <returns></returns>
    IDataParameter CreateParameter(string parameterName, string value); // TODO: vom command

    /// <summary>
    /// Opens the database.
    /// </summary>
    void Open(); // TODO: das macht der ctor

    /// <summary>
    /// Closes the database.
    /// </summary>
    void Close(); // => Dispose();
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
    /// Executes this command.
    /// </summary>
    void Execute();
  }


  class FakeNonQueryCommand : INonQueryCommand
  {
    #region IDisposable Implementation

    public void Dispose()
    {
      IsDisposed = true;
    }

    public bool IsDisposed { get; private set; } = false;

    #endregion

    #region AddParameter

    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

    #endregion

    #region Parameters

    public IReadOnlyDictionary<string, object> Parameters
    {
      get
      {
        return new ReadOnlyDictionary<string, object>(_parameters);
      }
    }
    private IDictionary<string, object> _parameters = new Dictionary<string, object>();

    #endregion

    #region Execute

    public void Execute()
    {
      WasExecuted = true;
    }

    public bool WasExecuted { get; private set; } = false;

    #endregion
  }
}
