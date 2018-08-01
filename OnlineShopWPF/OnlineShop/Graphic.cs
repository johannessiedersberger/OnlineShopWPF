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
    SQLiteConnection connection;
    public Graphic(int vram, string name)
    {
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name)";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$vram", vram);
        command.Parameters.AddWithValue("$name", name);
        command.ExecuteNonQuery();
      }
    }
  }
}
