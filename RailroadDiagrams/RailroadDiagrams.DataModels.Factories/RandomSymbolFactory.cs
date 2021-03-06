﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels.Factories
{
   public class RandomSymbolFactory : IFactory<SymbolData>
   {
      static readonly string[] TerminalNames = new string[] { "if", "else", "for", "while", "do", "class", "attached", "struct" };
      static readonly string[] NonTerminalNames = new string[] { "condition", "boolean-expression", "any-expression", "class-definition", "top-level-declaration" };


      Random random = new Random(DateTime.Now.GetHashCode());

      public SymbolData Create()
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

         rt.X = random.Next(1500);
         rt.Y = random.Next(1500);


         return rt;
      }
   }
}
