using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class DocumentData
   {
      [DataMember(Name ="name")]
      public string Name { get;  set; }

      [DataMember(Name = "sheets")]
      public List<SheetData> Sheets { get; set; } = new List<SheetData>();// { new SheetData() };

      [DataMember(Name ="latestSheet")]
      public int LatestSheetID { get; set; } = -1;

      [DataMember(Name ="openSheet")]
      public int OpenSheetID { get; set; } = 0;
   }
}
