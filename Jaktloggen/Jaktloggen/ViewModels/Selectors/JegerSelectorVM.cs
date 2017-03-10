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
    public class JegerSelectorGroup : ObservableRangeCollection<Jeger>
    {
        public String Name { get; private set; }
        public String ShortName { get; private set; }

        public JegerSelectorGroup(String Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }

        // Whatever other properties
    }

    [ImplementPropertyChanged]
    public class JegerSelectorVM
    {
        public Logg CurrentLogg { get; set; }
        public Jakt CurrentJakt { get; set; }
        public ObservableRangeCollection<JegerSelectorGroup> GroupedItems { get; set; }
        public List<Jeger> Jegere = new List<Jeger>();
        public JegerSelectorVM(Logg currentLogg)
        {

            CurrentLogg = currentLogg;
            GroupedItems = new ObservableRangeCollection<JegerSelectorGroup>();
        }

        public void BindData()
        {
            GroupedItems.Clear();
            CurrentJakt = App.Database.GetJakt(CurrentLogg.JaktId);
            Jegere = App.Database.GetJegere().ToList();
            if (CurrentLogg.JegerId > 0)
            {
                Jegere.Single(j => j.ID == CurrentLogg.JegerId).Selected = true;
            }

            var jegereInJakt = new JegerSelectorGroup("Velg en jeger fra jaktlaget", "");
            jegereInJakt.AddRange(Jegere.Where(j => CurrentJakt.JegerIds.Contains(j.ID)));
            
            if (jegereInJakt.Count > 0)
            {
                GroupedItems.Add(jegereInJakt);
            }
            

            var otherJegere = new JegerSelectorGroup("Legg til og velg annen jeger", "");
            otherJegere.AddRange(Jegere.Where(j => !CurrentJakt.JegerIds.Contains(j.ID)));
            GroupedItems.Add(otherJegere);
        }

        

        public void SetJeger(Jeger selectedJeger)
        {
            if (!CurrentJakt.JegerIds.Contains(selectedJeger.ID))
            {
                App.Database.AddJegerToJakt(CurrentLogg.JaktId, selectedJeger.ID);
            }
            CurrentLogg.JegerId = selectedJeger.ID;
            App.Database.SaveLogg(CurrentLogg);
        }


        public void RemoveJeger()
        {
            CurrentLogg.JegerId = 0;
            App.Database.SaveLogg(CurrentLogg);
        }
    }
}
