using BootcampCoreServices.Data;
using BootcampCoreServices.Model;
using BootcampCoreServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BootcampCoreServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj pełną ścieżkę do katalogu z plikami wejściowymi:");

            string path = Console.ReadLine();

            List<Request> requests = DataLoader.PopulateDbWithData(path);

            bool exitFlag = false;

            while (!exitFlag)
            {
                MainMenu();
                string userChoice = Console.ReadLine();
                string clientId;

                switch (userChoice)
                {
                    case "a":
                        Console.WriteLine($"Całkowita liczba zamówień: {ReportsGenerator.TotalNumberOfRequests(requests)}");
                        break;
                    case "b":
                        Console.Write("Podaj ID klienta: ");
                        clientId = Console.ReadLine();
                        Console.WriteLine($"Całkowita liczba zamówień dla klienta o ID = {clientId}: {ReportsGenerator.TotalNumberOfRequests(requests, clientId)}");
                        break;
                    case "c":
                        Console.WriteLine($"Łączna kwota zamówień: {ReportsGenerator.TotalValueOfRequests(requests)}");
                        break;
                    case "d":
                        Console.Write("Podaj ID klienta: ");
                        clientId = Console.ReadLine();
                        Console.WriteLine($"Łączna kwota zamówień dla klienta o ID = {clientId}: {ReportsGenerator.TotalValueOfRequests(requests, clientId)}");
                        break;
                    case "e":
                        Console.WriteLine("Lista wszystkich zamówień:");
                        ReportsGenerator.ListOfAllRequests(requests);
                        Console.WriteLine("Chcesz posortować raport? (t/n)");
                        string userSort = Console.ReadLine();
                        SortedReport_E_F(requests, userSort, false);
                        break;
                    case "f":
                        Console.Write("Podaj ID klienta: ");
                        clientId = Console.ReadLine();
                        if (requests.Count(x => x.ClientId == clientId) == 0)
                        {
                            Console.WriteLine("Brak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"Lista wszystkich zamówień dla klienta o ID = {clientId}:");
                        ReportsGenerator.ListOfAllRequests(requests, clientId);
                        Console.WriteLine("Chcesz posortować raport? (t/n)");
                        userSort = Console.ReadLine();
                        SortedReport_E_F(requests, userSort, true, clientId);
                        break;
                    case "g":
                        double averRequest = ReportsGenerator.TotalValueOfRequests(requests) / ReportsGenerator.TotalNumberOfRequests(requests);
                        Console.WriteLine($"Średnia wartość zamówienia: {averRequest.ToString("F", CultureInfo.InvariantCulture)}");
                        break;
                    case "h":
                        Console.Write("Podaj ID klienta: ");
                        clientId = Console.ReadLine();
                        double averRequestClientId = ReportsGenerator.TotalValueOfRequests(requests, clientId) / ReportsGenerator.TotalNumberOfRequests(requests, clientId);
                        if (double.IsNaN(averRequestClientId))
                        {
                            Console.WriteLine("Brak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"Średnia wartość zamówienia dla klienta o ID = {clientId}: " +
                                          $"{averRequestClientId.ToString("F", CultureInfo.InvariantCulture)}");
                        break;
                    case "i":
                        Console.WriteLine("Ilość zamówień pogrupowanych po nazwie:");
                        ReportsGenerator.RequestsAmountGroupedByName(requests);
                        Console.WriteLine("Chcesz posortować raport? (t/n)");
                        userSort = Console.ReadLine();
                        SortedReport_I_J(requests, userSort, false);
                        break;
                    case "j":
                        Console.Write("Podaj ID klienta: ");
                        clientId = Console.ReadLine();
                        if (requests.Count(x => x.ClientId == clientId) == 0)
                        {
                            Console.WriteLine("Brak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"Ilość zamówień pogrupowanych po nazwie dla klienta dla klienta o ID = {clientId}:");
                        ReportsGenerator.RequestsAmountGroupedByName(requests, clientId);
                        Console.WriteLine("Chcesz posortować raport? (t/n)");
                        userSort = Console.ReadLine();
                        SortedReport_I_J(requests, userSort, true, clientId);
                        break;
                    case "k":
                        double priceFrom;
                        double priceTo;
                        Console.Write("Podaj dolny przedział cen: ");
                        if (Double.TryParse(Console.ReadLine(), out double price1))
                            priceFrom = price1;
                        else
                        {
                            Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }
                        Console.Write("Podaj górny przedział cen: ");
                        if (Double.TryParse(Console.ReadLine(), out double price2))
                        {
                            priceTo = price2;
                            if (priceTo < priceFrom)
                            {
                                Console.WriteLine("Podano złą cenę. Górny przedział cen nie może być niższy od dolnego...");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }
                        if (requests.Count(c => c.Price >= priceFrom && c.Price <= priceTo) == 0)
                        {
                            Console.WriteLine("Brak zamówień w podanym przedziale cenowym...");
                            break;
                        }
                        Console.WriteLine($"Zamówienia w przedziale cenowym od {priceFrom} do {priceTo}:");
                        ReportsGenerator.RequestsInPriceRange(requests, priceFrom, priceTo);
                        Console.WriteLine("Chcesz posortować raport? (t/n)");
                        userSort = Console.ReadLine();
                        SortedReport_K(requests, userSort, priceFrom, priceTo);
                        break;
                    case "z":
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                        break;
                }
            }
        }

        private static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("******************************************************");
            Console.WriteLine("Wybierz raport lub wciśnij z aby zakończyć:");
            Console.WriteLine();
            Console.WriteLine("a\t - ilość zamówień " +
                              "\nb\t - ilość zamówień dla klienta o wskazanym ID" +
                              "\nc\t - łączna kwota zamówień" +
                              "\nd\t - łączna kwota zamówień dla klienta o wskazanym ID" +
                              "\ne\t - lista wszystkich zamówień" +
                              "\nf\t - lista wszystkich zamówień dla klienta o wskazanym ID" +
                              "\ng\t - średnia wartość zamówienia" +
                              "\nh\t - średnia wartość zamówienia dla klienta o wskazanym ID" +
                              "\ni\t - ilość zamówień pogrupowanych po nazwie" +
                              "\nj\t - ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym ID" +
                              "\nk\t - zamówienia w podanym przedziale cenowym");
            Console.WriteLine();
            Console.Write("Twój wybór: ");
        }

        private static void SortMenuFull()
        {
            Console.WriteLine("Wybierz rodzaj sortowania: ");
            Console.WriteLine("0 -> ID klienta" + "\n1 -> ID zamówienia" +
                              "\n2 -> Nazwa produktu" + "\n3 -> Ilość" + "\n4 -> Cena");
        }

        private static void SortMenuMini()
        {
            Console.WriteLine("Wybierz rodzaj sortowania: ");
            Console.WriteLine("0 -> Nazwa produktu" + "\n1 -> Ilość");
        }

        private static void SortedReport_E_F(IEnumerable<Request> requests, string userSort, bool cId, string clientId = "0")
        {
            switch (userSort)
            {
                case "n":
                    break;
                case "t":
                    SortMenuFull();
                    if (Int32.TryParse(Console.ReadLine(), out int sortChoice))
                    {
                        if (sortChoice != 0 && sortChoice != 1 && sortChoice != 2 && sortChoice != 3 && sortChoice != 4)
                        {
                            Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        if (cId)
                        {
                            Console.WriteLine($"Posortowana lista wszystkich zamówień dla klienta o ID = {clientId}:");
                            ReportsGenerator.ListOfAllRequests(requests, clientId, sortChoice);
                        }
                        else
                        {
                            Console.WriteLine("Posortowana lista wszystkich zamówień:");
                            ReportsGenerator.ListOfAllRequests(requests, sortChoice);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    break;
            }
        }

        private static void SortedReport_I_J(IEnumerable<Request> requests, string userSort, bool cId, string clientId = "0")
        {
            switch (userSort)
            {
                case "n":
                    break;
                case "t":
                    SortMenuMini();
                    if (Int32.TryParse(Console.ReadLine(), out int sortChoice))
                    {
                        if (sortChoice != 0 && sortChoice != 1)
                        {
                            Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        if (cId)
                        {
                            Console.WriteLine($"Posortowana ilość zamówień pogrupowanych po nazwie dla klienta o ID = {clientId}:");
                            ReportsGenerator.RequestsAmountGroupedByName(requests, clientId, sortChoice);
                        }
                        else
                        {
                            Console.WriteLine("Posortowana ilość zamówień pogrupowanych po nazwie:");
                            ReportsGenerator.RequestsAmountGroupedByName(requests, sortChoice);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    break;
            }
        }

        private static void SortedReport_K(IEnumerable<Request> requests, string userSort, double priceFrom, double priceTo)
        {
            switch (userSort)
            {
                case "n":
                    break;
                case "t":
                    SortMenuFull();
                    if (Int32.TryParse(Console.ReadLine(), out int sortChoice))
                    {
                        if (sortChoice != 0 && sortChoice != 1 && sortChoice != 2 && sortChoice != 3 && sortChoice != 4)
                        {
                            Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        Console.WriteLine("Zamówienia w przedziale cenowym od {priceFrom} do {priceTo}:");
                        ReportsGenerator.RequestsInPriceRange(requests, priceFrom, priceTo, sortChoice);
                    }
                    else
                    {
                        Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("Wprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    break;
            }
        }
    }
}
