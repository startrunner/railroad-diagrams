﻿using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadDiagrams.App.Model
{
   class Connection : ModelBase<ConnectionData>
   {
      public Connection(ConnectionData data) : base(data)
      {
      }
   }
}
