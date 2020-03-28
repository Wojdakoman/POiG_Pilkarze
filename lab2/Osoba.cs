using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Osoba
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }

        public Osoba(string imie, string nazwisko, int wiek)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
        }

        public Osoba(string dane)
        {
            string[] tab = dane.Split(';');
            Imie = tab[0];
            Nazwisko = tab[1];
            Wiek = Convert.ToInt32(tab[2]);
        }

        public void Change(string imie, string nazwisko, int wiek)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
        }
        public bool SameAs(Osoba o)
        {
            if (Imie != o.Imie) return false;
            if (Nazwisko != o.Nazwisko) return false;
            if (Wiek != o.Wiek) return false;
            return true;
        }
        public string ToFileString()
        {
            return $"{Imie};{Nazwisko};{Wiek}";
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Wiek}";
        }
    }
}
