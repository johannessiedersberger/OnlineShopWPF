using System;
using System.Data.SQLite;

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
    public string DataBaseSourceFile { get; private set; }

    /// <summary>
    /// Connection to the Database
    /// </summary>
    public SQLiteConnection Connection { get; private set; }

    /// <summary>
    /// Creates the Connection to the Database
    /// </summary>
    /// <param name="filePath"></param>
    public MySqliteDatabase(string file)
    {
      DataBaseSourceFile = file;
      Connection = new SQLiteConnection(string.Format("Data Source = {0}; Version =3;", file));
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
    /// Finalizer.
    /// </summary>
    ~MySqliteDatabase()
    {
      Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the Database
    /// </summary>
    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Implementation to dispose of objects of this type.
    /// </summary>
    /// <remarks>
    /// When <paramref name="disposing"/> is <b>false</b> do not access reference types
    /// as they might be already garbage collected.
    /// </remarks>
    /// <param name="disposing"><b>true</b> if called from <see cref="IDisposable.Dispose"/>;
    /// <b>false</b> if called from finalizer.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        Connection.Dispose();
      }
    }
  }

}
