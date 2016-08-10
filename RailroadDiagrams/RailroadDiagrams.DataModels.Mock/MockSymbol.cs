using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels.Mock
{
   public class MockSymbol : IMock<SymbolData>
   {
      static readonly string[] TerminalNames = new string[] { "if", "else", "for", "while", "do", "class", "attached", "struct" };
      static readonly string[] NonTerminalNames = new string[] { "condition", "boolean-expression", "any-expression", "class-definition", "top-level-declaration" };


      Random random = new Random(DateTime.Now.GetHashCode());
      MockSymbolEditorProperties mSymbolEditorProperties = new MockSymbolEditorProperties();


      public SymbolData Mock()
      {
         SymbolData rt = new SymbolData();

         rt.IsTerminal = random.Next() % 5 == 0;
         if(rt.IsTerminal)
         {
            rt.Text = TerminalNames[random.Next(TerminalNames.Length)];
         }
         else
         {
            rt.Text = NonTerminalNames[random.Next(NonTerminalNames.Length)];
         }

         rt.EditorProperties = mSymbolEditorProperties.Mock();

         return rt;
      }
   }
}
