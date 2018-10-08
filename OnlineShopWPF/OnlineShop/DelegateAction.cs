using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShop
{
  public class DelegateAction : ICommand
  {
    private Action _action;

    public DelegateAction(Action action)
    {
      _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    #region CanExecute

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    #endregion

    public void Execute(object parameter)
    {
      _action();
    }
  }
}
