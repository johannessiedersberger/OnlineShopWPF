using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
 
  /// <summary>
  /// Contains an order from a customer
  /// </summary>
  public class Order
  {
    /// <summary>
    /// The ID of the costumer in the Database
    /// </summary>
    public int CustomerId { get; private set; }
    /// <summary>
    /// The Order Date 
    /// </summary>
    public DateTime OrderDate { get; private set; }

    /// <summary>
    /// Sets the id and the date
    /// </summary>
    /// <param name="customerId">the customer id</param>
    /// <param name="date">the date</param>
    public Order(int customerId, DateTime date)
    {
      CustomerId = customerId;
      OrderDate = date;
    }
  }
}
