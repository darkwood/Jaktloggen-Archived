using System;

namespace Jaktloggen.Models
{
    public interface IPosition
    {
        string Latitude { get; set; }
        string Longitude { get; set; }
    }
}