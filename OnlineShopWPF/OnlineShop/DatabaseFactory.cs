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

    #region headphones
    /// <summary>
    /// Writes the HardDrive to the DataBase
    /// </summary>
    /// <param name="db"></param>
    public void WriteHeadPhoneToDatabase(int productId, bool wireless, bool microphone)
    {
      if (DoesHeadPhoneAlreadyExist(productId))
        return;
      using (var createHeadPhone = _db.CreateNonQueryCommand(CommandAddHeadPhone))
      {
        createHeadPhone.AddParameter("$id", productId);
        createHeadPhone.AddParameter("$wireless", wireless);
        createHeadPhone.AddParameter("$microphone", microphone);
        createHeadPhone.Execute();
      }
    }

    private const string CommandAddHeadPhone = "INSERT INTO Headphones(product_id, wireless, microphone_included) VALUES($id,$wireless,$microphone)";

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
    public void WriteProductToDataBase(string name, double price)
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
    public void WriteGraphicCardToDatabase(int vram, string name)
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
    /// Writes the HardDrive to the DataBase
    /// </summary>
    public void WriteHardDriveToDatabase(string type, int memory)
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
    public void WriteCPUToDatabase(int count, double clockRate, string name)
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
    #endregion
  }
}
