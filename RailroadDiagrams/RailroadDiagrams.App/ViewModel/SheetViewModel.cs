using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using RailroadDiagrams.App.Model;
using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace RailroadDiagrams.App.ViewModel
{
   class SheetViewModel
   {
      Sheet model;
      Dictionary<int, List<ConnectionViewModel>> pointConnectionAssoc = new Dictionary<int, List<ConnectionViewModel>>();

      public event EventHandler<EventArgs> SwitchToInvoked;

      [PreferredConstructor]
      public SheetViewModel() : this(null) { }

      public SheetViewModel(Sheet model)
      {
         SwitchTo = new RelayCommand(SwitchToExecute);
         CreateConnection = new RelayCommand<Tuple<int, int>>(CreateConnectionExecute);
         UpdateConnectorPosition = new RelayCommand<Tuple<int, Point>>(UpdateConnectorPositionExecute);

         this.model = model;
         if (model != null)
         {
            foreach (var symbolData in model.Data.Symbols)
            {
               var symbol = new SymbolViewModel(new Symbol(symbolData));
               Symbols.Add(symbol);
            }

            foreach(var connectionData in model.Data.Connections)
            {
               var conn = new ConnectionViewModel(new Connection(connectionData));
               AssociateConnectionPoints(conn);
               Connections.Add(conn);
            }
         };
      }

      public ICommand UpdateConnectorPosition { get; private set; }
      public ICommand CreateConnection { get; private set; }
      public ICommand SwitchTo { get; private set; }

      public ObservableCollection<SymbolViewModel> Symbols { get; private set; } =
         new ObservableCollection<SymbolViewModel>();
      public ObservableCollection<ConnectionViewModel> Connections { get; private set; } =
         new ObservableCollection<ConnectionViewModel>();

      public string Name
      {
         get { return model != null ? model.Data.Name : "???"; }
         set { if (model != null) model.Data.Name = value; }
      }

      public int ID
      {
         get { return model.Data.ID; }
      }

      void AssociateConnectionPoints(ConnectionViewModel vm)
      {
         var assoc = pointConnectionAssoc;
         if(!assoc.ContainsKey(vm.StartID))
         {
            assoc[vm.StartID] = new List<ConnectionViewModel>();
         }
         if(!assoc.ContainsKey(vm.EndID))
         {
            assoc[vm.EndID] = new List<ConnectionViewModel>();
         }
         assoc[vm.StartID].Add(vm);
         assoc[vm.EndID].Add(vm);
      }


      void SwitchToExecute() => SwitchToInvoked?.Invoke(this, new EventArgs());

      void CreateConnectionExecute(Tuple<int, int> connectorIds)
      {
         ConnectionData result = null;
         if (!model.TryCreateConnection(connectorIds.Item1, connectorIds.Item2, out result)) return;

         var rModel = new Connection(result);
         var vm = new ConnectionViewModel(rModel);
         AssociateConnectionPoints(vm);
         Connections.Add(vm);
      }

      void UpdateConnectorPositionExecute(Tuple<int, Point> arg)
      {
         var connectorID = arg.Item1;
         var newPos = arg.Item2;
         var assoc = pointConnectionAssoc;
         ;

         if (assoc.ContainsKey(connectorID))
         {
            foreach (var conn in assoc[connectorID])
            {
               if (conn.StartID == connectorID) conn.StartPosition = newPos;
               if (conn.EndID == connectorID) conn.EndPosition = newPos;
            }
         }
      }
   }
}