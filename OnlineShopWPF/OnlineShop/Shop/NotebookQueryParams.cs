using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// The notebook seach Data
  /// </summary>
  public class NotebookQueryParams : ProductQueryParams
  {   
    /// <summary>
    /// the cpu seach data
    /// </summary>
    public CPUQueryParams CPUQueryParams;
    /// <summary>
    /// The HardDrive search Data
    /// </summary>
    public HardDriveQueryParams HardDriveQueryParams;
    /// <summary>
    /// the graphic seach data
    /// </summary>
    public GraphicQueryParams GraphicQueryParams;
    /// <summary>
    /// The notebook-data seach data
    /// </summary>
    public NotebookDataQueryParams NotebookDataQueryParams; 
  }
}
