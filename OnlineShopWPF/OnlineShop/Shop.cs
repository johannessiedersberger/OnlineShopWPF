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

  public class CPU
  {
    SQLiteConnection connection;
    public CPU(int count, float clockRate, string name)
    {
      using (connection = new SQLiteConnection(@"Data Source = C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShop.db; Version=3"))
      {
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);
        command.CommandText = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES(?,?,?,?) ";
        command.Parameters.AddWithValue("param1", null);
        command.Parameters.AddWithValue("param2", count);
        command.Parameters.AddWithValue("param3", clockRate);
        command.Parameters.AddWithValue("param4", name);





        //command.CommandText = String.Format("INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES (NULL,{0},{1},\"{2}\")", count, clockRate.ToString(CultureInfo.InvariantCulture), name);
        command.ExecuteNonQuery();

      }
    }
  }
}
