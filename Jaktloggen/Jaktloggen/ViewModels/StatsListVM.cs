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
    public class StatsListVM
    {
        public ObservableRangeCollection<StatItem> ItemCollection { get; set; }
        private IEnumerable<Jeger> _jegere;
        private IEnumerable<Art> _arter;
        private IEnumerable<Logg> _loggs;

        public StatsListVM()
        {
            ItemCollection = new ObservableRangeCollection<StatItem>();
        }

        public void BindData()
        {
            ItemCollection = new ObservableRangeCollection<StatItem>();
            _jegere = App.Database.GetJegere();
            _loggs = App.Database.GetLoggs();
            _arter = App.Database.GetArter();

            ItemCollection.Add(new StatItem()
            {
                Title = "Felt vilt",
                Details = _loggs.Sum(s => s.Treff).ToString(),
                Items = GetArterHitStats()
            });
            ItemCollection.Add(new StatItem()
            {
                Title = "Observervasjoner",
                Details = _loggs.Sum(s => s.Sett).ToString(),
            });
            ItemCollection.Add(new StatItem()
            {
                Title = "Skudd",
                Details = _loggs.Sum(s => s.Skudd).ToString()
            });

            if (_jegere.Any())
            {
                var mostHitsHunter = GetJegereHitCount().First();
                var bestHunter = GetJegereHitRate().First();
                ItemCollection.Add(new StatItem()
                {
                    Title = "Beste treffprosent",
                    Details = $"{ bestHunter.Key.Navn} ({bestHunter.Value}%)",
                    Image = bestHunter.Key.Image
                });
                ItemCollection.Add(new StatItem()
                {
                    Title = "Flest treff",
                    Details = $"{mostHitsHunter.Key.Navn} ({mostHitsHunter.Value})",
                    Image = mostHitsHunter.Key.Image
                });
            }

            ItemCollection.Add(new StatItem()
            {
                Title = "Antall jaktturer",
                Details = App.Database.GetJakts().Count().ToString()
            });
            ItemCollection.Add(new StatItem()
            {
                Title = "Antall loggføringer",
                Details = _loggs.Count().ToString()
            });

            ItemCollection.Add(new StatItem()
            {
                Title = "Vis logging på kart",
                Details = ">"
            });
        }

        private Dictionary<Jeger, decimal> GetJegereHitRate()
        {
            var result = new Dictionary<Jeger, decimal>();
            foreach (var jeger in _jegere)
            {
                var mylogs = _loggs.Where(l => l.JegerId == jeger.ID);
                var shots = mylogs.Sum(m => m.Skudd);
                var hits = mylogs.Sum(m => m.Treff);
                var rate = shots > 0 ? Math.Round((decimal)hits * 100 / shots) : 0;
                result.Add(jeger, rate);
            }
            return result;
        }
        private Dictionary<Jeger, int> GetJegereHitCount()
        {
            var result = new Dictionary<Jeger, int>();
            foreach (var jeger in _jegere)
            {
                var mylogs = _loggs.Where(l => l.JegerId == jeger.ID);
                var hits = mylogs.Sum(m => m.Treff);
                result.Add(jeger, hits);
            }
            return result;
        }
        private List<StatItem> GetArterHitStats()
        {
            var result = new List<StatItem>();
            foreach (var art in _arter)
            {
                var mylogs = _loggs.Where(l => l.ArtId == art.ID);
                var hits = mylogs.Sum(m => m.Treff);
                if (hits > 0)
                {
                    result.Add(new StatItem
                    {
                        Title = art.Navn,
                        Details = hits.ToString(),
                        Image = art.Image
                    });
                }
            }

            return result;
        }
    }
}
