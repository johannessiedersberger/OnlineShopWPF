using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
  public class CPU
  {
    /// <summary>
    /// The number of kernels
    /// </summary>
    public int Count { get; private set; }
    /// <summary>
    /// The Clock rate in GHZ
    /// </summary>
    public double ClockRate { get; private set; }
    /// <summary>
    /// The name of the CPU
    /// </summary>
    public string Name { get; private set; }

    private IDatabase database = new SqliteDatabase("OnlineShop.db");

    /// <summary>
    /// Creates a CPU in the Database
    /// </summary>
    /// <param name="count">The number of kernels</param>
    /// <param name="clockRate">The clock rate in ghz </param>
    /// <param name="name">The name of the cpu </param>
    public CPU(int count, double clockRate, string name)
    {
      Count = count;
      ClockRate = clockRate;
      Name = name;

      if (GetId(name) != 0)
        return;

      using (var createCPU = database.CreateCommand(CommandAddCPU))
      {
        database.Open();
        createCPU.Parameters.Add(database.CreateParameter("$id", null));
        createCPU.Parameters.Add(database.CreateParameter("$count", count.ToString()));
        createCPU.Parameters.Add(database.CreateParameter("$clockRate", clockRate.ToString()));
        createCPU.Parameters.Add(database.CreateParameter("$name", name));
        createCPU.ExecuteNonQuery();
        database.Close();
      }
    }

    private const string CommandAddCPU = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";

    /// <summary>
    /// Returns the id from the Databse 
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetId(string name)
    {
      using (var getID = database.CreateCommand(CommandSelectID))
      {
        database.Open();
        getID.Parameters.Add(database.CreateParameter("$name", name));
        IDataReader reader = getID.ExecuteReader();

        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        database.Close();
        return id;
      }
    }

    private const string CommandSelectID = "SELECT cpu_id FROM Cpu WHERE name = $name";

  }
}
