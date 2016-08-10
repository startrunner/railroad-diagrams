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

namespace RailroadDiagrams.App.ViewModel
{
   class SymbolViewModel : ViewModelBase
   {
      Symbol model;

      public ICommand CreateConnection { get; private set; }

      public bool IsTerminal
      {
         get { return model != null ? model.Data.IsTerminal : false; }
         set { if (model != null) model.Data.IsTerminal = value; }
      }

      public string Text
      {
         get { return model != null ? model.Data.Text : "???"; }
         set { if (model != null) model.Data.Text = value; }
      }

      public Point PointPosition
      {
         get
         {
            if (model != null)
            {
               return new Point(model.Data.EditorProperties.X, model.Data.EditorProperties.Y);
            }
            else return new Point(0, 0);
         }
         set
         {

            if (model != null)
            {
               int x = (int)value.X;
               int y = (int)value.Y;
               model.Data.EditorProperties.X = x;
               model.Data.EditorProperties.Y = y;
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

      public SymbolViewModel(Symbol model, ICommand createConnectionCommad)
      {
         this.model = model;
         CreateConnection = createConnectionCommad;
      }

      

      [PreferredConstructor]
      public SymbolViewModel() : this(null, null) { }
   }
}
