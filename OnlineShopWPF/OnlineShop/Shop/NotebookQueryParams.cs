using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class NotebookQueryParams : ProductQueryParams
  {
    //Notebook
    public Range priceRange;
    public Range ramMemoryRange;
    public Range batteryTimeRange;
    public string os;
    
    public CPUQueryParams CPUQueryParams;

    public HardDriveQueryParams HardDriveQueryParams;

    public GraphicQueryParams GraphicQueryParams;

    
  }
}
