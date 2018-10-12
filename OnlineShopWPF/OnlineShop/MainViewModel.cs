using System.Collections.Generic;
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
          Name = notebook.Name,
          Price = notebook.Price,
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
    public Money Price { get; set; }
    public string Cpu { get; set; }
    public int Ram { get; set; }
    public int HdMemory { get; set; }
    public string HdType { get; set; }
  }

}

