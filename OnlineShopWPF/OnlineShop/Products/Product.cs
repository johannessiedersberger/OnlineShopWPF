using System;

namespace OnlineShop
{
  /// <summary>
  /// The Products that could be bought in the shop
  /// </summary>
  public class Product
  {
    /// <summary>
    /// The Name of the Product
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// The Price of the Product
    /// </summary>
    public double Price { get; private set; }


    /// <summary>
    /// Assigns the member variables
    /// </summary>
    /// <param name="name">The name of the Product</param>
    /// <param name="price">The price of the Product</param>
    public Product(string name, double price)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Invalid product name", nameof(name));

      if (double.IsInfinity(price) || double.IsNaN(price) || price < 0)
        throw new ArgumentException("Invalid price", nameof(price));

      Name = name;
      Price = price;
    }
  }

  enum Currency
  {
    Euro
  }

  struct Money : IEquatable<Money>
  {
    public Money(double amount)
    {
      ThrowOnInvalidAmount(amount);

      // must initialize backing store
      _amount = amount;
      _currency = Currency.Euro;
    }

    #region Amount

    public double Amount
    {
      get { return _amount; }
      set
      {
        ThrowOnInvalidAmount(value);
        _amount = value;
      }
    }
    private double _amount;

    private static void ThrowOnInvalidAmount(double amount)
    {
      if (double.IsNaN(amount) || double.IsInfinity(amount))
        throw new ArgumentException($"{nameof(amount)} must be real number", nameof(amount));
    }

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
