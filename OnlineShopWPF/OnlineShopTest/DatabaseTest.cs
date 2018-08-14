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

    static class TestCPU
    {
      public static int Count = 4;
      public static double ClockRate = 3.7;
      public static string Name = "INTEL CORE i7";
    }

    [Test]
    public void TestAddCPU()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      CPU cpu = new CPU(TestCPU.Count, TestCPU.ClockRate, TestCPU.Name);
      //When
      cpu.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$count"], Is.EqualTo(TestCPU.Count));
      Assert.That(fakeDB.NonQueries[0].Parameters["$clockRate"], Is.EqualTo(TestCPU.ClockRate));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo(TestCPU.Name));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));

      Assert.That(cpu.Count, Is.EqualTo(TestCPU.Count));
      Assert.That(cpu.ClockRate, Is.EqualTo(TestCPU.ClockRate));
      Assert.That(cpu.Name, Is.EqualTo(TestCPU.Name));
    }

    [Test]
    public void TestGetCPUIDException()
    {
      //Given
      CPU cpu = new CPU(TestCPU.Count, TestCPU.ClockRate, TestCPU.Name);
      //When /Then
      Assert.That(() => cpu.Id, Throws.TypeOf(typeof(NullReferenceException)));
    }

    [Test]
    public void TestGetCPUID()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      CPU cpu = new CPU(TestCPU.Count, TestCPU.ClockRate, TestCPU.Name);
      cpu.WriteToDatabase(fakeDB);
      //When /Then
      Assert.That(cpu.Id, Is.EqualTo(0));
    }

    #endregion

    #region graphic

    static class TestGraphic
    {     
      public static int Vram = 4;
      public static string Name = "NVIDIA GeForce GTX 1080ti";
    }

    [Test]
    public void TestAddGraphic()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(TestGraphic.Vram, TestGraphic.Name);
      //When
      graphic.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$vram"], Is.EqualTo(TestGraphic.Vram));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo(TestGraphic.Name));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));
      
      Assert.That(graphic.VRAM, Is.EqualTo(TestGraphic.Vram));
      Assert.That(graphic.Name, Is.EqualTo(TestGraphic.Name));
    }

    [Test]
    public void TestGraphicID()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(TestGraphic.Vram, TestGraphic.Name);
      graphic.WriteToDatabase(fakeDB);
      //when /then
      Assert.That(graphic.Id, Is.EqualTo(0));
    }

    [Test]
    public void TestGraphicIDExpception()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      Graphic graphic = new Graphic(TestGraphic.Vram, TestGraphic.Name);

      //when /then
      Assert.That(() => graphic.Id, Throws.TypeOf(typeof(NullReferenceException)));
    }
    #endregion

    #region HardDrive
    static class TestHardDrive
    {
      public static int Memory = 128;
      public static string Type = "ssd";
    }

    [Test]
    public void TestAddHardDrive()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      HardDrive hardDrive = new HardDrive(TestHardDrive.Memory, TestHardDrive.Type);
      //When
      hardDrive.WriteToDatabase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$type"], Is.EqualTo(TestHardDrive.Type));
      Assert.That(fakeDB.NonQueries[0].Parameters["$memory"], Is.EqualTo(TestHardDrive.Memory));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));

      Assert.That(hardDrive.Memory, Is.EqualTo(TestHardDrive.Memory));
      Assert.That(hardDrive.Type, Is.EqualTo(TestHardDrive.Type));
    }

    [Test]
    public void TestHardDriveID()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      HardDrive hardDrive = new HardDrive(TestHardDrive.Memory, TestHardDrive.Type);
      hardDrive.WriteToDatabase(fakeDB);
      //when /then
      Assert.That(hardDrive.Id, Is.EqualTo(0));
    }

    [Test]
    public void TestHardDriveIDExpception()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      HardDrive harddrive = new HardDrive(TestHardDrive.Memory, TestHardDrive.Type);

      //when /then
      Assert.That(() => harddrive.Id, Throws.TypeOf(typeof(NullReferenceException)));
    }

    #endregion

    #region product
    static class TestProduct
    {
      public static string Name = "OMEN by HP 17 Gaming Notebook";
      public static int Price = 1070;
    }

    public void TestAddProduct()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      Product p = new Product(TestProduct.Name,TestProduct.Price);
      //When
      p.WriteToDataBase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[0].Parameters["$id"], Is.EqualTo(null));
      Assert.That(fakeDB.NonQueries[0].Parameters["$name"], Is.EqualTo(TestProduct.Name));
      Assert.That(fakeDB.NonQueries[0].Parameters["$price"], Is.EqualTo(TestProduct.Price));
      Assert.That(fakeDB.NonQueries[0].WasExecuted, Is.EqualTo(true));

      Assert.That(TestProduct.Name, Is.EqualTo(TestProduct.Name));
      Assert.That(TestProduct.Price, Is.EqualTo(TestProduct.Price));
    }

    public void TestProductID()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      Product product = new Product(TestProduct.Name, TestProduct.Price);
      product.WriteToDataBase(fakeDB);
      //When /THen
      Assert.That(product.ID, Is.EqualTo(0));
    }

    public void TestProductIDException()
    {
      //Given 
      FakeDataBase fakeDB = new FakeDataBase();
      Product product = new Product(TestProduct.Name, TestProduct.Price);

      //when /then
      Assert.That(() => product.ID, Throws.TypeOf(typeof(NullReferenceException)));
    }
    #endregion

    #region notebook
    static class TestNotebook
    {
      public static int RAM = 16;
      public static int BatteryTime = 720;
      public static string OS = "windows";
    }

    [Test]
    public void NotebookAddTest()
    {
      //Given
      FakeDataBase fakeDB = new FakeDataBase();
      Product p = new Product("OMEN by HP 17 Gaming Notebook", 1070);
      CPU c = new CPU(4, 3.8, "Intel® Core™ i7-7700HQ");
      Graphic g = new Graphic(4, "NVIDIA® GeForce® GTX 1050 Ti");
      HardDrive h = new HardDrive(256, "ssd");

      p.WriteToDataBase(fakeDB);
      c.WriteToDatabase(fakeDB);
      g.WriteToDatabase(fakeDB);
      h.WriteToDatabase(fakeDB);

      //When
      Notebook nb = new Notebook(p.ID, g.Id, c.Id, h.Id, TestNotebook.RAM, TestNotebook.BatteryTime, TestNotebook.OS);
      nb.WriteToDataBase(fakeDB);
      //Then
      Assert.That(fakeDB.NonQueries[4].Parameters["$id"], Is.EqualTo(0));
      Assert.That(fakeDB.NonQueries[4].Parameters["$graphicId"], Is.EqualTo(0));
      Assert.That(fakeDB.NonQueries[4].Parameters["$cpuId"], Is.EqualTo(0));
      Assert.That(fakeDB.NonQueries[4].Parameters["$hardDriveId"], Is.EqualTo(0));
      Assert.That(fakeDB.NonQueries[4].Parameters["$ramMemory"], Is.EqualTo(TestNotebook.RAM));
      Assert.That(fakeDB.NonQueries[4].Parameters["$avgBatteryTime"], Is.EqualTo(TestNotebook.BatteryTime));
      Assert.That(fakeDB.NonQueries[4].Parameters["$os"], Is.EqualTo(TestNotebook.OS));
      Assert.That(fakeDB.NonQueries[4].WasExecuted, Is.EqualTo(true));

      Assert.That(TestProduct.Name, Is.EqualTo(TestProduct.Name));
      Assert.That(TestProduct.Price, Is.EqualTo(TestProduct.Price));

    }

    #endregion
  }
}
