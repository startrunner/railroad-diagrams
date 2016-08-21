using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using RailroadDiagrams.App.Model;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace RailroadDiagrams.App.ViewModel
{
   class SymbolViewModel : ViewModelBase
   {
      Symbol model;

      public SymbolViewModel(Symbol model)
      {
         this.model = model;
         UpdateLeftConnectorPosition = new RelayCommand<Point>(UpdateLeftConnectorPositionExecute);
         UpdateRightConnectorPosition = new RelayCommand<Point>(UpdateRightConnectorPositionExecute);
      }

      [PreferredConstructor]
      public SymbolViewModel() : this(null) { }

      public event EventHandler<ConnectionPointMovedEventArgs> ConnectionPointMoved;

      public ICommand UpdateLeftConnectorPosition { get; private set; }
      public ICommand UpdateRightConnectorPosition { get; private set; }

      #region Properties
      public bool IsTerminal
      {
         get { return model != null ? model.Data.IsTerminal : false; }
         set { if (model != null) model.Data.IsTerminal = value; }
      }

      public string Text
      {
         get { return model != null ? model.Text : "???"; }
         set { if (model != null) model.Text = value; }
      }

      

      public Point PointPosition
      {
         get
         {
            if (model != null)
            {
               return new Point(model.Data.X, model.Data.Y);
            }
            else return new Point(0, 0);
         }
         set
         {

            if (model != null)
            {
               int x = (int)value.X;
               int y = (int)value.Y;
               model.Data.X = x;
               model.Data.Y = y;
               RaisePropertyChanged(nameof(PointPosition));
            }
         }
      }

      public int LeftConnectionPointID
      {
         get { return model.Data.LeftConnectionPointID; }
      }

      public int RightConenctionPointID
      {
         get { return model.Data.RightConnectionPointID; }
      }

      #endregion

      private void UpdateLeftConnectorPositionExecute(Point obj)
      {
         ConnectionPointMoved?.Invoke(this, new ConnectionPointMovedEventArgs(model.Data.LeftConnectionPointID, obj));
      }

      private void UpdateRightConnectorPositionExecute(Point obj)
      {
         ConnectionPointMoved?.Invoke(this, new ConnectionPointMovedEventArgs(model.Data.RightConnectionPointID, obj));
      }
   }

   public class ConnectionPointMovedEventArgs:EventArgs
   {
      public int PointID { get; set; }
      public Point Position { get; set; }

      public ConnectionPointMovedEventArgs(int pointID, Point position)
      {
         this.PointID = pointID;
         this.Position = position;
      }
   }
}
