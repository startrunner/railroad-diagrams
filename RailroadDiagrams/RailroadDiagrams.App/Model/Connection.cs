using RailroadDiagrams.DataModels;

namespace RailroadDiagrams.App.Model
{
   public class Connection : ModelBase<ConnectionData>
   {
      public Connection(ConnectionData data) : base(data)
      {
      }
   }
}