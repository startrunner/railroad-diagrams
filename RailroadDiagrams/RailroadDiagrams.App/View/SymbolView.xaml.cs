using System;
using System.Collections.Generic;
using System.Diagnostics;
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
   /// Interaction logic for SymbolView.xaml
   /// </summary>
   /// 

   public partial class SymbolView : UserControl
   {
      bool editTextMode = false;
      
      public SymbolView()
      {
         InitializeComponent();

         OnIsTerminalChanged(this, new DependencyPropertyChangedEventArgs(IsTerminalProperty, !IsTerminal, IsTerminal));
         OnLeftConnectorIDValueChanged(this, new DependencyPropertyChangedEventArgs(LeftConnectorIDProperty, -1, 0));
         OnRightConnectorIDValueChanged(this, new DependencyPropertyChangedEventArgs(RightConnectorIDProperty, -1, 0));

         xTextBoxText.SetBinding(TextBox.TextProperty, new Binding(nameof(Text)) { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

         this.xConnectorDotLeft.Connected += OnConnectorDotConnected;
         this.xConnectorDotRight.Connected += OnConnectorDotConnected;
      }

      private void OnConnectorDotConnected(Object sender, ConnectorDotConnectedEventArgs e)
      {
         xCreateConnectionCommand.Execute(new Tuple<int, int>(e.ID1, e.ID2));
      }

      #region Dependency Properties
      public static readonly DependencyProperty IsTerminalProperty = DependencyProperty.Register(nameof(IsTerminal), typeof(bool), typeof(SymbolView), new PropertyMetadata(defaultValue: true, propertyChangedCallback: OnIsTerminalChanged));
      public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(SymbolView), new PropertyMetadata(""));
      public static readonly DependencyProperty UpperCaptionProperty = DependencyProperty.Register(nameof(UpperCaption), typeof(string), typeof(SymbolView), new PropertyMetadata(""));
      public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(SymbolView), new PropertyMetadata(new Point(0, 0), OnPositionValueChanged));
      public static readonly DependencyProperty LeftConnectorIDProperty = DependencyProperty.Register(nameof(LeftConnectorID), typeof(int), typeof(SymbolView), new PropertyMetadata(0, OnLeftConnectorIDValueChanged));
      public static readonly DependencyProperty RightConnectorIDProperty = DependencyProperty.Register(nameof(RightConnectorID), typeof(int), typeof(SymbolView), new PropertyMetadata(0, OnRightConnectorIDValueChanged));
      #endregion

      #region Properties
      public bool IsTerminal
      {
         get { return (bool)GetValue(IsTerminalProperty); }
         set { SetValue(IsTerminalProperty, value); }
      }
      public string Text
      {
         get { return GetValue(TextProperty) as string; }
         set { SetValue(TextProperty, value); }
      }
      public string UpperCaption
      {
         get { return GetValue(UpperCaptionProperty) as string; }
         set { SetValue(UpperCaptionProperty, value); }
      }
      public Point Position
      {
         get { return (Point)GetValue(PositionProperty); }
         set { SetValue(PositionProperty, value); }
      }
      public int LeftConnectorID
      {
         get { return (int)GetValue(LeftConnectorIDProperty); }
         set { SetValue(LeftConnectorIDProperty, value); }
      }
      public int RightConnectorID
      {
         get { return (int)GetValue(RightConnectorIDProperty); }
         set { SetValue(RightConnectorIDProperty, value); }
      }
      #endregion


      private static void OnIsTerminalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         if (d is SymbolView == false) return;

         bool isTerminal = (bool)e.NewValue;
         SymbolView view = d as SymbolView;
         Border border = view.xRectBorder;

         if (isTerminal)
         {
            border.CornerRadius = new CornerRadius(8);
            border.Background = new SolidColorBrush(Colors.LightGray);
         }
         else
         {
            border.CornerRadius = new CornerRadius(0);
            border.Background = new SolidColorBrush(Colors.White);
         }
         view.xMenuItemIsTerminal.IsChecked = isTerminal;
      }

      private static void OnPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         if (d is SymbolView == false) return;
         if (e.NewValue is Point == false) return;
         SymbolView view = d as SymbolView;
         Point newVal = (Point)e.NewValue;

         view.Margin = new Thickness(newVal.Y, newVal.X, 0, 0);
      }


      private static void OnLeftConnectorIDValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         SymbolView view = d as SymbolView;
         view.xConnectorDotLeft.UniqueID = view.LeftConnectorID;
      }


      private static void OnRightConnectorIDValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         SymbolView view = d as SymbolView;
         view.xConnectorDotRight.UniqueID = view.RightConnectorID;
      }

      private void Thumb_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
      {
         Point newPoint = new Point(Position.X + e.VerticalChange, Position.Y + e.HorizontalChange);
         Position = newPoint;
      }

      private void xThumb_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
      {
         EnableEditTextMode();
      }

      private void xTextBoxText_KeyUp(Object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Escape || e.Key== Key.Enter) DisableEditTextMode();
      }

      private void xTextBoxText_LostFocus(Object sender, RoutedEventArgs e)
      {
         DisableEditTextMode();
      }

      private void EnableEditTextMode()
      {
         if (editTextMode == false)
         {
            xThumb.IsEnabled = false;

            xTextBoxText.IsEnabled = true;
            xTextBoxText.SetValue(Panel.ZIndexProperty, 100);
            xTextBoxText.Focus();
            editTextMode = true;
            xTextBoxText.CaretIndex = xTextBoxText.Text.Length;
         }
      }

      private void DisableEditTextMode()
      {
         if (editTextMode == true)
         {
            xThumb.IsEnabled = true;

            xTextBoxText.IsEnabled = false;
            xTextBoxText.SetValue(Panel.ZIndexProperty, 0);
            editTextMode = false;
         }
      }

      private void xMenuItemIsTerminal_Click(Object sender, RoutedEventArgs e) => IsTerminal = !IsTerminal;
   }
}