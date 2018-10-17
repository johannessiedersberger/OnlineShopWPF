using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnlineShop
{
  public class MainViewModel
  {
    public MainViewModel()
    {
      FilterViewModel = new FilterViewModel();
    }

    public FilterViewModel FilterViewModel { get; private set; }
  }

  public class NotebookView
  {
    public NotebookView(Notebook notebook)
    {
      Name = notebook.Name;
      Price = notebook.Price;
      Cpu = notebook.Cpu.Name;
      Ram = notebook.RamInGB;
      HdMemory = notebook.HardDrive.MemoryInGB;
      HdType = notebook.HardDrive.Type;
    }

    public string Name { get; set; }
    public Money Price { get; set; }
    public string Cpu { get; set; }
    public int Ram { get; set; }
    public int HdMemory { get; set; }
    public string HdType { get; set; }
  }

}

