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

  /// <summary>
  /// SqliteQuery that returns a value
  /// </summary>
  public class SqliteQueryCommand : IQueryCommand
  {
    /// <summary>
    /// The SqliteQueryCommand
    /// </summary>
    public string CommandText { get; set; }
    /// <summary>
    /// The Sqlite Database Object
    /// </summary>
    public SqliteDatabase DB { get; private set; }
    
    private SQLiteCommand _command;

    /// <summary>
    /// Saves the Database as a memeber
    /// </summary>
    /// <param name="db"></param>
    public SqliteQueryCommand(SqliteDatabase db)
    {
      DB = db;
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
      DB.Dispose();
    }

    /// <summary>
    /// Executes the Reader and returns a SqliteDataReaderObject
    /// </summary>
    /// <returns></returns>
    public IReader ExecuteReader()
    {
      _command = new SQLiteCommand(DB.Connection);
      _command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }

      return new SqliteDataReader(_command);
    }
  }

  /// <summary>
  /// SqliteReader that returns the data which was selected by the query
  /// </summary>
  public class SqliteDataReader : IReader
  {
    private SQLiteCommand _command;

    /// <summary>
    /// Saves the command as a member
    /// </summary>
    /// <param name="command"></param>
    public SqliteDataReader(SQLiteCommand command)
    {
      _command = command;
    }

    /// <summary>
    /// Gets the data from the column by the index
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
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

  /// <summary>
  /// Sqlite Command that changes the database
  /// </summary>
  public class SqliteNonQueryCommand : INonQueryCommand
  {
    /// <summary>
    /// The SqliteCommand 
    /// </summary>
    public string CommandText { get; set; }
    private SqliteDatabase _db;
    private SQLiteCommand _command;

    /// <summary>
    /// Save the database as a member
    /// </summary>
    /// <param name="db"></param>
    public SqliteNonQueryCommand(SqliteDatabase db)
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
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      _db.Dispose();
    }

    /// <summary>
    /// Executes the SqliteQuery
    /// </summary>
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
