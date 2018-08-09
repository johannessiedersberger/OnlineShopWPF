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
  /// Database that uses SQLITE
  /// </summary>
  public class SqliteDatabase : IDatabase
  {
    /// <summary>
    /// Source of the Database
    /// </summary>
    public string ConnectionString { get; private set; }

    /// <summary>
    /// Connection to the Database
    /// </summary>
    public SQLiteConnection Connection { get; private set; }

    /// <summary>
    /// Creates the Connection to the Database
    /// </summary>
    /// <param name="fileName"></param>
    public SqliteDatabase(string fileName)
    {
      ConnectionString = fileName;
      Connection = new SQLiteConnection(string.Format("Data Source = {0}; Version =3;", Shop.file));
      Connection.Open();
    }

    /// <summary>
    /// Creates a new SQLITE command
    /// </summary>
    /// <param name="commandText">The command to execute</param>
    /// <returns></returns>
    public IQueryCommand CreateQueryCommand(string commandText)
    {
      var command = new SqliteQueryCommand(this);
      command.CommandText = commandText;
      return command;
    }

    /// <summary>
    /// Creats a new SqliteNonQueryCommand
    /// </summary>
    /// <param name="commandText">the command</param>
    /// <returns></returns>
    public INonQueryCommand CreateNonQueryCommand(string commandText)
    {
      var sqliteCommand = new SqliteNonQueryCommand(this);
      sqliteCommand.CommandText = commandText;
      return sqliteCommand;
    }

    /// <summary>
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      //Connection.Dispose();
    }
  }

  public class SqliteQueryCommand : IQueryCommand
  {
    public string CommandText { get; set; }
    public SqliteDatabase DB { get; private set; }
    public SQLiteCommand Command { get; private set; }

    public SqliteQueryCommand(SqliteDatabase db)
    {
      DB = db;
    }

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

    public void Dispose()
    {
      DB.Dispose();
    }

    public IReader ExecuteReader()
    {
      Command = new SQLiteCommand(DB.Connection);
      Command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        Command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }

      return new SqliteDataReader(Command);
    }
  }

  public class SqliteDataReader : IReader
  {
    private SQLiteCommand _command;

    public SqliteDataReader(SQLiteCommand command)
    {
      _command = command;
    }

    public object this[int i]
    {
      get
      {
        SQLiteDataReader reader = _command.ExecuteReader();
        reader.Read();
        return reader[i];
      }
    }

    public static IDictionary<string, object> ToDictionary(NameValueCollection col)
    {
      IDictionary<string, object> dict = new Dictionary<string, object>();

      foreach (var k in col.AllKeys)
      {
        dict.Add(k, col[k]);
      }
      return dict;
    }

    public IReadOnlyDictionary<string, object> Values
    {
      get { return new ReadOnlyDictionary<string, object>(_values); }
    }
    private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

    public void Dispose()
    {
      _command.Dispose();
    }
  }

  public class SqliteNonQueryCommand : INonQueryCommand
  {
    public string CommandText { get; set; }
    private SqliteDatabase _db;
    private SQLiteCommand _command;

    public SqliteNonQueryCommand(SqliteDatabase db)
    {
      _db = db;
    }

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

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Execute()
    {
      _command = new SQLiteCommand(_db.Connection);
      _command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }
      _command.ExecuteNonQuery();
    }
  }

}
