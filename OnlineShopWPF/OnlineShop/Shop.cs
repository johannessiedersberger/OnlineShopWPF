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
    private const string CommandGetNotebooksByPrice = "SELECT p.product_id, p.name, p.price FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE price BETWEEN $min AND $max";

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
    private const string CommandGetNotebooksByRAM = "SELECT p.product_id, p.name, p.price FROM Products As p INNER JOIN Notebooks AS n ON p.product_id = n.product_id WHERE n.ram_memory BETWEEN $min AND $max";
    #endregion
    #region headphones
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
