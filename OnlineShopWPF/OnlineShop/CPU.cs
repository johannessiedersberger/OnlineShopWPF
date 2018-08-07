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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count">The number of kernels</param>
    /// <param name="clockRate">The clock rate in ghz </param>
    /// <param name="name">The name of the cpu </param>
    public CPU(int count, double clockRate, string name)
    {
      Count = count;
      ClockRate = clockRate;
      Name = name;
    }

    public const string CommandAddCPU = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";

    /// <summary>
    /// Returns the id from the Databse
    /// If The Object does not exist in the database, the method returns 0
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetId(IDatabase db)
    {
      using (var getID = db.CreateCommand(CommandSelectID))
      {
        db.Open();
        getID.Parameters.Add(db.CreateParameter("$name", Name));
        IDataReader reader = getID.ExecuteReader();

        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        db.Close();
        return id;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public void WriteToDatabase(IDatabase db)
    {
      using (var createCPU = db.CreateCommand(CommandAddCPU))
      {
        db.Open();
        createCPU.Parameters.Add(db.CreateParameter("$id", null));
        createCPU.Parameters.Add(db.CreateParameter("$count", Count.ToString()));
        createCPU.Parameters.Add(db.CreateParameter("$clockRate", ClockRate.ToString()));
        createCPU.Parameters.Add(db.CreateParameter("$name", Name));
        createCPU.ExecuteNonQuery();
        db.Close();
      }
    }

    private const string CommandSelectID = "SELECT cpu_id FROM Cpu WHERE name = $name";

  }
}
