using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class OrderEntry
  {
    public int ProductId { get; private set; }
    public int OrdersId { get; private set; }
    public int Amount { get; private set; }

    public OrderEntry(int productId, int orderId, int amount)
    {
      ProductId = productId;
      OrdersId = orderId;
      Amount = amount;
    }
  }
}
