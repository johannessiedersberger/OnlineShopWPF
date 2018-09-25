using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OnlineShop
{
  /// <summary>
  /// SqliteQuery that returns a value
  /// </summary>
  public class MySqliteQueryCommand : IQueryCommand
  {
    /// <summary>
    /// The SqliteQueryCommand
    /// </summary>
    public string CommandText { get; set; }
    /// <summary>
    /// The Sqlite Database Object
    /// </summary>
    private MySqliteDatabase _db;

    private SQLiteCommand _command;

    /// <summary>
    /// Saves the Database as a memeber
    /// </summary>
    /// <param name="db"></param>
    public MySqliteQueryCommand(MySqliteDatabase db)
    {
      _db = db;
    }

    /// <summary>
    /// Contains the Parameters from the Query
    /// </summary>
    public IReadOnlyDictionary<string, object> Parameters
    {
      get
      {
        return new ReadOnlyDictionary<string, object>(_parameters);
      }
    }
    private IDictionary<string, object> _parameters = new Dictionary<string, object>();

    /// <summary>
    /// Ads a Parameter to the Parameter Dicitionary
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

    /// <summary>
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      _db.Dispose();
    }

    /// <summary>
    /// Executes the Reader and returns a SqliteDataReaderObject
    /// </summary>
    /// <returns></returns>
    public IReader ExecuteReader()
    {
      _command = new SQLiteCommand(_db.Connection);
      _command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }

      return new MySqliteDataReader(_command);
    }
  }
}
