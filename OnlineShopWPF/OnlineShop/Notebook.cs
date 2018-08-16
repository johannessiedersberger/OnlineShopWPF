using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  /// <summary>
  /// Notebook
  /// </summary>
  public class Notebook
  {
    /// <summary>
    /// The ID of the product
    /// </summary>
    public int ProductId { get; private set; }
    /// <summary>
    /// The id of the Graphic Card
    /// </summary>
    public int GraphicId { get; private set; }
    /// <summary>
    /// The id of the cpu
    /// </summary>
    public int CpuId { get; private set; }
    /// <summary>
    /// The ID of the hard drive
    /// </summary>
    public int HardDriveId { get; private set; }
    /// <summary>
    /// The Ram Memory of the Notebook
    /// </summary>
    public int RamMemory { get; private set; }
    /// <summary>
    /// The average Battery time of the notebook
    /// </summary>
    public int AverageBatteryTime { get; private set; }
    /// <summary>
    /// The Os of the notebook
    /// </summary>
    public string Os { get; private set; }

    private IDatabase _database;

    /// <summary>
    /// Creates a new notebook in the databse
    /// </summary>
    /// <param name="productId">the product id</param>
    /// <param name="graphicId">the notebook id</param>
    /// <param name="cpuId">the cpu id</param>
    /// <param name="hardDriveId">the graphic id</param>
    /// <param name="ramMemory">the ram</param>
    /// <param name="avgBatteryTime">the battery time</param>
    /// <param name="os">the os</param>
    public Notebook(int productId, int graphicId, int cpuId, int hardDriveId, int ramMemory, int avgBatteryTime, string os)
    {
      ProductId = productId;
      GraphicId = graphicId;
      CpuId = cpuId;
      HardDriveId = hardDriveId;
      RamMemory = ramMemory;
      AverageBatteryTime = avgBatteryTime;
      Os = os;
    }

    /// <summary>
    /// Writes the Notebook to the Database
    /// </summary>
    /// <param name="db">the database that will contain the cpu</param>
    public void WriteToDataBase(IDatabase db)
    {
      _database = db;

      if (DoesNotebookAlreadyExist())
        return;
      using (var createNotebook = _database.CreateNonQueryCommand(CommandCreateNotebook))
      {
        createNotebook.AddParameter("$id", ProductId);
        createNotebook.AddParameter("$graphicId", GraphicId);
        createNotebook.AddParameter("$cpuId", CpuId);
        createNotebook.AddParameter("$hardDriveId", HardDriveId);
        createNotebook.AddParameter("$ramMemory", RamMemory);
        createNotebook.AddParameter("$avgBatteryTime", AverageBatteryTime);
        createNotebook.AddParameter("$os", Os);

        int rowsAffected = createNotebook.Execute();
        if (rowsAffected != 1)
        {
          throw new DataException();
        }
          
      }
    }
    private const string CommandCreateNotebook = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";
    private bool DoesNotebookAlreadyExist()
    {

      using (var getID = _database.CreateQueryCommand(CommandSelectID))
      {
        getID.AddParameter("$id", ProductId);
        IReader reader = getID.ExecuteReader();
        reader.Read();
        return reader.HasRows;
      }
    }
    private const string CommandSelectID = "SELECT product_id FROM Notebooks WHERE product_id = $id";
  }
}

