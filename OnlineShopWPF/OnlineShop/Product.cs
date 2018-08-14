using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
  /// <summary>
  /// The Products that could be bought in the shop
  /// </summary>
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

    private IDatabase _database;

    /// <summary>
    /// Assigns the member variables
    /// </summary>
    /// <param name="name">The name of the Product</param>
    /// <param name="price">The price of the Product</param>
    public Product(string name, double price)
    {
      Name = name;
      Price = price;
    }

    /// <summary>
    /// Writes the Product to the Database
    /// </summary>
    /// <param name="database">The database that contains the Products</param>
    public void WriteToDataBase(IDatabase database)
    {
      _database = database;
      using (var createProduct = _database.CreateNonQueryCommand(CommandAddProduct))
      {
        createProduct.AddParameter("$id", null);
        createProduct.AddParameter("$name", Name);
        createProduct.AddParameter("$price", Price);
        createProduct.Execute();
      }
    }

    private const string CommandAddProduct = "INSERT INTO Products(product_id, name, price) VALUES($id, $name, $price) ";
    
    /// <summary>
    /// Gets the ID from the Product
    /// </summary>
    public int ID
    {
      get
      {
        if (_database == null)
          throw new NullReferenceException("No Database exists");

        using (var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$name", Name);
          IReader reader = getID.ExecuteReader();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    private const string CommandSelectID = "SELECT product_id FROM Products WHERE name = $name";

    /// <summary>
    /// Delets a Product from The Database
    /// </summary>
    /// <param name="id">The id from the product which sould be removed</param>
    public void DeleteProduct(int id)
    {
      using (var delete = _database.CreateNonQueryCommand(Delete))
      {
        delete.AddParameter("$id", id);
        delete.Execute();
      }
    }

    private const string Delete = "DELTE FROM Products WHERE product_id = $id";


  }
}
