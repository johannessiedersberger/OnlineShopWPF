using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class HeadPhone
  {
    public bool Wireless { get; private set; }

    public bool MicrophoneIncluded { get; private set; }
    public int ProductId { get; private set; }

    private IDatabase _database;

    public HeadPhone(int productId, bool wireless, bool microphone)
    {
      Wireless = wireless;
      MicrophoneIncluded = microphone;
    }

    /// <summary>
    /// Writes the HardDrive to the DataBase
    /// </summary>
    /// <param name="db"></param>
    public void WriteToDatabase(IDatabase db)
    {
      _database = db;
      if (DoesHeadPhoneAlreadyExist())
        return;
      using (var createHeadPhone = _database.CreateNonQueryCommand(CommandAddHeadPhone))
      {
        createHeadPhone.AddParameter("$id", null);
        createHeadPhone.AddParameter("$wireless", Wireless);
        createHeadPhone.AddParameter("$microphone", MicrophoneIncluded);
        createHeadPhone.Execute();
      }
    }

    private const string CommandAddHeadPhone = "INSERT INTO HeadPhones(product_id, wireless, microphone_included) VALUES($id,$wireless,$microphone)";

    private bool DoesHeadPhoneAlreadyExist()
    {

      using (var getID = _database.CreateQueryCommand(CommandSelectID))
      {
        getID.AddParameter("$id", ProductId);
        IReader reader = getID.ExecuteReader();
        reader.Read();
        return reader[0] != null;
      }
    }


    private const string CommandSelectID = "SELECT product_id FROM HeadPhones WHERE product_id = $id";
  }
}
