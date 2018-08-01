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
    SQLiteConnection connection;
    public HardDrive(int memory, string type)
    {
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO HardDrives(hard_drive_id, type, memory) VALUES($id,$type,$memory) ";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$type", type);
        command.Parameters.AddWithValue("$memory", memory);

        command.ExecuteNonQuery();
      }
    }
  }
}
