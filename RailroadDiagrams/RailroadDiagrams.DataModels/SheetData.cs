using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class SheetData
   {
      [DataMember(Name="id")]
      public int ID { get; set; }

      [DataMember(Name="symbols")]
      public HashSet<SymbolData> Symbols { get; set; } = new HashSet<SymbolData>();

      [DataMember(Name="name")]
      public string Name { get; set; } = "Unnamed Sheet";

      [DataMember(Name="latestSymbol")]
      public int LatestSymbolID { get; set; } = -1;

      [DataMember(Name = "latestConnectionPoint")]
      public int LatestConnectionPointID { get; set; } = -1;

      [DataMember(Name="latestConnection")]
      public int LatestConnectionID { get; set; } = -1;

      [DataMember(Name="connections")]
      public List<ConnectionData> Connections { get; set; } = new List<ConnectionData>();
   }
}
