using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class DatabaseFactory : IDisposable
  {
    private IDatabase _db;

    /// <summary>
    /// Sets the Databse 
    /// </summary>
    /// <param name="db">the Databse</param>
    public DatabaseFactory(IDatabase db)
    {
      _db = db;
    }

    /// <summary>
    /// Searches for the Products in the Database
    /// </summary>
    /// <param name="param"></param>
    /// <returns>Returns a list of Products</returns>
    public List<Product> FindMatchingProducts(ProductQueryParams param)
    {
      if (param == null)
        throw new ArgumentNullException(nameof(param));

      if (param is NotebookQueryParams notebookParams)
      {
        return NotebookSearchQueries.FindMatchingNotebooks(notebookParams, _db);
      }
      else
      {
        return ProductSearchQueries.FindMatchingProduct(param, _db);
      }
    }

    /// <summary>
    /// Delete a complete Notebook with CPU/HD/Graphic/Product
    /// </summary>
    /// <param name="nb"></param>
    public void DeleteCompleteNotebook(Notebook nb)
    {
      try
      {
        DeleteHardDrive(nb.HardDrive);
      }
      catch (InvalidOperationException) { }
      try
      {
        DeleteGraphic(nb.Graphic);
      }
      catch (InvalidOperationException) { }
      try
      {
        DeleteCPU(nb.Cpu);
      }
      catch (InvalidOperationException) { }
      try
      {
        DeleteNotebook(nb);
      }
      catch (InvalidOperationException) { }
      try
      {
        DeleteProduct(nb);
      }
      catch (InvalidOperationException) { }
    }

    #region products
    /// <summary>
    /// Writes the Product to the Database
    /// </summary>
    /// <param name="database">The database that contains the Products</param>
    public void AddProductToDataBase(Product product)
    {
      if (DoesProductAlreadyExist(product.Name))
        throw new ProductAlreadyExistsException("The Product already exists in the Databse");
      using (var createProduct = _db.CreateNonQueryCommand(CommandAddProduct))
      {
        createProduct.AddParameter("$id", null);
        createProduct.AddParameter("$name", product.Name);
        createProduct.AddParameter("$price", product.Price);
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
      using (var getProduct = _db.CreateQueryCommand(CommandGetProduct))
      {
        getProduct.AddParameter("$product_id", productId);
        IReader reader = getProduct.ExecuteReader();
        var result = new List<string>();
        while (reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new Product(result[0], double.Parse(result[1]));
      }
    }

    /// <summary>
    /// Gets the ID from the Product
    /// </summary>
    public int GetProductId(Product product)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectProductId))
      {
        getID.AddParameter("$name", product.Name);
        IReader reader = getID.ExecuteReader();
        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new ProductNotFoundException("The Given product could not be found");
      }
    }

    private const string CommandSelectProductId = "SELECT product_id FROM Products WHERE name = $name";

    /// <summary>
    /// Delets a Product from The Database
    /// </summary>
    /// <param name="productId">The id from the product which sould be removed</param>
    public void DeleteProduct(Product product)
    {
      int productId = GetProductId(product);
      using (var delete = _db.CreateNonQueryCommand(Delete))
      {
        delete.AddParameter("$id", productId);
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
    public void AddGraphicToDataBase(Graphic graphic)
    {
      if (DoesGraphicAlreadyExist(graphic.Name))
        throw new ProductAlreadyExistsException("The Graphic Card already exists in the Database");
      using (var createGraphic = _db.CreateNonQueryCommand(CommandAddGraphic))
      {
        createGraphic.AddParameter("$id", null);
        createGraphic.AddParameter("$vram", graphic.VRAM);
        createGraphic.AddParameter("$name", graphic.Name);
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

    /// <summary>
    /// Gets a Graphic Card by the Id
    /// </summary>
    /// <param name="graphicId"></param>
    /// <returns>returns a graphic card object</returns>
    public Graphic GetGraphicCard(int graphicId)
    {
      using (var getGraphicCard = _db.CreateQueryCommand(CommandGetGraphicCard))
      {
        getGraphicCard.AddParameter("$graphic_id", graphicId);
        IReader reader = getGraphicCard.ExecuteReader();
        var result = new List<string>();
        while (reader.TryReadNextRow(out object[] row))
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
    /// Delets a Graphic Card from The Database
    /// </summary>
    /// <param name="graphicId">The id from the product which sould be removed</param>
    public void DeleteGraphic(Graphic graphic)
    {
      int graphicId = GetGraphicCardId(graphic);
      if (CheckIfGraphicIsUsedInTWONotebook(graphicId))
        throw new InvalidOperationException("The graphic Card could not be deleted because it is used in two or more notebooks");
      using (var delete = _db.CreateNonQueryCommand(CommandDeleteGraphic))
      {
        delete.AddParameter("$id", graphicId);
        delete.Execute();
      }
    }

    private const string CommandDeleteGraphic = "DELETE FROM Graphics WHERE graphic_id = $id";

    /// <summary>
    /// Gets the ID from the Graphic-Card
    /// </summary>
    public int GetGraphicCardId(Graphic graphic)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectGraphicID))
      {
        getID.AddParameter("$name", graphic.Name);
        IReader reader = getID.ExecuteReader();

        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new ProductNotFoundException("The given Graphic card could not be found");
      }
    }

    private const string CommandSelectGraphicID = "SELECT graphic_id FROM Graphics WHERE name = $name";

    private bool CheckIfGraphicIsUsedInTWONotebook(int graphicId)
    {
      using (var delete = _db.CreateQueryCommand(CommandCheckGraphicUse))
      {
        delete.AddParameter("$id", graphicId);
        IReader reader = delete.ExecuteReader();
        int i = 0;
        while (reader.TryReadNextRow(out object[] row))
        {
          i++;
        }
        return i > 1;
      }
    }
    private const string CommandCheckGraphicUse =
      "SELECT product_id FROM Notebooks AS n" +
        " INNER JOIN Graphics As g ON g.graphic_id = n.graphic_id";

    #endregion

    #region hardDrive
    /// <summary>
    /// Adds a new hard drive to the database.
    /// </summary>
    public void AddNewHardDriveToDatabase(HardDrive hardDrive)
    {
      if (DoesHardDriveAlreadyExist(hardDrive.Type, hardDrive.Memory))
        throw new ProductAlreadyExistsException("the HardDrive already exists in the Database");
      using (var createHardDrive = _db.CreateNonQueryCommand(CommandAddHardDrive))
      {
        createHardDrive.AddParameter("$id", null);
        createHardDrive.AddParameter("$type", hardDrive.Type);
        createHardDrive.AddParameter("$memory", hardDrive.Memory);
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

    /// <summary>
    /// Gets a HardDrive by the Id
    /// </summary>
    /// <param name="hardDriveId"></param>
    /// <returns></returns>
    public HardDrive GetHardDrive(int hardDriveId)
    {
      using (var getHardDrive = _db.CreateQueryCommand(CommandGetHardDrive))
      {
        getHardDrive.AddParameter("$hard_drive_id", hardDriveId);
        IReader reader = getHardDrive.ExecuteReader();
        var result = new List<string>();
        while (reader.TryReadNextRow(out object[] row))
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
    public int GetHardDriveId(HardDrive hardDrive)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectIHardDriveID))
      {
        getID.AddParameter("$memory", hardDrive.Memory);
        getID.AddParameter("$type", hardDrive.Type);
        IReader reader = getID.ExecuteReader();

        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new ProductNotFoundException("The Given hard drive could not be found");
      }
    }
    private const string CommandSelectIHardDriveID = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";

    /// <summary>
    /// Delets a HardDrive from The Database
    /// </summary>
    /// <param name="hardDrive">The id from the product which sould be removed</param>
    public void DeleteHardDrive(HardDrive hardDrive)
    {
      int hardDriveId = GetHardDriveId(hardDrive);
      if (CheckIfHardDriveIsUsedInTWONotebook(hardDriveId))
        throw new InvalidOperationException("The HardDrive is used in two or more notebooks and could not be deleted");
      using (var delete = _db.CreateNonQueryCommand(CommandDeleteHardDrive))
      {
        delete.AddParameter("$id", hardDriveId);
        delete.Execute();
      }
    }
    private const string CommandDeleteHardDrive = "DELETE FROM HardDrives WHERE hard_drive_id = $id";

    private bool CheckIfHardDriveIsUsedInTWONotebook(int hardDriveId)
    {
      using (var delete = _db.CreateQueryCommand(CommandCheckHardDriveUse))
      {
        delete.AddParameter("$id", hardDriveId);
        IReader reader = delete.ExecuteReader();
        int i = 0;
        while (reader.TryReadNextRow(out object[] row))
        {
          i++;
        }
        return i > 1;
      }
    }
    private const string CommandCheckHardDriveUse =
      "SELECT product_id FROM Notebooks AS n" +
        " INNER JOIN HardDrives As h ON h.hard_drive_id = n.hard_drive_id ";
    #endregion

    #region cpu
    private const string CommandSelectCPUID = "SELECT cpu_id FROM Cpu WHERE name = $name";

    /// <summary>
    /// Gets the ID from the CPU
    /// </summary>
    public int GetCpuId(CPU cpu)
    {

      using (var getID = _db.CreateQueryCommand(CommandSelectCPUID))
      {
        getID.AddParameter("$name", cpu.Name);
        IReader reader = getID.ExecuteReader();
        while (reader.TryReadNextRow(out var row))
          return int.Parse(row[0].ToString());
        throw new ProductNotFoundException("The Given cpu could not be found");
      }

    }

    /// <summary>
    /// Writes the CPU into the Database
    /// </summary>
    /// <param name="db">The Database that contains the cpu</param>
    public void AddNewCpuToDatabase(CPU cpu)
    {
      if (DoesCPUAlreadyExist(cpu.Name))
        throw new ProductAlreadyExistsException("The CPU already exists in the Database");
      using (var createCPU = _db.CreateNonQueryCommand(CommandAddCPU))
      {
        createCPU.AddParameter("$id", null);
        createCPU.AddParameter("$count", cpu.Count);
        createCPU.AddParameter("$clockRate", cpu.ClockRate);
        createCPU.AddParameter("$name", cpu.Name);
        createCPU.Execute();
      }
    }

    private const string CommandAddCPU = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";

    /// <summary>
    /// Check if the CPU already exists
    /// </summary>
    /// <param name="name"></param>
    /// <returns>the name of the cpu</returns>
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

    /// <summary>
    /// Gets the CPU from the Database
    /// </summary>
    /// <param name="cpuId">the cpu id </param>
    /// <returns>Returns a CPU object</returns>
    public CPU GetCPU(int cpuId)
    {
      using (var getCPU = _db.CreateQueryCommand(CommandGetCPU))
      {
        getCPU.AddParameter("$cpu_id", cpuId);
        IReader reader = getCPU.ExecuteReader();
        var result = new List<string>();
        while (reader.TryReadNextRow(out object[] row))
        {
          for (int i = 0; i < row.Length; i++)
          {
            result.Add(row[i].ToString());
          }
        }
        return new CPU(int.Parse(result[0]), double.Parse(result[1]), result[2]);
      }
    }

    /// <summary>
    /// Delets a HardDrive from The Database
    /// </summary>
    /// <param name="cpu">The id from the product which sould be removed</param>
    public void DeleteCPU(CPU cpu)
    {
      int cpuId = GetCpuId(cpu);
      if (CheckIfCPUIsUsedInTWONotebook(cpuId))
        return;
      using (var delete = _db.CreateNonQueryCommand(CommandDeleteCPU))
      {
        delete.AddParameter("$id", cpuId);
        delete.Execute();
      }
    }
    private const string CommandDeleteCPU = "DELETE FROM CPU WHERE cpu_id = $id";

    private bool CheckIfCPUIsUsedInTWONotebook(int cpuId)
    {
      using (var delete = _db.CreateQueryCommand(CommandCheckCPUUse))
      {
        delete.AddParameter("$id", cpuId);
        IReader reader = delete.ExecuteReader();
        int i = 0;
        while (reader.TryReadNextRow(out object[] row))
        {
          i++;
        }
        return i > 1;
      }
    }
    private const string CommandCheckCPUUse =
      "SELECT product_id FROM Notebooks AS n" +
        " INNER JOIN CPU As c ON $id = n.cpu_id";


    #endregion

    #region notebooks
    /// <summary>
    /// Writes the Notebook to the Database
    /// </summary>
    /// <param name="db">the database that will contain the cpu</param>
    public void AddNewNotebookToDatabase(Notebook notebook)
    {
      AddNotebookComponents(notebook);

      if (DoesNotebookAlreadyExist(notebook))
        throw new ProductAlreadyExistsException();

      using (var createNotebook = _db.CreateNonQueryCommand(CommandCreateNotebook))
      {
        createNotebook.AddParameter("$id", GetProductId(notebook));
        createNotebook.AddParameter("$graphicId", GetGraphicCardId(notebook.Graphic));
        createNotebook.AddParameter("$cpuId", GetCpuId(notebook.Cpu));
        createNotebook.AddParameter("$hardDriveId", GetHardDriveId(notebook.HardDrive));
        createNotebook.AddParameter("$ramMemory", notebook.Ram);
        createNotebook.AddParameter("$avgBatteryTime", notebook.AverageBatteryTime);
        createNotebook.AddParameter("$os", notebook.Os);

        int rowsAffected = createNotebook.Execute();
        if (rowsAffected != 1)
        {
          throw new DataException();
        }
      }
    }

    private const string CommandCreateNotebook = "INSERT INTO Notebooks(product_id, graphic_id, cpu_id, hard_drive_id, ram_memory, average_battery_time, os) VALUES($id, $graphicId, $cpuId ,$hardDriveId, $ramMemory, $avgBatteryTime, $os) ";

    private void AddNotebookComponents(Notebook notebook)
    {
      try
      {
        AddProductToDataBase(notebook);
      }
      catch (ProductAlreadyExistsException) { }
      try
      {
        AddGraphicToDataBase(notebook.Graphic);
      }
      catch (ProductAlreadyExistsException) { }
      try
      {
        AddNewHardDriveToDatabase(notebook.HardDrive);
      }
      catch (ProductAlreadyExistsException) { }
      try
      {
        AddNewCpuToDatabase(notebook.Cpu);
      }
      catch (ProductAlreadyExistsException) { }
    }

    /// <summary>
    /// Checks if the Notebook Already exists in the Databse
    /// </summary>
    /// <param name="notebook">the notebook to seach for</param>
    /// <returns></returns>
    public bool DoesNotebookAlreadyExist(Notebook notebook)
    {
      using (var getID = _db.CreateQueryCommand(CommandSelectNotebookId))
      {
        getID.AddParameter("$id", GetProductId(notebook));
        IReader reader = getID.ExecuteReader();
        return reader.TryReadNextRow(out object[] row);
      }
    }
    private const string CommandSelectNotebookId = "SELECT product_id FROM Notebooks WHERE product_id = $id";

    /// <summary>
    /// Delets a Notebook from The Database
    /// </summary>
    /// <param name="id">The id from the product which sould be removed</param>
    public void DeleteNotebook(Notebook notebook)
    {
      int productId = GetProductId(notebook);
      using (var delete = _db.CreateNonQueryCommand(CommandDeleteNotebook))
      {
        delete.AddParameter("$id", productId);
        delete.Execute();
      }
    }
    private const string CommandDeleteNotebook = "DELETE FROM Notebooks WHERE product_id = $id";

    /// <summary>
    /// Gets the notebooks by the product 
    /// </summary>
    /// <param name="products"></param>
    /// <returns> Returns a list of notebooks</returns>
    public List<Notebook> GetNotebooks(List<Product> products)
    {
      var notebooks = new List<Notebook>();
      foreach (Product product in products)
      {
        notebooks.Add(GetNotebook(product));
      }
      return notebooks;
    }

    /// <summary>
    /// Gets a Notebook By the prodcuct
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Returns a Notebook Object</returns>
    public Notebook GetNotebook(Product product)
    {
      int productId = GetProductId(product);
      using (var getNotebook = _db.CreateQueryCommand(CommandGetNotebook))
      {
        getNotebook.AddParameter("$id", productId);
        IReader reader = getNotebook.ExecuteReader();
        Notebook nb = NotebookReader.ReadForNotebooks(reader, this)[0];
        if (nb != null)
          return nb;
        else
          throw new ProductNotFoundException();
      }
    }

    public void Dispose()
    {
      _db.Dispose();
    }

    private const string CommandGetNotebook = "SELECT * FROM Notebooks WHERE product_id = $id";
    #endregion

  }
}

