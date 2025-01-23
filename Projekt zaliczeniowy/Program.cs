

namespace ProjektZaliczeniowy
{
    class Program
    {

        static void Main()
        {
            // Tworzenie magazynu
            Magazyn magazyn = new Magazyn("Empik");

            // Tworzenie książek 


            Kryminal k1 = new Kryminal("Zbrodnia i Kara", "Fiodor Dostojewski", 60m, "01-01-2023", 16, "Psychologiczny");

            Kryminal k2 = new Kryminal("Milczenie owiec", "Thomas Harris", 70m, "01-01-2015", 89, "Sensacyjny");

            Podrecznik p1 = new Podrecznik("Analiza Matematyczna", "Zbigniew Jankowski", 100m, "01-01-2020", 50, "Matematyka", "Studia");

            Podrecznik p2 = new Podrecznik("Fizyka dla Licealistów", "Jan Kowalski", 80m, "01-01-2024", 44, "Fizyka", "Liceum");

            LiteraturaFaktu l1 = new LiteraturaFaktu("Chłopki", "Joanna Kuciel-Frydryszak", 40m, "17-05-2023", 5, "Reportaż");

            LiteraturaFaktu l2 = new LiteraturaFaktu("Inny Świat", "Gustaw Herling-Grudziński", 20m, "01-01-1986", 3, "Biografia");

            Fantastyka f1 = new Fantastyka("Ruiny Gorlanu", "John Flanagan", 20m, "08-06-2009", 10, "Fantasy");

            Fantastyka f2 = new Fantastyka("Płonący Most", "John Flanagan", 20m, "2009-04-22", 24, "Fantasy");

            LiteraturaFaktu l5 = new LiteraturaFaktu("Steve Jobs", "Walter Isaacson", 45m, "24-10-2011", 6, "Biografia");

            LiteraturaFaktu l3 = new LiteraturaFaktu("Zimowy Monarch", "Stephen Fry", 35m, "10-12-2015", 7, "Pamiętnik");

            LiteraturaFaktu l6 = new LiteraturaFaktu("Czarnobylska Modlitwa", "Swiatłana Aleksijewicz", 38m, "15-05-1997", 8, "Reportaż");

            LiteraturaFaktu l4 = new LiteraturaFaktu("Biała Gorączka", "Jacek Hugo-Bader", 30m, "05-03-2012", 4, "Reportaż");

            Fantastyka f3 = new Fantastyka("Brisingr", "Christopher Paolini", 25m, "20-09-2008", 15, "Horror");

            Fantastyka f5 = new Fantastyka("Lśnienie", "Stephen King", 30m, "28-01-1977", 12, "Horror");

            Fantastyka f4 = new Fantastyka("Ostatnie Życzenie", "Andrzej Sapkowski", 22m, "15-10-1993", 20, "Fantasy");

            Fantastyka f6 = new Fantastyka("Diuna", "Frank Herbert", 35m, "01-08-1965", 18, "ScienceFiction");

            Fantastyka f7 = new Fantastyka("Neuromancer", "William Gibson", 28m, "01-07-1984", 9, "ScienceFiction");
            Romans r1 = new Romans("Współcześnie - Romeo i Julia", "Anna Pałka", 50m, "15-01-2023", 10, "Tragiczny");
            Romans r2 = new Romans("Duma i Pycha", "Jane Hapawe", 40m, "01-06-2024", 50, "Współczesny");
            Romans r3 = new Romans("Kocham Cię", "Martyna Kulig", 45m, "15-08-2025", 12, "Młodzieżowy");
            Romans r4 = new Romans("The Great Gatsby", "F. Scott Fitzgerald", 60m, "10-07-2023", 18, "Historyczny");
            Romans r5 = new Romans("Bridget Jones's Diary", "Helen Fielding", 40m, "10-09-2022", 35, "Tragiczny");
            Romans r6 = new Romans("The Fault in Our Stars", "John Green", 50m, "01-12-2021", 25, "Młodzieżowy");
            Romans r7 = new Romans("P.S. I Love You", "Cecelia Ahern", 55m, "14-02-2024", 30, "Historyczny");
            Romans r8 = new Romans("P&P", "Cecelia Ahern", 55m, "14-02-2024", 30, "Współczesny");

            Kryminal k3 = new Kryminal("Obama Trail", "Stieg Larsson", 85m, "01-11-2020", 45, "Polityczny");
            Kryminal k4 = new Kryminal("Sherlock Holmes: A Study in Scarlet", "Arthur Conan Doyle", 70m, "20-10-2022", 35, "Psychologiczny");
            Kryminal k5 = new Kryminal("Gone Girl", "Gillian Flynn", 75m, "25-03-2019", 60, "Polityczny");
            Kryminal k6 = new Kryminal("The Silent Patient", "Alex Michaelides", 60m, "15-01-2020", 50, "Detektywistyczny");
            Kryminal k7 = new Kryminal("Big Little Lies", "Liane Moriarty", 80m, "12-06-2021", 40, "Sensacyjny");
            Kryminal k8 = new Kryminal("Małe zbrodnie", "Liane Moriarty", 80m, "12-06-2021", 40, "Detektywistyczny");
            Podrecznik p3 = new Podrecznik("Wprowadzenie do Logiki", "Jerzy Kulesza", 90m, "01-02-2023", 60, "Matematyka", "Studia");
            Podrecznik p4 = new Podrecznik("Dodawanie", "Maria Nowak", 120m, "01-03-2024", 75, "Chemia", "Podstawowka");
            Podrecznik p5 = new Podrecznik("Prawo zachowania energii", "Tomasz Kowalczyk", 80m, "01-07-2021", 40, "Fizyka", "Studia");
            Podrecznik p6 = new Podrecznik("Odejmowanie", "Piotr Borkowski", 90m, "01-09-2022", 50, "Matematyka", "Podstawowka");
            Podrecznik p7 = new Podrecznik("Podstawy C#", "Marek Nowak", 85m, "01-11-2023", 55, "Informatyka", "Studia");
            Podrecznik p8 = new Podrecznik("Chemia Organiczna", "Krzysztof Sienkiewicz", 70m, "15-01-2023", 35, "Chemia", "Liceum");
            Podrecznik p9 = new Podrecznik("Chemia nieorganiczna", "Marek Jacek", 100m, "01-04-2024", 60, "Chemia", "Liceum");
            Podrecznik p10 = new Podrecznik("Python", "Ewa Kwiatkowska", 95m, "01-06-2023", 50, "Informatyka", "Studia");



            // Dodawanie książek do magazynu
            magazyn.DodajDoMagazynu(r1);
            magazyn.DodajDoMagazynu(r2);
            magazyn.DodajDoMagazynu(r3);
            magazyn.DodajDoMagazynu(r4);
            magazyn.DodajDoMagazynu(r5);
            magazyn.DodajDoMagazynu(r6);
            magazyn.DodajDoMagazynu(r7);
            magazyn.DodajDoMagazynu(r8);

            magazyn.DodajDoMagazynu(k1);
            magazyn.DodajDoMagazynu(k2);
            magazyn.DodajDoMagazynu(k3);
            magazyn.DodajDoMagazynu(k4);
            magazyn.DodajDoMagazynu(k5);
            magazyn.DodajDoMagazynu(k6);
            magazyn.DodajDoMagazynu(k7);
            magazyn.DodajDoMagazynu(k8);

            magazyn.DodajDoMagazynu(p1);
            magazyn.DodajDoMagazynu(p2);
            magazyn.DodajDoMagazynu(p3);
            magazyn.DodajDoMagazynu(p4);
            magazyn.DodajDoMagazynu(p5);
            magazyn.DodajDoMagazynu(p6);
            magazyn.DodajDoMagazynu(p7);
            magazyn.DodajDoMagazynu(p8);
            magazyn.DodajDoMagazynu(p9);
            magazyn.DodajDoMagazynu(p10);
            magazyn.DodajDoMagazynu(f3);
            magazyn.DodajDoMagazynu(f4);
            magazyn.DodajDoMagazynu(f5);
            magazyn.DodajDoMagazynu(f6);
            magazyn.DodajDoMagazynu(f7);
            magazyn.DodajDoMagazynu(l3);
            magazyn.DodajDoMagazynu(l4);
            magazyn.DodajDoMagazynu(l5);
            magazyn.DodajDoMagazynu(l6);

            // Wyświetlanie wszystkich książek w magazynie
            Console.WriteLine("Książki w magazynie:");
            Console.WriteLine(magazyn);

            Console.WriteLine("Posortowane książki:");

            Console.WriteLine(magazyn);

            // Testowanie innych funkcji magazynu
            Console.WriteLine("\nDostawa: zwiększenie ilości książek.");
            magazyn.Dostawa(r1.Isbn, 5); // Dodanie 5 sztuk "Romeo i Julia"
            Console.WriteLine(r1);

            Console.WriteLine("\nSprzedaż: usunięcie książek.");
            magazyn.Sprzedaj(k1.Isbn, 3); // Sprzedaż 3 sztuk "Zbrodnia i Kara"
            Console.WriteLine(k1);

            Console.WriteLine("\nWartość magazynu:");
            Console.WriteLine($"{magazyn.ObliczWartosc():C}");


         


            magazyn.ZapiszDCXML("magazyn.xml");
            Magazyn? m1 = Magazyn.OdczytajDCXML("magazyn.xml");

            Console.WriteLine("\nTestowanie zakończone.");

 


        }
    }
}
