using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class SymbolEditorPropertiesData
   {
      [DataMember(Name = "x")]
      public int X { get; set; }

      [DataMember(Name = "y")]
      public int Y { get; set; }
   }
}
