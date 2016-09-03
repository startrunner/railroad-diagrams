using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RailroadDiagrams.App.Model
{
   partial class Sheet : ModelBase<SheetData>
   {
      Dictionary<int, ConnectionPointInfo> connectionPointInfoOf { get; set; }
        = new Dictionary<int, ConnectionPointInfo>();

      public HashSet<Symbol> Symbols { get; set; } = new HashSet<Symbol>();

      public bool TryCreateConnection(int point1ID, int point2ID, out ConnectionData result)
      {
         var info1 = GetConnectionPointInfo(point1ID);
         var info2 = GetConnectionPointInfo(point2ID);

         if ((info1.Type == ConnectionPointType.Leftside &&
            info2.Type == ConnectionPointType.Leftside) ||
            (info1.Type == ConnectionPointType.Rightside &&
            info2.Type == ConnectionPointType.Rightside))
         {
            result = null;
            return false;
         }

         int startID, endID;

         if (info1.Type == ConnectionPointType.Leftside && info2.Type == ConnectionPointType.Rightside)
         {
            startID = info2.ID;
            endID = info1.ID;
         }
         else if (info1.Type == ConnectionPointType.Rightside && info2.Type == ConnectionPointType.Leftside)
         {
            startID = info1.ID;
            endID = info2.ID;
         }
         else
         {
            throw new NotImplementedException();
         }

         if (Data.Connections.Where(conn => conn.StartID == startID && conn.EndID == endID).Count() != 0)
         {
            //Connection Already Exists :)
            result = null;
            return false;
         }

         result = new ConnectionData()
         {
            ID = ++Data.LatestConnectionID,
            StartID = startID,
            EndID = endID,
         };

         var curvePolygon = new CurvePolygonData();
         curvePolygon.Points.Add(new CurvePolygonPointData(++curvePolygon.LatestPointID, 1.0, 1.0));
         curvePolygon.Points.Add(new CurvePolygonPointData(++curvePolygon.LatestPointID, 0.5, 0.5));
         curvePolygon.Points.Add(new CurvePolygonPointData(++curvePolygon.LatestPointID, 0.0, 0.0));

         result.CurvePolygon = curvePolygon;

         Data.Connections.Add(result);
         return true;
      }

      public ConnectionPointInfo GetConnectionPointInfo(int pointID)
      {
         if (!connectionPointInfoOf.ContainsKey(pointID))
         {
            throw new InvalidOperationException($"A connection point with ID {pointID} does not exist");
         }
         return connectionPointInfoOf[pointID];
      }

      private void RegisterConnectionPointInfo(SymbolData sData)
      {
         connectionPointInfoOf[sData.LeftConnectionPointID] = new ConnectionPointInfo()
         {
            ID = sData.LeftConnectionPointID,
            OwnerID = sData.ID,
            Type = ConnectionPointType.Leftside
         };
         connectionPointInfoOf[sData.RightConnectionPointID] = new ConnectionPointInfo()
         {
            ID = sData.RightConnectionPointID,
            OwnerID = sData.ID,
            Type = ConnectionPointType.Rightside
         };
      }

      private Sheet(SheetData data) : base(data)
      {
         foreach (var sData in data.Symbols)
         {
            Symbols.Add(Symbol.Of(sData));
            RegisterConnectionPointInfo(sData);
         }
      }

      public static Sheet Of(SheetData data)
      {
         data.LogicUnit = data.LogicUnit as Sheet ?? new Sheet(data);
         return data.LogicUnit as Sheet;
      }
   }
}
