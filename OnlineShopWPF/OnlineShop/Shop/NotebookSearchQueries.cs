using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public static class NotebookSearchQueries
  {
    #region notebookQueries
    /// <summary>
    /// Finds the matching Notebooks
    /// </summary>
    /// <param name="notebookSearchData">the query params</param>
    /// <param name="db">the database</param>
    /// <returns></returns>
    public static List<Product> FindMatchingNotebooks(NotebookQueryParams notebookSearchData, IDatabase db)
    {
      List<IQueryPart> querieParts = GetQueryPartsNotebook(notebookSearchData);
      string QuerieText;
      
      if (querieParts.Count == 0)
        QuerieText = "SELECT product_id FROM Notebooks ";
      else
        QuerieText = QuerieCreation.CreateQueryText(querieParts);

      string CommandGetNotebooks = string.Format("SELECT * FROM " +
        "  ( {0} ) AS PID " +
        " INNER JOIN Products As p ON p.product_id = PID.product_id", QuerieText);

      using (var getNotebook = db.CreateQueryCommand(CommandGetNotebooks))
      {
        QuerieCreation.SetQueryParameters(getNotebook, querieParts);
        IReader reader = getNotebook.ExecuteReader();
        return ProductReader.ReadForProducts(reader);
      }
    }

    private static List<IQueryPart> GetQueryPartsNotebook(NotebookQueryParams param)
    {
      var queryParts = new List<IQueryPart>();
      FillQueryPartsWithGraphicQueries(queryParts, param);
      FillQueryPartsWithCPUQueries(queryParts, param);
      FillQueryPartsWithHardDriveQueries(queryParts, param);
      FillQueryPartsWithNotebookQuery(queryParts, param);
      FillQueryPartsWithNotebookProductQuery(queryParts, param);
      return queryParts;
    }

    private static void FillQueryPartsWithNotebookProductQuery(List<IQueryPart> queryParts, ProductQueryParams param)
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
  }
}
