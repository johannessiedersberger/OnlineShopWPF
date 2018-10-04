using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The seach data for a product
  /// </summary>
  public class ProductQueryParams
  {
    /// <summary>
    /// the nameof a product
    /// </summary>
    public string Name;
    /// <summary>
    /// the price of the product
    /// </summary>
    public Range Price;
  }
}
