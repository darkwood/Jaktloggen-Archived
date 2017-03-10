using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class Logg : EntityBase
    {
        public int ID { get; set; }
        public int Treff { get; set; }
        public int Skudd { get; set; }
        public int Sett { get; set; }
        public DateTime Dato { get; set; } = DateTime.Now;
        public int ArtId { get; set; }
        public int JegerId { get; set; }
        public int DogId { get; set; }
        public int JaktId { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string ImagePath { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Weather { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string WeaponType { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int ButchWeight { get; set; }
        public int Tags { get; set; }

        [XmlIgnore] [JsonIgnore]
        public string Title
        {
            get
            {
                var artnavn = ArtId > 0 ? Art.Navn : "sett";
                return $"{Sett} {artnavn}, {Skudd} skudd, {Treff} treff.";

            }
        }
        [XmlIgnore] [JsonIgnore]
        public string Details
        {
            get
            {
                var details = "";
                if (Dato.Date == DateTime.Now.Date)
                {
                    details += Dato.ToString("hh:mm", new CultureInfo("nb-NO"));
                }
                else
                {
                    details += Dato.ToString("dd MMM kl. hh:mm", new CultureInfo("nb-NO"));
                }
                if (JegerId > 0)
                {
                    details += " - " + Jeger.Navn;
                }

                return details;
            }
        }

        [XmlIgnore] [JsonIgnore]
        public string DateFormatted
        {
            get
            {
                if (Dato.Date == DateTime.Now.Date)
                {
                    return Dato.ToString("hh:mm", new CultureInfo("nb-NO"));
                }
                return Dato.ToString("dd MMM kl. hh:mm", new CultureInfo("nb-NO"));

            }
        }
        
        [XmlIgnore] [JsonIgnore]
        public ImageSource Image
        {
            get
            {
                return ImageSource.FromFile(string.IsNullOrWhiteSpace(ImagePath) ? "placeholder_log.jpg" : ImagePath);
            }
        }
        [XmlIgnore] [JsonIgnore]
        public string FangstText
        {
            get
            {
                var lbl = "";

                if (Art != null)
                    lbl = Treff + " " + Art.Navn + " skutt (" + Skudd + " skudd)";
                else
                    lbl = Skudd + " skudd, " + Treff + " treff";

                if (Sett > 0)
                    lbl += ", " + Sett + " sett";

                if (Jeger.ID > 0)
                {
                    lbl += " av " + Jeger.Firstname;
                }

                return lbl;

            }
        }


        [XmlIgnore] [JsonIgnore]
        public Jeger Jeger
        {
            get
            {
                if (JegerId == 0)
                {
                    return new Jeger();
                }

                return App.Database.GetJeger(JegerId);
            }
        }

        [XmlIgnore] [JsonIgnore]
        private Art _art;
        [XmlIgnore] [JsonIgnore]
        public Art Art {
            get
            {
                _art = ArtId == 0 ? new Art() : App.Database.GetArt(ArtId);
                return _art;
            }
            set
            {
                _art = value;
                ArtId = _art.ID;
            }
        }
        [XmlIgnore] [JsonIgnore]
        public Dog Dog
        {
            get
            {
                if (DogId == 0)
                {
                    return new Dog();
                }

                return App.Database.GetDog(DogId);
            }
        }

    }
}
