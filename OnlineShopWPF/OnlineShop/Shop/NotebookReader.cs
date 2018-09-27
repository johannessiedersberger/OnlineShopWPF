using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class NotebookReader
  {
    public static List<Notebook> ReadForNotebooks(IReader reader)
    {
      var notebooks = new List<Notebook>();
      while (reader.TryReadNextRow(out object[] row))
      {
        var notebookRows = new List<string>();
        for (int i = 0; i < row.Length; i++)
        {
          notebookRows.Add(row[i].ToString());
        }
        notebooks.Add(new Notebook(int.Parse(notebookRows[0]), int.Parse(notebookRows[1]), int.Parse(notebookRows[2]),
          int.Parse(notebookRows[3]), int.Parse(notebookRows[4]), int.Parse(notebookRows[5]), notebookRows[6]));
      }
      return notebooks;
    }
  }
}
