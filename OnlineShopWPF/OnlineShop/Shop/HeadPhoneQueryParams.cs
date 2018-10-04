using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The search data for a Headphone
  /// </summary>
  public class HeadPhoneQueryParams : ProductQueryParams
  {
    /// <summary>
    /// The SearchData for the Headphone Description
    /// </summary>
    public HeadPhoneDataQueryParams headPhoneData;
  }
}
