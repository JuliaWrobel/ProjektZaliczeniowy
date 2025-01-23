using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ProjektZaliczeniowy
{
    public class NieznalezionaKsiazka : Exception
    {
        public NieznalezionaKsiazka() : base("Nie znaleziono książki w magazynie.") { }

        public NieznalezionaKsiazka(string message) : base(message) { }
    }

    [DataContract]
    public class Magazyn 
    {
        [DataMember]
        public string nazwa; 
        [DataMember]
        public List<Ksiazka> ksiazki; 

        public List<Ksiazka> Ksiazki { get => ksiazki; set => ksiazki = value; } 


       
        public Magazyn(string nazwa)
        {
            this.nazwa = nazwa;
            ksiazki = new List<Ksiazka>();
        }

        public Magazyn()
        {
            Ksiazki = new List<Ksiazka>();
        }

       
     
        public decimal ObliczWartosc()
        {
            decimal wartosc = 0;
            foreach (Ksiazka ksiazka in ksiazki)
            {
                wartosc += ksiazka.ObliczCene() * ksiazka.Ilosc;
            }
            return wartosc;
        }

        public void DodajDoMagazynu(Ksiazka ksiazka) 
        {
            Ksiazka istnieje = ksiazki.FirstOrDefault(k => k.Isbn == ksiazka.Isbn);
            if (istnieje != null) 
            {
                throw new ArgumentException("Ta książka jest już w magazynie.");
            }
            else
            {
                ksiazki.Add(ksiazka);
            }
        }

        public void Dostawa(long isbn, int ilosc)
        {
            if (ilosc <= 0)
            {
                throw new ArgumentException("Ilość musi być większa niż 0.");
            }
            Ksiazka ksiazka = ksiazki.FirstOrDefault(k => k.Isbn == isbn);
            if (ksiazka == null)
            {
                throw new NieznalezionaKsiazka();
            }
            ksiazka.Ilosc += ilosc;
        }

        public void Sprzedaj(long isbn, int ilosc)
        {
            if (ilosc <= 0)
            {
                throw new ArgumentException("Ilość musi być większa niż 0.");
            }

            Ksiazka ksiazka = ksiazki.FirstOrDefault(k => k.Isbn == isbn);
            if (ksiazka == null)
            {
                throw new NieznalezionaKsiazka();
            }
            if (ilosc > ksiazka.Ilosc)
            {
                throw new InvalidOperationException("Nie ma wystarczającej liczby egzemplarzy w magazynie.");
            }
            ksiazka.Ilosc -= ilosc;
        }

        public void UsunKsiazke(long isbn)
        {
            Ksiazka ksiazka = ksiazki.FirstOrDefault(k => k.Isbn == isbn);
            if (ksiazka == null)
            {
                throw new NieznalezionaKsiazka();
            }
            ksiazki.Remove(ksiazka);
        }



        public IEnumerable<Ksiazka> WyszukajPoRodzaju(string rodzaj)
        {
            List<Ksiazka> wynik = new List<Ksiazka>();
            foreach (Ksiazka ksiazka in ksiazki)
            {
                if ((ksiazka is Romans romans && romans.ToString().Contains(rodzaj)) ||
                    (ksiazka is Kryminal kryminal && kryminal.ToString().Contains(rodzaj)) ||
                    (ksiazka is LiteraturaFaktu literaturaFaktu && literaturaFaktu.ToString().Contains(rodzaj)) ||
                    (ksiazka is Fantastyka fantastyka && fantastyka.ToString().Contains(rodzaj)))
                {
                    wynik.Add(ksiazka);
                }
            }
            return wynik;
        }

        public IEnumerable<Ksiazka> WyszukajPoAutorze(string autor)
        {
            return ksiazki.Where(k => k.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase));
        }
       
        public IEnumerable<Ksiazka> WyszukajPoKlasie(EnumKlasa klasa)
        {
            List<Ksiazka> wynik = new List<Ksiazka>();
            foreach (Podrecznik podrecznik in ksiazki.OfType<Podrecznik>())
            {
                if (podrecznik.Klasa == klasa)
                {
                    wynik.Add(podrecznik);
                }
            }
            return wynik;
        }

        public IEnumerable<Ksiazka> WyszukajPoPrzedmiocie(EnumPrzedmiot przedmiot)
        {
            List<Ksiazka> wynik = new List<Ksiazka>();
            foreach (Podrecznik podrecznik in ksiazki.OfType<Podrecznik>())
            {
                if (podrecznik.Przedmiot == przedmiot)
                {
                    wynik.Add(podrecznik);
                }
            }
            return wynik;
        }


        public void SortujKsiazkiPoTytule()
        {
            ksiazki.Sort();
        }

        public void SortujKsiazkiPoCenieRosnaco()
        {
            ksiazki.Sort((k1, k2) => k1.ObliczCene().CompareTo(k2.ObliczCene()));
        }

        public void SortujKsiazkiPoCenieMalejaco()
        {
            ksiazki.Sort((k1, k2) => k2.ObliczCene().CompareTo(k1.ObliczCene()));
        }

        public IEnumerable<Ksiazka> WyszukajPoTytule(string tytul)
        {
            return ksiazki.Where(k => k.Tytul.Contains(tytul, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Magazyn: {nazwa}");
            sb.AppendLine("Zawartość magazynu:");
            foreach (Ksiazka ksiazka in ksiazki)
            {
                sb.AppendLine(ksiazka.ToString());
            }
            return sb.ToString();
        }
        public Magazyn? Clone()
        {
            MemoryStream ms = new();
            DataContractSerializer dcs = new(typeof(Magazyn));
            dcs.WriteObject(ms, this);
            ms.Position = 0; // powrót na początek strumienia
            return (Magazyn?)dcs.ReadObject(ms);
        }

        public void ZapiszDCXML(string nazwa)
        {
            DataContractSerializer dsc = new(typeof(Magazyn));
            using XmlTextWriter writer = new("magazyn.xml", Encoding.UTF8);
            dsc.WriteObject(writer, this);
        }

        public static Magazyn? OdczytajDCXML(string nazwa)
        {
            if (!File.Exists(nazwa)) { return null; }
            DataContractSerializer dsc = new(typeof(Magazyn));
            using XmlTextReader reader = new("magazyn.xml");
            return (Magazyn?)dsc.ReadObject(reader);
        }
    }
}