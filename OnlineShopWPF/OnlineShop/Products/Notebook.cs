using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  /// <summary>
  /// The available Operating Systems on the Notebooks
  /// </summary>
  public enum OS
  {
    /// <summary>
    /// Microsof Windows
    /// </summary>
    windows,
    /// <summary>
    /// Linux
    /// </summary>
    linux,
    /// <summary>
    /// Apple Mac OS
    /// </summary>
    mac,
    /// <summary>
    /// No Operating System installed
    /// </summary>
    empty
  }

  /// <summary>
  /// Notebook
  /// </summary>
  public class Notebook : Product
  {
    /// <summary>
    /// The Graphic Card from the Notebook
    /// </summary>
    public Graphic Graphic { get; private set; }
    /// <summary>
    /// The CPU from the Notebook
    /// </summary>
    public CPU Cpu { get; private set; }
    /// <summary>
    /// The hardDrive from the Notebook
    /// </summary>
    public HardDrive HardDrive { get; private set; }
    /// <summary>
    /// The Ram-Memory from the Notebook
    /// </summary>
    public int Ram { get; private set; }
    /// <summary>
    /// The Average Battery Time from the Notebook in Minutes
    /// </summary>
    public int AverageBatteryTime { get; private set; }
    /// <summary>
    /// The installed Operating System from the notebook
    /// </summary>
    public OS Os { get; private set; }

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
    public Notebook(Product product, Graphic graphic, CPU cpu, HardDrive hardDrive, int ramMemory, int avgBatteryTime, OS os) 
      :base(product.Name, product.Price)
    {
      Graphic = graphic;
      Cpu = cpu;
      HardDrive = hardDrive;
      Ram = ramMemory;
      AverageBatteryTime = avgBatteryTime;
      Os = os;
    }

    
  }
}

