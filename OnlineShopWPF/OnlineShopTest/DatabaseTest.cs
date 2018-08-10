using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using OnlineShop;
using System.Data;

namespace OnlineShopTest
{
  class DatabaseTest
  {
    [Test]
    public void TestAddCPU()
    {
     
      CPU cpu = new CPU(fakeDB, 4, 3.7, "INTEL CORE i7");
      
    }


  }
}
