using NUnit.Framework;
using OnlineShop;

namespace OnlineShopTest
{
  class ViewModelTest
  {
    [Test]
    public void TestCPUViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.IsIntelCpu = true;
      filter.MinClockRate = 1;
      filter.MaxClockRate = 3.5;
      filter.MinCPUCores = 4;
      filter.MaxCPUCores = 4;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Cpu.Name.Contains("Intel"));
        Assert.That(nb.Cpu.ClockRate > 1 && nb.Cpu.ClockRate < 3.5 );
        Assert.That(nb.Cpu.Count == 4);
      }
    }

    [Test]
    public void TestHardDriveViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.IsSSD = true;
      filter.IsHDD = false;
      filter.MinHdMemory = 200;
      filter.MaxHdMemory = 300;
      foreach (Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.HardDrive.Type == "ssd");
        Assert.That(nb.HardDrive.Memory > 200 && nb.HardDrive.Memory < 300);
      }
    }

    [Test]
    public void TestGraphicViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.IsAMDGrapicCard = false;
      filter.IsNVIDIAGraphicCard = true;
      filter.MaxVram = 4;
      filter.MinVram = 1;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Graphic.Name.Contains("NVIDIA"));
        Assert.That(nb.Graphic.VRAM >= 1 && nb.Graphic.VRAM <= 4);
      }
    }

    [Test]
    public void TestNotebookViewModel()
    {
      FilterViewModel filter = new FilterViewModel();
      filter.NotebookName = "Dell";
      filter.MinRamMemory = 16;
      filter.MaxRamMemory = 16;
      filter.MaxBatteryTime = 1000;
      filter.MinBatteryTime = 901;
      filter.IsWindows = true;
      filter.IsLinux = false;
      filter.IsMac = false;
      filter.MaxPrice = 1000;
      filter.MinPrice = 500;
      foreach(Notebook nb in filter.GetNotebooks())
      {
        Assert.That(nb.Name.Contains("Dell"));
        Assert.That(nb.Ram == 16);
        Assert.That(nb.Price > 500 && nb.Price < 1000);
        Assert.That(nb.Os == OS.windows);
      }
    }

  }
}
