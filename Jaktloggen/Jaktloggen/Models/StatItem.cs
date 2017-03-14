using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using PropertyChanged;
using Xamarin.Forms;

namespace Jaktloggen.Models
{
    [ImplementPropertyChanged]
    public class StatItem
    {
        private string _details;
        public string Title { get; set; }

        public string Details
        {
            get
            {
                if (Count > 0)
                {
                    return Count.ToString();
                }
                return _details;
            }
            set { _details = value; }
        }

        public double Count { get; set; }
        public ImageSource Image { get; set; }
        public List<StatItem> Items { get; set; }
    }
}
