using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using RailroadDiagrams.App.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RailroadDiagrams.App.ViewModel
{
   class SheetViewModel
   {

      Sheet model;

      public string Name
      {
         get { return model != null ? model.Data.Name : "???"; }
         set { if (model != null) model.Data.Name = value; }
      }

      public int ID
      {
         get { return model.Data.ID; }
      }

      public ObservableCollection<SymbolViewModel> Symbols { get; private set; } = new ObservableCollection<SymbolViewModel>();


      public event EventHandler<EventArgs> SwitchToInvoked;

      public ICommand SwitchTo { get; private set; }
      void SwitchToExecute() => SwitchToInvoked?.Invoke(this, new EventArgs());

      public ICommand CreateConnection { get; private set; }
      private void CreateConnectionExecute(Tuple<int, int> p)
      {
         model.CreateConnection(p.Item1, p.Item2);
      }

      public SheetViewModel(Sheet model)
      {
         SwitchTo = new RelayCommand(SwitchToExecute);
         CreateConnection = new RelayCommand<Tuple<int, int>>(CreateConnectionExecute);

         this.model = model;
         if(model!=null)
         {
            foreach(var symbolData in model.Data.Symbols)
            {
               Symbols.Add(new SymbolViewModel(new Symbol(symbolData), CreateConnection));
            }
         };
      }

      [PreferredConstructor]
      public SheetViewModel() : this(null) { }
   }
}