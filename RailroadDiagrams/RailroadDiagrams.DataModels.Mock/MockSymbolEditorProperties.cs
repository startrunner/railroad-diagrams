using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels.Mock
{
   public class MockSymbolEditorProperties : IMock<SymbolEditorPropertiesData>
   {
      Random rand = new Random(DateTime.Now.GetHashCode());

      public SymbolEditorPropertiesData Mock()
      {
         SymbolEditorPropertiesData rt = new SymbolEditorPropertiesData();

         rt.X = rand.Next(3000);
         rt.Y = rand.Next(3000);

         return rt;
      }
   }
}
