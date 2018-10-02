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

    public int SliderValue
    {
      get => _sliderValue;
      set
      {
        _sliderValue = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderValue)));
      }
    }
    private int _sliderValue;
    
  }
}
