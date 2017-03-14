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
    public class JaktVM
    {
        private int jaktId;
        public Jakt CurrentJakt { get; set; }
        public ObservableRangeCollection<Logg> ItemCollection { get; set; } = new ObservableRangeCollection<Logg>();
        public ObservableRangeCollection<Jeger> Jegere { get; set; } = new ObservableRangeCollection<Jeger>();
        public ObservableRangeCollection<Dog> Dogs { get; set; } = new ObservableRangeCollection<Dog>();
        public string LogCountLabel { get; set; } = "Ingen loggføringer";
        public bool IsLoadingPosition { get; set; }
        public bool IsNew { get; set; }
        public IEnumerable<string> AllJaktNames { get; private set; }

        public JaktVM(Jakt jakt)
        {
            if (jakt.ID == 0)
            {
                IsNew = true;
                jakt.ID = App.Database.SaveJakt(jakt);
            }
            jaktId = jakt.ID;
            CurrentJakt = jakt;
        }

        

        public void BindData()
        {
            AllJaktNames = App.Database.GetJakts().Select(j => j.Sted).Distinct().Where(s => !string.IsNullOrWhiteSpace(s));
            CurrentJakt = null;
            CurrentJakt = App.Database.GetJakt(jaktId);

            var loggs = App.Database.GetLoggs().Where(l => l.JaktId == jaktId).ToList();
            LogCountLabel = loggs.Count() + " loggføringer";

            ItemCollection.ReplaceRange(loggs);

            Jegere.ReplaceRange(from j in App.Database.GetJegere()
                                where CurrentJakt.JegerIds.Contains(j.ID)
                                select j);

            Dogs.ReplaceRange(from j in App.Database.GetDogs()
                              where CurrentJakt.DogIds.Contains(j.ID)
                              select j);
           

        }
        public void Save()
        {
            App.Database.SaveJakt(CurrentJakt);
            
        }

        public void Delete()
        {
            App.Database.DeleteJakt(CurrentJakt);
        }
        public async Task SelectPicture()
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            if (mediaFile != null)
            {
                CurrentJakt.ImagePath = mediaFile.Path;
                Save();
            }
        }

        public Logg CreateLogg()
        {
            var logg = new Logg { JaktId = CurrentJakt.ID };

            logg.Dato = DateTime.Now;

            if (CurrentJakt.JegerIds.Count == 1)
            {
                logg.JegerId = CurrentJakt.JegerIds.First();
            } else if (App.Database.GetJegere().Count() == 1)
            {
                logg.JegerId = App.Database.GetJegere().Single().ID;
            }
            return logg;
        }
    }
}
