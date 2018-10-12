using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public enum Currency
  {
    Euro
  }

  public struct Money : IEquatable<Money>
  {
    public Money(decimal amount)
    {
      // must initialize backing store
      _amount = amount;
      _currency = Currency.Euro;
    }

    #region Amount

    public decimal Amount
    {
      get { return _amount; }
      set
      {
        _amount = value;
      }
    }
    private decimal _amount;


    #endregion

    #region Currency

    public Currency Currency
    {
      get => _currency;
    }
    private Currency _currency;

    #endregion

    #region ToString

    public override string ToString()
    {
      return $"{Amount}{GetCurrencySymbol()}";
    }

    private string GetCurrencySymbol()
    {
      switch (Currency)
      {
        default:
          throw new NotImplementedException($"Unsupported currency: {Currency}");
        case Currency.Euro:
          return "€";
      }
    }

    #endregion

    #region Equals

    public override bool Equals(object obj)
    {
      if (obj is Money other)
        return Equals(other);
      else
        return false;
    }

    public bool Equals(Money other)
    {
      return Currency == other.Currency && Amount == other.Amount;
    }

    #endregion

    #region Hash

    public override int GetHashCode()
    {
      return Amount.GetHashCode() ^ Currency.GetHashCode();
    }

    #endregion
  }
}
