using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace OnlineShop
{
  /// <summary>
  /// A CPU from a Notebook
  /// </summary>
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
    /// The Database that contains the cpu
    /// </summary>
    private IDatabase _database;

    /// <summary>
    /// Saves the Parameters as members
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

    private const string CommandSelectID = "SELECT cpu_id FROM Cpu WHERE name = $name";

    /// <summary>
    /// Gets the ID from the CPU
    /// </summary>
    public int Id
    {
      get
      {
        if (_database == null)
          throw new NullReferenceException("The Database does not exist");

        using (var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$name", Name);
          IReader reader = getID.ExecuteReader();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    /// <summary>
    /// Writes the CPU into the Database
    /// </summary>
    /// <param name="db">The Database that contains the cpu</param>
    public void WriteToDatabase(IDatabase db)
    {
      _database = db;
      using (var createCPU = db.CreateNonQueryCommand(CommandAddCPU))
      {
        createCPU.AddParameter("$id", null);
        createCPU.AddParameter("$count", Count);
        createCPU.AddParameter("$clockRate", ClockRate);
        createCPU.AddParameter("$name", Name);
        createCPU.Execute();
      }
    }

    private const string CommandAddCPU = "INSERT INTO Cpu(cpu_id, count, clock_rate, name) VALUES($id,$count,$clockRate,$name) ";

  }
}
