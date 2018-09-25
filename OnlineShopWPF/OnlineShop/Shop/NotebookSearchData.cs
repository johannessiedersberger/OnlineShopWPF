using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class NotebookSearchData
  {
    //Notebook
    public Range priceRange;
    public Range ramMemoryRange;
    public Range batteryTimeRange;
    public string os;
    
    //CPU
    public string cpuName;
    public Range cpuClockRate;
    public Range cpuCount;

    //HardDrive
    public Range hdMemoryRange;
    public string hdType;
    
    //Graphic
    public Range vramRange;
    public string graphicCardName;

    public string notebookManufacturer;
   
    
   
  }
}
