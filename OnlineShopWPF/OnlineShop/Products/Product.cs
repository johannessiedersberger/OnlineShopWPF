using System;

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
    public Money Price { get; private set; }

    /// <summary>
    /// Assigns the member variables and Checks the Parameters
    /// </summary>
    /// <param name="name">The name of the Product</param>
    /// <param name="price">The price of the Product</param>
    public Product(string name, Money price)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Invalid product name", nameof(name));

      Name = name;
      Price = price;
    }
  }
}

