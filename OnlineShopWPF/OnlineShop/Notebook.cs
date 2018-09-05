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
    private int _productId;
    private int _graphicId;
    private int _cpuId;
    private int _hardDriveId;
    private int RamMemory;
    private int AverageBatteryTime;
    private string Os;

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
      _productId = productId;
      _graphicId = graphicId;
      _cpuId = cpuId;
      _hardDriveId = hardDriveId;
      RamMemory = ramMemory;
      AverageBatteryTime = avgBatteryTime;
      Os = os;
    }

    
  }
}

