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
      public Sheet(SheetData data) : base(data)
      {
      }

      public Connection CreateConnection(int id1, int id2)
      {
         MessageBox.Show($"Connecting {id1} {id2}");
         return null; 
      }
   }
}
