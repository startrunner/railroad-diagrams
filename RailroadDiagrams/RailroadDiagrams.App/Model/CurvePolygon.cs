using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class CurvePolygon : ModelBase<CurvePolygonData>
   {
      private CurvePolygon(CurvePolygonData data) : base(data)
      {
      }

      public static CurvePolygon Of (CurvePolygonData data)
      {
         data.LogicUnit = data.LogicUnit as CurvePolygon ?? new CurvePolygon(data);
         return data.LogicUnit as CurvePolygon;
      }
   }
}
