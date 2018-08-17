using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

namespace OnlineShop
{
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
    private const string CommandGetNotebooksByPrice = "SELECT * FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE price BETWEEN $min AND $max";

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
    private const string CommandGetNotebooksByRAM = "SELECT * FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE n.ram_memory BETWEEN $min AND $max";

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
    private const string CommandGetNotebooksByOS = "SELECT * FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE n.os = $os";

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
    private const string CommandGetNotebooksByBatteryTime = "SELECT * FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE n.average_battery_time BETWEEN $min AND $max";

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
    private const string CommandGetNotebooksHardDriveSize = "SELECT * FROM Products As p" +
      " INNER JOIN Notebooks AS n ON p.product_id = n.product_id " +
      "INNER JOIN HardDrives AS h  WHERE h.memory BETWEEN $min AND $max";

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
    private const string CommandGetHeadPhonesByPrice = "SELECT p.product_id, p.name, p.price FROM Products As p INNER JOIN Headphones AS h ON p.product_id = h.product_id WHERE price BETWEEN $min AND $max";
    #endregion
  }
}
