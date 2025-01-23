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
    public class Romans : Ksiazka, IEquatable<Romans>, ICloneable
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
            Współczesny,
            Historyczny,
            Młodzieżowy,
            Tragiczny
        }


        public Romans(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc, string rodzaj) : base(tytul, autor, cena, dataPublikacji, ilosc)
        {

            Id = $"{Isbn}-R";
            dodatekDoCeny = 10;


            if (Enum.IsDefined(typeof(EnumRodzaj), rodzaj))
            {
                this.rodzaj = (EnumRodzaj)Enum.Parse(typeof(EnumRodzaj), rodzaj);
            }
            else
            {
                throw new NiepoprawnyRodzaj("Dozwolone rodzaje romansów: Współczesny, Historyczny, Młodzieżowy, Tragiczny");
            }
        }

        //uzwglednienie dodatku dla Romansu
        public override decimal ObliczCene()
        {
            decimal cena = base.ObliczCene() + dodatekDoCeny;
            return cena;
        }
        public virtual double ObliczCzasCzytania()
        {
            return base.ObliczCzasCzytania() * 1.5;
        }


        public override string ToString()
        {
            // ID wyświetlane na początku, a potem informacje o książce
            return $"{Id}, {base.ToString()}, Gatunek: {rodzaj} - Cena za sztukę: {ObliczCene():c},";
        }

        public bool Equals(Romans? other)
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
