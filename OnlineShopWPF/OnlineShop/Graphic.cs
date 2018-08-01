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
    public int VRAM { get; private set; }
    public string Name { get; private set; }

    public Graphic(int vram, string name)
    {
      VRAM = vram;
      Name = name;

      SQLiteConnection connection;
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

    public static int GetId(string name)
    {
      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "SELECT graphic_id FROM Graphics WHERE name = $name";
        command.Parameters.AddWithValue("$name", name);
        SQLiteDataReader reader = command.ExecuteReader();
        int id = 0;
        while(reader.Read())
          int.Parse(reader[0].ToString());
        return id;
      }
    }
  }
}
