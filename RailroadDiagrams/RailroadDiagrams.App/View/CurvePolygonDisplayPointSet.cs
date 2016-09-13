using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RailroadDiagrams.App.View
{
   public class CurvePolygonDisplayPointSet
   {
      static int idCount = 0;

      Dictionary<int, Point> numberPointAssoc = new Dictionary<int, Point>();
      List<Point> orderedPoints = null;
      int id = ++idCount;

      public event EventHandler<EventArgs> PointUpdated;

      public void UpdatePoint(int number, double x, double y)
      {
         orderedPoints = null;
         numberPointAssoc[number] = new Point(x, y);
         PointUpdated?.Invoke(this, new EventArgs());
      }

      public List<Point> GetOrderedPoints()
      {
         if(orderedPoints==null)
         {
            orderedPoints = new List<Point>();
            foreach(var point in numberPointAssoc.ToList().OrderBy(x=>x.Key))
            {
               orderedPoints.Add(point.Value);
            }
         }
         return orderedPoints;
      }
   }
}
