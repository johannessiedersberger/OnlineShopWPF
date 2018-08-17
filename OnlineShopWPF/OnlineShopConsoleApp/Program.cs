using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using OnlineShop;
namespace OnlineShopConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      MySqliteDatabase db = new MySqliteDatabase(Shop.file);
      CreateHPNotebook(db);
      CreateDellNotebook(db);
      CreateBeatsHeadPhone(db);
      CreateSonyHeadPhoone(db);

      IReader r = Shop.GetNotebooksByHardDriveSize(200,3000);
      while(r.Read())
        Console.WriteLine(r[0] +" "+ r[1] +" "+ r[2]);
    }

    static void CreateBeatsHeadPhone(MySqliteDatabase db)
    {
      Product p = new Product("Beats by Dr. Dre Beats Studio3 Wireless Kopfhörer, Mattschwarz", 247.00);
      p.WriteToDataBase(db);
      HeadPhone h = new HeadPhone(p.ID, true, false);
      h.WriteToDatabase(db);
    }

    static void CreateSonyHeadPhoone(MySqliteDatabase db)
    {
      Product p = new Product("SONY WH-1000XM2, Over-ear Kopfhörer, Near Field Communication, Headsetfunktion, Bluetooth, Schwarz", 259.00);
      p.WriteToDataBase(db);
      HeadPhone h = new HeadPhone(p.ID, true, true);
      h.WriteToDatabase(db);
    }

    static void CreateHPNotebook(MySqliteDatabase db)
    {     
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product("OMEN by HP 17 Gaming Notebook", 1070);
      CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
      HardDrive h = new HardDrive(256, "ssd");
      //DB   
      p.WriteToDataBase(db);
      c.WriteToDatabase(db);
      g.WriteToDatabase(db);
      h.WriteToDatabase(db);
      //Notebook
      Notebook notebook = new Notebook(p.ID, g.Id, c.Id, h.Id, 16, 720, "windows");
      notebook.WriteToDataBase(db);
    }

    static void CreateDellNotebook(MySqliteDatabase db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product("Dell G3 17", 828.99);
      CPU c = new CPU(4, 3.9, "Intel® Core™ i5-8300H");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050");
      HardDrive h = new HardDrive(128, "ssd");
      //DB
      p.WriteToDataBase(db);
      c.WriteToDatabase(db);
      g.WriteToDatabase(db);
      h.WriteToDatabase(db);
      //Notebook
      Notebook notebook = new Notebook(p.ID, g.Id, c.Id, h.Id, 16, 920, "windows");
      notebook.WriteToDataBase(db);
    }
  }
}