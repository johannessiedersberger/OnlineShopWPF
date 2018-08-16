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
    public Graphic(int vram, string name)
    {
      VRAM = vram;
      Name = name;
    }

    /// <summary>
    /// Writes the Cpu to the databse
    /// </summary>
    /// <param name="db">The Database that will contain the cpu</param>
    public void WriteToDatabase(IDatabase db)
    {
      _database = db;
      if (DoesGraphicAlreadyExist())
        return;
      using (var createGraphic = _database.CreateNonQueryCommand(CommandAddGraphic))
      {
        createGraphic.AddParameter("$id", null);
        createGraphic.AddParameter("$vram", VRAM);
        createGraphic.AddParameter("$name", Name);
        createGraphic.Execute();
      }
    }

    private const string CommandAddGraphic = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";

    private bool DoesGraphicAlreadyExist()
    {

      using (var getID = _database.CreateQueryCommand(CommandSelectID))
      {
        getID.AddParameter("$name", Name);
        IReader reader = getID.ExecuteReader();
        reader.Read();
        return reader.HasRows;
      }
    }

    /// <summary>
    /// Gets the ID from the Graphic-Card
    /// </summary>
    public int Id
    {
      get
      {
        if (_database == null)
          throw new NullReferenceException("No Database exists");

        using(var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$name", Name);
          IReader reader = getID.ExecuteReader();
          reader.Read();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    private const string CommandSelectID = "SELECT graphic_id FROM Graphics WHERE name = $name";

  }
}
