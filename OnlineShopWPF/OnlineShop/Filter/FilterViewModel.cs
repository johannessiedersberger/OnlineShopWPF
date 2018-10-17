using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace OnlineShop
{
  public enum GraphicManufacturer
  {
    NVIDIA,
    AMD
  }
  public enum HardDriveType
  {
    ssd,
    hdd
  }
  public enum CPUManufacturer
  {
    Intel,
    AMD
  }

  public class FilterViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private DatabaseFactory _db;
    private ObservableCollection<NotebookView> _notebookList;
    public ObservableCollection<NotebookView> NotebookList
    {
      get => _notebookList;
      set
      {
        _notebookList = value;
        FirePropertyChanged();
      }
    }

    public FilterViewModel()
    {
      _db = new DatabaseFactory(new MySqliteDatabase(Shop.file));
      _notebookList = new ObservableCollection<NotebookView>();
      ShowNotebooks();
      Search = new DelegateAction(ShowNotebooks);
    }

    #region cpucores
    public int MaxCPUSliderLenth = 16;

    public int MinCPUCores
    {
      get => _minCPUCores;
      set
      {
        if (_minCPUCores != value)
        {
          _minCPUCores = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minCPUCores = 0;


    public int MaxCPUCores
    {
      get => _maxCpuCores;
      set
      {
        if (_maxCpuCores != value)
        {
          _maxCpuCores = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxCpuCores = 16;
    #endregion

    #region cpuManufacturer

    public CPUManufacturer CPUManufacturer
    {
      get => _cpuManufacturer;
      set
      {
        if (_cpuManufacturer != value)
        {
          _cpuManufacturer = value;
          ShowNotebooks();
        }
      }
    }
    private CPUManufacturer _cpuManufacturer;
    #endregion

    #region cpuClockRate
    public static int MaxCPUClockRateSliderLength = 16;

    public double MinClockRate
    {
      get => _minClockRate;
      set
      {
        if (_minClockRate != value)
        {
          _minClockRate = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private double _minClockRate = 0;


    public double MaxClockRate
    {
      get => _maxClockRate;
      set
      {
        if (_maxClockRate != value)
        {
          _maxClockRate = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private double _maxClockRate = 10;

    #endregion

    #region graphicCardManufacturer

    public GraphicManufacturer GraphicCardManufacturer
    {
      get => _graphicManufacturer;
      set
      {
        if (_graphicManufacturer != value)
        {
          _graphicManufacturer = value;
          ShowNotebooks();
        }
      }
    }
    private GraphicManufacturer _graphicManufacturer;
    #endregion

    #region graphicCardVram

    public int MaxVramSliderRange = 16;

    public int MinVram
    {
      get => _minVram;
      set
      {
        if (_minVram != value)
        {
          _minVram = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minVram = 0;


    public int MaxVram
    {
      get => _maxVram;
      set
      {
        if (_maxVram != value)
        {
          _maxVram = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxVram = 16;

    #endregion

    #region hardDriveType

    public HardDriveType HDType
    {
      get => _hardDriveType;
      set
      {
        if (_hardDriveType != value)
        {
          _hardDriveType = value;
          ShowNotebooks();
        }
      }
    }
    private HardDriveType _hardDriveType;

    #endregion

    #region hardDriveMemory

    public int MaxHdMemorySliderRange { get; set; } = 5000;

    public int MinHdMemory
    {
      get => _minHdMemory;
      set
      {
        if (_minHdMemory != value)
        {
          _minHdMemory = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minHdMemory = 0;

    public int MaxHdMemory
    {
      get => _MaxHdMemory;
      set
      {
        if (_MaxHdMemory != value)
        {
          _MaxHdMemory = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _MaxHdMemory = 10000;

    #endregion

    #region os

    public OS OS
    {
      get => _os;
      set
      {
        if (_os != value)
        {
          _os = value;
          ShowNotebooks();
        }
      }
    }
    private OS _os;

    #endregion

    #region ram

    public int MaxRAMMemorySliderRange { get; set; } = 128;

    public int MinRamMemory
    {
      get => _minRamMemory;
      set
      {
        if (_minRamMemory != value)
        {
          _minRamMemory = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minRamMemory = 0;

    public int MaxRamMemory
    {
      get => _maxRamMemory;
      set
      {
        if (_maxRamMemory != value)
        {
          _maxRamMemory = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxRamMemory = 128;
    #endregion

    #region name
    public string NotebookName
    {
      get => _notebookName;
      set
      {
        if (_notebookName != value)
        {
          _notebookName = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private string _notebookName = "";
    #endregion

    #region battery

    public int MaxBatteryTimeSliderValue { get; set; } = 2000;

    public int MinBatteryTime
    {
      get => _minBatteryTime;
      set
      {
        if (_minBatteryTime != value)
        {
          _minBatteryTime = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minBatteryTime = 0;

    public int MaxBatteryTime
    {
      get => _maxBatteryTime;
      set
      {
        if (_maxBatteryTime != value)
        {
          _maxBatteryTime = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxBatteryTime = 2500;

    #endregion

    #region price

    public int MaxPriceSliderValue { get; set; } = 5000;

    public int MinPrice
    {
      get => _minPrice;
      set
      {
        if (_minPrice != value)
        {
          _minPrice = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minPrice = 0;

    public int MaxPrice
    {
      get => _maxPrice;
      set
      {
        if (_maxPrice != value)
        {
          _maxPrice = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxPrice = 5000;

    #endregion

    public ICommand Search { get; private set; }


    public List<Notebook> GetNotebooks()
    {
      return _db.GetNotebooks(_db.FindMatchingProducts(new NotebookQueryParams
      {
        CPUQueryParams = new CPUQueryParams
        {
          cpuCount = new Range(MinCPUCores, MaxCPUCores),
          cpuClockRate = new Range(MinClockRate, MaxClockRate),
          cpuName = this.CPUManufacturer.ToString(),
        },
        GraphicQueryParams = new GraphicQueryParams
        {
          graphicCardName = this.GraphicCardManufacturer.ToString(),
          vramRange = new Range(MinVram, MaxVram),
        },
        HardDriveQueryParams = new HardDriveQueryParams
        {
          hdType = HDType.ToString(),
          hdMemoryRange = new Range(MinHdMemory, MaxHdMemory),
        },
        NotebookDataQueryParams = new NotebookDataQueryParams
        {
          os = OS,
          ramMemoryRange = new Range(MinRamMemory, MaxRamMemory),
          batteryTimeRange = new Range(MinBatteryTime, MaxBatteryTime),
          priceRange = new PriceRange(new Money(decimal.Parse(MinPrice.ToString())), new Money(decimal.Parse(MaxPrice.ToString()))),
          notebookName = NotebookName,
        }
      }));
    }

    public void ShowNotebooks()
    {
      var notebooks = _db.FindMatchingNotebook(new NotebookQueryParams())
                         .Where(nb => nb.Graphic.Name.Contains(GraphicCardManufacturer.ToString())) // Graphic
                         .Where(nb => IsInRange(nb.Graphic.VRAMInGB,MinVram, MaxVram))
                         .Where(nb => nb.HardDrive.Type.Contains(HDType.ToString())) // HardDrive
                         .Where(nb => IsInRange(nb.HardDrive.MemoryInGB,MinHdMemory, MaxHdMemory))
                         .Where(nb => nb.Cpu.Name.Contains(CPUManufacturer.ToString())) // CPU
                         .Where(nb => IsInRange(nb.Cpu.ClockRateInGHZ, MinClockRate, MaxClockRate))
                         .Where(nb => IsInRange(nb.Cpu.NumCores, MinCPUCores, MaxCPUCores))
                         .Where(nb => IsInRange(nb.AverageBatteryTimeInMinutes,MinBatteryTime, MaxBatteryTime)) // Notebook
                         .Where(nb => nb.Name.Contains(NotebookName, StringComparison.OrdinalIgnoreCase))
                         .Where(nb => nb.Os == OS)
                         .Where(nb => IsInRange(Convert.ToInt16(nb.Price.Amount),MinPrice, MaxPrice))
                         .Where(nb => IsInRange(nb.RamInGB,MinRamMemory, MaxRamMemory));
      AddNotebooksToViewList(notebooks);
    }

    private bool IsInRange(int min, int max, int check)
    private static bool IsInRange<T>(T value, T min, T max) where T : IComparable<T>
    {
      return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    private void AddNotebooksToViewList(IEnumerable<Notebook> notebooks)
    {
      NotebookList.Clear();
      foreach (var notebookView in notebooks.Select(nb => new NotebookView(nb)))
      {
        NotebookList.Add(notebookView);
      }
    }
  }
}
