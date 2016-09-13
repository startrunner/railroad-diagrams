using MouseKeyboardActivityMonitor;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor.WinApi;
using System.Windows.Media;

namespace RailroadDiagrams.App.View
{
   public static class MouseProximityActivation
   {
      static MouseHookListener MouseHook = null;
      private static HashSet<IMouseProximityActivatable> Visuals = new HashSet<IMouseProximityActivatable>();

      public static void Register<T>(T visual) where T : FrameworkElement, IMouseProximityActivatable
      {
         Visuals.Add(visual);
      }
      public static void UnRegister<T>(T visual) where T: FrameworkElement, IMouseProximityActivatable
      {
         Visuals.Remove(visual);
      }

      static MouseProximityActivation()
      {
         MouseHook = new MouseHookListener(new AppHooker()) { Enabled = true };
         MouseHook.MouseMove += OnMouseGloballyMoved;
      }

      private static void OnMouseGloballyMoved(Object sender, MouseEventArgs e)
      {
         foreach (var visual in Visuals)
         {
            if ((visual as FrameworkElement).IsLoaded == false) continue;

            var mousePos = e.Location;
            var visualPos = visual.ActivationRangeCenter;

            double a = mousePos.X - visualPos.X;
            double b = mousePos.Y - visualPos.Y;

            double distSq = a * a + b * b;
            bool active = distSq <= visual.ActivationRangeSquared;

            visual.IsActivated = active;
         }
      }
   }

   /// <summary>
   /// Note: Should only be implemented by ancestors of FrameworkElement
   /// </summary>
   public interface IMouseProximityActivatable
   {
      /// <summary>
      /// The square of maxumum range (distance from mouse cursor) at which the visual is activated
      /// Being pre-squared is an optimization.
      /// </summary>
      double ActivationRangeSquared { get; }
      /// <summary>
      /// Point (RELATIVE TO SCREEN) from which the distance to the mouse cursor is measured
      /// </summary>
      Point ActivationRangeCenter { get; }

      /// <summary>
      /// Gets or sets whether or not the visual is range-activated
      /// </summary>
      bool IsActivated { get; set; }
   }
}
