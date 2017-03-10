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

    public class ArtGrouping : ObservableRangeCollection<Art>
    {
        public String Name { get; private set; }
        public String ShortName { get; private set; }

        public ArtGrouping(String Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }
    }

    [ImplementPropertyChanged]
    public class ArtListVM
    {
        public ObservableRangeCollection<ArtGrouping> GroupedItems { get; set; }
        public ArtListVM()
        {
            GroupedItems = new ObservableRangeCollection<ArtGrouping>();
        }

        public void BindData()
        {
            GroupedItems = new ObservableRangeCollection<ArtGrouping>();

            var artGroups = App.Database.GetArtGroups();
            var arter = App.Database.GetArter();
            foreach (var g in artGroups)
            {
                var arterInGroup = arter.Where(a => a.GroupId == g.ID);

                if (arterInGroup.Any())
                {
                    var ag = new ArtGrouping(g.Navn, "");

                    foreach (var art in arterInGroup)
                    {
                        ag.Add(art);
                    }

                    GroupedItems.Add(ag);
                }
            }
        }

        public void ArtSelected(Art art)
        {
            art.Selected = !art.Selected;
            //App.Database.SaveArt(art);
            if (art.Selected)
            {
                App.Database.AddSelectedArt(art);
            }
            else
            {
                App.Database.RemoveSelectedArt(art);
            }
            BindData();
        }
    }
}
