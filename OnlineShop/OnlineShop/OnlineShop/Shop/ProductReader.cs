using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class ProductReader
  {
    /// <summary>
    /// Returns a list of products based on the reader
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static List<Product> ReadForProducts(IReader reader)
    {
      var products = new List<Product>();
      while (reader.TryReadNextRow(out object[] row))
      {
        var productRows = new List<string>();
        for (int i = 0; i < row.Length; i++)
        {
          productRows.Add(row[i].ToString());
        }
        products.Add(new Product(productRows[2], new Money(decimal.Parse(productRows[3]))));
      }
      return products;
    }
  }
}
