//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SQLite;
//using System.Data;

//namespace OnlineShop
//{
//  public class Product
//  {
//    /// <summary>
//    /// The Name of the Product
//    /// </summary>
//    public string Name { get; private set; }
//    /// <summary>
//    /// The Price of the Product
//    /// </summary>
//    public double Price { get; private set; }

//    private IDatabase database = new SqliteDatabase("OnlineShop.db");

//    /// <summary>
//    /// Creates the Product in the Database
//    /// </summary>
//    /// <param name="name">The name of the Product</param>
//    /// <param name="price">The price of the Product</param>
//    public Product(string name, double price)
//    {
//      Name = name;
//      Price = price;

//      if (GetId(name) != 0)
//        return;

//      using (var createProduct = database.CreateQueryCommand(CommandAddProduct))
//      {
//        database.Open();
//        createProduct.Parameters.Add(database.CreateParameter("$id", null));
//        createProduct.Parameters.Add(database.CreateParameter("$name", name));
//        createProduct.Parameters.Add(database.CreateParameter("$price", price.ToString()));
//        createProduct.ExecuteNonQuery();
//        database.Dispose();
//      }
//    }

//    private const string CommandAddProduct = "INSERT INTO Products(product_id, name, price) VALUES($id, $name, $price) ";

//    /// <summary>
//    /// Returns the id from the Databse 
//    /// </summary>
//    /// <param name="memory"></param>
//    /// <param name="type"></param>
//    /// <returns></returns>
//    public int GetId(string name)
//    {
//      using (var getID = database.CreateQueryCommand(CommandSelectID))
//      {
//        database.Open();
//        getID.Parameters.Add(database.CreateParameter("$name", name));
//        IDataReader reader = getID.ExecuteReader();

//        int id = 0;
//        while (reader.Read())
//          id = int.Parse(reader[0].ToString());
//        database.Dispose();
//        return id;
//      }
//    }

//    private const string CommandSelectID = "SELECT product_id FROM Products WHERE name = $name";
//  }
//}
