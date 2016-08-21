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
         OnLeftConnectionPointIDValueChanged(this, new DependencyPropertyChangedEventArgs(LeftConnectionPointIDProperty, -1, 0));
         OnRightConnectionPointIDValueChanged(this, new DependencyPropertyChangedEventArgs(RightConnectionPointIDProperty, -1, 0));

         xTextBoxText.SetBinding(TextBox.TextProperty, new Binding(nameof(Text)) { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

         this.xConnectorDotLeft.Connected += OnConnectorDotConnected;
         this.xConnectorDotRight.Connected += OnConnectorDotConnected;
         this.xConnectorDotLeft.GotConnected += OnConnectorDotGotConnected;
         this.xConnectorDotRight.GotConnected += OnConnectorDotGotConnected;

         this.Loaded += (x, y) => UpdateConnectionPointPositions();
         this.DataContextChanged += (x, y) => UpdateConnectionPointPositions();
      }

      

      #region Dependency Properties
      public static readonly DependencyProperty IsTerminalProperty = DependencyProperty.Register(nameof(IsTerminal), typeof(bool), typeof(SymbolView), new PropertyMetadata(defaultValue: true, propertyChangedCallback: OnIsTerminalChanged));
      public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(SymbolView), new PropertyMetadata(""));
      public static readonly DependencyProperty UpperCaptionProperty = DependencyProperty.Register(nameof(UpperCaption), typeof(string), typeof(SymbolView), new PropertyMetadata(""));
      public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(SymbolView), new PropertyMetadata(new Point(0, 0), OnPositionValueChanged));
      public static readonly DependencyProperty LeftConnectionPointIDProperty = DependencyProperty.Register(nameof(LeftConnectionPointID), typeof(int), typeof(SymbolView), new PropertyMetadata(0, OnLeftConnectionPointIDValueChanged));
      public static readonly DependencyProperty RightConnectionPointIDProperty = DependencyProperty.Register(nameof(RightConnectionPointID), typeof(int), typeof(SymbolView), new PropertyMetadata(0, OnRightConnectionPointIDValueChanged));
      public static readonly DependencyProperty UpdateConnectionPointPositionCommandProperty = DependencyProperty.Register(nameof(UpdateConnectionPointPositionCommand), typeof(ICommand), typeof(SymbolView), new PropertyMetadata());
      public static readonly DependencyProperty CreateConnectionCommandProperty = DependencyProperty.Register(nameof(CreateConnectionCommand), typeof(ICommand), typeof(SymbolView), new PropertyMetadata());

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
      public int LeftConnectionPointID
      {
         get { return (int)GetValue(LeftConnectionPointIDProperty); }
         set { SetValue(LeftConnectionPointIDProperty, value); }
      }
      public int RightConnectionPointID
      {
         get { return (int)GetValue(RightConnectionPointIDProperty); }
         set { SetValue(RightConnectionPointIDProperty, value); }
      }
      public ICommand UpdateConnectionPointPositionCommand
      {
         get { return GetValue(UpdateConnectionPointPositionCommandProperty) as ICommand; }
         set { SetValue(UpdateConnectionPointPositionCommandProperty, value); }
      }
      public ICommand CreateConnectionCommand
      {
         get { return GetValue(CreateConnectionCommandProperty) as ICommand; }
         set { SetValue(CreateConnectionCommandProperty, value); }
      }
      #endregion

      #region DependencyProperty OnValueChanged Handlers
      private static void OnPositionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         if (d is SymbolView == false) return;
         if (e.NewValue is Point == false) return;
         SymbolView view = d as SymbolView;
         Point newVal = (Point)e.NewValue;

         view.Margin = new Thickness(newVal.X, newVal.Y, 0, 0);

         view.UpdateConnectionPointPositions();
      }
      private static void OnLeftConnectionPointIDValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         SymbolView view = d as SymbolView;
         view.xConnectorDotLeft.UniqueID = view.LeftConnectionPointID;
      }
      private static void OnRightConnectionPointIDValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         SymbolView view = d as SymbolView;
         view.xConnectorDotRight.UniqueID = view.RightConnectionPointID;
      }
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
      #endregion

      #region Control Event Handlers
      private void OnConnectorDotConnected(Object sender, ConnectorDotConnectedEventArgs e)
      {
         CreateConnectionCommand?.Execute(new Tuple<int, int>(e.ID1, e.ID2));
         UpdateConnectionPointPositions();
      }

      private void OnConnectorDotGotConnected(object sender, ConnectorDotConnectedEventArgs e)
      {
         UpdateConnectionPointPositions();
      }

      private void Thumb_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
      {
         Point newPoint = new Point(Position.X + e.HorizontalChange, Position.Y + e.VerticalChange);
         Position = newPoint;
      }

      private void xThumb_MouseDoubleClick(Object sender, MouseButtonEventArgs e) => EnableEditTextMode();
      private void xTextBoxText_TextChanged(Object sender, TextChangedEventArgs e) => UpdateConnectionPointPositions();
      private void xTextBoxText_LostFocus(Object sender, RoutedEventArgs e) => DisableEditTextMode();
      private void xMenuItemIsTerminal_Click(Object sender, RoutedEventArgs e) => IsTerminal = !IsTerminal;

      private void xTextBoxText_KeyUp(Object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Escape || e.Key== Key.Enter) DisableEditTextMode();
      }
      #endregion

      #region Methods
      private void UpdateConnectionPointPositions()
      {
         var leftRelativePos = xAnchorLeft.RelativePositionTo(this);
         var rightRelativePos = xAnchorRight.RelativePositionTo(this);
         var leftPos= new Point(Position.X + leftRelativePos.X, Position.Y + leftRelativePos.Y);
         var rightPos= new Point(Position.X + rightRelativePos.X, Position.Y + rightRelativePos.Y);

         UpdateConnectionPointPositionCommand?.Execute(new Tuple<int, Point>(LeftConnectionPointID, leftPos));
         UpdateConnectionPointPositionCommand?.Execute(new Tuple<int, Point>(RightConnectionPointID, rightPos));

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
      #endregion
   }
}