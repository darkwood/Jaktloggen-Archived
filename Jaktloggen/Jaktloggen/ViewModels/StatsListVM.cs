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

        private List<StatItem> _settList;
        private List<StatItem> _shotList;
        private List<StatItem> _hitList;
        public StatsListVM()
        {
            _jegere = App.Database.GetJegere();
            _loggs = App.Database.GetLoggs();
            _arter = App.Database.GetArter();

            ItemCollection = new ObservableRangeCollection<StatItem>();
        }

        public void BindData()
        {
            SetStatCollections();

            ItemCollection = new ObservableRangeCollection<StatItem>();
            

            ItemCollection.Add(new StatItem()
            {
                Title = "Felt vilt",
                Details = _loggs.Sum(s => s.Treff).ToString(),
                Items = _hitList
            });
            ItemCollection.Add(new StatItem()
            {
                Title = "Observervasjoner",
                Details = _loggs.Sum(s => s.Sett).ToString(),
                Items = _settList
            });
            ItemCollection.Add(new StatItem()
            {
                Title = "Skudd",
                Details = _loggs.Sum(s => s.Skudd).ToString(),
                Items = _shotList
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
            return result.OrderByDescending(o => o.Value).ToDictionary(o => o.Key, o => o.Value);
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
            return result.OrderByDescending(o => o.Value).ToDictionary(o => o.Key, o => o.Value);
        }
        private void SetStatCollections()
        {
            _settList = new List<StatItem>();
            _shotList = new List<StatItem>();
            _hitList = new List<StatItem>();

            foreach (var art in _arter)
            {
                var mylogs = _loggs.Where(l => l.ArtId == art.ID);
                var sett = mylogs.Sum(m => m.Sett);
                var shots = mylogs.Sum(m => m.Skudd);
                var hits = mylogs.Sum(m => m.Treff);
                if (sett > 0)
                {
                    _settList.Add(new StatItem
                    {
                        Title = art.Navn,
                        Count = sett,
                        Image = art.Image
                    });
                }
                if (shots > 0)
                {
                    _shotList.Add(new StatItem
                    {
                        Title = art.Navn,
                        Count = shots,
                        Image = art.Image
                    });
                }
                if (hits > 0)
                {
                    _hitList.Add(new StatItem
                    {
                        Title = art.Navn,
                        Count = hits,
                        Image = art.Image
                    });
                }
            }
            _settList = _settList.OrderByDescending(o => o.Count).ToList();
            _shotList = _shotList.OrderByDescending(o => o.Count).ToList();
            _hitList = _hitList.OrderByDescending(o => o.Count).ToList();
        }
    }
}
