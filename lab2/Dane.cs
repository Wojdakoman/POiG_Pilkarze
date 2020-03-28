using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    static class Dane
    {
        public static void Save(string path, Osoba[] tab)
        {
            using(StreamWriter stream = new StreamWriter(path))
            {
                foreach (Osoba o in tab)
                    stream.WriteLine(o.ToFileString());
                stream.Close();
            }
        }

        public static Osoba[] Load(string path)
        {
            Osoba[] ret = null;
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                ret = new Osoba[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    ret[i] = new Osoba(lines[i]);
                }
            }
            return ret;
        }
    }
}
