using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class CurvePolygonPointData:DataModelBase
   {
      [DataMember(Name ="id")]
      public int ID { get; set; }

      [DataMember(Name ="xScale")]
      public double XScale { get; set; }
      
      [DataMember(Name ="yScale")]
      public double YScale { get; set; }

      public CurvePolygonPointData() : this(-1, 0, 0) { }
      public CurvePolygonPointData(double xScale, double yScale) : this(-1, xScale, yScale) { }
      public CurvePolygonPointData(int id, double xScale, double yScale)
      {
         this.ID = id;
         this.XScale = xScale;
         this.YScale = yScale;
      }
   }
}
