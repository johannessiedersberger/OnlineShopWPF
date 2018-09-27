using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
 

  public class Order
  {
    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }

    public Order(int customerId, DateTime date)
    {
      CustomerId = customerId;
      OrderDate = date;
    }
  }
}
