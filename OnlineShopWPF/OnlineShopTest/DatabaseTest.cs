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
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      CPU cpu = new CPU(4, 3.7, "INTEL CORE i7");
      //When
      cpu.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$count"], Is.EqualTo(4));
      Assert.That(fakeDB.NonQueries[0].Parameters["$clockRate"], Is.EqualTo(3.7));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo("INTEL CORE i7"));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));
    }

    [Test]
    public void TestGetCPUID()
    {
      CPU cpu = new CPU(4, 3.7, "INTEL CORE i7");

    }

  }
}
