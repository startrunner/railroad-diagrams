using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class CurvePolygonPoint : ModelBase<CurvePolygonPointData>
   {
      private CurvePolygonPoint(CurvePolygonPointData data) : base(data)
      {
      }

      public static CurvePolygonPoint Of(CurvePolygonPointData data)
      {
         data.LogicUnit = data.LogicUnit as CurvePolygonPoint ?? new CurvePolygonPoint(data);
         return data.LogicUnit as CurvePolygonPoint;
      }
   }
}
