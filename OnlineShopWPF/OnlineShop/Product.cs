using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  public class Product
  {
    public Product(string name, double price)
    {
      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Products(product_id, name, price) VALUES($id, $name, $price) ";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$price", price);

        command.ExecuteNonQuery();

      }
    }
  }
}
