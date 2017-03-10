using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.IO;

namespace Jaktloggen.ViewModels
{
    public class SettingsVM
    {
        public string Title = "Verktøy";

        public Task Export()
        {
            throw new NotImplementedException();
            //File.Save(App.Database.GetJakts(), "jakt.xml");
        }

        public Task Import()
        {
            throw new NotImplementedException();
        }
    }
}
