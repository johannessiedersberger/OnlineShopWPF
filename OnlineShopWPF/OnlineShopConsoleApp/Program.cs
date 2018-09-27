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
      DatabaseFactory dbF = new DatabaseFactory(new MySqliteDatabase(Shop.file));

      //CreateHPNotebook(dbF);
      //CreateHPNotebook2(dbF);
      //dbF.DeleteCompleteNotebook(dbF.GetProductId("OMEN by HP 17 Gaming Notebook 2.0"));
      //dbF.DeleteCompleteNotebook(dbF.GetProductId("OMEN by HP 17 Gaming Notebook"));

      dbF.AddCustomerToDatabase(new CustomerData
      {
        City = "Munich",
        CreditCardNumber = 123456789,
        Email = "johannes.siedersberger@gmx.de",
        FirstName = "Johannes",
        LastName = "Siedersberger",
        Password = "zxaud90",
        PhoneNumber = 017395869,
        StreetName = "Hauptstraße",
        StreetNumber = 3,
        ZipCode = 82902
      });
    }

    //static void CreateSonyHeadPhoone(DatabaseFactory db)
    //{
    //  Product p = new Product("SONY WH-1000XM2, Over-ear Kopfhörer, Near Field Communication, Headsetfunktion, Bluetooth, Schwarz", 259.00);
    //  p.WriteToDataBase(db);
    //  HeadPhone h = new HeadPhone(p.ID, true, true);
    //  h.WriteToDatabase(db);
    //}

    static void CreateHPNotebook(DatabaseFactory db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product(0, "OMEN by HP 17 Gaming Notebook", 1070);
      CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
      HardDrive h = new HardDrive(256, "ssd");

      db.AddGraphicToDataBase(g);
      db.AddNewCpuToDatabase(c);
      db.AddNewHardDriveToDatabase(h);
      db.AddProductToDataBase(p);
      //Notebook
      Notebook notebook = new Notebook(db.GetProductId(p.Name), db.GetGraphicCardId(g.Name), db.GetCpuId(c.Name),
        db.GetHardDriveId(h.Type, h.Memory), 16, 900, "windows");    
      
      db.AddNewNotebookToDatabase(notebook);
    }

    static void CreateHPNotebook2(DatabaseFactory db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product(0, "OMEN by HP 17 Gaming Notebook 2.0", 1070);
      CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
      HardDrive h = new HardDrive(256, "ssd");

      db.AddGraphicToDataBase(g);
      db.AddNewCpuToDatabase(c);
      db.AddNewHardDriveToDatabase(h);
      db.AddProductToDataBase(p);
      //Notebook
      Notebook notebook = new Notebook(db.GetProductId(p.Name), db.GetGraphicCardId(g.Name), db.GetCpuId(c.Name),
        db.GetHardDriveId(h.Type, h.Memory), 16, 900, "windows");

      db.AddNewNotebookToDatabase(notebook);
    }

    static void CreateDellNotebook(DatabaseFactory db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product(0,"Dell G3 17", 828.99);
      CPU c = new CPU(4, 3.9, "Intel® Core™ i5-8300H");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050");
      HardDrive h = new HardDrive(256, "ssd");
      //DB
      db.AddProductToDataBase(p);
      db.AddNewCpuToDatabase(c);
      db.AddGraphicToDataBase(g);
      db.AddNewHardDriveToDatabase(h);
      //Notebook
      Notebook notebook = new Notebook(db.GetProductId(p.Name), db.GetGraphicCardId(g.Name), db.GetCpuId(c.Name)
        , db.GetHardDriveId(h.Type, h.Memory), 16, 920, "windows");
      db.AddNewNotebookToDatabase(notebook);
      
    }
  }
}