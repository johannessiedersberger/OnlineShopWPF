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
      ProductId = productId;
      Wireless = wireless;
      MicrophoneIncluded = microphone;
    }

    
  }
}
