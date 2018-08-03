using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  class SqliteDatabase : IDatabase
  {
    /// <summary>
    /// Connection to the Database
    /// </summary>
    public string ConnectionString { get; private set; }

    private SQLiteConnection _connection;
     
    /// <summary>
    /// Creates the Connection to the Database
    /// </summary>
    /// <param name="fileName"></param>
    public SqliteDatabase(string fileName)
    {
      ConnectionString = fileName;
      _connection = new SQLiteConnection(string.Format("Data Source = {0}; Version =3;", Shop.file));
    }

    /// <summary>
    /// Closes the Databse
    /// </summary>
    public void Close()
    {
      _connection.Close();
    }

    /// <summary>
    /// Creates a new SQLITE command
    /// </summary>
    /// <param name="commandText">The command to execute</param>
    /// <returns></returns>
    public IDbCommand CreateCommand(string commandText)
    {
      SQLiteCommand command = new SQLiteCommand(_connection);
      command.CommandText = commandText;
      return command;
    }

    /// <summary>
    /// Creates a Parameter for a SqliteQuery
    /// </summary>
    /// <param name="parameterName">The parameter</param>
    /// <param name="value">The value</param>
    /// <returns></returns>
    public IDataParameter CreateParameter(string parameterName, string value)
    {
      return new SQLiteParameter(parameterName, value);
    }

    /// <summary>
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      _connection.Dispose();
    }

    /// <summary>
    /// Opens the Databse
    /// </summary>
    public void Open()
    {
      _connection.Open();
    }
  }
}
