//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SQLite;
//using System.Data;

//namespace OnlineShop
//{
//  public class Graphic
//  {
//    /// <summary>
//    /// The Video RAM of the Graphic-Card in GB
//    /// </summary>
//    public int VRAM { get; private set; }
//    /// <summary>
//    /// The name of the Graphic-Card
//    /// </summary>
//    public string Name { get; private set; }

//    private IDatabase database = new SqliteDatabase("OnlineShop.db");

//    /// <summary>
//    /// Creates a Graphic-Card in the Database
//    /// </summary>
//    /// <param name="vram">The video RAM of the Graphic-Card</param>
//    /// <param name="name">The name of the Graphic-card</param>
//    public Graphic(int vram, string name)
//    {
//      VRAM = vram;
//      Name = name;

//      if (GetId(name) != 0)
//        return;

//      using (var createGraphic = database.CreateQueryCommand(CommandAddGraphic))
//      {
//        database.Open();
//        createGraphic.Parameters.Add(database.CreateParameter("$id", null));
//        createGraphic.Parameters.Add(database.CreateParameter("$vram", vram.ToString()));
//        createGraphic.Parameters.Add(database.CreateParameter("$name", name));
//        createGraphic.ExecuteNonQuery();
//        database.Dispose();
//      }
//    }

//    private const string CommandAddGraphic = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";

//    /// <summary>
//    /// Returns the id from the Databse 
//    /// </summary>
//    /// <param name="memory"></param>
//    /// <param name="type"></param>
//    /// <returns></returns>
//    public int GetId(string name)
//    {
//      using (var getID = database.CreateQueryCommand(CommandSelectID))
//      {
//        database.Open();
//        getID.Parameters.Add(database.CreateParameter("$name", name));
//        IDataReader reader = getID.ExecuteReader();

//        int id = 0;
//        while (reader.Read())
//          id = int.Parse(reader[0].ToString());
//        database.Dispose();
//        return id;
//      }
//    }

//    private const string CommandSelectID = "SELECT graphic_id FROM Graphics WHERE name = $name";

//  }
//}
