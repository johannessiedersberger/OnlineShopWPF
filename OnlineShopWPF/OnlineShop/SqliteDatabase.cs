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
    public string ConnectionString { get; private set; }

    private SQLiteConnection _connection;
    
    
    public SqliteDatabase(string fileName)
    {
      ConnectionString = fileName;
      _connection = new SQLiteConnection(string.Format("Data Source = {0}; Version =3;", Shop.file));
    }

    public void Close()
    {
      _connection.Close();
    }

    public IDbCommand CreateCommand(string commandText)
    {
      SQLiteCommand command = new SQLiteCommand(_connection);
      command.CommandText = commandText;
      return command;
    }

    public IDataParameter CreateParameter(string parameterName, string value)
    {
      return new SQLiteParameter(parameterName, value);
    }

    public void Dispose()
    {
      _connection.Dispose();
    }

    public void Open()
    {
      _connection.Open();
    }
  }
}
