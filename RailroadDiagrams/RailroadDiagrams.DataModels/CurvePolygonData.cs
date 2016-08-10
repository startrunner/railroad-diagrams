using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class CurvePolygonData
   {
      [DataMember(Name = "points")]
      public List<CurvePointData> Points { get; set; } = new List<CurvePointData>();
   }
}
