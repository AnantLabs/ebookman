using System;
using System.Diagnostics;

namespace EBookMan
{
    public static class Logger
    {
        public static void Error(string format, params object[] args)
        {
            Logger.Write("ERROR", format, args);
        }


        public static void Warning (string format, params object[] args)
        {
            Logger.Write("WARNING", format, args);
        }


        public static void Write (string category, string format, params object[] args)
        {
            if ( format == null )
                return;

            // TODO: implement writing to file

            Debug.WriteLine(string.Format(format, args), category);
        }

    }
}
