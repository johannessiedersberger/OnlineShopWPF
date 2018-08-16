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
    private const string CommandGetNotebooksByPrice = "SELECT product_id, name, price FROM Products WHERE price BETWEEN $min AND $max";
  }
 
}
