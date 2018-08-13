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
    public int VRAM { get; private set; }
    /// <summary>
    /// The name of the Graphic-Card
    /// </summary>
    public string Name { get; private set; }

    private IDatabase _database;
    /// <summary>
    /// Creates a Graphic-Card in the Database
    /// </summary>
    /// <param name="vram">The video RAM of the Graphic-Card</param>
    /// <param name="name">The name of the Graphic-card</param>
    public Graphic(IDatabase db, int vram, string name)
    {
      VRAM = vram;
      Name = name;
      _database = db;

      using (var createGraphic = _database.CreateNonQueryCommand(CommandAddGraphic))
      {
        createGraphic.AddParameter("$id", null);
        createGraphic.AddParameter("$vram", vram.ToString());
        createGraphic.AddParameter("$name", name);
        createGraphic.Execute();
      }
    }

    private const string CommandAddGraphic = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";

    /// <summary>
    /// Gets the ID from the Graphic-Card
    /// </summary>
    public int Id
    {
      get
      {
        using(var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$name", Name);
          IReader reader = getID.ExecuteReader();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    private const string CommandSelectID = "SELECT graphic_id FROM Graphics WHERE name = $name";

  }
}
