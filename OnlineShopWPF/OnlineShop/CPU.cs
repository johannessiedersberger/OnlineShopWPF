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
    public int Count { get; private set; }
    public double ClockRate { get; private set; }
    public string Name { get; private set; }

    SQLiteConnection connection;
    public CPU(int count, double clockRate, string name)
    {
      Count = count;
      ClockRate = clockRate;
      Name = name;

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

    public static int GetId(string name)
    {
      SQLiteConnection connection;
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "SELECT cpu_id FROM CPU WHERE name = $name";
        command.Parameters.AddWithValue("$name", name);
        SQLiteDataReader reader = command.ExecuteReader();
        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        return id;
      }
    }
  }
}
