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
    using System.IO;

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
        public int JaktId { get; set; }
        public List<int> DogIds { get; set; }
        public Logg CurrentLogg { get; set; }
        public ObservableRangeCollection<DogSelectorGroup> GroupedItems { get; set; }
        public List<Dog> Dogs = new List<Dog>();
        public DogSelectorVM(int jaktId, List<int> dogIds, Logg currentLogg = null)
        {
            JaktId = jaktId;
            DogIds = dogIds;
            CurrentLogg = currentLogg;
            GroupedItems = new ObservableRangeCollection<DogSelectorGroup>();
        }

        public void BindData()
        {
            GroupedItems.Clear();
            Dogs = App.Database.GetDogs().ToList();
            
            foreach (var dog in Dogs)
            {
                if (CurrentLogg != null)
                {
                    dog.Selected = dog.ID == CurrentLogg.DogId;
                }
                else
                {
                    dog.Selected = DogIds.Contains(dog.ID);
                }
            }
         
            var currentDogsHeader = "Dogs fra denne jakta";
            var otherDogsHeader = "Flere dogs";
            var allDogsHeader = "Velg dog";

            var dogsInJakt = Dogs.Where(j => DogIds.Contains(j.ID));
            if (dogsInJakt.Any())
            {
                var dogsInJaktGroup = new DogSelectorGroup(currentDogsHeader, "");
                dogsInJaktGroup.AddRange(dogsInJakt);
                GroupedItems.Add(dogsInJaktGroup);
            }

            var otherDogList = Dogs.Where(j => !DogIds.Contains(j.ID));
            if (otherDogList.Any())
            {
                var otherDogs = new DogSelectorGroup(dogsInJakt.Any() ? otherDogsHeader : allDogsHeader, "");
                otherDogs.AddRange(otherDogList);
                GroupedItems.Add(otherDogs);
            }

        }

        public void AddDog(Dog selectedDog)
        {
            if (!DogIds.Contains(selectedDog.ID))
            {
                DogIds = App.Database.AddDogToJakt(JaktId, selectedDog.ID);
            }

            if (CurrentLogg != null)
            {
                CurrentLogg.DogId = selectedDog.ID;
                App.Database.SaveLogg(CurrentLogg);
            }
        }

        public void RemoveDog(Dog selectedDog)
        {
            selectedDog.Selected = false;

            if (CurrentLogg != null)
            {
                CurrentLogg.Dog = new Dog();
                App.Database.SaveLogg(CurrentLogg);
            }

            if (DogIds.Contains(selectedDog.ID))
            {
                DogIds = App.Database.RemoveDogFromJakt(JaktId, selectedDog.ID);
            }
        }

        public void UpdateDogIds(Dog selectedDog)
        {
            if (DogIds.Contains(selectedDog.ID))
            {
                RemoveDog(selectedDog);
            }
            else
            {
                AddDog(selectedDog);
            }
        }
    }
}
