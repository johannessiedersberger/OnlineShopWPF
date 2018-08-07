using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace OnlineShop
{
  public class CustomerData
  {
    public string Email;
    public string Password;
    public string FirstName;
    public string LastName;
    public string StreetName;
    public int StreetNumber;
    public string City;
    public int ZipCode;
    public int CreditCardNumber;
    public int PhoneNumber;
  }

  public class Customer
  {
    public CustomerData CustomerData { get; private set; }

    private IDatabase database = new SqliteDatabase("OnlineShop.db");

    public Customer(CustomerData customerData)
    {
      CustomerData = customerData;


      if (GetId(customerData.Email) != 0)
        return;

      using (var createCustomer = database.CreateCommand(CommandAddCustomer))
      {
        database.Open();
        createCustomer.Parameters.Add(database.CreateParameter("$id", null));
        createCustomer.Parameters.Add(database.CreateParameter("$email", customerData.Email));
        createCustomer.Parameters.Add(database.CreateParameter("$password", customerData.Password));
        createCustomer.Parameters.Add(database.CreateParameter("$firstName", customerData.FirstName));
        createCustomer.Parameters.Add(database.CreateParameter("$lastName", customerData.LastName));
        createCustomer.Parameters.Add(database.CreateParameter("$streetName", customerData.StreetName));
        createCustomer.Parameters.Add(database.CreateParameter("$streetNumber", customerData.StreetNumber.ToString()));
        createCustomer.Parameters.Add(database.CreateParameter("$city", customerData.City));
        createCustomer.Parameters.Add(database.CreateParameter("$zipCode", customerData.ZipCode.ToString()));
        createCustomer.Parameters.Add(database.CreateParameter("$creditCardNumber", customerData.CreditCardNumber.ToString()));
        createCustomer.Parameters.Add(database.CreateParameter("$phone", customerData.PhoneNumber.ToString()));
        createCustomer.ExecuteNonQuery();
        database.Close();
      }
    }

    private const string CommandAddCustomer
      = "INSERT INTO Customers(customer_id, email, password, first_name, last_name, street_name, street_number, city, zip_code, credit_card_number, phone)" +
                              " VALUES($id,$email,$password,$firstName, $lastName, $streetName, $streetNumber, $city, $zipCode, $creditCardNumber, $phone)";

    /// <summary>
    /// Returns the id from the Databse 
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetId(string email)
    {
      using (var getID = database.CreateCommand(CommandSelectID))
      {
        database.Open();
        getID.Parameters.Add(database.CreateParameter("$email", email));
        IDataReader reader = getID.ExecuteReader();

        int id = 0;
        while (reader.Read())
          id = int.Parse(reader[0].ToString());
        database.Close();
        return id;
      }
    }

    private const string CommandSelectID = "SELECT customer_id FROM Customer WHERE email = $email";
  }
}
