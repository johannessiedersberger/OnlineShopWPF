using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class DatabaseFactory
  {
    private IDatabase _db;
    public DatabaseFactory(IDatabase db)
    {
      _db = db;
    }

    public List<Product> FindMatchingProducts(ProductQueryParams param)
    {
      if (param is NotebookQueryParams)
      {
        return NotebookQueries.FindMatchingNotebooks((NotebookQueryParams)param, _db);
      }
      if (param is HeadPhoneQueryParams)
      {
        return HeadPhoneQueries.FindMatchingHeadphone((HeadPhoneQueryParams)param, _db);
      }
      if (param is ProductQueryParams) //Has to be the last one
      {
        return ProductQueries.FindMatchingProduct(param, _db);
      }   
      return null;
    }

    #region headphones
    /// <summary>
    /// Writes the HardDrive to the DataBase
    /// </summary>
    /// <param name="db"></param>
    public void AddNewHeadPhoneToDatabase(int productId, bool wireless)
    {
      if (DoesHeadPhoneAlreadyExist(productId))
        return;
      using (var createHeadPhone = _db.CreateNonQueryCommand(CommandAddHeadPhone))
      {
        createHeadPhone.AddParameter("$id", productId);
        createHeadPhone.AddParameter("$wireless", wireless);
        createHeadPhone.Execute();
      }
    }

    private const string CommandAddHeadPhone = "INSERT INTO Headphones(product_id, wireless) VALUES($id,$wireless)";

    private bool DoesHeadPhoneAlreadyExist(int productId)
    {

      using (var getID = _db.CreateQueryCommand(CommandSelectID))
      {
        getID.AddParameter("$id", productId);
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }

    private const string CommandSelectID = "SELECT product_id FROM Headphones WHERE product_id = $id";
    #endregion

    #region products
    /// <summary>
    /// Writes the Product to the Database
    /// </summary>
    /// <param name="database">The database that contains the Products</param>
    public void AddProductToDataBase(string name, double price)
    {
      if (DoesProductAlreadyExist(name))
        return;
      using (var createProduct = _db.CreateNonQueryCommand(CommandAddProduct))
      {
        createProduct.AddParameter("$id", null);
        createProduct.AddParameter("$name", name);
        createProduct.AddParameter("$price", price);
        createProduct.Execute();
      }
    }

    private const string CommandAddProduct = "INSERT INTO Products(product_id, name, price) VALUES($id, $name, $price) ";

    private bool DoesProductAlreadyExist(string name)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectProductId))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }
    private const string CommandGetProduct = "SELECT name, price FROM PRODUCTS WHERE $product_id = product_id";

    public Product GetProduct(int productId)
    {
      using(var getProduct = _db.CreateQueryCommand(CommandGetProduct))
      {
        getProduct.AddParameter("$product_id", productId);
        IReader reader = getProduct.ExecuteReader();
        var result = new List<string>();
        while(reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new Product(productId, result[0], double.Parse(result[1]));
      }
    }

    /// <summary>
    /// Gets the ID from the Product
    /// </summary>
    public int GetProductId(string name)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectProductId))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();
        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new InvalidOperationException("The Given product could not be found");
      }

    }

    private const string CommandSelectProductId = "SELECT product_id FROM Products WHERE name = $name";

    /// <summary>
    /// Delets a Product from The Database
    /// </summary>
    /// <param name="id">The id from the product which sould be removed</param>
    public void DeleteProduct(int id)
    {
      using (var delete = _db.CreateNonQueryCommand(Delete))
      {
        delete.AddParameter("$id", id);
        delete.Execute();
      }
    }

    private const string Delete = "DELETE FROM Products WHERE product_id = $id";
    #endregion

    #region graphic
    /// <summary>
    /// Writes the Cpu to the databse
    /// </summary>
    /// <param name="db">The Database that will contain the cpu</param>
    public void AddGraphicToDataBase(int vram, string name)
    {
      if (DoesGraphicAlreadyExist(name))
        return;
      using (var createGraphic = _db.CreateNonQueryCommand(CommandAddGraphic))
      {
        createGraphic.AddParameter("$id", null);
        createGraphic.AddParameter("$vram", vram);
        createGraphic.AddParameter("$name", name);
        createGraphic.Execute();
      }
    }

    private const string CommandAddGraphic = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";

    private bool DoesGraphicAlreadyExist(string name)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectGraphicID))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }

    private const string CommandGetGraphicCard = "SELECT vram,name FROM Graphics WHERE $graphic_id = graphic_id";

    public Graphic GetGraphicCard(int graphicId)
    {
      using (var getGraphicCard = _db.CreateQueryCommand(CommandGetGraphicCard))
      {
        getGraphicCard.AddParameter("$graphic_id", graphicId);
        IReader reader = getGraphicCard.ExecuteReader();
        var result = new List<string>();
        while(reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new Graphic(int.Parse(result[0]), result[1]);
      }
    }

    /// <summary>
    /// Gets the ID from the Graphic-Card
    /// </summary>
    public int GetGraphicCardId(string name)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectGraphicID))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();

        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new InvalidOperationException("The given Graphic card could not be found");
      }
    }

    private const string CommandSelectGraphicID = "SELECT graphic_id FROM Graphics WHERE name = $name";
    #endregion

    #region hardDrive
    /// <summary>
    /// Adds a new hard drive to the database.
    /// </summary>
    public void AddNewHardDriveToDatabase(string type, int memory)
    {
      if (DoesHardDriveAlreadyExist(type, memory))
        return;
      using (var createHardDrive = _db.CreateNonQueryCommand(CommandAddHardDrive))
      {
        createHardDrive.AddParameter("$id", null);
        createHardDrive.AddParameter("$type", type);
        createHardDrive.AddParameter("$memory", memory);
        createHardDrive.Execute();
      }
    }

    private const string CommandAddHardDrive = "INSERT INTO HardDrives(hard_drive_id, type, memory) VALUES($id,$type,$memory)";

    public bool DoesHardDriveAlreadyExist(string type, int memory)
    {

      using (var getID = _db.CreateQueryCommand(CommandSelectIHardDriveID))
      {
        getID.AddParameter("$memory", memory);
        getID.AddParameter("$type", type);
        IReader reader = getID.ExecuteReader();

        return reader.TryReadNextRow(out object[] row);
      }
    }

    private const string CommandGetHardDrive = "SELECT memory, type FROM HardDrives WHERE $hard_drive_id = hard_drive_id";

    public HardDrive GetHardDrive(int hardDriveId)
    {
      using(var getHardDrive = _db.CreateQueryCommand(CommandGetHardDrive))
      {
        getHardDrive.AddParameter("$hard_drive_id", hardDriveId);
        IReader reader = getHardDrive.ExecuteReader();
        var result = new List<string>();
        while(reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new HardDrive(int.Parse(result[0]), result[1]);
      }
    }

    /// <summary>
    /// Gets the ID from the HardDrive
    /// </summary>
    public int GetHardDriveId(string type, int memory)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectIHardDriveID))
      {
        getID.AddParameter("$memory", memory);
        getID.AddParameter("$type", type);
        IReader reader = getID.ExecuteReader();

        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new InvalidOperationException("The Given gaphic card could not be found");
      }
    }

    private const string CommandSelectIHardDriveID = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";
    #endregion

    #region cpu
    private const string CommandSelectCPUID = "SELECT cpu_id FROM Cpu WHERE name = $name";

    /// <summary>
    /// Gets the ID from the CPU
    /// </summary>
    public int GetCpuId(string name)
    {

      using (var getID = _db.CreateQueryCommand(CommandSelectCPUID))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();
        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new InvalidOperationException("The Given cpu could not be found");
      }

    }

    /// <summary>
    /// Writes the CPU into the Database
    /// </summary>
    /// <param name="db">The Database that contains the cpu</param>
    public void AddNewCpuToDatabase(int count, double clockRate, string name)
    {
      if (DoesCPUAlreadyExist(name))
        return;
      using (var createCPU = _db.CreateNonQueryCommand(CommandAddCPU))
      {
        createCPU.AddParameter("$id", null);
        createCPU.AddParameter("$count", count);
        createCPU.AddParameter("$clockRate", clockRate);
        createCPU.AddParameter("$name", name);
        createCPU.Execute();
      }
    }

    private const string CommandAddCPU = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";

    public bool DoesCPUAlreadyExist(string name)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectCPUID))
      {
        getID.AddParameter("$name", name);
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }
    private const string CommandGetCPU = "SELECT count,clock_rate,name FROM CPU WHERE $cpu_id = cpu_id ";
    public CPU GetCPU(int cpuId)
    {
      using(var getCPU = _db.CreateQueryCommand(CommandGetCPU))
      {
        getCPU.AddParameter("cpu_id", cpuId);
        IReader reader = getCPU.ExecuteReader();
        var result = new List<string>();
        while(reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new CPU(int.Parse(result[0]), double.Parse(result[1]), result[2]);
      }
    }
    #endregion

    #region notebooks
    /// <summary>
    /// Writes the Notebook to the Database
    /// </summary>
    /// <param name="db">the database that will contain the cpu</param>
    public void AddNewNotebookToDatabase(int productId, int graphicId, int cpuId, int hardDriveid, int ramMemory, int avgBatteryTime, string os)
    {
      if (DoesNotebookAlreadyExist(productId))
        return;
      using (var createNotebook = _db.CreateNonQueryCommand(CommandCreateNotebook))
      {
        createNotebook.AddParameter("$id", productId);
        createNotebook.AddParameter("$graphicId", graphicId);
        createNotebook.AddParameter("$cpuId", cpuId);
        createNotebook.AddParameter("$hardDriveId", hardDriveid);
        createNotebook.AddParameter("$ramMemory", ramMemory);
        createNotebook.AddParameter("$avgBatteryTime", avgBatteryTime);
        createNotebook.AddParameter("$os", os);

        int rowsAffected = createNotebook.Execute();
        if (rowsAffected != 1)
        {
          throw new DataException();
        }
      }
    }

    private const string CommandCreateNotebook = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";
    public bool DoesNotebookAlreadyExist(int productId)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectNotebookId))
      {
        getID.AddParameter("$id", productId);
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }
    private const string CommandSelectNotebookId = "SELECT product_id FROM Notebooks WHERE product_id = $id";
    #endregion

   

   

  


  }
}
