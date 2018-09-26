using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public class QuerieCreation
  {
    #region queryCreation
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
    #endregion
  }
}
