using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using MouseKeyboardActivityMonitor.WinApi;
using MouseKeyboardActivityMonitor;

namespace RailroadDiagrams.App.View
{
   /// <summary>
   /// Interaction logic for CurvePolygonPointView.xaml
   /// </summary>
   public partial class CurvePolygonPointView : UserControl, IMouseProximityActivatable
   {
      const double ActivateDistanceSQ = 10000;

      static HashSet<CurvePolygonPointView> CurrentlyLoaded = new HashSet<CurvePolygonPointView>();

      bool _isActivated=true;
      double width=0, heigth=0;
      Point start, end;

      public CurvePolygonPointView()
      {
         InitializeComponent();

         if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
         {
            (this as IMouseProximityActivatable).IsActivated = true;
         }
         else
         {
            (this as IMouseProximityActivatable).IsActivated = false;
            MouseProximityActivation.Register(this);
         }
      }
      
      public static readonly DependencyProperty ConnectionStartPositionProperty = DependencyProperty.Register(nameof(ConnectionStartPosition), typeof(Point), typeof(CurvePolygonPointView), new PropertyMetadata(new Point(), ConnnectionStartPositionValueChanged));
      public static readonly DependencyProperty ConnectionEndPositionProperty = DependencyProperty.Register(nameof(ConnectionEndPosition), typeof(Point), typeof(CurvePolygonPointView), new PropertyMetadata(new Point(), ConnectionEndPositionValueChanged));
      public static readonly DependencyProperty XScaleProperty = DependencyProperty.Register(nameof(XScale), typeof(double), typeof(CurvePolygonPointView), new PropertyMetadata(0.0, XScaleValueChanged));
      public static readonly DependencyProperty YScaleProperty = DependencyProperty.Register(nameof(YScale), typeof(double), typeof(CurvePolygonPointView), new PropertyMetadata(0.0, YScaleValueChanged));
      public static readonly DependencyProperty UpdateXYScalesCommandProperty = DependencyProperty.Register(nameof(UpdateXYScalesCommand), typeof(ICommand), typeof(CurvePolygonPointView), new PropertyMetadata(null));
      public static readonly DependencyProperty DisplayPointSetProperty = DependencyProperty.Register(nameof(DisplayPointSet), typeof(CurvePolygonDisplayPointSet), typeof(CurvePolygonPointView), new PropertyMetadata(defaultValue: null));
      public static readonly DependencyProperty PointNumberProperty = DependencyProperty.Register(nameof(PointNumber), typeof(int), typeof(CurvePolygonPointView), new PropertyMetadata(defaultValue: -1));

      #region IRangeActivatable Properties
      Double IMouseProximityActivatable.ActivationRangeSquared { get { return ActivateDistanceSQ; } }
      Point IMouseProximityActivatable.ActivationRangeCenter { get { return PointToScreen(new Point()); } }
      bool IMouseProximityActivatable.IsActivated
      {
         get { return _isActivated; }
         set
         {
            if (_isActivated == value) return;
            _isActivated = value;
            this.Visibility = value ? Visibility.Visible : Visibility.Hidden;
         }
      }
      #endregion

      public Point ConnectionStartPosition
      {
         get { return (Point)GetValue(ConnectionStartPositionProperty); }
         set { SetValue(ConnectionStartPositionProperty, value); }
      }
      public Point ConnectionEndPosition
      {
         get { return (Point)GetValue(ConnectionEndPositionProperty); }
         set { SetValue(ConnectionEndPositionProperty, value); }
      }
      public double XScale
      {
         get { return (double)GetValue(XScaleProperty); }
         set { SetValue(XScaleProperty, value); }
      }
      public double YScale
      {
         get { return (double)GetValue(YScaleProperty); }
         set { SetValue(YScaleProperty, value); }
      }
      public ICommand UpdateXYScalesCommand
      {
         get { return GetValue(UpdateXYScalesCommandProperty) as ICommand; }
         set { SetValue(UpdateXYScalesCommandProperty, value); }
      }
      public CurvePolygonDisplayPointSet DisplayPointSet
      {
         get { return GetValue(DisplayPointSetProperty) as CurvePolygonDisplayPointSet; }
         set { SetValue(DisplayPointSetProperty, value); }
      }
      public int PointNumber
      {
         get { return (int)GetValue(PointNumberProperty); }
         set { SetValue(PointNumberProperty, value); }
      }

      private void OnLoaded(Object sender, RoutedEventArgs e) => CurrentlyLoaded.Add(this);
      private void OnUnloaded(Object sender, RoutedEventArgs e) => CurrentlyLoaded.Remove(this);

      private static void ConnnectionStartPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;

         view.start = (Point)e.NewValue;
         view.UpdateXYDimensions();

         view?.UpdatePosition();
      }
      private static void ConnectionEndPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;

         view.end = (Point)e.NewValue;
         view.UpdateXYDimensions();

         view?.UpdatePosition();
      }
      private static void XScaleValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;
         view?.UpdatePosition();
      }
      private static void YScaleValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;
         view?.UpdatePosition();
      }

      private void UpdateXYDimensions()
      {
         width = Math.Abs(start.X - end.X);
         heigth = Math.Abs(start.Y - end.Y);
      }

      private void UpdatePosition()
      {
         double x = width * XScale;
         double y = heigth * YScale;

         if (start.X > end.X)
         {
            x = width - x;
         }
         if (start.Y > end.Y)
         {
            y = heigth - y;
         }

         this.DisplayPointSet?.UpdatePoint(PointNumber, x, y);

         Margin = new Thickness(x, y, Margin.Right, Margin.Bottom);
         
      }

      private void xThumb_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
      {
         double x = Margin.Left;
         double y = Margin.Top;

         x += e.HorizontalChange;
         y += e.VerticalChange;

         if (start.X > end.X)
         {
            x = width - x;
         }
         if (start.Y > end.Y)
         {
            y = heigth - y;
         }


         if (width != 0)
         {
            XScale = x / width;
         }

         if (heigth != 0)
         {
            YScale = y / heigth;
         }
      }
   }
}
