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
  public class MySqliteDatabase : IDatabase
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
    public MySqliteDatabase(string fileName)
    {
      ConnectionString = fileName;
      Connection = new SQLiteConnection(string.Format("Data Source = {0}; Version =3;", fileName));
      Connection.Open();
    }

    /// <summary>
    /// Creates a new SQLITE command
    /// </summary>
    /// <param name="commandText">The command to execute</param>
    /// <returns></returns>
    public IQueryCommand CreateQueryCommand(string commandText)
    {
      var command = new MySqliteQueryCommand(this);
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
      var sqliteCommand = new MySqliteNonQueryCommand(this);
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

  /// <summary>
  /// SqliteReader that returns the data which was selected by the query
  /// </summary>
  public class MySqliteDataReader : IReader
  {
    private SQLiteCommand _command;
    private SQLiteDataReader _reader;
    /// <summary>
    /// Saves the command as a member
    /// Executes the reader 
    /// Saves The Values of the reader in the Dictionary
    /// </summary>
    /// <param name="command"></param>
    public MySqliteDataReader(SQLiteCommand command)
    {
      _command = command;
      _reader = _command.ExecuteReader();
    }

    /// <summary>
    /// Returns the names of the columns
    /// </summary>
    public string[] ColumnNames
    {
      get
      {
        var row = new List<string>();
        while (_reader.Read())
        {
          row = Enumerable.Range(0, _reader.FieldCount).Select(_reader.GetName).ToList();
        }
        return row.ToArray();
      }
    }

    /// <summary>
    /// Reads the next row
    /// </summary>
    /// <param name="row"></param>
    /// <returns>the query result</returns>
    public bool TryReadNextRow(out object[] row)
    {
      _reader.Read();

      if (_reader.HasRows)
      {
        var columns = new object[_reader.FieldCount];
        for (int i = 0; i < columns.Length; i++)
        {
          columns[i] = _reader[i];
        }

        row = columns;
        return true;
      }
      else
      {
        row = default(object[]);
        return false;
      }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
      _command.Dispose();
    }
  }

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
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      _db.Dispose();
    }

    /// <summary>
    /// Executes the SqliteQuery
    /// </summary>
    public int Execute()
    {
      SQLiteCommand _command = new SQLiteCommand(_db.Connection);
      _command.CommandText = CommandText;
      foreach (var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }
      return _command.ExecuteNonQuery();
    }
  }
}
