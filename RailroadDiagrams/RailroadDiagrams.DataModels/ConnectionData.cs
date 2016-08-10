using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class ConnectionData
   {
      [DataMember(Name = "startID")]
      public int StartID { get; set; }

      [DataMember(Name = "endId")]
      public int EndID { get; set; }
      
      [DataMember(Name ="curePoly")]
      public CurvePolygonData CurvePolygon { get; set; }
   }
}
