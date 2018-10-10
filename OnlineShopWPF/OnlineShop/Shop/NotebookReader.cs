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
        var notebookRows = new Dictionary<string,string>();
        string[] columnNames = reader.ColumnNames;
        for (int i = 0; i < row.Length; i++)
        {
          notebookRows.Add(columnNames[i], row[i].ToString());
        }
        
        notebooks.Add(new Notebook(
          product: db.GetProduct(int.Parse(notebookRows["product_id"])), 
          graphic: db.GetGraphicCard(int.Parse(notebookRows["graphic_id"])), 
          cpu: db.GetCPU(int.Parse(notebookRows["cpu_id"])), 
          hardDrive: db.GetHardDrive(int.Parse(notebookRows["hard_drive_id"])),
          ramMemory: int.Parse(notebookRows["ram_memory"]), 
          avgBatteryTime: int.Parse(notebookRows["average_battery_time"]), 
          os: ParseEnum(notebookRows["os"]))); 
      }
      return notebooks;
    }
    private static OS ParseEnum(string osDbString)
    {
      Enum.TryParse(osDbString, out OS os);
      return os;
    }
  }
}
