using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class ConnectionPointInfo
   {
      public int ID { get; set; }
      public int OwnerID { get; set; }
      public ConnectionPointType Type { get; set; }
   }
}
