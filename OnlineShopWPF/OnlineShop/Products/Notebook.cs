using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  /// <summary>
  /// Contains the Os on the notebooks
  /// </summary>
  public static class OS
  {
    /// <summary>
    /// WINDOWS
    /// </summary>
    public static string windows = "windows";
    /// <summary>
    /// Linux
    /// </summary>
    public static string linux = "linux";
    /// <summary>
    /// Mac
    /// </summary>
    public static string macos = "macos";
  }

  /// <summary>
  /// Notebook
  /// </summary>
  public class Notebook 
  {
    public Product Product { get; private set; }
    public Graphic Graphic { get; private set; }
    public CPU Cpu { get; private set; }
    public HardDrive HardDrive { get; private set; }
    public int Ram { get; private set; }
    public int AverageBatteryTime { get; private set; }
    public string Os { get; private set; }

    /// <summary>
    /// Creates a new notebook in the databse
    /// </summary>
    /// <param name="product">the product id</param>
    /// <param name="graphic">the notebook id</param>
    /// <param name="cpu">the cpu id</param>
    /// <param name="hardDrive">the graphic id</param>
    /// <param name="ramMemory">the ram</param>
    /// <param name="avgBatteryTime">the battery time</param>
    /// <param name="os">the os</param>
    public Notebook(Product product, Graphic graphic, CPU cpu, HardDrive hardDrive, int ramMemory, int avgBatteryTime, string os)
    {
      Product = product;
      Graphic = graphic;
      Cpu = cpu;
      HardDrive = hardDrive;
      Ram = ramMemory;
      AverageBatteryTime = avgBatteryTime;
      Os = os;
    }

    
  }
}

