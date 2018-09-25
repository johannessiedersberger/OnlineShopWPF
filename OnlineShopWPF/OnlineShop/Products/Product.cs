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

    public int ProductId { get; private set; }

    /// <summary>
    /// Assigns the member variables
    /// </summary>
    /// <param name="name">The name of the Product</param>
    /// <param name="price">The price of the Product</param>
    public Product(int productId, string name, double price)
    {
      Name = name;
      Price = price;
      ProductId = productId;
    }

    


  }
}
