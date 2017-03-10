using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Jaktloggen.Models
{
    [ImplementPropertyChanged]
    public class LoggTypeGroup
    {
        public int ID { get; set; }
        public string Navn { get; set; }
    }
}
