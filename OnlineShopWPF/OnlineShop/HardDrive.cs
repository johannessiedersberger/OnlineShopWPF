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
      if (CheckIfHardDriveExists(memory, type))
        return;

      Memory = memory;
      Type = type;

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
    /// Checks if the HardDrive already exists in the Databse
    /// </summary>
    /// <param name="memory">memory in GB</param>
    /// <param name="type">ssd or hdd</param>
    /// <returns></returns>
    public bool CheckIfHardDriveExists(int memory, string type)
    {
      using (var checkHardDrive = database.CreateCommand(CommandCheckHardDrive))
      {
        database.Open();
        checkHardDrive.Parameters.Add(database.CreateParameter("$type", type));
        checkHardDrive.Parameters.Add(database.CreateParameter("$memory", memory.ToString()));

        IDataReader reader = checkHardDrive.ExecuteReader();
        return IsNotEmpty(reader); //Close Databse
      }    
    }

    private const string CommandCheckHardDrive = "SELECT memory, type FROM HardDrives WHERE memory = $memory AND type = $type";

    private bool IsNotEmpty(IDataReader reader)
    {
      bool foundValues = false;
      while (reader.Read())
        if (reader[0].ToString() != null)
          foundValues = true;

      database.Close();
      return foundValues;
    }

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

        return ID(reader);//Close Database
      }
    }

    private const string CommandSelectID = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";

    private int ID(IDataReader reader)
    {
      int id = 0;
      while (reader.Read())
        id = int.Parse(reader[0].ToString());
      database.Close();
      return id;
    }
  }
}
