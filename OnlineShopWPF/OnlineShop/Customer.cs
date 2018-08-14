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

    private IDatabase _database;

    public Customer(IDatabase database, CustomerData customerData)
    {
      CustomerData = customerData;
      _database = database;

    }

    /// <summary>
    /// Writes the Customer to the Database
    /// </summary>
    /// <param name="db">The Database which contains the customers</param>
    public void WriteToDataBase(IDatabase db)
    {
      using (var createCustomer = _database.CreateNonQueryCommand(CommandAddCustomer))
      {
        createCustomer.AddParameter("$id", null);
        createCustomer.AddParameter("$email", CustomerData.Email);
        createCustomer.AddParameter("$password", CustomerData.Password);
        createCustomer.AddParameter("$firstName", CustomerData.FirstName);
        createCustomer.AddParameter("$lastName", CustomerData.LastName);
        createCustomer.AddParameter("$streetName", CustomerData.StreetName);
        createCustomer.AddParameter("$streetNumber", CustomerData.StreetNumber.ToString());
        createCustomer.AddParameter("$city", CustomerData.City);
        createCustomer.AddParameter("$zipCode", CustomerData.ZipCode.ToString());
        createCustomer.AddParameter("$creditCardNumber", CustomerData.CreditCardNumber.ToString());
        createCustomer.AddParameter("$phone", CustomerData.PhoneNumber.ToString());
        createCustomer.Execute();
      }
    }

    private const string CommandAddCustomer
      = "INSERT INTO Customers(customer_id, email, password, first_name, last_name, street_name, street_number, city, zip_code, credit_card_number, phone)" +
                              " VALUES($id,$email,$password,$firstName, $lastName, $streetName, $streetNumber, $city, $zipCode, $creditCardNumber, $phone)";


    /// <summary>
    /// Gets the ID from the Customer
    /// </summary>
    public int Id
    {
      get
      {
        using (var getID = _database.CreateQueryCommand(CommandSelectID))
        {
          getID.AddParameter("$email", CustomerData.Email);
          IReader reader = getID.ExecuteReader();
          return Convert.ToInt16(reader[0]);
        }
      }
    }

    private const string CommandSelectID = "SELECT customer_id FROM Customer WHERE email = $email";
  }
}
