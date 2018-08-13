using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop;

namespace OnlineShopTest
{
  public class FakeDataBase : IDatabase
  {
    public List<FakeNonQueryCommand> NonQueries { get; private set; } = new List<FakeNonQueryCommand>();
    public List<FakeQueryCommand> Queryies { get; private set; } = new List<FakeQueryCommand>();

    public INonQueryCommand CreateNonQueryCommand(string commandText)
    {
      FakeNonQueryCommand command = new FakeNonQueryCommand();
      NonQueries.Add(command);
      return command;
    }

    public IQueryCommand CreateQueryCommand(string commandText)
    {
      FakeQueryCommand command = new FakeQueryCommand();
      Queryies.Add(command);
      return command;
    }

    #region Dispose
    public void Dispose()
    {
      WasDisposed = true;
    }
    public bool WasDisposed { get; private set; } = false;
    #endregion

  }

  public class FakeNonQueryCommand : INonQueryCommand
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

  public class FakeQueryCommand : IQueryCommand // TODO: Complete Interface
  {
    public IReadOnlyDictionary<string, object> Parameters
    {
      get
      {
        return new ReadOnlyDictionary<string, object>(_parameters);
      }
    }
    private IDictionary<string, object> _parameters = new Dictionary<string, object>();

    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

    #region IDispoable implementation
    public void Dispose()
    {
      WasDisposed = true;
    }
    public bool WasDisposed { get; private set; } = false;
    #endregion

    public IReader ExecuteReader()
    {
      return new FakeDataReader();
    }
  }

  public class FakeDataReader : IReader
  {
    public object this[int i]
    {
      get
      {
        return 0;
      }
    }

    public IReadOnlyDictionary<string, object> Values { get; private set; } = new Dictionary<string, object>();

    public void Dispose()
    {
      
    }
  }
}
