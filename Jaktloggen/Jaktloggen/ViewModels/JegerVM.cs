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
    public class JegerVM
    {

        public bool JegerExists
        {
            get { return CurrentJeger.ID > 0; }
        }

        public Jeger CurrentJeger { get; set; }
        public JegerVM(Jeger currentJeger)
        {
            if (currentJeger.ID == 0)
            {
                currentJeger.ID = App.Database.SaveJeger(currentJeger);
            }
            CurrentJeger = currentJeger;
        }

        public void Init()
        {
            var id = CurrentJeger?.ID ?? 0;
            CurrentJeger = null;
            CurrentJeger = App.Database.GetJeger(id);
        }

        public int Save()
        {
            return App.Database.SaveJeger(CurrentJeger);
        }

        public void Delete()
        {
            App.Database.DeleteJeger(CurrentJeger);
        }

        public event Action ShouldTakePicture = () => { };

        public async Task SelectPicture()
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            if (mediaFile != null)
            {
                CurrentJeger.ImagePath = mediaFile.Path;
                Save();
            }
        }
    }
}
