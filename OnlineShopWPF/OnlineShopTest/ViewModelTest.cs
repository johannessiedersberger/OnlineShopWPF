using NUnit.Framework;
using OnlineShop;

namespace OnlineShopTest
{
  class ViewModelTest
  {  
    /// <summary>
    /// Sets the CPU Properties in the View Model
    /// and Checks if the returned notebooks have the right attributes
    /// </summary>
    [Test]
    public void TestCPUViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.CPUManufacturer = CPUManufacturer.Intel;
      filter.MinClockRate = 1;
      filter.MaxClockRate = 4;
      filter.MinCPUCores = 4;
      filter.MaxCPUCores = 4;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Cpu.Name.Contains("Intel"));
        Assert.That(nb.Cpu.ClockRateInGHZ > 1 && nb.Cpu.ClockRateInGHZ < 4 );
        Assert.That(nb.Cpu.NumCores == 4);
      }
    }

    /// <summary>
    /// Sets the HardDrive Properties in the View Model
    /// and Checks if the returned notebooks have the right attributes
    /// </summary>
    [Test]
    public void TestHardDriveViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.HDType = HardDriveType.ssd;
      filter.MinHdMemory = 200;
      filter.MaxHdMemory = 300;
      foreach (Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.HardDrive.Type == "ssd");
        Assert.That(nb.HardDrive.MemoryInGB > 200 && nb.HardDrive.MemoryInGB < 300);
      }
    }

    /// <summary>
    /// Sets the Graphic Properties in the View Model
    /// and Checks if the returned notebook have the right attributes
    /// </summary>
    [Test]
    public void TestGraphicViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.GraphicCardManufacturer = GraphicManufacturer.NVIDIA;
      filter.MaxVram = 4;
      filter.MinVram = 1;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Graphic.Name.Contains("NVIDIA"));
        Assert.That(nb.Graphic.VRAMInGB >= 1 && nb.Graphic.VRAMInGB <= 4);
      }
    }

    /// <summary>
    /// Sets the Notebook Properties in the View Model
    /// and Checks if the returned notebook have the right attributes
    /// </summary>
    [Test]
    public void TestNotebookViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.NotebookName = "Dell";
      filter.MinRamMemory = 16;
      filter.MaxRamMemory = 16;
      filter.MaxBatteryTime = 1000;
      filter.MinBatteryTime = 901;
      filter.OS = OS.windows;
      filter.MaxPrice = 1000;
      filter.MinPrice = 500;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Name.Contains("Dell"));
        Assert.That(nb.RamInGB == 16);
        Assert.That(nb.Price.Amount > 500 && nb.Price.Amount < 1000);
        Assert.That(nb.Os == OS.windows);
      }
    }

  }
}
