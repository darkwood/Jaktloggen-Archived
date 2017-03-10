using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Jaktloggen.Annotations;
using Jaktloggen.Data;
using Jaktloggen.Helpers;
using Jaktloggen.IO;
using Jaktloggen.Models;

using MvvmHelpers;
using PropertyChanged;
using Xamarin.Forms;

namespace Jaktloggen.ViewModels
{
    [ImplementPropertyChanged]
    public class LoggVM
    {
        public Logg CurrentLogg { get; set; }
        public IEnumerable<Logg> AllLogs { get; private set; }

        public LoggVM(Logg currentLogg)
        {
            if (currentLogg.ID == 0)
            {
                currentLogg.ID = App.Database.SaveLogg(currentLogg);
            }
            CurrentLogg = currentLogg;
        }

        public void BindData()
        {
            AllLogs = App.Database.GetLoggs();
            CurrentLogg = App.Database.GetLogg(CurrentLogg.ID);
        }

        public void Save()
        {
            App.Database.SaveLogg(CurrentLogg);
        }
        public void Delete()
        {
            App.Database.DeleteLog(CurrentLogg);
        }
        public async Task SelectPicture()
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            if (mediaFile != null)
            {
                CurrentLogg.ImagePath = mediaFile.Path;
                Save();
            }
        }
    }
}
