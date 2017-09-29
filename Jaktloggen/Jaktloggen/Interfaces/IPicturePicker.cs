using System.IO;
using System.Threading.Tasks;

namespace Jaktloggen.Interfaces
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
