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
        //_reader.Read();
        if (_reader.HasRows)
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
}
