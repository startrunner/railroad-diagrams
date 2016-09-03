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
      public String Text
      {
         get { return Data.Text; }
         set { Data.Text = value; }
      }

      private Symbol(SymbolData data) : base(data)
      {

      }

      public static Symbol Of(SymbolData data)
      {
         data.LogicUnit = data.LogicUnit as Symbol ?? new Symbol(data);
         return data.LogicUnit as Symbol;
      }
   }
}
