using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class ProductAlreadyExistsException : Exception
  {
    public ProductAlreadyExistsException()
    {
    }

    public ProductAlreadyExistsException(string message)
        : base(message)
    {
    }

    public ProductAlreadyExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
