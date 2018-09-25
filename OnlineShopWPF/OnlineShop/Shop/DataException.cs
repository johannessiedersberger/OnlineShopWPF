using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// An Exception that is thrown when something with the database fails
  /// </summary>
  public class DataException : Exception
  {
    public DataException()
    {
    }

    public DataException(string message)
        : base(message)
    {
    }

    public DataException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
