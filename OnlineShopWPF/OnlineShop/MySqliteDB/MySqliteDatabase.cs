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

}
