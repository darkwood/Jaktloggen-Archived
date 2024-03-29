using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jaktloggen.iOS.IO;
using Jaktloggen.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileUtility))]
namespace Jaktloggen.iOS.IO
{
    public class FileUtility : IFileUtility
    {
        public void Save(string filename, string text)
        {
            string filePath = GetFilePath(filename);
            File.WriteAllText(filePath, text);
        }
        
        public string Load(string filename)
        {
            string filePath = GetFilePath(filename);
            return File.ReadAllText(filePath);
        }

        public bool Exists(string filename)
        {
            string filePath = GetFilePath(filename);
            return File.Exists(filePath);
        }

        public void LogError(string error)
        {
            string filePath = GetFilePath("error.txt");
            var errorlog = File.ReadAllLines(filePath)?.ToList();
            if (errorlog == null)
            {
                errorlog = new List<string>();
            }
            errorlog.Add(error);
            File.WriteAllText(filePath, String.Join(System.Environment.NewLine, errorlog));
        }

        public void Delete(string filename)
        {
            if (Exists(filename))
            {
                string filePath = GetFilePath(filename);
                File.Delete(filePath);
            }
        }

        public DateTime GetLastWriteTime(string filename)
        {
            string filePath = GetFilePath(filename);
            return File.GetLastWriteTime(filePath);
        }
        private string GetFilePath(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return filePath;
        }
    }
}