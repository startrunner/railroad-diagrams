using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels.Mock
{
   public class MockDocument : IMock<DocumentData>
   {
      Random rand = new Random(DateTime.Now.GetHashCode());
      MockSheet mSheet = new MockSheet();

      public DocumentData Mock()
      {
         DocumentData rt = new DocumentData();

         int sCount = 3 + rand.Next(7);
         for(int i=0;i<sCount;i++)
         {
            var sheet = mSheet.Mock();
            sheet.ID = ++rt.LatestSheetID;
            sheet.Name = $"Sheet {sheet.ID+1}";
            rt.Sheets.Add(sheet);
         }

         return rt;
      }
   }
}
