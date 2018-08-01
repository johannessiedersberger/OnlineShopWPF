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
      Product p = new Product("OMEN by HP 17 Gaming Notebook", 1070);

      CPU c = new CPU(4, 3.80f, "Intel® Core™ i7-7700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");

      Notebook n = new Notebook(
        new NotebookData()
        {
          ProductId = 1,
          GraphicId = 1,
          CpuId = 1,
          HardDriveId = 1,
          RamMemory = 8,
          AverageBatteryTime = 11,
          Os = "windwos"

        });
    }


  }
}