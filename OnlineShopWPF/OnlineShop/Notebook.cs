using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  public class NotebookData
  {
    public int ProductId;
    public int GraphicId;
    public int CpuId;
    public int HardDriveId;
    public int RamMemory;
    public int AverageBatteryTime;
    public string Os;
  }

  public class Notebook
  {
    public NotebookData NotebookData { get; private set; } 
    
    public Notebook(NotebookData data)
    {
      NotebookData = data;

      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$graphicId", data.GraphicId);
        command.Parameters.AddWithValue("$cpuId", data.CpuId);
        command.Parameters.AddWithValue("$hardDriveId", data.HardDriveId);
        command.Parameters.AddWithValue("$ramMemory", data.RamMemory);
        command.Parameters.AddWithValue("$avgBatteryTime", data.AverageBatteryTime);
        command.Parameters.AddWithValue("$os", data.Os);

        command.ExecuteNonQuery();
      }
    }
  }
}
