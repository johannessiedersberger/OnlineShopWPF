using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// Represents a Headphone in the Database
  /// </summary>
  public class HeadPhone
  {
    /// <summary>
    /// Contains if it is wireless or not
    /// </summary>
    public bool Wireless { get; private set; }
    /// <summary>
    /// The product id in the databse
    /// </summary>
    public int ProductId { get; private set; }

    /// <summary>
    /// Sets the ProductId and the wirless boolean
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="wireless"></param>
    public HeadPhone(int productId, bool wireless)
    {
      ProductId = productId;
      Wireless = wireless;
    }
  }
}
