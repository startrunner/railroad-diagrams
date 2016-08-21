using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class CurvePolygonData
   {
      [DataMember(Name = "points")]
      public List<CurvePolygonPointData> Points { get; set; } = new List<CurvePolygonPointData>();

      [DataMember(Name ="latestPoint")]
      public int LatestPointID { get; set; } = -1;
   }
}
