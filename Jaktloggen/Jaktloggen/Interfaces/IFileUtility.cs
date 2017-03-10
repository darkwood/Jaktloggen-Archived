using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaktloggen.Interfaces
{
    public interface IFileUtility
    {
        void Save(string filename, string text);
        string Load(string filename);
        DateTime GetLastWriteTime(string filename);
        bool Exists(string filename);
        void LogError(string error);
        void Delete(string filename);
    }
}
