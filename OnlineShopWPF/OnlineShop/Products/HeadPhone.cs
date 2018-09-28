using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class HeadPhone
  {
    public bool Wireless { get; private set; }
    public int ProductId { get; private set; }

    public HeadPhone(int productId, bool wireless)
    {
      ProductId = productId;
      Wireless = wireless;
    }
  }
}
