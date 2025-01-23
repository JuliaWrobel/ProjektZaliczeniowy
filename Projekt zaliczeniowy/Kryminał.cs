using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowy
{
    public class NiepoprawnyRodzaj : Exception
    {
        public NiepoprawnyRodzaj() : base("Nieprawidłowy rodzaj ksiązki.") { }

        public NiepoprawnyRodzaj(string message) : base(message) { }
    }

    [DataContract]
    public class Kryminal : Ksiazka, IEquatable<Kryminal>, ICloneable

    {
        [DataMember]
        private string id;
        [DataMember]
        public EnumRodzaj rodzaj;
        [DataMember]
        private decimal dodatekDoCeny;

        public string Id { get => id; set => id = value; }

        public enum EnumRodzaj
        {
            Detektywistyczny,
            Sensacyjny,
            Psychologiczny,
            Polityczny

        }

        public Kryminal(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc, string rodzaj) : base(tytul, autor, cena, dataPublikacji, ilosc)
        {
            Id = $"{Isbn}-K";
            dodatekDoCeny = 15;


            if (Enum.IsDefined(typeof(EnumRodzaj), rodzaj))
            {
                this.rodzaj = (EnumRodzaj)Enum.Parse(typeof(EnumRodzaj), rodzaj);
            }
            else
            {
                throw new NiepoprawnyRodzaj("Dozwolone rodzaje kryminałów: Detektywistyczny, Sensacyjny, Psychologiczny, Polityczny ");
            }
        }

        //uzwglednienie dodatku dla Kryminalu
        public override decimal ObliczCene()
        {
            decimal cenaPoDodatku = base.ObliczCene() + dodatekDoCeny;
            return cenaPoDodatku;
        }

        public virtual double ObliczCzasCzytania()
        {
            return base.ObliczCzasCzytania() * 2;
        }

        public override string ToString()
        {
            return $"{Id}, {base.ToString()}, Gatunek: {rodzaj} - Cena za sztukę: {ObliczCene():c},";
        }

        public bool Equals(Kryminal? other)
        {
            if (other == null)
            {
                return false;
            }

            return Isbn == other.Isbn &&
                   this.rodzaj == other.rodzaj &&
                   DataPublikacji == other.DataPublikacji &&
                   Autor == other.Autor;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

