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
        return FindMatchingNotebooks((NotebookQueryParams)param);
      }
      if(param is ProductQueryParams)
      {
        return FindMatchingProduct(param);
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

    #region productQueries
    private List<Product> FindMatchingProduct(ProductQueryParams productQueryParams)
    {
      List<IQueryPart> querieParts = GetQuerypartsProduct(productQueryParams);

      string CommandGetNotebooks = string.Format("SELECT * FROM " +
        "  ( {0} ) AS PID " +
        " INNER JOIN Products As p ON p.product_id = PID.product_id", CreateQueryText(querieParts));

      var products = new List<Product>();
      using (var getNotebook = _db.CreateQueryCommand(CommandGetNotebooks))
      {
        SetQueryParameters(getNotebook, querieParts);
        IReader reader = getNotebook.ExecuteReader();
        while (reader.TryReadNextRow(out object[] row))
        {
          var productRows = new List<string>();
          for (int i = 0; i < row.Length; i++)
          {
            productRows.Add(row[i].ToString());
          }
          products.Add(new Product(int.Parse(productRows[1]), productRows[2], double.Parse(productRows[3])));
        }
        return products;
      }
    }

    private List<IQueryPart> GetQuerypartsProduct(ProductQueryParams param)
    {
      var querieParts = new List<IQueryPart>();
      CheckProduct(querieParts, param);
      return querieParts;
    }

    private static void CheckProduct(List<IQueryPart> queryParts, ProductQueryParams param)
    {
      if (param.Name != null)
        queryParts.Add(GetProductsByName(param.Name));
      if (param.Price != null)
        queryParts.Add(GetProductsByPriceQuery(param.Price));
    }
    
    private static IQueryPart GetProductsByName(string name)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetProductsByName);
      getNotebook.AddParameter("$productName", "%" + name + "%");

      return getNotebook;
    }
    private const string CommandGetProductsByName =
        "SELECT p.product_id FROM Products AS p WHERE p.name LIKE $productName";

    private static IQueryPart GetProductsByPriceQuery(Range range)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetProductsByPriceQuery);
      getNotebook.AddParameter("$minProductPrice", range.Min);
      getNotebook.AddParameter("$maxProductPrice", range.Max);
      return getNotebook;
    }
    private const string CommandGetProductsByPriceQuery =
        "SELECT p.product_id FROM Products AS p WHERE p.price BETWEEN $minProductPrice AND $maxProductPrice";
    #endregion

    #region notebookQueries
    private List<Product> FindMatchingNotebooks(NotebookQueryParams notebookSearchData)
    {
      List<IQueryPart> querieParts = GetQueryPartsNotebook(notebookSearchData);
      string CommandGetNotebooks =string.Format( "SELECT * FROM " +
        "  ( {0} ) AS PID " +
        " INNER JOIN Products As p ON p.product_id = PID.product_id", CreateQueryText(querieParts));
               
      var notebooks = new List<Product>();
      using (var getNotebook = _db.CreateQueryCommand(CommandGetNotebooks))
      {
        SetQueryParameters(getNotebook, querieParts);
        IReader reader = getNotebook.ExecuteReader();
        while (reader.TryReadNextRow(out object[] row))
        {
          var notebookRows = new List<string>();
          for (int i = 0; i < row.Length; i++)
          {
            notebookRows.Add(row[i].ToString());
          }
          notebooks.Add(new Product(int.Parse(notebookRows[1]), notebookRows[2], double.Parse(notebookRows[3])));
        }
        return notebooks;
      }
    }

    private List<IQueryPart> GetQueryPartsNotebook(NotebookQueryParams param)
    {
      var queryParts = new List<IQueryPart>();
      FillQueryPartsWithGraphicQueries(queryParts, param);
      FillQueryPartsWithCPUQueries(queryParts, param);
      FillQueryPartsWithHardDriveQueries(queryParts, param);
      FillQueryPartsWithNotebookQuery(queryParts, param);
      FillQueryPartsWithProductQuery(queryParts, param);
      return queryParts;     
    }  

    private static void FillQueryPartsWithProductQuery(List<IQueryPart> queryParts, ProductQueryParams param)
    {
      if (param.Price != null)
        queryParts.Add(GetNotebooksByPriceQuery(param.Price));
      if (param.Name != null)
        queryParts.Add(GetNotebooksByName(param.Name));
    }

    private static void FillQueryPartsWithGraphicQueries(List<IQueryPart> queryParts, NotebookQueryParams param)
    {
      if (param.GraphicQueryParams == null)
        return;
      if (param.GraphicQueryParams.graphicCardName != null)
        queryParts.Add(GetNotebooksByGraphicCardName(param.GraphicQueryParams.graphicCardName));
      if (param.GraphicQueryParams.vramRange != null)
        queryParts.Add(GetNotebooksByVRAMMemory(param.GraphicQueryParams.vramRange));
    }

    private static void FillQueryPartsWithHardDriveQueries(List<IQueryPart> queryParts, NotebookQueryParams param)
    {
      if (param.HardDriveQueryParams == null)
        return;
      if (param.HardDriveQueryParams.hdMemoryRange != null)
        queryParts.Add(GetNotebooksByhardDriveMemory(param.HardDriveQueryParams.hdMemoryRange));
      if (param.HardDriveQueryParams.hdType != null)
        queryParts.Add(GetNotebooksByhardDriveType(param.HardDriveQueryParams.hdType));
    }

    private static void FillQueryPartsWithCPUQueries(List<IQueryPart> queryParts, NotebookQueryParams param)
    {
      if (param.CPUQueryParams == null)
        return;
      if (param.CPUQueryParams.cpuCount != null)
        queryParts.Add(GetNotebooksByCpuCountQuery(param.CPUQueryParams.cpuCount));
      if (param.CPUQueryParams.cpuName != null)
        queryParts.Add(GetNotebooksByCPUNameQuery(param.CPUQueryParams.cpuName));
      if (param.CPUQueryParams.cpuClockRate != null)
        queryParts.Add(GetNotebooksByCPUClockRateQuery(param.CPUQueryParams.cpuClockRate));
    }

    private static void FillQueryPartsWithNotebookQuery(List<IQueryPart> queryParts, NotebookQueryParams param)
    {
      if (param.NotebookDataQueryParams == null)
        return;
      if (param.NotebookDataQueryParams.priceRange != null)
        queryParts.Add(GetNotebooksByPriceQuery(param.NotebookDataQueryParams.priceRange));
      if (param.NotebookDataQueryParams.os != null)
        queryParts.Add(GetNotebooksByOsQuery(param.NotebookDataQueryParams.os));
      if (param.NotebookDataQueryParams.batteryTimeRange != null)
        queryParts.Add(GetNotebooksByBatteryTimeQuery(param.NotebookDataQueryParams.batteryTimeRange));
      if (param.NotebookDataQueryParams.ramMemoryRange != null)
        queryParts.Add(GetNotebooksByRAMQuery(param.NotebookDataQueryParams.ramMemoryRange));
    }

    #region queryCreation
    private string CreateQueryText(List<IQueryPart> parts)
    {
      string query = "";
      for (int i = 0; i < parts.Count(); i++)
      {
        query += parts[i].QueryText + " ";
        if (i + 1 < parts.Count)
          query += " INTERSECT ";
      }
      return query;
    }

    private void SetQueryParameters(IQueryCommand mainQuery, List<IQueryPart> subQueries)
    {
      foreach (IQueryPart subQuery in subQueries)
      {
        foreach (KeyValuePair<string, object> parameter in subQuery.Parameters)
        {
          mainQuery.AddParameter(parameter.Key, parameter.Value);
        }
      }
    }
    #endregion

    #region notebook
    private static IQueryPart GetNotebooksByPriceQuery(Range range)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByPriceSubQuery);
      getNotebook.AddParameter("$minPrice", range.Min);
      getNotebook.AddParameter("$maxPrice", range.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByPriceSubQuery =
        "SELECT n.product_id FROM Notebooks AS n INNER JOIN Products AS p ON n.product_id = p.product_id WHERE p.price BETWEEN $minPrice AND $maxPrice";

    private static IQueryPart GetNotebooksByName(string name)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByName);
      getNotebook.AddParameter("$notebookName", "%" + name + "%");

      return getNotebook;
    }
    private const string CommandGetNotebooksByName =
        "SELECT n.product_id FROM Notebooks AS n INNER JOIN Products AS p ON n.product_id = p.product_id WHERE p.name LIKE $notebookName";

    private static IQueryPart GetNotebooksByBatteryTimeQuery(Range time)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByBatteryTime);
      getNotebook.AddParameter("$minTime", time.Min);
      getNotebook.AddParameter("$maxTime", time.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByBatteryTime =
      "SELECT n.product_id FROM Notebooks AS n WHERE n.average_battery_time BETWEEN $minTime AND $maxTime";

    private static IQueryPart GetNotebooksByRAMQuery(Range ram)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByRAM);
      getNotebook.AddParameter("$minRam", ram.Min);
      getNotebook.AddParameter("$maxRam", ram.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByRAM =
      "SELECT n.product_id FROM Notebooks AS n WHERE n.ram_memory BETWEEN $minRam AND $maxRam";

    private static IQueryPart GetNotebooksByOsQuery(string os)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByOS);
      getNotebook.AddParameter("$os", "%" + os + "%");
      return getNotebook;
    }
    private const string CommandGetNotebooksByOS =
      "SELECT n.product_id FROM Notebooks AS n WHERE n.os LIKE $os";

    #endregion
    
    #region cpu
    private static IQueryPart GetNotebooksByCpuCountQuery(Range cpuCount)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByCpuCount);
      getNotebook.AddParameter("$minCount", cpuCount.Min);
      getNotebook.AddParameter("$maxCount", cpuCount.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByCpuCount =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN CPU AS c ON n.cpu_id = c.cpu_id WHERE c.count BETWEEN $minCount AND $maxCount";
   

    private static IQueryPart GetNotebooksByCPUNameQuery(string name)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByCpuName);
      getNotebook.AddParameter("$name", "%" + name + "%");
      return getNotebook;
    }
    private const string CommandGetNotebooksByCpuName =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN CPU AS c ON n.cpu_id = c.cpu_id WHERE c.name LIKE $name";

    private static IQueryPart GetNotebooksByCPUClockRateQuery(Range clockRate)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksCpuClockRate);
      getNotebook.AddParameter("$minClockRate", clockRate.Min);
      getNotebook.AddParameter("$maxClockRate", clockRate.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksCpuClockRate =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN CPU AS c ON n.cpu_id = c.cpu_id WHERE c.clock_rate BETWEEN $minClockRate AND $maxClockRate";
    #endregion

    #region harddrives
    private static IQueryPart GetNotebooksByhardDriveMemory(Range size)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByHardDriveSize);
      getNotebook.AddParameter("$minSize", size.Min);
      getNotebook.AddParameter("$maxSize", size.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByHardDriveSize =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN HardDrives AS h ON n.hard_drive_id = h.hard_drive_id WHERE h.memory BETWEEN $minSize AND $maxSize";

    private static IQueryPart GetNotebooksByhardDriveType(string type)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByHardDriveType);
      getNotebook.AddParameter("$type", "%" + type + "%");
      
      return getNotebook;
    }
    private const string CommandGetNotebooksByHardDriveType =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN HardDrives AS h ON n.hard_drive_id = h.hard_drive_id WHERE h.type LIKE $type";
    #endregion

    #region graphic
    private static IQueryPart GetNotebooksByVRAMMemory(Range memory)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByVRAMMemory);
      getNotebook.AddParameter("$minVRAM", memory.Min);
      getNotebook.AddParameter("$maxVRAM", memory.Max);
      return getNotebook;
    }
    private const string CommandGetNotebooksByVRAMMemory =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN Graphics AS g ON n.graphic_id= g.graphic_id WHERE g.vram BETWEEN $minVRAM AND $maxVRAM";

    private static IQueryPart GetNotebooksByGraphicCardName(string name)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetNotebooksByGraphicCardName);
      getNotebook.AddParameter("$graphicCardName", "%" + name + "%");

      return getNotebook;
    }
    private const string CommandGetNotebooksByGraphicCardName =
      "SELECT n.product_id FROM Notebooks AS n INNER JOIN Graphics AS g ON n.graphic_id= g.graphic_id WHERE g.name LIKE $graphicCardName";
    #endregion

    
    #endregion

    #region headPhoneQueries



    #endregion


  }
}
