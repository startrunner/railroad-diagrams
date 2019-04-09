using System;

namespace RailroadDiagrams.DataModels.Factories
{
    public class RandomDocumentFactory : IFactory<DocumentData>
   {
      Random rand = new Random(DateTime.Now.GetHashCode());
      RandomSheetFactory mSheet = new RandomSheetFactory();

      public DocumentData Create()
      {
         DocumentData rt = new DocumentData();

         int sCount = 3 + rand.Next(7);
         for(int i=0;i<sCount;i++)
         {
            var sheet = mSheet.Create();
            sheet.ID = ++rt.LatestSheetID;
            sheet.Name = $"Sheet {sheet.ID+1}";
            rt.Sheets.Add(sheet);
         }

         return rt;
      }
   }
}
