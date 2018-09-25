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
