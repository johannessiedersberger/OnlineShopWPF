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
    #region cpu

    [Test]
    public void TestAddCPU()
    {
      //Given
      int count = 4;
      double clockRate = 3.7;
      string name = "INTEL CORE i7";
      FakeDataBase fakeDB = new FakeDataBase();
      CPU cpu = new CPU(count, clockRate, name);
      //When
      cpu.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$count"], Is.EqualTo(count));
      Assert.That(fakeDB.NonQueries[0].Parameters["$clockRate"], Is.EqualTo(clockRate));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo(name));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));

      Assert.That(cpu.Count, Is.EqualTo(count));
      Assert.That(cpu.ClockRate, Is.EqualTo(clockRate));
      Assert.That(cpu.Name, Is.EqualTo(name));
    }

    [Test]
    public void TestGetCPUIDException()
    {
      //Given
      CPU cpu = new CPU(4, 3.7, "INTEL CORE i7");
      //When /Then
      Assert.That(() => cpu.Id, Throws.TypeOf(typeof(NullReferenceException)));
    }

    [Test]
    public void TestGetCPUID()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      CPU cpu = new CPU(4, 3.7, "INTEL CORE i7");
      cpu.WriteToDatabase(fakeDB);
      //When /Then
      Assert.That(cpu.Id, Is.EqualTo(0));
    }

    #endregion

    #region graphic
    [Test]
    public void TestAddGraphic()
    {
      //Given
      int vram = 4;
      string name = "NVIDIA GeForce GTX 1080ti";
      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(vram, name);
      //When
      graphic.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$vram"], Is.EqualTo(vram));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo(name));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));
      
      Assert.That(graphic.VRAM, Is.EqualTo(vram));
      Assert.That(graphic.Name, Is.EqualTo(name));
    }

    [Test]
    public void TestGraphicID()
    {
      //Given 
      int vram = 4;
      string name = "NVIDIA GeForce GTX 1080ti";

      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(vram, name);
      graphic.WriteToDatabase(fakeDB);
      //when /then
      Assert.That(graphic.Id, Is.EqualTo(0));
    }

    [Test]
    public void TestGraphicIDExpception()
    {
      //Given 
      int vram = 4;
      string name = "NVIDIA GeForce GTX 1080ti";

      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(vram, name);

      //when /then
      Assert.That(graphic.Id, Throws.TypeOf(typeof(NullReferenceException)));
    }
    #endregion
  }
}
