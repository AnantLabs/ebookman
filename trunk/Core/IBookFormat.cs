using System;
using System.Drawing;

namespace EBookMan
{
    public interface IBookFormat
    {
        string Name
        {
            get;
        }

        Image Icon
        {
            get;
        }

        Guid Guid
        {
            get;
        }

        string[] SupportedExtensions
        {
            get;
        }

        bool IsSupported(string path);

        bool GetMetaData(string path, out Book book, IAsyncProcessHost progress);
    }
}
