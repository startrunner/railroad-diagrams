using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class Connection : ModelBase<ConnectionData>
   {
      private Connection(ConnectionData data) : base(data)
      {
      }

      public static Connection Of(ConnectionData data)
      {
         data.LogicUnit=data.LogicUnit as Connection ?? new Connection(data);
         return data.LogicUnit as Connection;
      }
   }
}
