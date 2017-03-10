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
    public class DogSelectorGroup : ObservableRangeCollection<Dog>
    {
        public String Name { get; private set; }
        public String ShortName { get; private set; }

        public DogSelectorGroup(String Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }

        // Whatever other properties
    }

    [ImplementPropertyChanged]
    public class DogSelectorVM
    {
        public Logg CurrentLogg { get; set; }
        public Jakt CurrentJakt { get; set; }
        public ObservableRangeCollection<DogSelectorGroup> GroupedItems { get; set; }
        public DogSelectorVM(Logg currentLogg)
        {

            CurrentLogg = currentLogg;
            GroupedItems = new ObservableRangeCollection<DogSelectorGroup>();
        }

        public void BindData()
        {
            GroupedItems.Clear();
            CurrentJakt = App.Database.GetJakt(CurrentLogg.JaktId);
            var hunder = App.Database.GetDogs().ToList();
            if (CurrentLogg.DogId > 0)
            {
                hunder.Single(j => j.ID == CurrentLogg.DogId).Selected = true;
            }
            var hunderInJakt = new DogSelectorGroup("Velg en hund fra jaktlaget", "");
            hunderInJakt.AddRange(hunder.Where(j => CurrentJakt.DogIds.Contains(j.ID)));
            if (hunderInJakt.Count > 0)
            {
                GroupedItems.Add(hunderInJakt);
            }

            var otherDogs = new DogSelectorGroup("Legg til og velg annen hund", "");
            otherDogs.AddRange(hunder.Where(j => !CurrentJakt.DogIds.Contains(j.ID)));
            GroupedItems.Add(otherDogs);
        }

        public void SetDog(Dog selectedDog)
        {
            if (!CurrentJakt.DogIds.Contains(selectedDog.ID))
            {
                App.Database.AddDogToJakt(CurrentLogg.JaktId, selectedDog.ID);
            }
            CurrentLogg.DogId = selectedDog.ID;
            App.Database.SaveLogg(CurrentLogg);
        }


        public void RemoveDog()
        {
            CurrentLogg.DogId = 0;
            App.Database.SaveLogg(CurrentLogg);
        }
    }
}
