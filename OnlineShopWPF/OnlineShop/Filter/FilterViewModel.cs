using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OnlineShop
{
  public class FilterViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;
    public int MinCPUSliderText { get; private set; }

    public int MinCPUCores
    {
      get => _minCPUCores;
      set
      {
        _minCPUCores = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinCPUCores)));
        MinCPUSliderText = value;
      }
    }
    private int _minCPUCores;

  }
}
