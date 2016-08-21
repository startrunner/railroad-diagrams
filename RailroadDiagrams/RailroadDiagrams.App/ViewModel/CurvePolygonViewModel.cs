using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using RailroadDiagrams.App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.ViewModel
{
   class CurvePolygonViewModel:ViewModelBase
   {
      CurvePolygon model=null;

      [PreferredConstructor]
      public CurvePolygonViewModel() { }

      public CurvePolygonViewModel(CurvePolygon model)
      {
         this.model = model;
         foreach(var point in model.Data.Points)
         {
            var pointModel = new CurvePolygonPoint(point);
            var vm = new CurvePolygonPointViewModel(pointModel);
            Points.Add(vm);
         }
      }

      public ObservableCollection<CurvePolygonPointViewModel> Points { get; private set; } = new ObservableCollection<CurvePolygonPointViewModel>();
   }
}
