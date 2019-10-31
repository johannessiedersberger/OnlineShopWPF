using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  /// <summary>
  /// A Part of the SearchQuery
  /// </summary>
  public interface IQueryPart
  {
    /// <summary>
    /// The Query Text
    /// </summary>
    string QueryText { get; }

    /// <summary>
    /// Ads a Parameter to the Dictionary
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    void AddParameter(string name, object value);
    /// <summary>
    /// Gets the read only collection of parameters.
    /// </summary>
    IReadOnlyDictionary<string, object> Parameters { get; }
  }

  /// <summary>
  /// A QueryPart for SQLITE
  /// </summary>
  public class MySqliteQueryPart : IQueryPart
  {
    /// <summary>
    /// Sets the QueryText
    /// </summary>
    /// <param name="queryText"></param>
    public MySqliteQueryPart(string queryText)
    {
      QueryText = queryText;   
    }

    /// <summary>
    /// The QueryText
    /// </summary>
    public string QueryText { get; private set; }

    /// <summary>
    /// Ads Parameters
    /// </summary>
    /// <param name="name">the nameo of the parameter</param>
    /// <param name="value">the value of the parameter</param>
    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

    /// <summary>
    /// The Parameters from the Query
    /// </summary>
    public IReadOnlyDictionary<string, object> Parameters
    {
      get
      {
        return new ReadOnlyDictionary<string, object>(_parameters);
      }
    }

    private IDictionary<string, object> _parameters = new Dictionary<string, object>();

  }
}