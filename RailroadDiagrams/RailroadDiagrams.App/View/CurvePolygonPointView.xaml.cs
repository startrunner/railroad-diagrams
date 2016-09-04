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
using RandomColorGenerator;
using System.Diagnostics;

namespace RailroadDiagrams.App.View
{
   /// <summary>
   /// Interaction logic for CurvePolygonPointView.xaml
   /// </summary>
   public partial class CurvePolygonPointView : UserControl
   {
      private static Random rand = new Random(DateTime.Now.ToString().GetHashCode());

      public CurvePolygonPointView()
      {
         InitializeComponent();
         RandomColor.Seed(this.GetHashCode());

         xThumb.Background = new SolidColorBrush(RandomColor.GetColor(ColorScheme.Random, Luminosity.Light));
         ;
      }

      public static readonly DependencyProperty ConnectionStartPositionProperty = DependencyProperty.Register(nameof(ConnectionStartPosition), typeof(Point), typeof(CurvePolygonPointView), new PropertyMetadata(new Point(), ConnnectionStartPositionValueChanged));
      public static readonly DependencyProperty ConnectionEndPositionProperty = DependencyProperty.Register(nameof(ConnectionEndPosition), typeof(Point), typeof(CurvePolygonPointView), new PropertyMetadata(new Point(), ConnectionEndPositionValueChanged));
      public static readonly DependencyProperty XScaleProperty = DependencyProperty.Register(nameof(XScale), typeof(double), typeof(CurvePolygonPointView), new PropertyMetadata(0.0, XScaleValueChanged));
      public static readonly DependencyProperty YScaleProperty = DependencyProperty.Register(nameof(YScale), typeof(double), typeof(CurvePolygonPointView), new PropertyMetadata(0.0, YScaleValueChanged));

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


      private static void ConnnectionStartPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;
         view?.UpdatePosition();
      }
      private static void ConnectionEndPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var view = d as CurvePolygonPointView;
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

      private void UpdatePosition()
      {
         var start = ConnectionStartPosition;
         var end = ConnectionEndPosition;

         double width = Math.Abs(start.X - end.X);
         double heigth = Math.Abs(start.Y - end.Y);

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

         Margin = new Thickness(x, y, Margin.Right, Margin.Bottom);
      }
   }
}
