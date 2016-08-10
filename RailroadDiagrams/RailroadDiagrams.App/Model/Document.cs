using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   partial class Document: ModelBase<DocumentData>
   {
      public Sheet AddSheet()
      {
         var data = new SheetData() { ID = ++this.Data.LatestSheetID, Name = $"Sheet {this.Data.LatestSheetID + 1}" };
         this.Data.Sheets.Add(data);

         return new Sheet(data);
      }

      public void SwitchToSheet(int id)
      {
         this.Data.OpenSheetID = id;
         OpenSheet = new Sheet(this.Data.Sheets.Where(x => x.ID == id).FirstOrDefault());
      }

      public Sheet OpenSheet { get; private set; }

      public Document(DocumentData data) : base(data)
      {
         this.OpenSheet = new Sheet(data.Sheets.Where(x => x.ID == data.OpenSheetID).FirstOrDefault());
      }
   }
}
