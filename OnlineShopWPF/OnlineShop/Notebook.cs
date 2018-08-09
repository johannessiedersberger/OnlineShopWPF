//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SQLite;

//namespace OnlineShop
//{
//  public class NotebookData
//  {
//    public int ProductId;
//    public int GraphicId;
//    public int CpuId;
//    public int HardDriveId;
//    public int RamMemory;
//    public int AverageBatteryTime;
//    public string Os;
//  }

//  public class Notebook
//  {
//    /// <summary>
//    /// The Data of The Notebook
//    /// </summary>
//    public NotebookData NotebookData { get; private set; } 
    
//    private IDatabase database = new SqliteDatabase("OnlineShop.db");

//    /// <summary>
//    /// Creates a new Notebook in the Database
//    /// </summary>
//    /// <param name="data"></param>
//    public Notebook(NotebookData data)
//    {
//      NotebookData = data;
      
//      using (var createNotebook = database.CreateQueryCommand(CommandCreateNotebook))
//      {
//        database.Open();
//        createNotebook.Parameters.Add(database.CreateParameter("$id", null));
//        createNotebook.Parameters.Add(database.CreateParameter("$graphicId", data.GraphicId.ToString()));
//        createNotebook.Parameters.Add(database.CreateParameter("$cpuId", data.CpuId.ToString()));
//        createNotebook.Parameters.Add(database.CreateParameter("$hardDriveId", data.HardDriveId.ToString()));
//        createNotebook.Parameters.Add(database.CreateParameter("$ramMemory", data.RamMemory.ToString()));
//        createNotebook.Parameters.Add(database.CreateParameter("$avgBatteryTime", data.AverageBatteryTime.ToString()));
//        createNotebook.Parameters.Add(database.CreateParameter("$os", data.Os));
//        createNotebook.ExecuteNonQuery();
//        database.Dispose();
//      }
//    }

//    private const string CommandCreateNotebook = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";
//  }
//}
