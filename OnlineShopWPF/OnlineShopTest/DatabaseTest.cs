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

    public static string testDb = @"C:\Users\jsiedersberger\Documents\GitHub\OnlineShopWPF\OnlineShopWPF\OnlineShopTest.db";

    [Test]
    public void TestAddCPU()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        CPU cpu = new CPU(numCores: 4, clockRate: 2.5, name: "INTEL CORE i5 3200k");
        db.AddNewCpuToDatabase(cpu);
        Assert.That(() => db.AddNewCpuToDatabase(cpu), Throws.TypeOf<ProductAlreadyExistsException>());
        int cpuId = db.GetCpuId(cpu);
        Assert.That(cpu.Name, Is.EqualTo(db.GetCPU(cpuId).Name));
      }
    }

    [Test]
    public void TestAddGraphic()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Graphic graphic = new Graphic(4, "NVIDIA GEFORCE 980TI");
        db.AddGraphicToDataBase(graphic);
        Assert.That(() => db.AddGraphicToDataBase(graphic), Throws.TypeOf<ProductAlreadyExistsException>());
        int graphicId = db.GetGraphicCardId(graphic);
        Assert.That(graphic.Name, Is.EqualTo(db.GetGraphicCard(graphicId).Name));
      }
    }

    [Test]
    public void TestAddHardDrive()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        HardDrive hardDrive = new HardDrive(1024, "ssd");
        db.AddNewHardDriveToDatabase(hardDrive);
        Assert.That(() => db.AddNewHardDriveToDatabase(hardDrive), Throws.TypeOf<ProductAlreadyExistsException>());
        int hardDriveId = db.GetHardDriveId(hardDrive);
        Assert.That(hardDrive.Memory, Is.EqualTo(db.GetHardDrive(hardDriveId).Memory));
        Assert.That(hardDrive.Type, Is.EqualTo(db.GetHardDrive(hardDriveId).Type));
      }
    }

    [Test]
    public void TestAddProduct()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Product p = new Product("NOTEBOOK", 1000);
        db.AddProductToDataBase(p);
        Assert.That(() => db.AddProductToDataBase(p), Throws.TypeOf<ProductAlreadyExistsException>());
        int productId = db.GetProductId(p);
        Assert.That(p.Name, Is.EqualTo(db.GetProduct(productId).Name));
      }
    }

    [Test]
    public void TestAddNotebook()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        db.AddNewNotebookToDatabase(nb);
        Assert.That(() => db.AddNewNotebookToDatabase(nb), Throws.TypeOf<ProductAlreadyExistsException>());
        Assert.That(nb.Name, Is.EqualTo(db.GetNotebook(nb).Name));
      }
    }

    [Test]
    public void TestDeleteNotebook()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        Notebook nb2 = CreateNotebook2();
        db.AddNewNotebookToDatabase(nb);
        db.AddNewNotebookToDatabase(nb2);
        db.DeleteCompleteNotebook(nb);
        db.DeleteCompleteNotebook(nb2);
        Assert.That(() => db.GetNotebook(nb), Throws.TypeOf<ProductNotFoundException>());
        Assert.That(() => db.GetCpuId(nb.Cpu), Throws.TypeOf<ProductNotFoundException>());
        Assert.That(() => db.GetGraphicCardId(nb.Graphic), Throws.TypeOf<ProductNotFoundException>());
        Assert.That(() => db.GetHardDriveId(nb.HardDrive), Throws.TypeOf<ProductNotFoundException>());
        Assert.That(() => db.GetProductId(nb), Throws.TypeOf<ProductNotFoundException>());
      }
    }

    [Test]
    public void TestGetNotebooksbyCPU()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        Notebook nb2 = CreateNotebook2();
        Notebook nb3 = CreateNotebook3();
        db.AddNewNotebookToDatabase(nb);
        db.AddNewNotebookToDatabase(nb2);
        db.AddNewNotebookToDatabase(nb3);
        List<Product> notebooks = db.FindMatchingProducts(new NotebookQueryParams
        {
          CPUQueryParams = new CPUQueryParams
          {
            cpuName = "INTEL",
            cpuClockRate = new OnlineShop.Range(5.5, 5.5),
            cpuCount = new OnlineShop.Range(4, 4),
          }
        });
        Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Name));
      }
    }

    [Test]
    public void TestGetNotebooksbyGraphic()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        Notebook nb2 = CreateNotebook2();
        Notebook nb3 = CreateNotebook3();
        db.AddNewNotebookToDatabase(nb);
        db.AddNewNotebookToDatabase(nb2);
        db.AddNewNotebookToDatabase(nb3);
        List<Product> notebooks = db.FindMatchingProducts(new NotebookQueryParams
        {
          GraphicQueryParams = new GraphicQueryParams
          {
            graphicCardName = "NVIDIA GEFORCE 1080TI",
            vramRange = new OnlineShop.Range(4, 4),
          }
        });
        Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Name));
      }
    }

    [Test]
    public void TestGetNotebooksbyHardDrive()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        Notebook nb2 = CreateNotebook2();
        Notebook nb3 = CreateNotebook3();
        db.AddNewNotebookToDatabase(nb);
        db.AddNewNotebookToDatabase(nb2);
        db.AddNewNotebookToDatabase(nb3);
        List<Product> notebooks = db.FindMatchingProducts(new NotebookQueryParams
        {
          HardDriveQueryParams = new HardDriveQueryParams
          {
            hdType = "ssd",
            hdMemoryRange = new OnlineShop.Range(2048, 2048),
          }
        });
        Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Name));
      }
    }

    [Test]
    public void TestGetNotebooksbyNotebookData()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        Notebook nb2 = CreateNotebook2();
        Notebook nb3 = CreateNotebook3();
        db.AddNewNotebookToDatabase(nb);
        db.AddNewNotebookToDatabase(nb2);
        db.AddNewNotebookToDatabase(nb3);
        List<Product> notebooks = db.FindMatchingProducts(new NotebookQueryParams
        {
          NotebookDataQueryParams = new NotebookDataQueryParams
          {
            batteryTimeRange = new OnlineShop.Range(950, 950),
            notebookName = "ASUS",
            os = "linux",
            priceRange = new OnlineShop.Range(999.99, 999.99),
            ramMemoryRange = new OnlineShop.Range(32, 32),
          }
        });
        Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Name));
      }
    }

    [Test]
    public void TestGetProducts()
    {
      var databasePath = MyTestSqliteDatabase.CreateTempPath();
      using (var db = new DatabaseFactory(new MyTestSqliteDatabase(databasePath)))
      {
        Notebook nb = CreateNotebook();
        db.AddNewNotebookToDatabase(nb);
        List<Product> notebooks = db.FindMatchingProducts(new ProductQueryParams
        {
          Price = new OnlineShop.Range(1199.99, 1199.99),
          Name = "DELL GAMING NOTEBOOK",
        });
        Assert.That(notebooks[0].Name, Is.EqualTo(nb.Name));
      }
    }

    private Notebook CreateNotebook()
    {
      Product product = new Product("DELL GAMING NOTEBOOK", 1199.99);
      CPU cpu = new CPU(4, 2.5, "INTEL CORE i5 3200k");
      Graphic graphic = new Graphic(4, "NVIDIA GEFORCE 980TI");
      HardDrive hardDrive = new HardDrive(1024, "ssd");
      return new Notebook(product, graphic, cpu, hardDrive, 16, 900, OS.windows);

    }
    private Notebook CreateNotebook2()
    {
      Product product = new Product("DELL GAMING NOTEBOOK 2.0", 1199.99);
      CPU cpu = new CPU(4, 2.5, "INTEL CORE i5 3200k");
      Graphic graphic = new Graphic(4, "NVIDIA GEFORCE 980TI");
      HardDrive hardDrive = new HardDrive(1024, "ssd");
      return new Notebook(product, graphic, cpu, hardDrive, 16, 900, OS.windows);
    }
    private Notebook CreateNotebook3()
    {
      Product product = new Product("ASUS", 999.99);
      CPU cpu = new CPU(4, 5.5, "INTEL CORE i7 7200HQ");
      Graphic graphic = new Graphic(4, "NVIDIA GEFORCE 1080TI");
      HardDrive hardDrive = new HardDrive(2048, "ssd");
      return new Notebook(product, graphic, cpu, hardDrive, 32, 950, OS.linux);
    }

  }
}
