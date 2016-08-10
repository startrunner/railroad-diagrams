using System;
using System.Collections.Generic;
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
      public static bool CheckIfMouseIsPhisicallyOver(this Control visual)
      {
         return new Rect(0, 0, visual.ActualWidth, visual.ActualHeight).Contains(Mouse.GetPosition(visual));
      }
   }
}
