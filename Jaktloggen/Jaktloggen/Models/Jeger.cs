using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PropertyChanged;
using Xamarin.Forms;

namespace Jaktloggen.Models
{
    [ImplementPropertyChanged]
    public class Jeger : EntityBase
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public bool IsMe { get; set; }
        public bool Selected { get; set; }

        [XmlIgnore] [JsonIgnore]
        public string VisningsNavn
        {
            get
            {
                if (Firstname == null && Lastname == null)
                {
                    return "Velg jeger";
                }
                return Firstname;
            }
        }
        [XmlIgnore] [JsonIgnore]
        public string Navn => Firstname + " " + Lastname;

        [XmlIgnore] [JsonIgnore]
        public ImageSource IconSource
        {
            get
            {
                var icon = Selected ? "starred.png" : "starred_not.png";
                return ImageSource.FromFile(icon);
            }
        }

        [XmlIgnore] [JsonIgnore]
        public ImageSource Image
        {
            get
            {
                return ImageSource.FromFile(string.IsNullOrWhiteSpace(ImagePath) ? "placeholder_hunter.jpg" : ImagePath);
            }
            
        }
    }
}
