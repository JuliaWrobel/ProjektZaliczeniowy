using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowy
{
    
    public enum EnumPrzedmiot
    { Matematyka, Fizyka, Informatyka, Chemia }
    public enum EnumKlasa
    { Podstawowka, Liceum, Studia }

    [DataContract]
    public class Podrecznik : Ksiazka, IEquatable<Podrecznik>, ICloneable

    {
        [DataMember]
        private string id;
        [DataMember]
        public EnumPrzedmiot przedmiot;
        [DataMember]
        private decimal dodatekDoCeny;
        [DataMember]
        private EnumKlasa klasa;

        public EnumPrzedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public EnumKlasa Klasa { get => klasa; set => klasa = value; }
        public string Id { get => id; set => id = value; }



        public Podrecznik(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc, string przedmiot, string klasa)
         : base(tytul, autor, cena, dataPublikacji, ilosc)
        {
            Id = $"{Isbn}-PODR";
            dodatekDoCeny = 40;


            // Konwertowanie stringa na odpowiedni EnumPrzedmiot
            if (Enum.IsDefined(typeof(EnumPrzedmiot), przedmiot))
            {
                Przedmiot = (EnumPrzedmiot)Enum.Parse(typeof(EnumPrzedmiot), przedmiot);
            }
            else
            {
                throw new ArgumentException("Nieprawidłowy przedmiot [Matematyka, Fizyka, Informatyka, Chemia]");
            }

            // Konwertowanie stringa na odpowiedni EnumKlasa
            if (Enum.IsDefined(typeof(EnumKlasa), klasa))
            {
                Klasa = (EnumKlasa)Enum.Parse(typeof(EnumKlasa), klasa);
            }
            else
            {
                throw new ArgumentException("Nieprawidłowa klasa [Podstawowka, Liceum, Studia]");
            }
        }



        //uzwglednienie dodatku dla Podrecznika
        public override decimal ObliczCene()
        {
            decimal cenaPoDodatku = base.ObliczCene() + dodatekDoCeny;
            return cenaPoDodatku;
        }

        public virtual double ObliczCzasCzytania()
        {
            return base.ObliczCzasCzytania() * 3;
        }

        public override string ToString()
        {
            return $"{Id}, {base.ToString()}, Przedmiot: {Przedmiot}, Poziom nauki: {Klasa} - Cena za sztukę: {ObliczCene():c},";
        }

        public bool Equals(Podrecznik? other)
        {
            if (other == null) 
            {
                return false;
            }

            return Isbn == other.Isbn &&
                   Przedmiot == other.Przedmiot &&
                   Klasa == other.Klasa &&
                   DataPublikacji == other.DataPublikacji &&
                   Autor == other.Autor;

        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
