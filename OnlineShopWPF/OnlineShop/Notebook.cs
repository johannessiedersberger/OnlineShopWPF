//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SQLite;

//namespace OnlineShop
//{
//  public static class OS
//  {
//    public static string windows = "windows";
//    public static string linux = "linux";
//    public static string macos = "macos";
//  }
                                                
//  /// <summary>
//  /// Notebook
//  /// </summary>
//  public class Notebook
//  {
   
//    public Graphic GraphicCard
//    {
      
//      get
//      {
//        if (DoesNotebookAlreadyExist() == false)
//          throw new InvalidOperationException("The Notebook has to be written to the database");

//        using (var getGrahpic = _database.CreateQueryCommand(CommandGetGraphicCard))
//        {
//          getGrahpic.AddParameter("$graphicId", _graphicId);
//          IReader r = getGrahpic.ExecuteReader();
          
//          return null;

//        }
//      }
//    }
//    private const string CommandGetGraphicCard = "SELECT * FROM Graphics WHERE graphic_id = $graphicId";


//    private int _productId;
//    private int _graphicId;
//    private int _cpuId;
//    private int _hardDriveId;
//    private int RamMemory;
//    private int AverageBatteryTime;
//    private string Os;
//    private IDatabase _database;

//    /// <summary>
//    /// Creates a new notebook in the databse
//    /// </summary>
//    /// <param name="productId">the product id</param>
//    /// <param name="graphicId">the notebook id</param>
//    /// <param name="cpuId">the cpu id</param>
//    /// <param name="hardDriveId">the graphic id</param>
//    /// <param name="ramMemory">the ram</param>
//    /// <param name="avgBatteryTime">the battery time</param>
//    /// <param name="os">the os</param>
//    public Notebook(int productId, int graphicId, int cpuId, int hardDriveId, int ramMemory, int avgBatteryTime, string os)
//    {
//      _productId = productId;
//      _graphicId = graphicId;
//      _cpuId = cpuId;
//      _hardDriveId = hardDriveId;
//      RamMemory = ramMemory;
//      AverageBatteryTime = avgBatteryTime;
//      Os = os;
//    }

//    /// <summary>
//    /// Writes the Notebook to the Database
//    /// </summary>
//    /// <param name="db">the database that will contain the cpu</param>
//    public void WriteToDataBase(IDatabase db)
//    {
//      _database = db;

//      if (DoesNotebookAlreadyExist())
//        return;
//      using (var createNotebook = _database.CreateNonQueryCommand(CommandCreateNotebook))
//      {
//        createNotebook.AddParameter("$id", _productId);
//        createNotebook.AddParameter("$graphicId", _graphicId);
//        createNotebook.AddParameter("$cpuId", _cpuId);
//        createNotebook.AddParameter("$hardDriveId", _hardDriveId);
//        createNotebook.AddParameter("$ramMemory", RamMemory);
//        createNotebook.AddParameter("$avgBatteryTime", AverageBatteryTime);
//        createNotebook.AddParameter("$os", Os);

//        int rowsAffected = createNotebook.Execute();
//        if (rowsAffected != 1)
//        {
//          throw new DataException();
//        }        
//      }
//    }

//    private const string CommandCreateNotebook = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";
//    private bool DoesNotebookAlreadyExist()
//    {
//      if (_database == null)
//        return false;

//      using (var getID = _database.CreateQueryCommand(CommandSelectID))
//      {
//        getID.AddParameter("$id", _productId);
//        IReader reader = getID.ExecuteReader();
//        reader.Read();
//        return reader.HasRows;
//      }
//    }
//    private const string CommandSelectID = "SELECT product_id FROM Notebooks WHERE product_id = $id";
//  }
//}

