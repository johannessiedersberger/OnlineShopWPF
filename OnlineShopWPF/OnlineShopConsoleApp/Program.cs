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
      CPU c = new CPU(1234567, 3.5f, "INTEL PENTIUM");
      Graphic g = new Graphic(4, "NVIDIA GeForce 1080TI");
      HardDrive h = new HardDrive(256, "ssd");
    }
  }
}