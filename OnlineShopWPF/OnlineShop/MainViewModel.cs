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
    public static ObservableCollection<NotebookView> ProductList { get; private set; } = new ObservableCollection<NotebookView>();
    
    public MainViewModel()
    {
      
    }

    public static void ShowNotebooks(List<Notebook> notebooks)
    {
      ProductList.Clear();
      foreach (Notebook notebook in notebooks)
      {
        NotebookView view = new NotebookView
        {
          Name = notebook.Product.Name,
          Price = notebook.Product.Price,
          Cpu = notebook.Cpu.Name,
          Ram = notebook.Ram,
          HdMemory = notebook.HardDrive.Memory,
          HdType = notebook.HardDrive.Type,
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

