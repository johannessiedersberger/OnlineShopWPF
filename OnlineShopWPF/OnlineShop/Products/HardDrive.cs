using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
  /// <summary>
  /// The HardDirve 
  /// </summary>
  public class HardDrive
  {
    /// <summary>
    /// The Memory of the HardDisk in GB
    /// </summary>
    public int Memory { get; private set; }
    /// <summary>
    /// The type of the HardDisk (SSD or HDD)
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// Assign the Member variables 
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    public HardDrive(int memory, string type)
    {
      Memory = memory;
      Type = type; 
    }

    
  }
}
