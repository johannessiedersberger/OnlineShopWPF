using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class MainViewModel
  {

    public List<NotebookView> Notebooks { get; private set; } = new List<NotebookView>();

    public MainViewModel()
    {
      ShowNotebooks();
    }

    private void ShowNotebooks()
    {
      DatabaseFactory dbF = new DatabaseFactory(new MySqliteDatabase(Shop.file));
      List<Product> products = dbF.FindMatchingProducts(new NotebookQueryParams { });
      foreach (Product notebookProduct in products)
      {
        Notebook nb = dbF.GetNotebook(notebookProduct);
        NotebookView view = new NotebookView
        {
          Name = notebookProduct.Name,
          Price = notebookProduct.Price,
          Cpu = nb.Cpu.Name,
          Ram = nb.Ram,
          HdMemory = nb.HardDrive.Memory,
          HdType = nb.HardDrive.Type,
        };
        Notebooks.Add(view);
      }
      
    }
  }

  public class NotebookView
  {
    public string Name { get; set; }
    public double Price { get; set; }
    public string Cpu { get; set; }
    public int Ram { get; set; }
    public int HdMemory { get; set; }
    public string HdType { get; set; }
  }
}

