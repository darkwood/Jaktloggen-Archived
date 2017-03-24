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

namespace Jaktloggen.ViewModels.Stats
{
    [ImplementPropertyChanged]
    public class StatsDetailsVM
    {
        public StatItem StatItem { get; set; }
        public ObservableRangeCollection<StatItem> ItemCollection { get; set; }
        public DateTime DateFrom { get; set; } = DateTime.Now.AddYears(-1);
        public DateTime DateTo { get; set; } = DateTime.Now;

        public StatsDetailsVM(StatItem statItem)
        {
            StatItem = statItem;
            ItemCollection = new ObservableRangeCollection<StatItem>();
            if (statItem.Items != null)
            {
                ItemCollection.AddRange(statItem.Items);
            }
        }
    }
}
