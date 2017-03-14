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
        public bool IsNew { get; set; }
        public bool IsLoadingPosition { get; set; }
        public LoggVM(Logg currentLogg)
        {
            if (currentLogg.ID == 0)
            {
                IsNew = true;
                currentLogg.ID = App.Database.SaveLogg(currentLogg);
            }
            CurrentLogg = currentLogg;
        }

        public async void BindData()
        {
            AllLogs = App.Database.GetLoggs();
            CurrentLogg = App.Database.GetLogg(CurrentLogg.ID);
            if (IsNew)
            {
                //await TryGetPosition();
            }
        }

        private async Task TryGetPosition()
        {
            ToggleLoadPosition();

            var position = await XLabsHelper.GetPosition();
            if (position != null)
            {
                CurrentLogg.Latitude = position.Latitude.ToString();
                CurrentLogg.Longitude = position.Longitude.ToString();

                Save();
            }

            ToggleLoadPosition();
        }

        private void ToggleLoadPosition()
        {
            IsLoadingPosition = !IsLoadingPosition;
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
