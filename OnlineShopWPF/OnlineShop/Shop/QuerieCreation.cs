using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class QuerieCreation
  {
    /// <summary>
    /// Creates a query text based on the parts of a query
    /// </summary>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string CreateQueryText(List<IQueryPart> parts)
    {
      string query = "";
      for (int i = 0; i < parts.Count(); i++)
      {
        query += parts[i].QueryText + " ";
        if (i + 1 < parts.Count)
          query += " INTERSECT ";
      }
      return query;
    }

    /// <summary>
    /// Sets the queryParameters into the mainQuery
    /// </summary>
    /// <param name="mainQuery">the mainQuery</param>
    /// <param name="subQueries">all the subQueries</param>
    public static void SetQueryParameters(IQueryCommand mainQuery, List<IQueryPart> subQueries)
    {
      foreach (IQueryPart subQuery in subQueries)
      {
        foreach (KeyValuePair<string, object> parameter in subQuery.Parameters)
        {
          mainQuery.AddParameter(parameter.Key, parameter.Value);
        }
      }
    }
  }
}
