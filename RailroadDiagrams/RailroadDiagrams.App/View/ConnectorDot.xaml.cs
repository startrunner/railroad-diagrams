using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace RailroadDiagrams.App.View
{
   /// <summary>
   /// Interaction logic for ConnectorDot.xaml
   /// </summary>
   public partial class ConnectorDot : UserControl,IMouseProximityActivatable
   {
      const double ActivationRangeSq= 10000;

      #region Static Members
      static readonly object StaticLock = new object();

      static readonly DropShadowEffect HoverEffect = new DropShadowEffect()
      {
         RenderingBias = RenderingBias.Quality,
         Opacity = .99,
         Color = Colors.Pink,
         ShadowDepth = 0,
         BlurRadius = 10
      };

      static ConnectorDot currentlyHoveredDot = null;

      static int idCounter = 0;
      #endregion

      bool _isActivated = true;

      #region Static Events
      delegate void AnyDotDraggingHandler(ConnectorDot sender, DragDeltaEventArgs e);
      static event AnyDotDraggingHandler AnyDotDragging;
      #endregion

      #region Events

      /// <summary>
      /// Invoked when a connection has been made by dragging from this dot
      /// </summary>
      public event EventHandler<ConnectorDotConnectedEventArgs> Connected;


      /// <summary>
      /// Invoked when a connection has been made by dragging from another dot
      /// </summary>
      public event EventHandler<ConnectorDotConnectedEventArgs> GotConnected;
      #endregion

      #region Members
      Line connectionLine = new Line()
      {
         StrokeThickness = 1.5,
         Stroke = new SolidColorBrush(Colors.DarkGray),
         StrokeDashArray = new DoubleCollection { 3, 1.5 },
         X1 = 0,
         Y1 = 0
      };

      bool isInHoverMode = false;
      #endregion

      #region Properties
      public int UniqueID { get; set; } = ++idCounter;

      Double IMouseProximityActivatable.ActivationRangeSquared { get { return ActivationRangeSq; } }
      Point IMouseProximityActivatable.ActivationRangeCenter { get { return PointToScreen(new Point()); } }

      bool IMouseProximityActivatable.IsActivated
      {
         get { return _isActivated; }
         set
         {
            _isActivated = value;

            if (this.xConnThumb.IsDragging == false)
            {
               this.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
         }
      }


      #endregion

      public ConnectorDot()
      {
         InitializeComponent();
         AnyDotDragging += OnAnyDotDragging;

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

      private void OnAnyDotDragging(ConnectorDot that, DragDeltaEventArgs e)
      {
         if (xLayoutRoot.CheckIfMouseIsPhisicallyOver())
         {
            this.EnterHoverMode();
         }
         else
         {
            this.LeaveHoverMode();
         }
      }

      private void xBorderDragDrop_GiveFeedback(Object sender, GiveFeedbackEventArgs e)
      {
         var mousePostion = Mouse.GetPosition(xLineHost);
         connectionLine.X2 = mousePostion.X;
         connectionLine.Y2 = mousePostion.Y;
         //Debug.WriteLine(mousePostion.ToString());
         e.Handled = true;
      }

      private void xConnThumb_DragDelta(Object sender, DragDeltaEventArgs e)
      {
         AnyDotDragging?.Invoke(this, e);
         var mousePosition = Mouse.GetPosition(xLineHost);
         connectionLine.X2 = mousePosition.X;
         connectionLine.Y2 = mousePosition.Y;
         if (!xLineHost.Children.Contains(connectionLine))
         {
            xLineHost.Children.Add(connectionLine);
         }
         e.Handled = false;
      }

      private void xConnThumb_DragCompleted(Object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
      {
         xLineHost.Children.Remove(connectionLine);
         if (currentlyHoveredDot != null)
         {
            var that = currentlyHoveredDot;
            int thisID = this.UniqueID;
            int thatID = that.UniqueID;

            var args = new ConnectorDotConnectedEventArgs(thisID, thatID);

            this.Connected?.Invoke(this, args);
            that.GotConnected?.Invoke(that, args);

            //Debug.WriteLine($"Connecting {thisID} to {thatID}");
         }

         (this as IMouseProximityActivatable).IsActivated = (this as IMouseProximityActivatable).IsActivated;

      }

      private void EnterHoverMode()
      {
         if (isInHoverMode) return;

         xEllipseDot.Effect = HoverEffect;
         currentlyHoveredDot = this;
         isInHoverMode = true;
      }

      private void LeaveHoverMode()
      {
         if (!isInHoverMode) return;

         xEllipseDot.Effect = null;
         lock (StaticLock)
         {
            if (currentlyHoveredDot == this)
            {
               currentlyHoveredDot = null;
            }
         }
         isInHoverMode = false;
      }

      private void xConnThumb_MouseEnter(Object sender, MouseEventArgs e) => EnterHoverMode();

      private void xConnThumb_MouseLeave(Object sender, MouseEventArgs e) => LeaveHoverMode();
   }

   public class ConnectorDotConnectedEventArgs
   {
      public int ID1 { get; private set; }
      public int ID2 { get; private set; }

      public ConnectorDotConnectedEventArgs(int id1, int id2)
      {
         this.ID1 = id1;
         this.ID2 = id2;
      }
   }
}
