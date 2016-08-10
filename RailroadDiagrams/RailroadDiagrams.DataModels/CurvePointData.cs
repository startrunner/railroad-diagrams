using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.DataModels
{
   [DataContract]
   public class CurvePointData
   {
      [DataMember(Name = "x")]
      /// <summary>
      /// 0.00 to 1.00 (from top to bottom)
      /// </summary>
      public double X { get; set; }

      [DataMember(Name = "y")]
      /// <summary>
      /// 0.00 to 1.00 (from left to right)
      /// </summary>
      public double Y { get; set; }
   }
}
