using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels.Mock
{
   public class MockSheet : IMock<SheetData>
   {
      Random rand = new Random(DateTime.Now.GetHashCode());
      MockSymbol mSymbol = new MockSymbol();

      public SheetData Mock()
      {
         SheetData rt = new SheetData();

         int nos = 10 + rand.Next(12);
         for(int i=0;i<nos;i++)
         {
            var symbol = mSymbol.Mock();
            symbol.ID = ++rt.LatestSymbolID;
            symbol.LeftConnectionPointID = ++rt.LatestConnectionPointID;
            symbol.RightConnectionPointID = ++rt.LatestConnectionPointID;
            rt.Symbols.Add(symbol);
         }

         return rt;
      }
   }
}
