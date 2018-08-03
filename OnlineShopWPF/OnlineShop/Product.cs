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
    /// <summary>
    /// The Name of the Product
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// The Price of the Product
    /// </summary>
    public double Price { get; private set; }

    private IDatabase database = new SqliteDatabase("OnlineShop.db");

    /// <summary>
    /// Creates the Product in the Database
    /// </summary>
    /// <param name="name">The name of the Product</param>
    /// <param name="price">The price of the Product</param>
    public Product(string name, double price)
    {
      Name = name;
      Price = price;


      using (var createProduct = database.CreateCommand(CommandAddProduct))
      {
        database.Open();
        createProduct.Parameters.Add(database.CreateParameter("$id", null));
        createProduct.Parameters.Add(database.CreateParameter("$name", name));
        createProduct.Parameters.Add(database.CreateParameter("$price", price.ToString()));
        createProduct.ExecuteNonQuery();
        database.Close();
      }
    }

    private const string CommandAddProduct = "INSERT INTO Products(product_id, name, price) VALUES($id, $name, $price) ";



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
