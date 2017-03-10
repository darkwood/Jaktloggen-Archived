using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PropertyChanged;
using Xamarin.Forms;

namespace Jaktloggen.Models
{
    [ImplementPropertyChanged]
    public class Jakt : EntityBase
    {
        public int ID { get; set; }
        public string Sted { get; set; }
        public DateTime DatoFra { get; set; }
        public DateTime DatoTil  { get; set; }
        public List<int> JegerIds { get; set; }
        public List<int> DogIds { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImagePath { get; set; }
        public string Notes { get; set; }
        

        public Jakt()
        {
            JegerIds = new List<int>();
            DogIds = new List<int>();
            DatoFra = DateTime.Now.Date;
            DatoTil = DateTime.Now.Date;
        }
            

        [XmlIgnore] [JsonIgnore]
        public string DatoFraTil {
            get
            {
                if (DatoFra.Date == DatoTil.Date)
                {
                    return DatoFra.ToString("dd MMMM", new CultureInfo("nb-NO"));
                }

                return DatoFra.ToString("dd", new CultureInfo("nb-NO")) + " - " + DatoTil.ToString("dd MMMM", new CultureInfo("nb-NO"));
            }
        }
        [XmlIgnore] [JsonIgnore]
        public string Title
        {
            get
            {
                return string.IsNullOrWhiteSpace(Sted) ? "Sted ikke valgt" : Sted;

            }
        }
        [XmlIgnore] [JsonIgnore]
        public string Details
        {
            get { return DatoFraTil; }
        }

        [XmlIgnore] [JsonIgnore]
        public string Position
        {
            get { return string.IsNullOrWhiteSpace(Latitude) ? "Posisjon ikke satt" : Latitude + ", " + Longitude; }
        }
        
        [XmlIgnore] [JsonIgnore]
        public ImageSource Image
        {
            get
            {
                return ImageSource.FromFile(string.IsNullOrWhiteSpace(ImagePath) ? "placeholder_hunt.jpg" : ImagePath);
            }
        }

    }
}
