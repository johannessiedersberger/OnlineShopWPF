using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;

namespace OnlineShop
{
  public class MainViewModel
  {
    public ObservableCollection<NotebookView> ProductList { get; private set; } = new ObservableCollection<NotebookView>();

    public MainViewModel()
    {
      ShowNotebooks();
    }

    public void ShowNotebooks()
    {
      ProductList.Clear();
      DatabaseFactory dbF = new DatabaseFactory(new MySqliteDatabase(Shop.file));
      List<Product> products = dbF.FindMatchingProducts(new NotebookQueryParams { });
      foreach (Product notebookProduct in products)
      {
        Notebook nb = dbF.GetNotebook(notebookProduct);
        NotebookView view = new NotebookView
        {
          Name = nb.Product.Name,
          Price = nb.Product.Price,
          Cpu = nb.Cpu.Name,
          Ram = nb.Ram,
          HdMemory = nb.HardDrive.Memory,
          HdType = nb.HardDrive.Type,
        };
        ProductList.Add(view);
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

