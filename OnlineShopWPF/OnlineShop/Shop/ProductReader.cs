using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class ProductReader
  {
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
        products.Add(new Product(int.Parse(productRows[1]), productRows[2], double.Parse(productRows[3])));
      }
      return products;
    }
  }
}
