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

    public class LoggTypeGrouping : ObservableRangeCollection<LoggType>
    {
        public String Name { get; private set; }
        public String ShortName { get; private set; }

        public LoggTypeGrouping(String Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }
    }

    [ImplementPropertyChanged]
    public class LoggTypeListVM
    {
        public ObservableRangeCollection<LoggTypeGrouping> GroupedItems { get; set; }
        public LoggTypeListVM()
        {
            GroupedItems = new ObservableRangeCollection<LoggTypeGrouping>();
        }

        public void BindData()
        {
            GroupedItems = new ObservableRangeCollection<LoggTypeGrouping>();

            var loggTypeGroups = App.Database.GetLoggTypeGroups();
            var loggTyper = App.Database.GetLoggTyper();
            foreach (var g in loggTypeGroups)
            {
                var loggTyperInGroup = loggTyper.Where(a => a.GroupId == g.ID);

                if (loggTyperInGroup.Any())
                {
                    var ag = new LoggTypeGrouping(g.Navn, g.Navn.Substring(0, 3));

                    foreach (var loggType in loggTyperInGroup)
                    {
                        ag.Add(loggType);
                    }

                    GroupedItems.Add(ag);
                }
            }
        }

        public void LoggTypeSelected(LoggType loggType)
        {
            loggType.Selected = !loggType.Selected;
            App.Database.SaveLoggType(loggType);
            BindData();
        }
    }
}
