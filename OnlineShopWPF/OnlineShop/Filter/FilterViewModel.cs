using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnlineShop
{
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

    private string cpuName
    {
      get
      {
        if (IsIntelCpu)
          return "INTEL";
        if (IsAMDCpu)
          return "AMD";
        else
          return "";
      }
    }
    public bool IsIntelCpu
    {
      get => _isIntelCPU;
      set
      {
        if (_isIntelCPU != value)
        {
          _isIntelCPU = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isIntelCPU = false;


    public bool IsAMDCpu
    {
      get => _isAmdCpu;
      set
      {
        if (_isAmdCpu != value)
        {
          _isAmdCpu = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isAmdCpu = false;

    #endregion

    #region cpuClockRate
    public static int MaxCPUClockRateSliderLength = 16;

    public int MinClockRate
    {
      get => _minClockRate;
      set
      {
        if(_minClockRate != value)
        {
          _minClockRate = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _minClockRate = 0;


    public int MaxClockRate
    {
      get => _maxClockRate;
      set
      {
        if(_maxClockRate != value)
        {
          _maxClockRate = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxClockRate = 10;

    #endregion

    #region graphicCardManufacturer

    private string graphicName
    {
      get
      {
        if (IsNVIDIAGraphicCard)
          return "NVIDIA";
        if (IsAMDGrapicCard)
          return "AMD";
        else
          return "";
      }
    }
    public bool IsNVIDIAGraphicCard
    {
      get => _IsNVIDIAGraphicCard;
      set
      {
        if(_IsNVIDIAGraphicCard != value)
        {
          _IsNVIDIAGraphicCard = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _IsNVIDIAGraphicCard = false;

    public bool IsAMDGrapicCard
    {
      get => _IsAMDGrapicCard;
      set
      {
        if(_IsAMDGrapicCard != value)
        {
          _IsAMDGrapicCard = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _IsAMDGrapicCard = false;

    #endregion

    #region graphicCardVram

    public int MaxVramSliderRange = 16;

    public int MinVram
    {
      get => _minVram;
      set
      {
        if(_minVram != value)
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
        if(_maxVram != value)
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

    private string hdType
    {
      get
      {
        if (IsSSD)
          return "ssd";
        if (IsHDD)
          return "hdd";
        else
          return "";
      }
    }

    public bool IsSSD
    {
      get => _isSSd;
      set
      {
        if(_isSSd != value)
        {
          _isSSd = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isSSd = false;


    public bool IsHDD
    {
      get => _isHdd;
      set
      {
        if(_isHdd != value)
        {
          _isHdd = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isHdd = false;

    #endregion

    #region hardDriveMemory

    public int MaxHdMemorySliderRange { get; set; } = 5000;

    public int MinHdMemory
    {
      get => _minHdMemory;
      set
      {
        if(_minHdMemory != value)
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
        if(_MaxHdMemory != value)
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

    private OS OS
    {
      get
      {
        if (IsWindows)
          return OS.windows;
        if (IsLinux)
          return OS.linux;
        if (IsMac)
          return OS.mac;
        else
          return OS.empty;
      }
    }

    public bool IsWindows
    {
      get => _isWindows;
      set
      {
        if(_isWindows != value)
        {
          _isWindows = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isWindows = false;

    public bool IsLinux
    {
      get => _isLinux;
      set
      {
        if(_isLinux != value)
        {
          _isLinux = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isLinux = false;

    public bool IsMac
    {
      get => _isMac;
      set
      {
        if(_isMac != value)
        {
          _isMac = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private bool _isMac = false;

    #endregion

    #region ram

    public int MaxRAMMemorySliderRange { get; set; } = 128;

    public int MinRamMemory
    {
      get => _minRamMemory;
      set
      {
        if(_minRamMemory != value)
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
        if(_maxRamMemory != value)
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
        if(_notebookName != value)
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
        if(_minBatteryTime != value)
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
        if(_maxBatteryTime != value)
        {
          _maxBatteryTime = value;
          FirePropertyChanged();
          ShowNotebooks();
        }
      }
    }
    private int _maxBatteryTime = 2000;

    #endregion

    #region price

    public int MaxPriceSliderValue { get; set; } = 5000;

    public int MinPrice
    {
      get => _minPrice;
      set
      {
        if(_minPrice != value)
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
        if(_maxPrice != value)
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
          cpuName = this.cpuName
        },
        GraphicQueryParams = new GraphicQueryParams
        {
          graphicCardName = this.graphicName,
          vramRange = new Range(MinVram, MaxVram),
        },
        HardDriveQueryParams = new HardDriveQueryParams
        {
          hdType = hdType,
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
      var notebooks = GetNotebooks();
      NotebookList.Clear();
      foreach (Notebook notebook in notebooks)
      {
        NotebookView view = new NotebookView
        {
          Name = notebook.Name,
          Price = notebook.Price,
          Cpu = notebook.Cpu.Name,
          Ram = notebook.RamInGB,
          HdMemory = notebook.HardDrive.MemoryInGB,
          HdType = notebook.HardDrive.Type,
        };
        NotebookList.Add(view);
      }
    }
  }
}
