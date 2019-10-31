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
    public static string file = @"C:\Users\johan\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db";

    static MySqliteDatabase _database = new MySqliteDatabase(file);

  }
}
