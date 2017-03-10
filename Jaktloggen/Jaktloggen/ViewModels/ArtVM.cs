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
    public class ArtVM
    {

        public bool ArtExists
        {
            get { return CurrentArt.ID > 0; }
        }

        public Art CurrentArt { get; set; }
        public ArtVM(Art currentArt)
        {
            if (currentArt.ID == 0)
            {
                currentArt.GroupId = 100;
                currentArt.ID = App.Database.SaveArt(currentArt);
            }
            CurrentArt = currentArt;
        }

        public void BindData()
        {
            var id = CurrentArt.ID;
            CurrentArt = null;
            CurrentArt = App.Database.GetArt(id);
        }

        public void Save()
        {
            App.Database.SaveArt(CurrentArt);
        }

        public void Delete()
        {
            App.Database.DeleteArt(CurrentArt);
        }

        public async Task SelectPicture()
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            if(mediaFile != null) { 
                CurrentArt.ImagePath = mediaFile.Path;
                Save();
            }
        }
    }
}
