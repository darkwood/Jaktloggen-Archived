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
using Jaktloggen.IO;
using Jaktloggen.Models;

using MvvmHelpers;
using PropertyChanged;
using Xamarin.Forms;

namespace Jaktloggen.ViewModels
{
    [ImplementPropertyChanged]
    public class DogListVM
    {
        public ObservableRangeCollection<Dog> ItemCollection { get; set; }
        public DogListVM()
        {
            ItemCollection = new ObservableRangeCollection<Dog>();
        }

        public void BindData()
        {
            ItemCollection.ReplaceRange(App.Database.GetDogs());
        }
    }
}
