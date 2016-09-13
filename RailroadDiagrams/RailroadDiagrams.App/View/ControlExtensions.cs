using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RailroadDiagrams.App.View
{
   public static class ControlExtensions
   {
      public static bool CheckIfMouseIsPhisicallyOver(this FrameworkElement visual)
      {
         return new Rect(0, 0, visual.ActualWidth, visual.ActualHeight).Contains(Mouse.GetPosition(visual));
      }

      public static Point RelativePositionTo(this FrameworkElement control1, Control control2)
      {
         try
         {
            Point pos1 = control1.PointToScreen(new Point());
            Point pos2 = control2.PointToScreen(new Point());
            return new Point(pos1.X - pos2.X, pos1.Y - pos2.Y);

         }
         catch
         {
            return new Point();
         }
      }
   }
}
