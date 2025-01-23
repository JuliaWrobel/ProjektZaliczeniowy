using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowy
{
    [DataContract]
    public class Fantastyka : Ksiazka, IEquatable<Fantastyka>, ICloneable
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
            Horror, 
            Fantasy,
            ScienceFiction
        }


        public Fantastyka(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc, string rodzaj) : base(tytul, autor, cena, dataPublikacji, ilosc)
        {

            Id = $"{Isbn}-Ft";
            dodatekDoCeny = 15;


            if (Enum.IsDefined(typeof(EnumRodzaj), rodzaj))
            {
                this.rodzaj = (EnumRodzaj)Enum.Parse(typeof(EnumRodzaj), rodzaj);
            }
            else
            {
                throw new NiepoprawnyRodzaj("Dozwolone rodzaje fantastyki: Fantasy, Horror, ScienceFiction");
            }
        }

        public override decimal ObliczCene()
        {
            decimal cena = base.ObliczCene() + dodatekDoCeny;
            return cena;
        }
        public virtual double ObliczCzasCzytania()
        {
            return base.ObliczCzasCzytania();
        }


        public override string ToString()
        {
            return $"{Id}, {base.ToString()}, Gatunek: {rodzaj} - Cena za sztukę: {ObliczCene():c},";
        }

        public bool Equals(Fantastyka? other)
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
