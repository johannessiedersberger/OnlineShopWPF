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

      //dbF.AddCustomerToDatabase(new CustomerData
      //{
      //  City = "Munich",
      //  CreditCardNumber = 123456789,
      //  Email = "johannes.siedersberger@gmx.de",
      //  FirstName = "Johannes",
      //  LastName = "Siedersberger",
      //  Password = "zxaud90",
      //  PhoneNumber = 017395869,
      //  StreetName = "Hauptstraße",
      //  StreetNumber = 3,
      //  ZipCode = 82902
      //});
      //Order order = new Order(2, DateTime.Now);
      //dbF.AddOrderToDatabase(order);
      //OrderEntry orderEntry = new OrderEntry(122, dbF.GetOrderID(order), 1);     
      //dbF.AddOrderEntrieToDatabase(orderEntry);
      dbF.DeleteCompleteNotebook(133);
      CreateHPNotebook(dbF);
      CreateMSINotebook(dbF);
      CreateDellNotebook(dbF);
      CreateAllienWareNOtebook(dbF);    
    }

    static void CreateSonyHeadPhoone(DatabaseFactory db)
    {
      Product p = new Product(0,"SONY WH-1000XM2, Over-ear Kopfhörer, Near Field Communication, Headsetfunktion, Bluetooth, Schwarz", 259.00);
      db.AddProductToDataBase(p);
      HeadPhone h = new HeadPhone(db.GetProductId(p.Name), false);
      db.AddNewHeadPhoneToDatabase(h);
    }

    static void CreateBeats(DatabaseFactory db)
    {
      Product p = new Product(0, "Beats Studio 3 Wireless Kopfhörer", 295.00);
      db.AddProductToDataBase(p);
      HeadPhone h = new HeadPhone(db.GetProductId(p.Name), true);
      db.AddNewHeadPhoneToDatabase(h);
    }

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

    static void CreateMSINotebook(DatabaseFactory db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product(0, "MSI GP73 Leopard", 2000.00);
      CPU c = new CPU(4, 3.3, "Intel® Core™ i7-5700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 720 Ti");
      HardDrive h = new HardDrive(512, "ssd");

      db.AddGraphicToDataBase(g);
      db.AddNewCpuToDatabase(c);
      db.AddNewHardDriveToDatabase(h);
      db.AddProductToDataBase(p);
      //Notebook
      Notebook notebook = new Notebook(db.GetProductId(p.Name), db.GetGraphicCardId(g.Name), db.GetCpuId(c.Name),
        db.GetHardDriveId(h.Type, h.Memory), 32, 900, "windows");

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

    static void CreateAllienWareNOtebook(DatabaseFactory db)
    {
      //OMEN by HP 17 Gaming Notebook
      Product p = new Product(0, "ALIENWARE 15", 1899.00);
      CPU c = new CPU(4, 3.9, "Intel® Core™ i7-8750H ");
      Graphic g = new Graphic(8, "NVIDIA® GeForce® GTX 1070 OC");
      HardDrive h = new HardDrive(1024, "ssd");
      //DB
      db.AddProductToDataBase(p);
      db.AddNewCpuToDatabase(c);
      db.AddGraphicToDataBase(g);
      db.AddNewHardDriveToDatabase(h);
      //Notebook
      Notebook notebook = new Notebook(db.GetProductId(p.Name), db.GetGraphicCardId(g.Name), db.GetCpuId(c.Name)
        , db.GetHardDriveId(h.Type, h.Memory), 16, 2000, "windows");
      db.AddNewNotebookToDatabase(notebook);
    }
  }
}