using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RailroadDiagrams.DataModels.Factories;
using Newtonsoft.Json;

namespace RailroadDiagrams.DataModels.Tests
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void TestMethod1()
      {
         var data = new RandomDocumentFactory().Create();

         string json = JsonConvert.SerializeObject(data, Formatting.Indented);
         ;
      }
   }
}
