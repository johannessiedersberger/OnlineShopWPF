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
    public DatabaseFactory db = new DatabaseFactory(new MySqliteDatabase(testDb));

    [Test]
    public void TestAddCPU()
    {
      db.DeleteEveryThing();
      CPU cpu = new CPU(4, 2.5, "INTEL CORE i5 3200k");
      db.AddNewCpuToDatabase(cpu);
      db.AddNewCpuToDatabase(cpu);
      int cpuId = db.GetCpuId(cpu);
      Assert.That(cpu.Name, Is.EqualTo(db.GetCPU(cpuId).Name));
    }

    [Test]
    public void TestAddGraphic()
    {
      db.DeleteEveryThing();
      Graphic graphic = new Graphic(4, "NVIDIA GEFORCE 980TI");
      db.AddGraphicToDataBase(graphic);
      db.AddGraphicToDataBase(graphic);
      int graphicId = db.GetGraphicCardId(graphic);
      Assert.That(graphic.Name, Is.EqualTo(db.GetGraphicCard(graphicId).Name));
    }

    [Test]
    public void TestAddHardDrive()
    {
      db.DeleteEveryThing();
      HardDrive hardDrive = new HardDrive(1024, "ssd");
      db.AddNewHardDriveToDatabase(hardDrive);
      db.AddNewHardDriveToDatabase(hardDrive);
      int hardDriveId = db.GetHardDriveId(hardDrive);
      Assert.That(hardDrive.Memory, Is.EqualTo(db.GetHardDrive(hardDriveId).Memory));
      Assert.That(hardDrive.Type, Is.EqualTo(db.GetHardDrive(hardDriveId).Type));

    }

    [Test]
    public void TestAddProduct()
    {
      db.DeleteEveryThing();
      Product p = new Product("NOTEBOOK", 1000);
      db.AddProductToDataBase(p);
      db.AddProductToDataBase(p);
      int productId = db.GetProductId(p);
      Assert.That(p.Name, Is.EqualTo(db.GetProduct(productId).Name));
      
    }

    [Test]
    public void TestAddNotebook()
    {
      db.DeleteEveryThing();
      Notebook nb = CreateNotebook();
      db.AddNewNotebookToDatabase(nb);
      db.AddNewNotebookToDatabase(nb);
      Assert.That(nb.Product.Name, Is.EqualTo(db.GetNotebook(nb.Product).Product.Name));
           
    }

    [Test]
    public void TestDeleteNotebook()
    {
      db.DeleteEveryThing();
      Notebook nb = CreateNotebook();
      Notebook nb2 = CreateNotebook2();
      db.AddNewNotebookToDatabase(nb);
      db.AddNewNotebookToDatabase(nb2);
      db.DeleteCompleteNotebook(nb);
      db.DeleteCompleteNotebook(nb2);
      Assert.That(() => db.GetNotebook(nb.Product), Throws.InvalidOperationException);
      Assert.That(() => db.GetCpuId(nb.Cpu), Throws.InvalidOperationException);
      Assert.That(() => db.GetGraphicCardId(nb.Graphic), Throws.InvalidOperationException);
      Assert.That(() => db.GetHardDriveId(nb.HardDrive), Throws.InvalidOperationException);
      Assert.That(() => db.GetProductId(nb.Product), Throws.InvalidOperationException);
      
    }

    [Test]
    public void TestGetNotebooksbyCPU()
    {
      db.DeleteEveryThing();
      Notebook nb = CreateNotebook();
      Notebook nb2 = CreateNotebook2();
      Notebook nb3 = CreateNotebook3();
      db.AddNewNotebookToDatabase(nb);
      db.AddNewNotebookToDatabase(nb2);
      db.AddNewNotebookToDatabase(nb3);
      List<Product> notebooks = db.FindMatchingProducts(new NotebookQueryParams {
        CPUQueryParams = new CPUQueryParams {
          cpuName ="INTEL",
          cpuClockRate = new OnlineShop.Range(5.5,5.5),
          cpuCount = new OnlineShop.Range(4,4),
        }});
      Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Product.Name));
    }

    [Test]
    public void TestGetNotebooksbyGraphic()
    {
      db.DeleteEveryThing();
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
          vramRange = new OnlineShop.Range(4,4),
        }
      });
      Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Product.Name));
    }

    [Test]
    public void TestGetNotebooksbyHardDrive()
    {
      db.DeleteEveryThing();
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
      Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Product.Name));
    }

    [Test]
    public void TestGetNotebooksbyNotebookData()
    {
      db.DeleteEveryThing();
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
          batteryTimeRange = new OnlineShop.Range(950,950),
          notebookName = "ASUS",
          os = "linux",
          priceRange = new OnlineShop.Range(999.99, 999.99),
          ramMemoryRange = new OnlineShop.Range(32,32),
        }
      });
      Assert.That(notebooks[0].Name, Is.EqualTo(nb3.Product.Name));
    }

    [Test]
    public void TestGetProducts()
    {
      db.DeleteEveryThing();
      Notebook nb = CreateNotebook();
      db.AddNewNotebookToDatabase(nb);
      List<Product> notebooks = db.FindMatchingProducts(new ProductQueryParams {
        Price = new OnlineShop.Range(1199.99, 1199.99),
        Name = "DELL GAMING NOTEBOOK",
      });
      Assert.That(notebooks[0].Name, Is.EqualTo(nb.Product.Name));
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
