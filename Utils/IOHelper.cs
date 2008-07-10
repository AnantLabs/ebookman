using System;
using System.IO;

namespace EBookMan
{
    public static class IOHelper
    {
        public static string CompletePath(string path)
        {
            if ( string.IsNullOrEmpty(path) )
                return path;

            return ( path[ path.Length - 1 ] == Path.DirectorySeparatorChar ) ? path : path + Path.DirectorySeparatorChar;
        }


        public static string GetUniqueFileName(string path)
        {
            path = Path.GetFullPath(path);

            string folder = Path.GetDirectoryName(path);

            if ( !Directory.Exists(folder) )
                Directory.CreateDirectory(folder);

            if ( !File.Exists(path) )
                return path;

            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int counter = 1;

            do
            {
                path = string.Format("{0}\\{1}{2:D4}{3}", folder, filename, counter++, extension);
            }
            while ( File.Exists(path) );

            return path;
        }


        public static string Normalize(string path)
        {
            if ( path.LastIndexOfAny(IOHelper.illegalChars) != -1 )
            {
                foreach ( char ch in IOHelper.illegalChars )
                    path = path.Replace(ch, '_');
            }

            return path;
        }


        private static readonly char[] illegalChars = new char[] {'.',':',';','\\','/','\n','\r'};
    }
}
