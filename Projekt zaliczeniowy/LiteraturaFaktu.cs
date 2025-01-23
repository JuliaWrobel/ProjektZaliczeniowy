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
    public class LiteraturaFaktu : Ksiazka, IEquatable<LiteraturaFaktu>, ICloneable
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
            Reportaż,
            Biografia,
            Pamiętnik
        }


        public LiteraturaFaktu(string tytul, string autor, decimal cena, string dataPublikacji, int ilosc, string rodzaj) : base(tytul, autor, cena, dataPublikacji, ilosc)
        {

            Id = $"{Isbn}-F";
            dodatekDoCeny = 30;


            if (Enum.IsDefined(typeof(EnumRodzaj), rodzaj))
            {
                this.rodzaj = (EnumRodzaj)Enum.Parse(typeof(EnumRodzaj), rodzaj);
            }
            else
            {
                throw new NiepoprawnyRodzaj("Dozwolone rodzaje literatury faktu: Reportaż, Biografia, Pamietnik");
            }
        }

        public override decimal ObliczCene()
        {
            decimal cena = base.ObliczCene() + dodatekDoCeny;
            return cena;
        }
        public virtual double ObliczCzasCzytania()
        {
            return base.ObliczCzasCzytania() * 2.5;
        }


        public override string ToString()
        {
            return $"{Id}, {base.ToString()}, Gatunek: {rodzaj} - Cena za sztukę: {ObliczCene():c},";
        }

        public bool Equals(LiteraturaFaktu? other)
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