using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   public class ModelBase<TData>
   {
      public TData Data { get; private set; }
      public ModelBase(TData data)
      {
         this.Data = data;
      }
   }
}
