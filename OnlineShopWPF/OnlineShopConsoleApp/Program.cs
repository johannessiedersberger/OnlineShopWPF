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
      
      //dbF.AddGraphicToDataBase(4, "NVIDIA TITAN X");
      //Console.WriteLine("GraphicID " + dbF.GetGraphicCardId("NVIDIA TITAN X"));

      //dbF.AddNewHardDriveToDatabase("ssd", 1011);
      //Console.WriteLine("HardDriveID " + dbF.GetHardDriveId("ssd", 1011));

      //dbF.AddNewCpuToDatabase(4, 9.9, "INTEL CORE i9 8800k");
      //Console.WriteLine("CPU ID " + dbF.GetCpuId("INTEL CORE i9 8800k"));

      //dbF.AddProductToDataBase("ULTRA GAMING NOTEBOOK", 3000);

      //dbF.AddNewNotebookToDatabase(dbF.GetProductId("ULTRA GAMING NOTEBOOK"), dbF.GetGraphicCardId("NVIDIA TITAN X"), dbF.GetCpuId("INTEL CORE i9 8800k"), dbF.GetHardDriveId("ssd", 1011), 128, 10000, "windows");
      //Console.WriteLine(dbF.DoesNotebookAlreadyExist(dbF.GetProductId("ULTRA GAMING NOTEBOOK")));
     

      dbF.GetNotebooks(new NotebookSearchData
      {
        priceRange = new PriceRange(0, 100000000000)
      });
    }

    //static void CreateBeatsHeadPhone(DatabaseFactory db)
    //{
    //  Product p = new Product("Beats by Dr. Dre Beats Studio3 Wireless Kopfhörer, Mattschwarz", 247.00);
    //  p.WriteToDataBase(db);
    //  HeadPhone h = new HeadPhone(p.ID, true, false);
    //  h.WriteToDatabase(db);
    //}

    //static void CreateSonyHeadPhoone(DatabaseFactory db)
    //{
    //  Product p = new Product("SONY WH-1000XM2, Over-ear Kopfhörer, Near Field Communication, Headsetfunktion, Bluetooth, Schwarz", 259.00);
    //  p.WriteToDataBase(db);
    //  HeadPhone h = new HeadPhone(p.ID, true, true);
    //  h.WriteToDatabase(db);
    //}

    //static void CreateHPNotebook(DatabaseFactory db)
    //{
    //  //OMEN by HP 17 Gaming Notebook
    //  Product p = new Product("OMEN by HP 17 Gaming Notebook 2.0", 1070);
    //  CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
    //  Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
    //  HardDrive h = new HardDrive(256, "ssd");
    //  //DB   
    //  p.WriteToDataBase(db);
    //  c.WriteToDatabase(db);
    //  g.WriteToDatabase(db);
    //  h.WriteToDatabase(db);
    //  //Notebook
    //  Notebook notebook = new Notebook(p.ID, g.Id, c.Id, h.Id, 16, 720, "windows");
    //  notebook.WriteToDataBase(db);
    //  Graphic gr = notebook.GraphicCard;
    //}

    //static void CreateDellNotebook(DatabaseFactory db)
    //{
    //  //OMEN by HP 17 Gaming Notebook
    //  Product p = new Product("Dell G3 17", 828.99);
    //  CPU c = new CPU(4, 3.9, "Intel® Core™ i5-8300H");
    //  Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050");
    //  HardDrive h = new HardDrive(128, "ssd");
    //  //DB
    //  p.WriteToDataBase(db);
    //  c.WriteToDatabase(db);
    //  g.WriteToDatabase(db);
    //  h.WriteToDatabase(db);
    //  //Notebook
    //  Notebook notebook = new Notebook(p.ID, g.Id, c.Id, h.Id, 16, 920, "windows");
    //  notebook.WriteToDataBase(db);
    //}
  }
}