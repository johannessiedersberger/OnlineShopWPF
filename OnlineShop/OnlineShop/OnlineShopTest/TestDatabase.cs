using OnlineShop;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace OnlineShopTest
{
  /// <summary>
  /// A Database for Testing
  /// </summary>
  public class MyTestSqliteDatabase : MySqliteDatabase
  {
    #region Member Fields

    private readonly string FileName;

    #endregion

    /// <summary>
    /// Creates the Path for a Database File in the Temp folder
    /// </summary>
    /// <returns></returns>
    public static string CreateTempPath()
    {
      return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString()}.db");
    }

    /// <summary>
    /// Creates the DB-File with the Tables
    /// </summary>
    /// <param name="fileName"></param>
    public MyTestSqliteDatabase(string fileName) 
      : base(CreateDataBase(fileName))
    {
      FileName = Connection.FileName;
      CreateTables();
    } 

    private static string CreateDataBase(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof(fileName));

      SQLiteConnection.CreateFile(fileName);
      return fileName;
    }

    protected override void Dispose(bool disposing)
    {
      Debug.Assert(disposing, "This object must be disposed!");

      base.Dispose(disposing);
      try
      {
        File.Delete(FileName);
      }
      catch
      {
        // swallow exceptions: Dispose must not throw
      }
    }

    private void CreateTables()
    {
      var createCPUTable = CreateNonQueryCommand(CommandCreateCPUTable);
      createCPUTable.Execute();
      var createGraphicTable = CreateNonQueryCommand(CommandCreateGraphicTable);
      createGraphicTable.Execute();
      var createHardDriveTable = CreateNonQueryCommand(CommandCreateHardDriveTable);
      createHardDriveTable.Execute();
      var createProductTable = CreateNonQueryCommand(CommandCreateProductTable);
      createProductTable.Execute();
      var createNotebookTable = CreateNonQueryCommand(CommandCreateNotebookTable);
      createNotebookTable.Execute();
    }

    private const string CommandCreateCPUTable = "CREATE TABLE `Cpu` (	`cpu_id`	INTEGER PRIMARY KEY AUTOINCREMENT,	`count`	INTEGER,	`clock_rate`	REAL,	`name`	TEXT); ";
    private const string CommandCreateGraphicTable = "CREATE TABLE `Graphics` (	`graphic_id`	INTEGER PRIMARY KEY AUTOINCREMENT,	`vram`	INTEGER,	`name`	TEXT);";
    private const string CommandCreateHardDriveTable = "CREATE TABLE `HardDrives` (	`memory`	INTEGER,	`type`	TEXT,	`hard_drive_id`	INTEGER PRIMARY KEY AUTOINCREMENT);";
    private const string CommandCreateProductTable = "CREATE TABLE `Products` (	`product_id`	INTEGER PRIMARY KEY AUTOINCREMENT,	`name`	TEXT,	`price`	REAL);";
    private const string CommandCreateNotebookTable = "CREATE TABLE `Notebooks` (	`product_id`	INTEGER,	`graphic_id`	INTEGER,	`cpu_id`	INTEGER,	`hard_drive_id`	INTEGER," +
      "	`ram_memory`	INTEGER,	`average_battery_time`	INTEGER,	`os`	TEXT,	PRIMARY KEY(`product_id`),	FOREIGN KEY(`product_id`) REFERENCES `Products`(`product_id`));";

  }

}
