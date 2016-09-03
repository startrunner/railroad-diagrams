using Microsoft.Win32;
using Newtonsoft.Json;
using RailroadDiagrams.App.ViewModel;
using RailroadDiagrams.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
 
namespace RailroadDiagrams.App.Model
{
   class ApplicationContext
   {
      public const string FileFilter = "JSON Document (*.json)|*.json";

      public Document OpenDocument { get; set; }
      public string FileLocation { get; set; } = null;

      public event EventHandler OpenDocumentChanged;


      public Sheet ActiveSheet
      {
         get
         {
            return Sheet.Of(OpenDocument.Data.Sheets.Where(x => x.ID == OpenDocument.Data.OpenSheetID).First());
         }
      }

      public void Open()
      {
         OpenFileDialog dialog = new OpenFileDialog() { Filter = FileFilter };
         dialog.ShowDialog();

         if (!File.Exists(dialog.FileName)) return;

         string json = File.ReadAllText(dialog.FileName);
         DocumentData data = JsonConvert.DeserializeObject<DocumentData>(json);
         OpenDocument = Document.Of(data);
         FileLocation = dialog.FileName;
         OpenDocumentChanged?.Invoke(this, new EventArgs());
      }
      public void New()
      {
         ;
      }
      public void Save()
      {
         if(FileLocation==null)
         {
            SaveAs();
         }
         else
         {
            SaveTo(FileLocation);
         }
      }
      public void SaveAs()
      {
         SaveFileDialog dialog = new SaveFileDialog() { Filter = FileFilter };
         dialog.ShowDialog();
         if (string.IsNullOrEmpty(dialog.FileName)) return;
         SaveTo(dialog.FileName);
      }

      private void SaveTo(string path)
      {
         string content = JsonConvert.SerializeObject(OpenDocument.Data, Formatting.Indented);
         using (StreamWriter writer = new StreamWriter(path, false))
         {
            writer.Write(content);
            writer.Close();
         }
         FileLocation = path;
      }
   }
}
