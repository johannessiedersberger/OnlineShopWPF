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
  /// Sqlite Command that changes the database
  /// </summary>
  public class MySqliteNonQueryCommand : INonQueryCommand
  {
    /// <summary>
    /// The SqliteCommand 
    /// </summary>
    public string CommandText { get; set; }

    private MySqliteDatabase _db;

    private SQLiteCommand _command;

    /// <summary>
    /// Save the database as a member
    /// </summary>
    /// <param name="db"></param>
    public MySqliteNonQueryCommand(MySqliteDatabase db)
    {
      _db = db;
    }

    /// <summary>
    /// Contains the Parameters from the query
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
    /// Ads a Parameter to the Dictionary
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

    /// <summary>
    /// Executes the SqliteQuery
    /// </summary>
    public int Execute()
    {
      _command = new SQLiteCommand(_db.Connection);
      _command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }
      return _command.ExecuteNonQuery();
    }

    public void Dispose()
    {
      _command.Dispose();
    }
  }
}
