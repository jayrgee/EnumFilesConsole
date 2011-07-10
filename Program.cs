using System;
using System.Collections.Generic;
using System.Configuration;
using EnumFilesConsole.Core;

namespace EnumFilesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SongTrawler st = new SongTrawler();
            string source = ConfigurationManager.AppSettings["src"];
            string rootPath = ConfigurationManager.AppSettings[source];
            st.GetSongs(rootPath);
            Console.WriteLine("Complete.");
            Console.ReadLine();
        }
    }
}
