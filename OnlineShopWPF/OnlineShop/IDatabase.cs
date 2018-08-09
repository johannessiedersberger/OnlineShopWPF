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
    void Execute();
  }

  public interface IQueryCommand : IDatabaseCommand
  {
    IReader ExecuteReader();
  }

  public interface IReader : IDisposable
  {
    IReadOnlyDictionary<string, string> Values { get; }
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

  class FakeQueryCommand : IQueryCommand // TODO: Complete Interface
  {
    public IReadOnlyDictionary<string, object> Parameters => throw new NotImplementedException();

    public void AddParameter(string name, object value)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public IReader ExecuteReader()
    {
      throw new NotImplementedException();
    }
  }

  class FakeDataBase : IDatabase
  {
    public INonQueryCommand CreateNonQueryCommand(string commandText)
    {
      return new FakeNonQueryCommand();
    }

    public void Dispose()
    {
      WasDisposed = true;
    }

    public IQueryCommand CreateQueryCommand(string commandText)
    {
      return new FakeQueryCommand();
    }

    public bool WasDisposed { get; private set; } = false;
  }
}
