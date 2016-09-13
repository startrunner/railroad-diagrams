using System;
using System.Collections.Generic;
using System.Diagnostics;
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
   /// Interaction logic for MiddleButtonThumb.xaml
   /// </summary>
   public partial class MiddleButtonThumb : UserControl
   { 
      bool isCaptured=false;
      Point startPosition;
      Point lastPosition;

      public event DragDeltaEventHandler DragDelta;
      public event DragCompletedEventHandler DragCompleted;

      public MiddleButtonThumb()
      {
         InitializeComponent();
      }

      private Point GetMousePosition() => this.PointToScreen(Mouse.GetPosition(this));

      private void xBorder_MouseDown(Object sender, MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Middle) return;
         startPosition = lastPosition = GetMousePosition();
         isCaptured=Mouse.Capture(xBorder, CaptureMode.Element);
         e.Handled = true;
      }

      private void xBorder_MouseMove(Object sender, MouseEventArgs e)
      {
         if(isCaptured)
         {
            var currentPosition = GetMousePosition();
            DragDelta?.Invoke(this, new DragDeltaEventArgs(currentPosition.X - lastPosition.X, currentPosition.Y - lastPosition.Y));
            lastPosition = currentPosition;
            e.Handled = true;
         }
         Debug.WriteLine("mouse move");
      }

      private void xBorder_MouseUp(Object sender, MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Middle) return;
         CeaseDragging(false);
         e.Handled = true;
      }

      private void CeaseDragging(bool isCancellation)
      {
         ;
         if (isCaptured)
         {
            isCaptured = false;
            Mouse.Capture(this, CaptureMode.None);
            ReleaseMouseCapture();

            var currentPosition = GetMousePosition();
            DragCompleted?.Invoke(this, new DragCompletedEventArgs(currentPosition.X - startPosition.X, currentPosition.Y - startPosition.Y, isCancellation));
         }
      }

      public void CancelDrag()
      {
         CeaseDragging(true);
      }
   }
}
