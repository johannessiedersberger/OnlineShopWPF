using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// Contains the Notebook Search Data
  /// </summary>
  public class NotebookDataQueryParams
  {
    /// <summary>
    /// The price of the notebook
    /// </summary>
    public Range priceRange;
    /// <summary>
    /// the ram of the notebook
    /// </summary>
    public Range ramMemoryRange;
    /// <summary>
    /// The battery-time of the notebook
    /// </summary>
    public Range batteryTimeRange;
    /// <summary>
    /// The os of the notebook
    /// </summary>
    public string os;
    /// <summary>
    /// the name of the notebook
    /// </summary>
    public string notebookName;
  }
}
