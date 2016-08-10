using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class SymbolData
   {
      [DataMember(Name = "id")]
      public int ID { get; set; }

      [DataMember(Name = "content")]
      public string Text { get; set; }

      [DataMember(Name = "isTerminal")]
      public bool IsTerminal { get; set; }

      [DataMember(Name = "editorProperties")]
      public SymbolEditorPropertiesData EditorProperties { get; set; } = new SymbolEditorPropertiesData();

      [DataMember(Name ="leftConnectionPoint")]
      public int LeftConnectionPointID { get; set; } = -1;

      [DataMember(Name ="rightConnectionPoint")]
      public int RightConnectionPointID { get; set; } = -1;
   }
}
