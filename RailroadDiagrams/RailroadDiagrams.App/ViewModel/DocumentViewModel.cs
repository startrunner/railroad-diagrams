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

      public SheetViewModel OpenSheet { get; private set; } = null;
      public ObservableCollection<SheetViewModel> Sheets { get; set; } = new ObservableCollection<SheetViewModel>();
      public string Name
      {
         get { return model != null ? model.Data.Name : "???"; }
         set { if (model != null) model.Data.Name = value; }
      }
      public ICommand AddSheet { get; private set; }


      private void RegisterSheet(SheetViewModel sheet)
      {
         sheet.SwitchToInvoked += OnSheetSwitchToRequested;
         Sheets.Add(sheet);
      }

      private void OnSheetSwitchToRequested(Object sender, EventArgs e)
      {
         var sheet = sender as SheetViewModel;
         int id = sheet.ID;
         model.SwitchToSheet(id);

         OpenSheet = new SheetViewModel(model.OpenSheet);
         RaisePropertyChanged(nameof(OpenSheet));
      }

      void AddSheetExecute()
      {
         var vm = new SheetViewModel(model.AddSheet());
         RegisterSheet(vm);
      }


      public DocumentViewModel(Document model)
      {
         AddSheet = new RelayCommand(AddSheetExecute);


         this.model = model;

         if (model.OpenSheet != null) OpenSheet = new SheetViewModel(model.OpenSheet);

         foreach(var sheetData in model.Data.Sheets)
         {
            RegisterSheet(new SheetViewModel(new Sheet(sheetData)));
         }
      }
   }
}
