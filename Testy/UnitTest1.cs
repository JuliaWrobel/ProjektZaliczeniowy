namespace ProjektZaliczeniowy
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class KsiazkaTesty
        {
            Ksiazka ksiazka = new Ksiazka("Testowa Ksi¹¿ka", "Autor Testowy", 50m, "01-01-2023", 10);

            //testy konstruktora
            [TestMethod]
            public void KonstruktorPoprawneDane()
            {
                Assert.AreEqual("Testowa Ksi¹¿ka", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(50m, ksiazka.CenaBazowa);
                Assert.AreEqual(10, ksiazka.Ilosc);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnaWartoœæ))]
            public void KonstruktorNieprawidlowaIlosc()
            {
                Ksiazka ksiazkaNiepoprawna = new Ksiazka("Test", "Autor", 50m, "01-01-2023", 0);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnaWartoœæ))]
            public void KonstruktorNieprawidlowaCena()
            {
                Ksiazka ksiazkaNiepoprawna = new Ksiazka("Test", "Autor", 0m, "01-01-2023", 10);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnyFormatDaty))]
            public void KonstruktorNieprawidlowyFormatDaty()
            {
                Ksiazka ksiazkaNiepoprawna = new Ksiazka("Test", "Autor", 50m, "2023-99-99", 10);
            }


            //sprawdzam czy dwie rózne ksiazki maja rozne isbn
            [TestMethod]
            public void UnikalneIsbn()
            {

                Ksiazka ksiazka1 = new Ksiazka("Test1", "Autor1", 50m, "01-01-2023", 10);
                Ksiazka ksiazka2 = new Ksiazka("Test2", "Autor2", 60m, "02-01-2023", 5);

                Assert.AreNotEqual(ksiazka1.Isbn, ksiazka2.Isbn, "ISBN powinny byæ unikalne.");
            }

            [TestMethod]
            public void TestObliczCene()
            {
                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(50m, cena);
            }

            [TestMethod]
            public void TestObliczCzasCzytania()
            {

                ksiazka.LiczbaStron = 200;

                double czasCzytania = ksiazka.ObliczCzasCzytania();

                Assert.AreEqual(400.0, czasCzytania);
            }

            [TestMethod]
            public void ToStringPoprawnieSformatowanyOpis()
            {
                ksiazka.LiczbaStron = 200;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = " \"Testowa Ksi¹¿ka\" Autor Testowy; Ilosc: 10, Liczba stron: 200\n" +
                                       "data wydania: 01-01-2023";

                Assert.AreEqual(oczekiwanyOpis, opis);
            }

            // testowanie setterow z walidacja
            [TestMethod]
            [ExpectedException(typeof(NiepoprawnaWartoœæ))]
            public void WlasciwoscIloscNieprawidlowaWartosc()
            {
                Ksiazka ksiazka = new Ksiazka();
                ksiazka.Ilosc = 0;
            }

            [TestMethod]
            public void WlasciwoscIloscPrawidlowaWartosc()
            {

                Ksiazka ksiazka = new Ksiazka();

                ksiazka.Ilosc = 5;

                Assert.AreEqual(5, ksiazka.Ilosc);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnaWartoœæ))]
            public void WlasciwoscCenaBazowaNieprawidlowaWartosc()
            {
                Ksiazka ksiazka = new Ksiazka();
                ksiazka.CenaBazowa = 0;
            }

            [TestMethod]
            public void WlasciwoscCenaBazowaPrawidlowaWartosc()
            {
                Ksiazka ksiazka = new Ksiazka();
                ksiazka.CenaBazowa = 50m;

                Assert.AreEqual(50m, ksiazka.CenaBazowa);
            }




          


            //porównywanie wg tytu³u
            [TestMethod]
            public void CompareToSortowaniePoTytule()
            {
                Ksiazka ksiazka1 = new Ksiazka("A Tytul", "Autor1", 50m, "01-01-2023", 10);
                Ksiazka ksiazka2 = new Ksiazka("B Tytul", "Autor2", 60m, "02-01-2023", 10);

                int wynik = ksiazka1.CompareTo(ksiazka2);

                Assert.IsTrue(wynik < 0);
            }


            //porównywanie po dacie w przypadku gdy mamy takie same tytu³y
            [TestMethod]
            public void CompareToSortowaniePoDacieGdyTytulyTakieSame()
            {

                Ksiazka ksiazka1 = new Ksiazka("Test Tytul", "Autor1", 50m, "01-01-2023", 10);
                Ksiazka ksiazka2 = new Ksiazka("Test Tytul", "Autor2", 60m, "02-01-2023", 10);

                int wynik = ksiazka1.CompareTo(ksiazka2);

                Assert.IsTrue(wynik < 0);
            }



        }
        [TestClass]
        public class FantastykaTesty
        {
            [TestMethod]
            public void KonstruktorPoprawneDane()
            {
                Fantastyka ksiazka = new Fantastyka("Testowa Fantastyka", "Autor Testowy", 50m, "01-01-2023", 10, "Fantasy");

                Assert.AreEqual("Testowa Fantastyka", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(50m, ksiazka.CenaBazowa);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
                Assert.AreEqual(10, ksiazka.Ilosc);
                Assert.AreEqual(Fantastyka.EnumRodzaj.Fantasy, ksiazka.rodzaj);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnyRodzaj))]
            public void KonstruktorNieprawidlowyRodzaj()
            {
                Fantastyka ksiazka = new Fantastyka("Tytul", "Autor", 50m, "01-01-2023", 10, "NieznanyGatunek");
            }

            [TestMethod]
            public void ObliczCenePoprawnaCena()
            {
                Fantastyka ksiazka = new Fantastyka("Tytul", "Autor", 50m, "01-01-2023", 10, "Horror");

                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(65m, cena);
            }

            [TestMethod]
            public void ToStringPoprawnyFormat()
            {
                Fantastyka ksiazka = new Fantastyka("Tytul", "Autor", 50m, "01-01-2023", 10, "ScienceFiction");
                ksiazka.LiczbaStron = 300;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = $"{ksiazka.Id},  \"Tytul\" Autor; Ilosc: 10, Liczba stron: 300\n" +
                                        "data wydania: 01-01-2023, Gatunek: ScienceFiction - Cena za sztukê: 65,00 z³,";
                Assert.AreEqual(oczekiwanyOpis, opis);
            }




            [TestMethod]
            public void EqualsZwracaFalseDlaRoznychObiektow()
            {
                Fantastyka ksiazka1 = new Fantastyka("Tytul", "Autor", 50m, "01-01-2023", 10, "Fantasy");
                Fantastyka ksiazka2 = new Fantastyka("Inny Tytul", "Inny Autor", 50m, "01-01-2023", 10, "Horror");

                Assert.IsFalse(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void EqualsZwracaTrueDlaIdentycznychObiektow()
            {
                Fantastyka ksiazka1 = new Fantastyka("Tytul", "Autor", 60m, "01-01-2023", 5, "Horror");
                Fantastyka ksiazka2 = (Fantastyka)ksiazka1.Clone();

                Assert.IsTrue(ksiazka1.Equals(ksiazka2));
            }


            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {
                Fantastyka ksiazka = new Fantastyka("Tytul", "Autor", 50m, "01-01-2023", 10, "Fantasy");

                Fantastyka klon = (Fantastyka)ksiazka.Clone();

                Assert.AreNotSame(ksiazka, klon);
                Assert.AreEqual(ksiazka.Tytul, klon.Tytul);
                Assert.AreEqual(ksiazka.Autor, klon.Autor);
                Assert.AreEqual(ksiazka.Ilosc, klon.Ilosc);
                Assert.AreEqual(ksiazka.rodzaj, klon.rodzaj);
                Assert.AreEqual(ksiazka.Id, klon.Id);
                Assert.AreEqual(ksiazka.ObliczCene(), klon.ObliczCene());
            }

        }

        [TestClass]
        public class KryminalTesty
        {
            [TestMethod]
            public void KonstruktorPoprawneDaneKryminal()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "Detektywistyczny");

                Assert.AreEqual($"{ksiazka.Isbn}-K", ksiazka.Id);
                Assert.AreEqual("Testowy Krymina³", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(60m, ksiazka.CenaBazowa);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
                Assert.AreEqual(5, ksiazka.Ilosc);
                Assert.AreEqual(Kryminal.EnumRodzaj.Detektywistyczny, ksiazka.rodzaj);
            }


            [TestMethod]
            [ExpectedException(typeof(NiepoprawnyRodzaj))]
            public void KonstruktorNieprawidlowyRodzaj()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "NieznanyRodzaj");
            }

            [TestMethod]
            public void ObliczCenePoprawnaCenaZDodatkiem()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "Sensacyjny");

                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(75m, cena);
            }


            [TestMethod]
            public void ToStringPoprawnyFormat()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "Polityczny");
                ksiazka.LiczbaStron = 300;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = $"{ksiazka.Id},  \"Testowy Krymina³\" Autor Testowy; Ilosc: 5, Liczba stron: 300\n" +
                        "data wydania: 01-01-2023, Gatunek: Polityczny - Cena za sztukê: 75,00 z³,";
                Assert.AreEqual(oczekiwanyOpis, opis);
            }


            [TestMethod]
            public void ObliczCzasCzytaniaPodwojnyCzasCzytania()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "Psychologiczny");
                ksiazka.LiczbaStron = 200;


                double czasCzytania = ksiazka.ObliczCzasCzytania();


                Assert.AreEqual(800.0, czasCzytania);
            }

            [TestMethod]
            public void EqualsZwracaFalseDlaRoznychObiektow()
            {
                Kryminal ksiazka1 = new Kryminal("Tytul", "Autor", 50m, "01-01-2023", 10, "Detektywistyczny");
                Kryminal ksiazka2 = new Kryminal("Inny Tytul", "Inny Autor", 50m, "01-01-2023", 10, "Sensacyjny");

                Assert.IsFalse(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void EqualsZwracaTrueDlaIdentycznychObiektow()
            {
                Kryminal ksiazka1 = new Kryminal("Tytul", "Autor", 60m, "01-01-2023", 5, "Detektywistyczny");
                Kryminal ksiazka2 = (Kryminal)ksiazka1.Clone();

                Assert.IsTrue(ksiazka1.Equals(ksiazka2));
            }


            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {
                Kryminal ksiazka = new Kryminal("Testowy Krymina³", "Autor Testowy", 60m, "01-01-2023", 5, "Detektywistyczny");

                Kryminal klon = (Kryminal)ksiazka.Clone();

                Assert.AreNotSame(ksiazka, klon);
                Assert.AreEqual(ksiazka.Tytul, klon.Tytul);
                Assert.AreEqual(ksiazka.Autor, klon.Autor);
                Assert.AreEqual(ksiazka.CenaBazowa, klon.CenaBazowa);
                Assert.AreEqual(ksiazka.Ilosc, klon.Ilosc);
                Assert.AreEqual(ksiazka.rodzaj, klon.rodzaj);
                Assert.AreEqual(ksiazka.ObliczCene(), klon.ObliczCene());
            }


        }

        [TestClass]
        public class RomansTesty
        {
            [TestMethod]
            public void KonstruktorPoprawneDaneRomans()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "Wspó³czesny");

                Assert.AreEqual("Testowy Romans", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(50m, ksiazka.CenaBazowa);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
                Assert.AreEqual(5, ksiazka.Ilosc);
                Assert.AreEqual(Romans.EnumRodzaj.Wspó³czesny, ksiazka.rodzaj);
                Assert.AreEqual($"{ksiazka.Isbn}-R", ksiazka.Id);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnyRodzaj))]
            public void KonstruktorNieprawidlowyRodzaj()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "NieznanyRodzaj");
            }

            [TestMethod]
            public void ObliczCenePoprawnaCenaZDodatkiem()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "Historyczny");

                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(60m, cena);
            }

            [TestMethod]
            public void ObliczCzasCzytaniaRomans()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "Tragiczny");
                ksiazka.LiczbaStron = 200;

                double czasCzytania = ksiazka.ObliczCzasCzytania();

                Assert.AreEqual(600.0, czasCzytania);
            }

            [TestMethod]
            public void ToStringPoprawnyFormat()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "M³odzie¿owy");
                ksiazka.LiczbaStron = 300;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = $"{ksiazka.Id},  \"Testowy Romans\" Autor Testowy; Ilosc: 5, Liczba stron: 300\n" +
                                        "data wydania: 01-01-2023, Gatunek: M³odzie¿owy - Cena za sztukê: 60,00 z³,";
                Assert.AreEqual(oczekiwanyOpis, opis);
            }

            [TestMethod]
            public void EqualsZwracaFalseDlaRoznychObiektow()
            {
                Romans ksiazka1 = new Romans("Romans 1", "Autor 1", 50m, "01-01-2023", 5, "Historyczny");
                Romans ksiazka2 = new Romans("Romans 2", "Autor 2", 50m, "01-01-2023", 5, "Wspó³czesny");

                Assert.IsFalse(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void EqualsZwracaTrueDlaIdentycznychObiektow()
            {
                Romans ksiazka1 = new Romans("Romans", "Autor", 50m, "01-01-2023", 5, "Historyczny");
                Romans ksiazka2 = (Romans)ksiazka1.Clone();

                Assert.IsTrue(ksiazka1.Equals(ksiazka2));
            }


            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {
                Romans ksiazka = new Romans("Testowy Romans", "Autor Testowy", 50m, "01-01-2023", 5, "Tragiczny");

                Romans klon = (Romans)ksiazka.Clone();

                Assert.AreNotSame(ksiazka, klon);
                Assert.AreEqual(ksiazka.Tytul, klon.Tytul);
                Assert.AreEqual(ksiazka.Autor, klon.Autor);
                Assert.AreEqual(ksiazka.CenaBazowa, klon.CenaBazowa);
                Assert.AreEqual(ksiazka.Ilosc, klon.Ilosc);
                Assert.AreEqual(ksiazka.rodzaj, klon.rodzaj);
                Assert.AreEqual(ksiazka.ObliczCene(), klon.ObliczCene());
            }


        }

        [TestClass]
        public class LiteraturaFaktuTesty
        {
            [TestMethod]
            public void KonstruktorPoprawneDaneLiteraturaFaktu()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "Reporta¿");

                Assert.AreEqual("Testowa Literatura", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(70m, ksiazka.CenaBazowa);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
                Assert.AreEqual(3, ksiazka.Ilosc);
                Assert.AreEqual(LiteraturaFaktu.EnumRodzaj.Reporta¿, ksiazka.rodzaj);
                Assert.AreEqual($"{ksiazka.Isbn}-F", ksiazka.Id);
            }

            [TestMethod]
            [ExpectedException(typeof(NiepoprawnyRodzaj))]
            public void KonstruktorNieprawidlowyRodzaj()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "NieznanyRodzaj");
            }


            [TestMethod]
            public void ObliczCenePoprawnaCenaZDodatkiem()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "Biografia");

                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(100m, cena);
            }


            [TestMethod]
            public void ObliczCzasCzytaniaLF()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "Pamiêtnik");
                ksiazka.LiczbaStron = 100;

                double czasCzytania = ksiazka.ObliczCzasCzytania();

                Assert.AreEqual(500.0, czasCzytania);
            }

            [TestMethod]
            public void ToStringPoprawnyFormat()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "Reporta¿");
                ksiazka.LiczbaStron = 200;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = $"{ksiazka.Id},  \"Testowa Literatura\" Autor Testowy; Ilosc: 3, Liczba stron: 200\n" +
                                        "data wydania: 01-01-2023, Gatunek: Reporta¿ - Cena za sztukê: 100,00 z³,";
                Assert.AreEqual(oczekiwanyOpis, opis);
            }


            [TestMethod]
            public void EqualsZwracaFalseDlaRoznychObiektow()
            {
                LiteraturaFaktu ksiazka1 = new LiteraturaFaktu("Literatura 1", "Autor 1", 70m, "01-01-2023", 3, "Biografia");
                LiteraturaFaktu ksiazka2 = new LiteraturaFaktu("Literatura 2", "Autor 2", 70m, "01-01-2023", 3, "Pamiêtnik");

                Assert.IsFalse(ksiazka1.Equals(ksiazka2));
            }


            [TestMethod]
            public void EqualsZwracaTrueDlaIdentycznychObiektow()
            {
                LiteraturaFaktu ksiazka1 = new LiteraturaFaktu("Literatura", "Autor", 70m, "01-01-2023", 3, "Reporta¿");
                LiteraturaFaktu ksiazka2 = (LiteraturaFaktu)ksiazka1.Clone();

                Assert.IsTrue(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {
                LiteraturaFaktu ksiazka = new LiteraturaFaktu("Testowa Literatura", "Autor Testowy", 70m, "01-01-2023", 3, "Pamiêtnik");

                LiteraturaFaktu klon = (LiteraturaFaktu)ksiazka.Clone();

                Assert.AreNotSame(ksiazka, klon);
                Assert.AreEqual(ksiazka.Tytul, klon.Tytul);
                Assert.AreEqual(ksiazka.Autor, klon.Autor);
                Assert.AreEqual(ksiazka.CenaBazowa, klon.CenaBazowa);
                Assert.AreEqual(ksiazka.Ilosc, klon.Ilosc);
                Assert.AreEqual(ksiazka.rodzaj, klon.rodzaj);
                Assert.AreEqual(ksiazka.ObliczCene(), klon.ObliczCene());
            }


        }


        [TestClass]
        public class PodrecznikTesty
        {
            [TestMethod]
            public void KonstruktorPoprawneDanePodrecznik()
            {

                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Matematyka", "Liceum");

                Assert.AreEqual($"{ksiazka.Isbn}-PODR", ksiazka.Id);
                Assert.AreEqual("Testowy Podrecznik", ksiazka.Tytul);
                Assert.AreEqual("Autor Testowy", ksiazka.Autor);
                Assert.AreEqual(100m, ksiazka.CenaBazowa);
                Assert.AreEqual(new DateTime(2023, 1, 1), ksiazka.DataPublikacji);
                Assert.AreEqual(5, ksiazka.Ilosc);
                Assert.AreEqual(EnumPrzedmiot.Matematyka, ksiazka.Przedmiot);
                Assert.AreEqual(EnumKlasa.Liceum, ksiazka.Klasa);
            }



            [TestMethod]

            [ExpectedException(typeof(ArgumentException))]
            public void KonstruktorNieprawidlowyPrzedmiot()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Historia", "Liceum");
            }

            [TestMethod]

            [ExpectedException(typeof(ArgumentException))]
            public void KonstruktorNieprawidlowaKlasa()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Matematyka", "Technikum");
            }

            [TestMethod]
            public void ObliczCenePoprawnaCenaZDodatkiem()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Fizyka", "Studia");

                decimal cena = ksiazka.ObliczCene();

                Assert.AreEqual(140m, cena);
            }

            [TestMethod]
            public void ObliczCzasCzytaniaPodrecznik()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Informatyka", "Podstawowka");
                ksiazka.LiczbaStron = 200;

                double czasCzytania = ksiazka.ObliczCzasCzytania();

                Assert.AreEqual(1200.0, czasCzytania);
            }

            [TestMethod]
            public void ToStringPoprawnyFormat()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Chemia", "Studia");
                ksiazka.LiczbaStron = 300;

                string opis = ksiazka.ToString();

                string oczekiwanyOpis = $"{ksiazka.Id},  \"Testowy Podrecznik\" Autor Testowy; Ilosc: 5, Liczba stron: 300\n" +
                                        "data wydania: 01-01-2023, Przedmiot: Chemia, Poziom nauki: Studia - Cena za sztukê: 140,00 z³,";
                Assert.AreEqual(oczekiwanyOpis, opis);
            }

            [TestMethod]
            public void EqualsZwracaFalseDlaRoznychObiektow()
            {
                Podrecznik ksiazka1 = new Podrecznik("Podrecznik 1", "Autor 1", 100m, "01-01-2023", 5, "Matematyka", "Liceum");
                Podrecznik ksiazka2 = new Podrecznik("Podrecznik 2", "Autor 2", 100m, "01-01-2023", 5, "Chemia", "Studia");

                Assert.IsFalse(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void EqualsZwracaTrueDlaIdentycznychObiektow()
            {
                Podrecznik ksiazka1 = new Podrecznik("Podrecznik", "Autor", 100m, "01-01-2023", 5, "Fizyka", "Studia");
                Podrecznik ksiazka2 = (Podrecznik)ksiazka1.Clone();

                Assert.IsTrue(ksiazka1.Equals(ksiazka2));
            }

            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {
                Podrecznik ksiazka = new Podrecznik("Testowy Podrecznik", "Autor Testowy", 100m, "01-01-2023", 5, "Matematyka", "Liceum");

                Podrecznik klon = (Podrecznik)ksiazka.Clone();

                Assert.AreNotSame(ksiazka, klon);
                Assert.AreEqual(ksiazka.Tytul, klon.Tytul);
                Assert.AreEqual(ksiazka.Autor, klon.Autor);
                Assert.AreEqual(ksiazka.CenaBazowa, klon.CenaBazowa);
                Assert.AreEqual(ksiazka.Ilosc, klon.Ilosc);
                Assert.AreEqual(ksiazka.Przedmiot, klon.Przedmiot);
                Assert.AreEqual(ksiazka.Klasa, klon.Klasa);
                Assert.AreEqual(ksiazka.ObliczCene(), klon.ObliczCene());
            }


        }

        [TestClass]

        public class MagazynTesty
        {
            [TestMethod]
            public void KonstruktorPoprawnieInicjalizujeMagazyn()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");

                Assert.AreEqual("Testowy Magazyn", magazyn.nazwa);
                Assert.AreEqual(0, magazyn.ksiazki.Count);
            }

            [TestMethod]
            public void ObliczWartoscPoprawnaSumaWartosci()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 2);
                Ksiazka ksiazka2 = new Ksiazka("Ksiazka 2", "Autor 2", 30m, "01-01-2023", 3);

                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                decimal wartosc = magazyn.ObliczWartosc();

                Assert.AreEqual(190m, wartosc);
            }

            [TestMethod]
            public void DodajDoMagazynuDodajeKsiazke()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 2);

                magazyn.DodajDoMagazynu(ksiazka);

                Assert.AreEqual(1, magazyn.ksiazki.Count);
                Assert.AreSame(ksiazka, magazyn.ksiazki[0]);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void DodajDoMagazynuWyjatekDlaDuplikatu()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 2);

                magazyn.DodajDoMagazynu(ksiazka);
                magazyn.DodajDoMagazynu(ksiazka);
            }

            [TestMethod]
            public void DostawaZwiekszaIloscKsiazek()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 2);
                magazyn.DodajDoMagazynu(ksiazka);

                magazyn.Dostawa(ksiazka.Isbn, 3);

                Assert.AreEqual(5, ksiazka.Ilosc);
            }

            [TestMethod]
            [ExpectedException(typeof(NieznalezionaKsiazka))]
            public void DostawaWyjatekDlaNieznanejKsiazki()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                magazyn.Dostawa(9999999999999, 3);
            }

            [TestMethod]
            public void SprzedajZmniejszaIloscKsiazek()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                magazyn.DodajDoMagazynu(ksiazka);

                magazyn.Sprzedaj(ksiazka.Isbn, 3);

                Assert.AreEqual(2, ksiazka.Ilosc);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void SprzedajWyjatekDlaZbytDuzejIlosci()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 2);
                magazyn.DodajDoMagazynu(ksiazka);

                magazyn.Sprzedaj(ksiazka.Isbn, 3);
            }

            [TestMethod]
            public void UsunKsiazkeUsuwanie()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                magazyn.DodajDoMagazynu(ksiazka);

                magazyn.UsunKsiazke(ksiazka.Isbn);

                Assert.AreEqual(0, magazyn.ksiazki.Count);
            }

            [TestMethod]
            [ExpectedException(typeof(NieznalezionaKsiazka))]
            public void UsunKsiazkeWyjatekDlaNieznanejKsiazki()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");

                magazyn.UsunKsiazke(9999999999999);
            }

            [TestMethod]
            public void WyszukajPoRodzajuZwracaKsiazkiOkreslonegoRodzaju()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Kryminal kryminal = new Kryminal("Kryminal 1", "Autor 1", 50m, "01-01-2023", 5, "Detektywistyczny");
                Romans romans = new Romans("Romans 1", "Autor 2", 60m, "01-01-2023", 3, "Wspó³czesny");
                magazyn.DodajDoMagazynu(kryminal);
                magazyn.DodajDoMagazynu(romans);

                IEnumerable<Ksiazka> wynik = magazyn.WyszukajPoRodzaju("Detektywistyczny");

                Assert.AreEqual(1, wynik.Count());
                Assert.AreSame(kryminal, wynik.First());
            }


            [TestMethod]
            public void WyszukajPoAutorzeZwracaKsiazkiOkreslonegoAutora()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("Ksiazka 2", "Autor 2", 60m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                IEnumerable<Ksiazka> wynik = magazyn.WyszukajPoAutorze("Autor 1");

                Assert.AreEqual(1, wynik.Count());
                Assert.AreSame(ksiazka1, wynik.First());
            }

            [TestMethod]
            public void WyszukajPoKlasieZwracaKsiazkiOkreslonejKlasy()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Podrecznik podrecznik1 = new Podrecznik("Podrecznik 1", "Autor 1", 100m, "01-01-2023", 5, "Matematyka", "Liceum");
                Podrecznik podrecznik2 = new Podrecznik("Podrecznik 2", "Autor 2", 80m, "01-01-2023", 3, "Fizyka", "Podstawowka");
                magazyn.DodajDoMagazynu(podrecznik1);
                magazyn.DodajDoMagazynu(podrecznik2);

                IEnumerable<Ksiazka> wynik = magazyn.WyszukajPoKlasie(EnumKlasa.Liceum);

                Assert.AreEqual(1, wynik.Count());
                Assert.AreSame(podrecznik1, wynik.First());
            }

            [TestMethod]
            public void WyszukajPoPrzedmiocieZwracaKsiazkiOkreslonegoPrzedmiotu()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Podrecznik podrecznik1 = new Podrecznik("Podrecznik 1", "Autor 1", 100m, "01-01-2023", 5, "Matematyka", "Liceum");
                Podrecznik podrecznik2 = new Podrecznik("Podrecznik 2", "Autor 2", 80m, "01-01-2023", 3, "Fizyka", "Podstawowka");
                magazyn.DodajDoMagazynu(podrecznik1);
                magazyn.DodajDoMagazynu(podrecznik2);

                IEnumerable<Ksiazka> wynik = magazyn.WyszukajPoPrzedmiocie(EnumPrzedmiot.Matematyka);

                Assert.AreEqual(1, wynik.Count());
                Assert.AreSame(podrecznik1, wynik.First());
            }


            [TestMethod]
            public void SortujKsiazkiPoTytuleSortujeAlfabetycznie()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("B Ksiazka", "Autor 1", 50m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("A Ksiazka", "Autor 2", 60m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                magazyn.SortujKsiazkiPoTytule();

                Assert.AreEqual("A Ksiazka", magazyn.ksiazki[0].Tytul);
                Assert.AreEqual("B Ksiazka", magazyn.ksiazki[1].Tytul);
            }


            [TestMethod]
            public void SortujKsiazkiPoCenieRosnacoSortowanie()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 60m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("Ksiazka 2", "Autor 2", 50m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                magazyn.SortujKsiazkiPoCenieRosnaco();

                Assert.AreEqual(50m, magazyn.ksiazki[0].CenaBazowa);
                Assert.AreEqual(60m, magazyn.ksiazki[1].CenaBazowa);
            }

            [TestMethod]
            public void SortujKsiazkiPoCenieMalejacoSortowanie()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("Ksiazka 2", "Autor 2", 60m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                magazyn.SortujKsiazkiPoCenieMalejaco();

                Assert.AreEqual(60m, magazyn.ksiazki[0].CenaBazowa);
                Assert.AreEqual(50m, magazyn.ksiazki[1].CenaBazowa);
            }


            [TestMethod]
            public void WyszukajPoTytuleZwracaTytuly()
            {
                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("Inna Ksiazka", "Autor 2", 60m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                IEnumerable<Ksiazka> wynik = magazyn.WyszukajPoTytule("Ksiazka");

                Assert.AreEqual(2, wynik.Count());
            }

            [TestMethod]
            public void ToStringMagazyn()
            {

                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                Ksiazka ksiazka2 = new Ksiazka("Ksiazka 2", "Autor 2", 60m, "01-01-2023", 3);
                magazyn.DodajDoMagazynu(ksiazka1);
                magazyn.DodajDoMagazynu(ksiazka2);

                ksiazka1.LiczbaStron = 184;
                ksiazka2.LiczbaStron = 446;


                string opis = magazyn.ToString();


                Assert.IsTrue(opis.Contains("Magazyn: Testowy Magazyn"));
                Assert.IsTrue(opis.Contains("Zawartoœæ magazynu:"));
                Assert.IsTrue(opis.Contains("\"Ksiazka 1\" Autor 1; Ilosc: 5, Liczba stron: 184"));
                Assert.IsTrue(opis.Contains("\"Ksiazka 2\" Autor 2; Ilosc: 3, Liczba stron: 446"));
                Assert.IsTrue(opis.Contains("data wydania: 01-01-2023"));
            }



            [TestMethod]
            public void CloneZwracaNowyObiektZTymiSamymiWartosciami()
            {

                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka1 = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                magazyn.DodajDoMagazynu(ksiazka1);

                Magazyn klonMagazynu = magazyn.Clone();

                Assert.IsNotNull(klonMagazynu);
                Assert.AreNotSame(magazyn, klonMagazynu);
                Assert.AreEqual(magazyn.nazwa, klonMagazynu.nazwa);
                Assert.AreEqual(magazyn.ksiazki.Count, klonMagazynu.ksiazki.Count);
                Assert.AreEqual(magazyn.ksiazki[0].Tytul, klonMagazynu.ksiazki[0].Tytul);
            }

            [TestMethod]
            public void SerializacjaIZapisPoprawnosc()
            {

                Magazyn magazyn = new Magazyn("Testowy Magazyn");
                Ksiazka ksiazka = new Ksiazka("Ksiazka 1", "Autor 1", 50m, "01-01-2023", 5);
                magazyn.DodajDoMagazynu(ksiazka);


                magazyn.ZapiszDCXML("magazyn.xml");
                Magazyn? odczytanyMagazyn = Magazyn.OdczytajDCXML("magazyn.xml");


                Assert.IsNotNull(odczytanyMagazyn);
                Assert.AreEqual(magazyn.nazwa, odczytanyMagazyn!.nazwa);
                Assert.AreEqual(magazyn.ksiazki.Count, odczytanyMagazyn.ksiazki.Count);
            }






        }
    }
}