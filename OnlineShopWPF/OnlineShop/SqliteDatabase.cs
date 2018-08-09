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
  public class SqliteDatabase : IDatabase
  {
    /// <summary>
    /// Connection to the Database
    /// </summary>
    public string ConnectionString { get; private set; }

    public SQLiteConnection Connection { get;private set;}
     
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
    public SqliteDatabase _db;
    private SQLiteCommand _command;

    public SqliteQueryCommand(SqliteDatabase db)
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

    public IReader ExecuteReader()
    {
      _command = new SQLiteCommand(_db.Connection);
      _command.CommandText = CommandText;
      foreach(var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }
      
      return new SqliteDataReader(_command);
    }
  }

  public class SqliteDataReader : IReader
  {
    private SQLiteCommand _command;

    public SqliteDataReader(SQLiteCommand command)
    {
      _command = command;
      SQLiteDataReader reader = _command.ExecuteReader();
      _values = ToDictionary(reader.GetValues());
    }

    public static IDictionary<string, string> ToDictionary(NameValueCollection col)
    {
      IDictionary<string, string> dict = new Dictionary<string, string>();
      foreach (var k in col.AllKeys)
      {
        dict.Add(k, col[k]);
      }
      return dict;
    }

    public IReadOnlyDictionary<string,string> Values
    {
      get { return new ReadOnlyDictionary<string, string>(_values); }
    }
    private readonly IDictionary<string,string> _values = new Dictionary<string, string>();

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
      foreach(var p in Parameters)
      {
        _command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
      }
      _command.ExecuteNonQuery();
    }
  }

}
