using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace OnlineShop
{
  /// <summary>
  /// Stores the complete Customer Data
  /// </summary>
  public class CustomerData
  {
    /// <summary>
    /// The Customer Email
    /// </summary>
    public string Email;
    /// <summary>
    /// The Customer Password
    /// </summary>
    public string Password;
    /// <summary>
    /// The Customers First name
    /// </summary>
    public string FirstName;
    /// <summary>
    /// The Customers Last name
    /// </summary>
    public string LastName;
    /// <summary>
    /// The Customers street name
    /// </summary>
    public string StreetName;
    /// <summary>
    /// The Customers Street number
    /// </summary>
    public int StreetNumber;
    /// <summary>
    /// The Customers City
    /// </summary>
    public string City;
    /// <summary>
    /// The Customers ZipCode
    /// </summary>
    public int ZipCode;
    /// <summary>
    /// The Customers Creditcardnumber
    /// </summary>
    public int CreditCardNumber;
    /// <summary>
    /// The Customers Phone Number
    /// </summary>
    public int PhoneNumber;
  }

  public class Customer
  {
    /// <summary>
    /// Stores the complete Data from The Customer
    /// </summary>
    public CustomerData CustomerData { get; private set; }

    public Customer(CustomerData customerData)
    {
      CustomerData = customerData;
    }

   
  }
}
