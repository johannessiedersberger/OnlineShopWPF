using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
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

    private IDatabase database = new SqliteDatabase("OnlineShop.db");

    /// <summary>
    /// Creates a HardDrive in the Database
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    public HardDrive(int memory, string type)
    {
      Memory = memory;
      Type = type;

      if (GetId(memory, type) != 0)
        return;

      using (var createHardDrive = database.CreateCommand(CommandAddHardDrive))
      {
        database.Open();
        createHardDrive.Parameters.Add(database.CreateParameter("$id", null));
        createHardDrive.Parameters.Add(database.CreateParameter("$type", type));
        createHardDrive.Parameters.Add(database.CreateParameter("$memory", memory.ToString()));
        createHardDrive.ExecuteNonQuery();
        database.Close();
      }
    }

    private const string CommandAddHardDrive = "INSERT INTO HardDrives(hard_drive_id, type, memory) VALUES($id,$type,$memory)";

    /// <summary>
    /// Returns the id from the Databse 
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetId(int memory, string type)
    {
      using (var getID = database.CreateCommand(CommandSelectID))
      {
        database.Open();
        getID.Parameters.Add(database.CreateParameter("$memory", memory.ToString()));
        getID.Parameters.Add(database.CreateParameter("$type", type));
        IDataReader reader = getID.ExecuteReader();
        
        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        database.Close();
        return id;     
      }
    }

    private const string CommandSelectID = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";
  }
}
