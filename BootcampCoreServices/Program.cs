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
                        Console.WriteLine($"\nCałkowita liczba zamówień: {ReportsGenerator.TotalNumberOfRequests(requests)}");
                        break;
                    case "b":
                        Console.Write("\nPodaj ID klienta: ");
                        clientId = Console.ReadLine();
                        Console.WriteLine($"\nCałkowita liczba zamówień dla klienta o ID = {clientId}: {ReportsGenerator.TotalNumberOfRequests(requests, clientId)}");
                        break;
                    case "c":
                        Console.WriteLine($"\nŁączna kwota zamówień: {ReportsGenerator.TotalValueOfRequests(requests)}");
                        break;
                    case "d":
                        Console.Write("\nPodaj ID klienta: ");
                        clientId = Console.ReadLine();
                        Console.WriteLine($"\nŁączna kwota zamówień dla klienta o ID = {clientId}: {ReportsGenerator.TotalValueOfRequests(requests, clientId)}");
                        break;
                    case "e":
                        Console.WriteLine("\nLista wszystkich zamówień:\n");
                        ReportsGenerator.ListOfAllRequests(requests);
                        Console.Write("Chcesz posortować raport? (t/n): ");
                        string userSort = Console.ReadLine();
                        SortedReport_E_F(requests, userSort, false);
                        break;
                    case "f":
                        Console.Write("\nPodaj ID klienta: ");
                        clientId = Console.ReadLine();
                        if (requests.Count(x => x.ClientId == clientId) == 0)
                        {
                            Console.WriteLine("\nBrak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"\nLista wszystkich zamówień dla klienta o ID = {clientId}:\n");
                        ReportsGenerator.ListOfAllRequests(requests, clientId);
                        Console.Write("Chcesz posortować raport? (t/n): ");
                        userSort = Console.ReadLine();
                        SortedReport_E_F(requests, userSort, true, clientId);
                        break;
                    case "g":
                        double averRequest = ReportsGenerator.TotalValueOfRequests(requests) / ReportsGenerator.TotalNumberOfRequests(requests);
                        Console.WriteLine($"\nŚrednia wartość zamówienia: {averRequest.ToString("F", CultureInfo.InvariantCulture)}");
                        break;
                    case "h":
                        Console.Write("\nPodaj ID klienta: ");
                        clientId = Console.ReadLine();
                        double averRequestClientId = ReportsGenerator.TotalValueOfRequests(requests, clientId) / ReportsGenerator.TotalNumberOfRequests(requests, clientId);
                        if (double.IsNaN(averRequestClientId))
                        {
                            Console.WriteLine("\nBrak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"\nŚrednia wartość zamówienia dla klienta o ID = {clientId}: " +
                                          $"{averRequestClientId.ToString("F", CultureInfo.InvariantCulture)}");
                        break;
                    case "i":
                        Console.WriteLine("\nIlość zamówień pogrupowanych po nazwie:\n");
                        ReportsGenerator.RequestsAmountGroupedByName(requests);
                        Console.Write("Chcesz posortować raport? (t/n): ");
                        userSort = Console.ReadLine();
                        SortedReport_I_J(requests, userSort, false);
                        break;
                    case "j":
                        Console.Write("\nPodaj ID klienta: ");
                        clientId = Console.ReadLine();
                        if (requests.Count(x => x.ClientId == clientId) == 0)
                        {
                            Console.WriteLine("\nBrak klienta o wskazanym ID...");
                            break;
                        }
                        Console.WriteLine($"\nIlość zamówień pogrupowanych po nazwie dla klienta dla klienta o ID = {clientId}:\n");
                        ReportsGenerator.RequestsAmountGroupedByName(requests, clientId);
                        Console.Write("Chcesz posortować raport? (t/n): ");
                        userSort = Console.ReadLine();
                        SortedReport_I_J(requests, userSort, true, clientId);
                        break;
                    case "k":
                        double priceFrom;
                        double priceTo;
                        Console.Write("\nPodaj dolny przedział cen: ");
                        if (Double.TryParse(Console.ReadLine(), out double price1))
                            priceFrom = price1;
                        else
                        {
                            Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }
                        Console.Write("Podaj górny przedział cen: ");
                        if (Double.TryParse(Console.ReadLine(), out double price2))
                        {
                            priceTo = price2;
                            if (priceTo < priceFrom)
                            {
                                Console.WriteLine("\nPodano złą cenę. Górny przedział cen nie może być niższy od dolnego...");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }
                        if (requests.Count(c => c.Price >= priceFrom && c.Price <= priceTo) == 0)
                        {
                            Console.WriteLine("\nBrak zamówień w podanym przedziale cenowym...");
                            break;
                        }
                        Console.WriteLine($"\nZamówienia w przedziale cenowym od {priceFrom} do {priceTo}:\n");
                        ReportsGenerator.RequestsInPriceRange(requests, priceFrom, priceTo);
                        Console.Write("Chcesz posortować raport? (t/n): ");
                        userSort = Console.ReadLine();
                        SortedReport_K(requests, userSort, priceFrom, priceTo);
                        break;
                    case "z":
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                        break;
                }
            }
        }

        private static void MainMenu()
        {            
            Console.WriteLine("\n*****************************************************************************");
            Console.WriteLine("Wybierz raport lub wciśnij z aby zakończyć: \n");
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
                              "\nk\t - zamówienia w podanym przedziale cenowym\n");
            Console.Write("Twój wybór: ");
        }

        private static void SortMenuFull()
        {
            Console.WriteLine("\nWybierz rodzaj sortowania:\n");
            Console.WriteLine("1 -> ID klienta" + "\n2 -> ID zamówienia" +
                              "\n3 -> Nazwa produktu" + "\n4 -> Ilość" + "\n5 -> Cena\n");
            Console.Write("Twój wybór: ");
        }

        private static void SortMenuMini()
        {
            Console.WriteLine("\nWybierz rodzaj sortowania:\n");
            Console.WriteLine("1 -> Nazwa produktu" + "\n2 -> Ilość\n");
            Console.Write("Twój wybór: ");
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
                        if (sortChoice != 1 && sortChoice != 2 && sortChoice != 3 && sortChoice != 4 && sortChoice != 5)
                        {
                            Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        if (cId)
                        {
                            Console.WriteLine($"\nPosortowana lista wszystkich zamówień dla klienta o ID = {clientId}:\n");
                            ReportsGenerator.ListOfAllRequests(requests, clientId, sortChoice);
                        }
                        else
                        {
                            Console.WriteLine("\nPosortowana lista wszystkich zamówień:\n");
                            ReportsGenerator.ListOfAllRequests(requests, sortChoice);
                        }

                    }
                    else
                    {
                        Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
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
                        if (sortChoice != 1 && sortChoice != 2)
                        {
                            Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        if (cId)
                        {
                            Console.WriteLine($"\nPosortowana ilość zamówień pogrupowanych po nazwie dla klienta o ID = {clientId}:\n");
                            ReportsGenerator.RequestsAmountGroupedByName(requests, clientId, sortChoice);
                        }
                        else
                        {
                            Console.WriteLine("\nPosortowana ilość zamówień pogrupowanych po nazwie:\n");
                            ReportsGenerator.RequestsAmountGroupedByName(requests, sortChoice);
                        }

                    }
                    else
                    {
                        Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
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
                        if (sortChoice != 1 && sortChoice != 2 && sortChoice != 3 && sortChoice != 4 && sortChoice != 5)
                        {
                            Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                            break;
                        }

                        Console.WriteLine($"\nZamówienia w przedziale cenowym od {priceFrom} do {priceTo}:\n");
                        ReportsGenerator.RequestsInPriceRange(requests, priceFrom, priceTo, sortChoice);
                    }
                    else
                    {
                        Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    }
                    break;
                default:
                    Console.WriteLine("\nWprowadzono nieprawidłowy znak. Może uda się następnym razem... ;)");
                    break;
            }
        }
    }
}