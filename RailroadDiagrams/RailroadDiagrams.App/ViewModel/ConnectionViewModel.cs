using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailroadDiagrams.DataModels;
using RailroadDiagrams.App.Model;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Collections.ObjectModel;

namespace RailroadDiagrams.App.ViewModel
{
   class ConnectionViewModel:ViewModelBase
   {
      Connection model;
      Point valStartPosition, valEndPosition;

      public ConnectionViewModel(Connection model)
      {
         this.model = model;

         foreach(var point in model.Data.CurvePolygon.Points)
         {
            
         }
      }

      [PreferredConstructor]
      public ConnectionViewModel() { }

      public int ID { get { return model.Data.ID; } }
      public int StartID { get { return model.Data.StartID; } }
      public int EndID { get { return model.Data.EndID; } }
      public ObservableCollection<Point> CurvePolygon { get; private set; } = new ObservableCollection<Point>();

      public Point StartPosition
      {
         get { return valStartPosition; }
         set
         {
            valStartPosition = value;
            RaisePropertyChanged(nameof(StartPosition));
         }
      }

      public Point EndPosition
      {
         get { return valEndPosition; }
         set
         {
            valEndPosition = value;
            RaisePropertyChanged(nameof(EndPosition));
         }
      }

   }
}
