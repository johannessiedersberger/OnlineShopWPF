using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class NotebookReader
  {
    /// <summary>
    /// Returns Notebooks by a Data Reader
    /// </summary>
    /// <param name="reader">the reader</param>
    /// <param name="db">the database</param>
    /// <returns></returns>
    public static List<Notebook> ReadForNotebooks(IReader reader, DatabaseFactory db)
    {
      var notebooks = new List<Notebook>();
      while (reader.TryReadNextRow(out object[] row))
      {
        var notebookRows = new List<string>();
        for (int i = 0; i < row.Length; i++)
        {
          notebookRows.Add(row[i].ToString());
        }
        notebooks.Add(new Notebook(
          product: db.GetProduct(int.Parse(notebookRows[0])), 
          graphic: db.GetGraphicCard(int.Parse(notebookRows[1])), 
          cpu: db.GetCPU(int.Parse(notebookRows[2])), 
          hardDrive: db.GetHardDrive(int.Parse(notebookRows[3])),
          ramMemory: int.Parse(notebookRows[4]), 
          avgBatteryTime: int.Parse(notebookRows[5]), 
          os: notebookRows[6])); 
      }
      return notebooks;
    }
  }
}
