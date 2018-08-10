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
      SqliteDatabase db = new SqliteDatabase(Shop.file);
      CPU cpu = new CPU(4, 4.4, "INTEL CORE i7");
      cpu.WriteToDatabase(db);
      int id = cpu.Id;
    }

    //static void CreateHPNotebook()
    //{
    //  //OMEN by HP 17 Gaming Notebook
    //  Product p = new Product("OMEN by HP 17 Gaming Notebook", 1070);
    //  CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
    //  Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
    //  HardDrive h = new HardDrive(256, "ssd");
     
    //}

    //static void CreateDellNotebook()
    //{
    //  //OMEN by HP 17 Gaming Notebook
    //  Product p = new Product("Dell G3 17", 828.99);
    //  CPU c = new CPU(4, 3.9, "Intel® Core™ i5-8300H");
    //  Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050");
    //  HardDrive h = new HardDrive(128, "ssd");
      
    //}
  }
}