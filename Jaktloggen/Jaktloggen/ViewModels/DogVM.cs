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
    public class DogVM
    {

        public bool DogExists
        {
            get { return CurrentDog.ID > 0; }
        }

        public Dog CurrentDog { get; set; }
        public DogVM(Dog currentDog)
        {
            if (currentDog.ID == 0)
            {
                currentDog.ID = App.Database.SaveDog(currentDog);
            }
            CurrentDog = currentDog;
        }

        public void BindData()
        {
            var id = CurrentDog.ID;
            CurrentDog = null;
            CurrentDog = App.Database.GetDog(id);
        }

        public void Save()
        {
            App.Database.SaveDog(CurrentDog);
        }

        public void Delete()
        {
            App.Database.DeleteDog(CurrentDog);
        }
        public async Task SelectPicture()
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            if (mediaFile != null)
            {
                CurrentDog.ImagePath = mediaFile.Path;
                Save();
            }
        }
    }
}
