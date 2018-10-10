using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{

  /// <summary>
  /// Is Thrown if a Product could not be found in the database
  /// </summary>
  public class ProductNotFoundException : Exception
  {
    public ProductNotFoundException()
    {
    }

    public ProductNotFoundException(string message)
        : base(message)
    {
    }

    public ProductNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
