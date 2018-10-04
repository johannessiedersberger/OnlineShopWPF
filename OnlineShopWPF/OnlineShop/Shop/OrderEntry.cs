using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The OrderEntry which contains a part of an order
  /// </summary>
  public class OrderEntry
  {
    /// <summary>
    /// the prodcut
    /// </summary>
    public int ProductId { get; private set; }
    /// <summary>
    /// the order id
    /// </summary>
    public int OrdersId { get; private set; }
    /// <summary>
    /// the amount
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Sets the productid the orderEntryid and the amount
    /// </summary>
    /// <param name="productId">the prodict id</param>
    /// <param name="orderId"> the order id</param>
    /// <param name="amount">the prodcut id</param>
    public OrderEntry(int productId, int orderId, int amount)
    {
      ProductId = productId;
      OrdersId = orderId;
      Amount = amount;
    }
  }
}
