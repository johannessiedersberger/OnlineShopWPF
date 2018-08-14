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

    private IDatabase _database;

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

    /// <summary>
    /// Writes the HardDrive to the DataBase
    /// </summary>
    /// <param name="db"></param>
    public void WriteToDatabase(IDatabase db)
    {
      _database = db;
      using (var createHardDrive = _database.CreateNonQueryCommand(CommandAddHardDrive))
      {
        createHardDrive.AddParameter("$id", null);
        createHardDrive.AddParameter("$type", Type);
        createHardDrive.AddParameter("$memory", Memory);
        createHardDrive.Execute();
      }
    }

    private const string CommandAddHardDrive = "INSERT INTO HardDrives(hard_drive_id, type, memory) VALUES($id,$type,$memory)";

    public bool CheckIfHardDriveExists(IDatabase db)
    {

      using (var getID = db.CreateQueryCommand(CommandSelectID))
      {
        getID.AddParameter("$memory", Memory);
        getID.AddParameter("$type", Type);
        IReader reader = getID.ExecuteReader();
        return reader[0] != null;
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

        using (var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$memory", Memory);
          getID.AddParameter("$type", Type);
          IReader reader = getID.ExecuteReader();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    private const string CommandSelectID = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";
  }
}
