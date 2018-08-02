using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
  public class HardDrive
  {
    public int Memory { get; private set; }
    public string Type { get; private set; }

    public HardDrive(int memory, string type)
    {
      Memory = memory;
      Type = type;

      SqliteDatabase database = new SqliteDatabase("OnlineShop.db");

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

    public static bool CheckIfHardDriveExists(int memory, string type)
    {
      using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "SELECT memory, type FROM HardDrives WHERE memory = $memory AND type = $type";
        command.Parameters.AddWithValue("$type", type);
        command.Parameters.AddWithValue("$memory", memory);
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
          if (reader[0].ToString() != null)
            return true;
        return false;
      }
    }

    public static int GetId(int memory, string type)
    {
      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "SELECT hard_drive_id FROM HardDrives WHERE memory = $memory AND type = $type";
        command.Parameters.AddWithValue("$memory", memory);
        command.Parameters.AddWithValue("$type", type);
        SQLiteDataReader reader = command.ExecuteReader();

        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        return id;
      }
    }
  }
}
