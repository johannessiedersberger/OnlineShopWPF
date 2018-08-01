using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace OnlineShop
{
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
}
