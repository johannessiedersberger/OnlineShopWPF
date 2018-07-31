using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

namespace OnlineShop
{
  public class Shop
  {

  }

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

  public class CPU
  {
    SQLiteConnection connection;
    public CPU(int count, float clockRate, string name)
    {
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$count", count);
        command.Parameters.AddWithValue("$clockRate", clockRate);
        command.Parameters.AddWithValue("$name", name);

        command.ExecuteNonQuery();

      }
    }
  }

  public class Graphic
  {
    SQLiteConnection connection;
    public Graphic(int vram, string name)
    {
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Graphics(graphic_id, vram, name) VALUES($id,$vram,$name) ";
        command.Parameters.AddWithValue("$id", null);
        command.Parameters.AddWithValue("$vram", vram);
        command.Parameters.AddWithValue("$name", name);
        command.ExecuteNonQuery();
      }
    }
  }
}
