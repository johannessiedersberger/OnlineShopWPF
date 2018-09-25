using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  public static class OS
  {
    public static string windows = "windows";
    public static string linux = "linux";
    public static string macos = "macos";
  }

  /// <summary>
  /// Notebook
  /// </summary>
  public class Notebook
  {
    public int ProductId { get; private set; }
    public int GraphicId { get; private set; }
    public int CpuId { get; private set; }
    public int HardDriveId { get; private set; }
    public int RamMemory { get; private set; }
    public int AverageBatteryTime { get; private set; }
    public string Os { get; private set; }

    /// <summary>
    /// Creates a new notebook in the databse
    /// </summary>
    /// <param name="productId">the product id</param>
    /// <param name="graphicId">the notebook id</param>
    /// <param name="cpuId">the cpu id</param>
    /// <param name="hardDriveId">the graphic id</param>
    /// <param name="ramMemory">the ram</param>
    /// <param name="avgBatteryTime">the battery time</param>
    /// <param name="os">the os</param>
    public Notebook(int productId, int graphicId, int cpuId, int hardDriveId, int ramMemory, int avgBatteryTime, string os)
    {
      ProductId = productId;
      GraphicId = graphicId;
      CpuId = cpuId;
      HardDriveId = hardDriveId;
      RamMemory = ramMemory;
      AverageBatteryTime = avgBatteryTime;
      Os = os;
    }

    
  }
}

