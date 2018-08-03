using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
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

    private IDatabase database = new SqliteDatabase("OnlineShop.db");

    /// <summary>
    /// Creates a Graphic-Card in the Database
    /// </summary>
    /// <param name="vram">The video RAM of the Graphic-Card</param>
    /// <param name="name">The name of the Graphic-card</param>
    public Graphic(int vram, string name)
    {
      VRAM = vram;
      Name = name;

      using (var createGraphic = database.CreateCommand(CommandAddGraphic))
      {
        database.Open();
        createGraphic.Parameters.Add(database.CreateParameter("$id", null));
        createGraphic.Parameters.Add(database.CreateParameter("$vram", vram.ToString()));
        createGraphic.Parameters.Add(database.CreateParameter("$name", name));
        createGraphic.ExecuteNonQuery();
        database.Close();
      }
    }

    private const string CommandAddGraphic = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";

  }
}
