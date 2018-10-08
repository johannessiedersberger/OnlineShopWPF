using System.ComponentModel;
using System.Windows.Input;

namespace OnlineShop
{
  public class FilterViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private static DatabaseFactory db = new DatabaseFactory(new MySqliteDatabase(Shop.file));

    public FilterViewModel()
    {
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
        _minCPUCores = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minCPUCores = 0;


    public int MaxCPUCores
    {
      get => _maxCpuCores;
      set
      {
        _maxCpuCores = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _isIntelCPU = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private bool _isIntelCPU = false;


    public bool IsAMDCpu
    {
      get => _isAmdCpu;
      set
      {
        _isAmdCpu = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _minClockRate = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minClockRate = 0;


    public int MaxClockRate
    {
      get => _maxClockRate;
      set
      {
        _maxClockRate = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _IsNVIDIAGraphicCard = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private bool _IsNVIDIAGraphicCard = false;


    public bool IsAMDGrapicCard
    {
      get => _IsAMDGrapicCard;
      set
      {
        _IsAMDGrapicCard = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _minVram = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minVram = 0;


    public int MaxVram
    {
      get => _maxVram;
      set
      {
        _maxVram = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _isSSd = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private bool _isSSd = false;


    public bool IsHDD
    {
      get => _isHdd;
      set
      {
        _isHdd = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _minHdMemory = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minHdMemory = 0;

    public int MaxHdMemory
    {
      get => _MaxHdMemory;
      set
      {
        _MaxHdMemory = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _MaxHdMemory = 10000;

    #endregion

    #region os

    private string OS
    {
      get
      {
        if (IsWindows)
          return "windows";
        if (IsLinux)
          return "linux";
        if (IsMac)
          return "mac";
        else
          return "";
      }
    }

    public bool IsWindows
    {
      get => _isWindows;
      set
      {
        _isWindows = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private bool _isWindows = false;

    public bool IsLinux
    {
      get => _isLinux;
      set
      {
        _isLinux = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private bool _isLinux = false;

    public bool IsMac
    {
      get => _isMac;
      set
      {
        _isMac = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _minRamMemory = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minRamMemory = 0;

    public int MaxRamMemory
    {
      get => _maxRamMemory;
      set
      {
        _maxRamMemory = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _notebookName = value;
        FirePropertyChanged();
        ShowNotebooks();
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
        _minBatteryTime = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minBatteryTime = 0;

    public int MaxBatteryTime
    {
      get => _maxBatteryTime;
      set
      {
        _maxBatteryTime = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _maxBatteryTime = 2000;

    #endregion

    #region price

    public int MaxPriceSliderValue { get; set; } = 10000;

    public int MinPrice
    {
      get => _minPrice;
      set
      {
        _minPrice = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _minPrice = 0;

    public int MaxPrice
    {
      get => _maxPrice;
      set
      {
        _maxPrice = value;
        FirePropertyChanged();
        ShowNotebooks();
      }
    }
    private int _maxPrice = 10000;

    #endregion

    public ICommand Search { get; private set; }

    private void ShowNotebooks()
    {
      MainViewModel.ShowNotebooks(db.GetNotebooks(db.FindMatchingProducts(new NotebookQueryParams
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
          priceRange = new Range(MinPrice, MaxPrice),
          notebookName = NotebookName,
        }
      }
      )));
    }
  }
}
