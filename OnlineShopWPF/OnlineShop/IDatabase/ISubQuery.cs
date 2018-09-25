using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
  public interface IQueryPart
  {
    string QueryText { get; }

    void AddParameter(string name, object value);
    /// <summary>
    /// Gets the read only collection of parameters.
    /// </summary>
    IReadOnlyDictionary<string, object> Parameters { get; }
  }

  public class MySqliteQueryPart : IQueryPart
  {
    public MySqliteQueryPart(string queryText)
    {
      QueryText = queryText;
      
    }

    public string QueryText { get; private set; }

    public void AddParameter(string name, object value)
    {
      _parameters.Add(name, value);
    }

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