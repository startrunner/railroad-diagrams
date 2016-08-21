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

namespace RailroadDiagrams.App.View
{
   /// <summary>
   /// Interaction logic for ConnectionView.xaml
   /// </summary>
   public partial class ConnectionView : UserControl
   {
      public ConnectionView()
      {
         InitializeComponent();
      }

      public static readonly DependencyProperty StartPositionProperty = DependencyProperty.Register(nameof(StartPosition), typeof(Point), typeof(ConnectionView), new PropertyMetadata(new Point(), StartPositionValueChanged));
      public static readonly DependencyProperty EndPositionProperty = DependencyProperty.Register(nameof(EndPosition), typeof(Point), typeof(ConnectionView), new PropertyMetadata(new Point(), EndPositionValueChanged));
      //public static readonly DependencyProperty 

      public Point StartPosition
      {
         get { return (Point)GetValue(StartPositionProperty); }
         set { SetValue(StartPositionProperty, value); }
      }

      public Point EndPosition
      {
         get { return (Point)GetValue(EndPositionProperty); }
         set { SetValue(EndPositionProperty, value); }
      }

      static void EndPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         (d as ConnectionView)?.UpdatePosition();
      }

      static void StartPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         (d as ConnectionView)?.UpdatePosition();
      }

      private void UpdatePosition()
      {
         var startPos = StartPosition;
         var endPos = EndPosition;

         double minX = Math.Min(startPos.X, endPos.X);
         double maxX = startPos.X + endPos.X - minX;

         double minY = Math.Min(startPos.Y, endPos.Y);
         double maxY = startPos.Y + endPos.Y - minY;

         double width = maxX - minX;
         double height = maxY - minY;

         Margin = new Thickness(minX, minY, 0, 0);
         Width = width;
         Height = height;
      }
   }
}
