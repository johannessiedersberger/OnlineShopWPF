using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class ProductSearchQueries
  {
    #region productQueries
    /// <summary>
    /// Returns a list of products by the given searchData
    /// </summary>
    /// <param name="productQueryParams">The seachdata</param>
    /// <param name="db">the database</param>
    /// <returns></returns>
    public static List<Product> FindMatchingProduct(ProductQueryParams productQueryParams, IDatabase db)
    {
      List<IQueryPart> querieParts = GetQuerypartsProduct(productQueryParams);

      string queryText;
      if (querieParts.Count() == 0)
        queryText = "SELECT product_id FROM Products";
      else
        queryText = QuerieCreation.CreateQueryText(querieParts);
      string CommandGetNotebooks = string.Format("SELECT * FROM " +
        "  ( {0} ) AS PID " +
        " INNER JOIN Products As p ON p.product_id = PID.product_id", queryText);
     
      using (var getNotebook = db.CreateQueryCommand(CommandGetNotebooks))
      {
        QuerieCreation.SetQueryParameters(getNotebook, querieParts);
        using (IReader reader = getNotebook.ExecuteReader())
        {
          return ProductReader.ReadForProducts(reader);
        }
      }
    }

    private static List<IQueryPart> GetQuerypartsProduct(ProductQueryParams param)
    {
      var querieParts = new List<IQueryPart>();
      FillProductQuerie(querieParts, param);
      return querieParts;
    }

    private static void FillProductQuerie(List<IQueryPart> queryParts, ProductQueryParams param)
    {
      if (param.Name != null)
        queryParts.Add(GetProductsByName(param.Name));
      if (param.Price != null)
        queryParts.Add(GetProductsByPriceQuery(param.Price));
    }

    private static IQueryPart GetProductsByName(string name)
    {
      MySqliteQueryPart getProduct = new MySqliteQueryPart(CommandGetProductsByName);
      getProduct.AddParameter("$productName", "%" + name + "%");

      return getProduct;
    }
    private const string CommandGetProductsByName =
        "SELECT p.product_id FROM Products AS p WHERE p.name LIKE $productName";

    private static IQueryPart GetProductsByPriceQuery(Range range)
    {
      MySqliteQueryPart getProduct = new MySqliteQueryPart(CommandGetProductsByPriceQuery);
      getProduct.AddParameter("$minProductPrice", range.Min);
      getProduct.AddParameter("$maxProductPrice", range.Max);
      return getProduct;
    }
    private const string CommandGetProductsByPriceQuery =
        "SELECT p.product_id FROM Products AS p WHERE p.price BETWEEN $minProductPrice AND $maxProductPrice";
    #endregion
  }
}
