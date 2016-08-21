using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   public class CurvePolygonPointData
   {
      public int ID { get; set; }
      public double XScale { get; set; }
      public double YScale { get; set; }

      public CurvePolygonPointData() : this(-1, 0, 0) { }
      public CurvePolygonPointData(double xScale, double yScale) : this(-1, xScale, yScale) { }
      public CurvePolygonPointData(int id, double xScale, double yScale)
      {
         this.ID = id;
         this.XScale = XScale;
         this.YScale = YScale;
      }
   }
}
