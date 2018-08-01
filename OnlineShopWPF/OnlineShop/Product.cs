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
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Product(string name, double price)
    {
      Name = name;
      Price = price;

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

    public static int GetId(string name)
    {
      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "SELECT product_id FROM Products WHERE name = $name";
        command.Parameters.AddWithValue("$name", name);
        SQLiteDataReader reader = command.ExecuteReader();

        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        return id;
      }
    }
  }
}
