using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using RailroadDiagrams.App.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RailroadDiagrams.App.ViewModel
{
   class ApplicationContextViewModel : ViewModelBase
   {
      ApplicationContext model;

      public DocumentViewModel OpenDocument { get; private set; }

      public ICommand New { get; private set; }
      private void NewExecite()
      {
         model?.New();
      }

      public ICommand Open { get; private set; }
      private void OpenExecute()
      {
         model?.Open();
      }

      public ICommand Save { get; private set; }
      private void SaveExecute()
      {
         model?.Save();
      }

      public ICommand SaveAs { get; private set; }
      private void SaveAsExecute()
      {
         model?.SaveAs();
      }

      public ApplicationContextViewModel(ApplicationContext model)
      {
         this.model = model;

         if(model!=null)
         {
            model.OpenDocumentChanged += OnModelOpenDocumentChanged;
         }

         this.OpenDocument = new DocumentViewModel(model.OpenDocument);

         New = new RelayCommand(NewExecite);
         Open = new RelayCommand(OpenExecute);
         Save = new RelayCommand(SaveExecute);
         SaveAs = new RelayCommand(SaveAsExecute);
      }

      private void OnModelOpenDocumentChanged(Object sender, EventArgs e)
      {
         OpenDocument = new DocumentViewModel(model.OpenDocument);
         RaisePropertyChanged(nameof(OpenDocument));
      }

      [PreferredConstructor]
      public ApplicationContextViewModel() : this(null) { }
   }
}
