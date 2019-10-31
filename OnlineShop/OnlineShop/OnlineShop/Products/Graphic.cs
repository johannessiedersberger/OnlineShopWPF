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
  /// A Graphic Card froma Notebook
  /// </summary>
  public class Graphic
  {
    /// <summary>
    /// The Video RAM of the Graphic-Card in GB
    /// </summary>
    public int VRAMInGB { get; private set; }
    /// <summary>
    /// The name of the Graphic-Card
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Creates a Graphic-Card in the Database
    /// </summary>
    /// <param name="vramInGB">The video RAM of the Graphic-Card</param>
    /// <param name="name">The name of the Graphic-card</param>
    public Graphic(int vramInGB, string name)
    {
      VRAMInGB = vramInGB;
      Name = name;
    }

    

  }
}
