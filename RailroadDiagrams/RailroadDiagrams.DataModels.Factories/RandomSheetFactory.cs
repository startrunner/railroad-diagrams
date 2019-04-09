using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailroadDiagrams.DataModels;

namespace RailroadDiagrams.DataModels.Factories
{
   public class RandomSheetFactory : IFactory<SheetData>
   {
      Random rand = new Random(DateTime.Now.GetHashCode());
      RandomSymbolFactory mSymbol = new RandomSymbolFactory();

      public SheetData Create()
      {
         SheetData rt = new SheetData();
         rt.AlignmentGridUnitSize = 5;

         int symbolCount = 10 + rand.Next(12);
         for(int i=0;i<symbolCount;i++)
         {
            var symbol = mSymbol.Create();
            symbol.ID = ++rt.LatestSymbolID;
            symbol.LeftConnectionPointID = ++rt.LatestConnectionPointID;
            symbol.RightConnectionPointID = ++rt.LatestConnectionPointID;

            symbol.X -= symbol.X % rt.AlignmentGridUnitSize;
            symbol.Y -= symbol.Y % rt.AlignmentGridUnitSize;

            rt.Symbols.Add(symbol);
         }


         return rt;
      }
   }
}
