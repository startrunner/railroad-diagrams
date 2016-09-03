using RailroadDiagrams.App.Model;
using RailroadDiagrams.App.View;
using RailroadDiagrams.App.ViewModel;
using RailroadDiagrams.DataModels;
using RailroadDiagrams.DataModels.Mock;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RailroadDiagrams.App
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      ApplicationContext context = null;


      public App() : base()
      {
         DocumentData data = new MockDocument().Mock();
         Document document = Document.Of(data);
         DocumentViewModel vm = new DocumentViewModel(document);

         context = new ApplicationContext()
         {
            OpenDocument = document
         };

         var mainView = new DocumentView();

         mainView.DataContext = new ApplicationContextViewModel(context);

         mainView.Show();
      }
   }
}
