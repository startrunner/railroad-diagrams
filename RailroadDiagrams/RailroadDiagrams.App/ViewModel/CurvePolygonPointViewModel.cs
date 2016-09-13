using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using RailroadDiagrams.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.ViewModel
{
   class CurvePolygonPointViewModel:ViewModelBase
   {
      CurvePolygonPoint model;

      [PreferredConstructor]
      public CurvePolygonPointViewModel():this(null) { }

      public CurvePolygonPointViewModel(CurvePolygonPoint model)
      {
         this.model = model;
      }

      public int ID
      {
         get { return model.Data.ID; }
      }

      public int Number
      {
         get { return model.Data.Number; }
      }

      public double XScale
      {
         get { return model.Data.XScale; }
         set { model.Data.XScale = value; }
      }

      public double YScale
      {
         get { return model.Data.YScale; }
         set { model.Data.YScale = value; }
      }
   }
}
