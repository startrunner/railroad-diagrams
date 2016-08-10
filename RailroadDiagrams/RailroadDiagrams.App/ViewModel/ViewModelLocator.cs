/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RailroadDiagrams.App"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace RailroadDiagrams.App.ViewModel
{
   /// <summary>
   /// This class contains static references to all the view models in the
   /// application and provides an entry point for the bindings.
   /// </summary>
   class ViewModelLocator
   {
      /// <summary>
      /// Initializes a new instance of the ViewModelLocator class.
      /// </summary>
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

         ////if (ViewModelBase.IsInDesignModeStatic)
         ////{
         ////    // Create design time view services and models
         ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
         ////}
         ////else
         ////{
         ////    // Create run time view services and models
         ////    SimpleIoc.Default.Register<IDataService, DataService>();
         ////}

         SimpleIoc.Default.Register<ApplicationContextViewModel>();
         SimpleIoc.Default.Register<DocumentViewModel>();
         SimpleIoc.Default.Register<SheetViewModel>();
         SimpleIoc.Default.Register<SymbolViewModel>();
      }


      public DocumentViewModel Document
      {
         get { return ServiceLocator.Current.GetInstance<DocumentViewModel>(); }
      }

      public SheetViewModel Sheet
      {
         get { return ServiceLocator.Current.GetInstance<SheetViewModel>(); }
      }

      public ApplicationContextViewModel ApplicationContext
      {
         get { return ServiceLocator.Current.GetInstance<ApplicationContextViewModel>(); }
      }

      public SymbolViewModel Symbol
      {
         get { return ServiceLocator.Current.GetInstance<SymbolViewModel>(); }
      }

      public static void Cleanup()
      {
         // TODO Clear the ViewModels
      }
   }
}