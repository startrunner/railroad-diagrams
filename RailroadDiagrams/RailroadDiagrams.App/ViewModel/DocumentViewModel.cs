using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailroadDiagrams.App.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using RailroadDiagrams.DataModels;

namespace RailroadDiagrams.App.ViewModel
{
   class DocumentViewModel : ViewModelBase
   {
      Document model;

      public DocumentViewModel(Document model)
      {
         AddSheet = new RelayCommand(AddSheetExecute);
         SwitchToSheet = new RelayCommand<int>(SwtichToSheetExecute);

         this.model = model;

         if (model.OpenSheet != null) OpenSheet = new SheetViewModel(model.OpenSheet);

         foreach (var sheetData in model.Data.Sheets)
         {
            RegisterSheet(new SheetViewModel(Sheet.Of(sheetData)));
         }
      }

      public SheetViewModel OpenSheet { get; private set; } = null;
      public ObservableCollection<SheetViewModel> Sheets { get; set; } = new ObservableCollection<SheetViewModel>();
      public string Name
      {
         get { return model != null ? model.Data.Name : "???"; }
         set { if (model != null) model.Data.Name = value; }
      }
      public ICommand AddSheet { get; private set; }
      public ICommand SwitchToSheet { get; private set; }


      private void RegisterSheet(SheetViewModel sheet)
      {
         Sheets.Add(sheet);
      }

      private void SwtichToSheetExecute(int sheetID)
      {
         model.SwitchToSheet(sheetID);
         OpenSheet = new SheetViewModel(model.OpenSheet);
         RaisePropertyChanged(nameof(OpenSheet));
      }

      void AddSheetExecute()
      {
         var vm = new SheetViewModel(model.AddSheet());
         RegisterSheet(vm);
      }
   }
}
