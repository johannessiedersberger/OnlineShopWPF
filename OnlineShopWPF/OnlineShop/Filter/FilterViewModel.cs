using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OnlineShop
{
  public class FilterViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;
    private static DatabaseFactory db = new DatabaseFactory(new MySqliteDatabase(Shop.file));

    #region cpucores
    public int MaxCPUSliderLenth = 16;

    public int MinCPUCores
    {
      get => _minCPUCores;
      set
      {
        _minCPUCores = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinCPUCores)));
        RunQuery();
      }
    }
    private int _minCPUCores = 0;


    public int MaxCPUCores
    {
      get => _maxCpuCores;
      set
      {
        _maxCpuCores = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxCPUCores)));
        RunQuery();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsIntelCpu)));
        RunQuery();
      }
    }
    private bool _isIntelCPU = false;


    public bool IsAMDCpu
    {
      get => _isAmdCpu;
      set
      {
        _isAmdCpu = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAMDCpu)));
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinClockRate)));
        RunQuery();
      }
    }
    private int _minClockRate = 0;


    public int MaxClockRate
    {
      get => _maxClockRate;
      set
      {
        _maxClockRate = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxClockRate)));
        RunQuery();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNVIDIAGraphicCard)));
        RunQuery();
      }
    }
    private bool _IsNVIDIAGraphicCard = false;


    public bool IsAMDGrapicCard
    {
      get => _IsAMDGrapicCard;
      set
      {
        _IsAMDGrapicCard = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAMDGrapicCard)));
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinVram)));
        RunQuery();
      }
    }
    private int _minVram = 0;


    public int MaxVram
    {
      get => _maxVram;
      set
      {
        _maxVram = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxVram)));
        RunQuery();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSSD)));
        RunQuery();
      }
    }
    private bool _isSSd = false;


    public bool IsHDD
    {
      get => _isHdd;
      set
      {
        _isHdd = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsHDD)));
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinHdMemory)));
        RunQuery();
      }
    }
    private int _minHdMemory = 0;

    public int MaxHdMemory
    {
      get => _MaxHdMemory;
      set
      {
        _MaxHdMemory = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxHdMemory)));
        RunQuery();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWindows)));
        RunQuery();
      }
    }
    private bool _isWindows = false;

    public bool IsLinux
    {
      get => _isLinux;
      set
      {
        _isLinux = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLinux)));
      }
    }
    private bool _isLinux = false;

    public bool IsMac
    {
      get => _isMac;
      set
      {
        _isMac = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsMac)));
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinRamMemory)));
        RunQuery();
      }
    }
    private int _minRamMemory = 0;

    public int MaxRamMemory
    {
      get => _maxRamMemory;
      set
      {
        _maxRamMemory = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxRamMemory)));
        RunQuery();
      }
    }
    private int _maxRamMemory = 128;
    #endregion

    #region battery

    public int MaxBatteryTimeSliderValue { get; set; } = 2000;

    public int MinBatteryTime
    {
      get => _minBatteryTime;
      set
      {
        _minBatteryTime = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinBatteryTime)));
        RunQuery();
      }
    }
    private int _minBatteryTime = 0;

    public int MaxBatteryTime
    {
      get => _maxBatteryTime;
      set
      {
        _maxBatteryTime = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxBatteryTime)));
        RunQuery();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinPrice)));
        RunQuery();
      }
    }
    private int _minPrice = 0;

    public int MaxPrice
    {
      get => _maxPrice;
      set
      {
        _maxPrice = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxPrice)));
        RunQuery();
      }
    }
    private int _maxPrice = 10000;

    #endregion



    private void RunQuery()
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
          batteryTimeRange= new Range(MinBatteryTime, MaxBatteryTime),
        }
        
      })));
    }
  }
}
