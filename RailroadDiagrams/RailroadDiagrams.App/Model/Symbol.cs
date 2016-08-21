using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class Symbol : ModelBase<SymbolData>
   {
      public Symbol(SymbolData data) : base(data)
      {
         
      }

      public String Text
      {
         get { return Data.Text; }
         set { Data.Text = value; }
      }
   }
}
