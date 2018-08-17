using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

namespace OnlineShop
{
  /// <summary>
  /// The OnlineShop that filters the products
  /// </summary>
  public class Shop
  {
    /// <summary>
    /// The Path to the Database File
    /// </summary>
    public static string file = @"C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db";

    static MySqliteDatabase _database = new MySqliteDatabase(file);

    #region notebook
    /// <summary>
    /// Executes a Query that selects all Notebooks with the given price range
    /// </summary>
    /// <param name="min">min price</param>
    /// <param name="max">max price</param>
    /// <returns>Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByPrice(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByPrice))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByPrice = 
      "SELECT * FROM Products As p " +
        "INNER JOIN Notebooks AS n ON p.product_id = n.product_id " +
        "WHERE price BETWEEN $min AND $max";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given ram-memory range
    /// </summary>
    /// <param name="min">min ram</param>
    /// <param name="max">max ram</param>
    /// <returns>Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByRAM(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByRAM))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByRAM = 
      "SELECT * FROM Products As p " +
        "INNER JOIN Notebooks AS n ON p.product_id = n.product_id " +
        "WHERE n.ram_memory BETWEEN $min AND $max";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given OS
    /// </summary>
    /// <param name="os">the os from the Notebook</param>
    /// <returns>Reader Object with the Selected Notebooks</returns>
    public static IReader GetNotebooksByOS(string os)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByOS))
      {
        getNotebook.AddParameter("$os", os);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByOS =
      "SELECT * FROM Products As p" +
        "INNER JOIN Notebooks AS n ON p.product_id = n.product_id " +
        "WHERE n.os = $os";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given battery time range
    /// </summary>
    /// <param name="min">min battery time</param>
    /// <param name="max">max battery</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByBatteryTime(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByBatteryTime))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByBatteryTime =
      "SELECT * FROM Products As p " +
        "INNER JOIN Notebooks AS n ON p.product_id = n.product_id " +
        "WHERE n.average_battery_time BETWEEN $min AND $max";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given memory size  
    /// </summary>
    /// <param name="min">min memory</param>
    /// <param name="max">max memory</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByHardDriveSize(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksHardDriveSize))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksHardDriveSize = 
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN HardDrives H ON N.hard_drive_id = H.hard_drive_id " +
              "WHERE H.memory BETWEEN $min AND $max ";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given Ram Size  
    /// </summary>
    /// <param name="min">min vram</param>
    /// <param name="max">max vram</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByVideoRAM(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByVideoRAM))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByVideoRAM =
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN Graphics G ON N.graphic_id = G.graphic_id " +
              "WHERE G.vram BETWEEN $min AND $max ";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given cpu count 
    /// </summary>
    /// <param name="min">min count</param>
    /// <param name="max">max count</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByCPUCount(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByCPUCount))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByCPUCount =
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN Cpu C ON N.cpu_id = C.cpu_id " +
              "WHERE C.count BETWEEN $min AND $max ";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given cpu clock rate 
    /// </summary>
    /// <param name="min">min clock rate</param>
    /// <param name="max">max clock rate</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByCPUClockRate(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByCPUClockRate))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByCPUClockRate =
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN Cpu C ON N.cpu_id = C.cpu_id " +
              "WHERE C.clock_rate BETWEEN $min AND $max ";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given cpu-manufacturer
    /// </summary>
    /// <param name="name">manufacturer</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByCPUName(string name)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByCPUName))
      {
        getNotebook.AddParameter("$name", "%"+name+"%");
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByCPUName =
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN Cpu C ON N.cpu_id = C.cpu_id " +
              "WHERE C.name LIKE $name ";

    /// <summary>
    /// Executes a Query that selects all Notebooks with the given graphic-card manufacturer
    /// </summary>
    /// <param name="name">manufacturer</param>
    /// <returns> Reader Object with the selected Notebooks</returns>
    public static IReader GetNotebooksByGraphicName(string name)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetNotebooksByGraphicName))
      {
        getNotebook.AddParameter("$name", "%" + name + "%");
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetNotebooksByGraphicName =
      "SELECT * FROM Products P " +
          "INNER JOIN Notebooks N ON P.product_id = N.product_id " +
              "INNER JOIN Graphics G ON N.graphic_id = G.graphic_id " +
              "WHERE G.name LIKE $name";

    #endregion

    #region headphones
    /// <summary>
    /// Executes a Query that selects all Headphones by the price
    /// </summary>
    /// <param name="min">min price</param>
    /// <param name="max">max price</param>
    /// <returns>Reader Object with the selected HeadPhones</returns>
    public static IReader GetHeadPhonesByPrice(double min, double max)
    {
      using (var getNotebook = _database.CreateQueryCommand(CommandGetHeadPhonesByPrice))
      {
        getNotebook.AddParameter("$min", min);
        getNotebook.AddParameter("$max", max);
        IReader reader = getNotebook.ExecuteReader();
        return reader;
      }
    }
    private const string CommandGetHeadPhonesByPrice = 
      "SELECT p.product_id, p.name, p.price FROM Products As p " +
        "INNER JOIN Headphones AS h ON p.product_id = h.product_id " +
        "WHERE price BETWEEN $min AND $max";
    #endregion
  }
}
