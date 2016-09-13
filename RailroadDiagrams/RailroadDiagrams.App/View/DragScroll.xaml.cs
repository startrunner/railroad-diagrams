using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RailroadDiagrams.App.View
{
   /// <summary>
   /// Interaction logic for DragScroll.xaml
   /// </summary>
   public partial class DragScroll : UserControl
   {
      public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(nameof(ScrollViewer), typeof(ScrollViewer), typeof(DragScroll), new PropertyMetadata());

      ScrollViewer scrollViewerParent = null;

      public ScrollViewer ScrollViewer
      {
         get { return GetValue(ScrollViewerProperty) as ScrollViewer; }
         set { SetValue(ScrollViewerProperty, value); }
      }

      public DragScroll()
      {
         InitializeComponent();
      }


      private void FindScrollViewerParent()
      {
         scrollViewerParent = null;
         FrameworkElement item = this;
         while(item!=null)
         {
            if(item is ScrollViewer)
            {
               scrollViewerParent = item as ScrollViewer;
            }
            item = item.Parent as FrameworkElement;
         }
      }

      private void xThumb_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
      {
         var viewer = ScrollViewer ?? scrollViewerParent;
         if (viewer == null) return;

         viewer.ScrollToHorizontalOffset(viewer.HorizontalOffset - e.HorizontalChange);
         viewer.ScrollToVerticalOffset(viewer.VerticalOffset - e.VerticalChange);

         xThumb.Cursor = Cursors.ScrollAll;

         e.Handled = true;
      }
      private void xThumb_DragCompleted(Object sender, DragCompletedEventArgs e)
      {
         xThumb.Cursor = null;
      }

      private void xControl_Loaded(Object sender, RoutedEventArgs e)
      {
         FindScrollViewerParent();
      }

   }
}
