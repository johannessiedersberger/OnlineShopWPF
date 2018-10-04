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
    public static int MaxCPUSliderLenth = 16;

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
    private int _maxCpuCores = MaxCPUSliderLenth;
    #endregion

    #region cpuManufacturer

    private string cpuName
    {
      get
      {
        if (IsIntelCpu)
          return "INTEL";
        else if (IsAMDCpu)
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
    private int _maxClockRate = MaxCPUSliderLenth;

    #endregion
    private void RunQuery()
    {

      MainViewModel.ShowNotebooks(db.GetNotebooks(db.FindMatchingProducts(new NotebookQueryParams
      {
        CPUQueryParams = new CPUQueryParams { cpuCount = new Range(MinCPUCores, MaxCPUCores),
          cpuClockRate=new Range(MinClockRate,MaxClockRate), cpuName = this.cpuName }


      })));
    }
  }
}
