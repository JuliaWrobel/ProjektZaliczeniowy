using System;
using System.Runtime.Serialization;

namespace ProjektZaliczeniowy
{
    public class NiepoprawnaWartość : Exception
    {
        public NiepoprawnaWartość() : base("Ilość książek nie może być mniejsza lub równa zeru.") { }

        public NiepoprawnaWartość(string message) : base(message) { }
    }

    public class NiepoprawnyFormatDaty : Exception
    {
        public NiepoprawnyFormatDaty() : base("Podano nieprawidłowy format daty.") { }

        public NiepoprawnyFormatDaty(string message) : base(message) { }
    }


    [DataContract]
    [KnownType(typeof(Romans))]
    [KnownType(typeof(Kryminal))]
    [KnownType(typeof(Podrecznik))]
    [KnownType(typeof(LiteraturaFaktu))]
    [KnownType(typeof(Fantastyka))]

    public class Ksiazka : IComparable<Ksiazka> // BYŁO ABSTRACT
    {
        private static long globalIsbn; // Globalna wartość startowa
        [DataMember]
        private long isbn; // Pole instancyjne
        [DataMember]
        private string tytul;
        [DataMember]
        private string autor;
        [DataMember]
        private decimal cenaBazowa;
        [IgnoreDataMember]
        private DateTime dataPublikacji;
        [DataMember(Name = "DataPublikacji")]
        private string DataPublikacjiString { get; set; }
        [DataMember]
        private int ilosc;
        [DataMember]
        private int liczbaStron;

        static Ksiazka()
        {
            globalIsbn = 1234567890123;
        }
        public string Tytul { get => tytul; set => tytul = value; }
        public string Autor { get => autor; set => autor = value; }
        public DateTime DataPublikacji { get => dataPublikacji; set => dataPublikacji = value; }
        public long Isbn { get => isbn; set => isbn = value; }
        public int Ilosc
        {
            get => ilosc;
            set
            {
                if (value <= 0)
                {
                    throw new NiepoprawnaWartość("Ilość książek nie może być mniejsza lub równa 0.");
                }
                ilosc = value;
            }
        }
        public decimal CenaBazowa
        {
            get => cenaBazowa;
            set
            {
                if (value <= 0)
                {
                    throw new NiepoprawnaWartość("Cena bazowa nie może być mniejsza lub równa 0.");
                }
                cenaBazowa = value;
            }
        }

        public int LiczbaStron { get => liczbaStron; set => liczbaStron = value; }


        public Ksiazka()
        {
            Isbn = globalIsbn++;
            Tytul = string.Empty;
            Autor = string.Empty;
            CenaBazowa = 10;
            DataPublikacji = DateTime.Now;
            Ilosc = 1;
            Random rand = new Random();
            LiczbaStron = rand.Next(50, 500);
        }
        // Konstruktor parametryczny
        public Ksiazka(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc) : this()
        {

            Tytul = tytul;
            Autor = autor;
            CenaBazowa = cena;
            Ilosc = ilosc;

            //sprawdzenie, czy data wprowadzona zostala w odpowiednim formacie
            if (!DateTime.TryParseExact(dataPublikacji, new string[]
        { "dd-MM-yyyy", "yyyy-MM-dd", "dd-MMMM-yy", "dd/MM/yyyy", "dd.MM.yyyy", "yyyy/MM/dd", "yyyy.MM.dd" },
        null, System.Globalization.DateTimeStyles.None, out DateTime d))
            {
                throw new NiepoprawnyFormatDaty("Dozwolone formaty: dd-MM-yyyy, yyyy-MM-dd, dd-MMMM-yy, dd/MM/yyyy, dd.MM.yyyy, yyyy/MM/dd, yyyy.MM.dd");
            }

            DataPublikacji = d;
        }
        //wykorzytanie metod wirtualnych
        public virtual decimal ObliczCene()
        {

            return CenaBazowa;
        }
        public virtual double ObliczCzasCzytania()
        {
            double czasNaStrone = 2.0; // Średni czas czytania jednej strony w minutach
            return (double)(LiczbaStron * czasNaStrone);
        }


        //ToString()
        public override string ToString()
        {
            return $" \"{Tytul}\" {Autor}; Ilosc: {Ilosc}, Liczba stron: {LiczbaStron}\n" +
                $"data wydania: {DataPublikacji:dd-MM-yyyy}";
        }

        public int CompareTo(Ksiazka? other)
        {
            int cmp = Tytul.CompareTo(other.Tytul);
            if (cmp != 0)// roztrzyga kolejnosc
            {
                return cmp;
            }
            return DataPublikacji.CompareTo(other.DataPublikacji);
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            DataPublikacjiString = DataPublikacji.ToString("yyyy-MM-dd");
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            if (DateTime.TryParse(DataPublikacjiString, out DateTime parsedDate))
            {
                DataPublikacji = parsedDate;
            }
        }
    }
}