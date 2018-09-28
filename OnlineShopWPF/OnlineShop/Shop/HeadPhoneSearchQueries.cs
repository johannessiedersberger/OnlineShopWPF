using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class HeadPhoneSearchQueries
  {
    #region headPhoneQueries

    public static List<Product> FindMatchingHeadphone(HeadPhoneQueryParams headphoneQueryParams, IDatabase db)
    {
      List<IQueryPart> querieParts = GetQuerypartsHeadPhone(headphoneQueryParams);
      string QueryText;
      if (querieParts.Count() == 0)
        QueryText = "SELECT product_id FROM HeadPhones";
      else
        QueryText = QuerieCreation.CreateQueryText(querieParts);

      string CommandGetNotebooks = string.Format("SELECT * FROM " +
        "  ( {0} ) AS PID " +
        " INNER JOIN Products As p ON p.product_id = PID.product_id",QueryText );

      using (var getNotebook = db.CreateQueryCommand(CommandGetNotebooks))
      {
        QuerieCreation.SetQueryParameters(getNotebook, querieParts);
        IReader reader = getNotebook.ExecuteReader();
        return ProductReader.ReadForProducts(reader);
      }
    }

    private static List<IQueryPart> GetQuerypartsHeadPhone(HeadPhoneQueryParams param)
    {
      var querieParts = new List<IQueryPart>();
      FillProductHeadPhoneQuery(querieParts, param);
      FillHeadPhoneQuery(querieParts, param);
      return querieParts;
    }

    private static void FillHeadPhoneQuery(List<IQueryPart> queryParts, HeadPhoneQueryParams param)
    {
      if (param.headPhoneData == null)
        return;
      if (param.headPhoneData.Wireless != null)
        queryParts.Add(GetHeadPhonesByWireless(param.headPhoneData.Wireless));
    }

    private static void FillProductHeadPhoneQuery(List<IQueryPart> queryParts, HeadPhoneQueryParams param)
    {
      if (param.Name != null)
        queryParts.Add(GetHeadPhonesByname(param.Name));
      if (param.Price != null)
        queryParts.Add(GetHeadPhonesByPrice(param.Price));
    }

    private static IQueryPart GetHeadPhonesByname(string name)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetHeadPhonesByname);
      getNotebook.AddParameter("$headPhoneName", "%" + name + "%");
      return getNotebook;
    }
    private const string CommandGetHeadPhonesByname =
        "SELECT h.product_id FROM HeadPhones AS h " +
          "INNER JOIN Products AS p ON h.product_id = p.product_id " +
            "WHERE p.name LIKE $headPhoneName";

    private static IQueryPart GetHeadPhonesByPrice(Range price)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetHeadPhonesByPrice);
      getNotebook.AddParameter("$minHeadPhonePrice", price.Min);
      getNotebook.AddParameter("$maxHeadPhonePrice", price.Max);
      return getNotebook;
    }
    private const string CommandGetHeadPhonesByPrice =
       "SELECT h.product_id FROM HeadPhones AS h " +
        "INNER JOIN Products AS p ON h.product_id = p.product_id " +
          "WHERE p.price BETWEEN $minHeadPhonePrice AND $maxHeadPhonePrice";

    private static IQueryPart GetHeadPhonesByWireless(bool wireless)
    {
      MySqliteQueryPart getNotebook = new MySqliteQueryPart(CommandGetHeadPhonesByWireless);
      getNotebook.AddParameter("$wireless", wireless);
      return getNotebook;
    }
    private const string CommandGetHeadPhonesByWireless =
       "SELECT h.product_id FROM HeadPhones AS h " +
          "WHERE h.wireless = $wireless";

    #endregion

  }
}
