using BootcampCoreServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootcampCoreServices.ViewModel
{
    public static class ReportsGenerator
    {
        // a
        public static int TotalNumberOfRequests(IEnumerable<Request> requests)
        {
            return requests.GroupBy(x => new { x.ClientId, x.RequestId }).Count();
        }

        // b
        public static int TotalNumberOfRequests(IEnumerable<Request> requests, string clientId)
        {
            return requests.Where(c => c.ClientId == clientId).GroupBy(id => id.RequestId).Count();
        }

        // c
        public static double TotalValueOfRequests(IEnumerable<Request> requests)
        {
            return (double) requests.Sum(t => t.Price * t.Quantity);
        }

        // d
        public static double TotalValueOfRequests(IEnumerable<Request> requests, string clientId)
        {
            return (double) requests.Where(c => c.ClientId == clientId).Sum(t => t.Price * t.Quantity);
        }

        // e
        public static void ListOfAllRequests(IEnumerable<Request> requests, int orderBy = 0)
        {
            switch (orderBy)
            {
                case 1:
                    PrintReport(requests.OrderBy(x => x.RequestId));
                    break;
                case 2:
                    PrintReport(requests.OrderBy(x => x.Name));
                    break;
                case 3:
                    PrintReport(requests.OrderBy(x => x.Quantity));
                    break;
                case 4:
                    PrintReport(requests.OrderBy(x => x.Price));
                    break;
                default:
                    PrintReport(requests.OrderBy(x => x.ClientId));
                    break;
            }
        }

        // f
        public static void ListOfAllRequests(IEnumerable<Request> requests, string clientId, int orderBy = 0)
        {
            switch (orderBy)
            {
                case 1:
                    PrintReport(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.RequestId));
                    break;
                case 2:
                    PrintReport(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Name));
                    break;
                case 3:
                    PrintReport(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Quantity));
                    break;
                case 4:
                    PrintReport(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Price));
                    break;
                default:
                    PrintReport(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.ClientId));
                    break;
            }
        }

        private static void PrintReport(IEnumerable<Request> requests)
        {
            Console.WriteLine("C_Id \t R_Id \t name \t Qty \t Price");
            foreach (var item in requests)
            {
                Console.WriteLine(item.ToString());
            }
        }

        // raporty g oraz h, które prezentują wartości średnie powstają na bazie stworzonych wcześniej raportów a, b, c, d

        // i
        public static void RequestsAmountGroupedByName(IEnumerable<Request> requests, int orderBy = 0)
        {
            var requestsGroupedByName = requests.GroupBy(x => new { x.ClientId, x.RequestId, x.Name }).Select(x => new { x.Key.Name }).GroupBy(x => x.Name);

            var tmpObjList = new List<Request>();

            foreach (var item in requestsGroupedByName)
            {
                var tmpObj = new Request
                {
                    Name = item.Key,
                    Quantity = item.Count()
                };
                tmpObjList.Add(tmpObj);

            }

            switch (orderBy)
            {
                case 1:
                    foreach (var item in tmpObjList.OrderBy(x => x.Quantity))
                    {
                        Console.WriteLine(item.Name + "\t" + item.Quantity);
                    }
                    break;
                default:
                    foreach (var item in tmpObjList.OrderBy(x => x.Name))
                    {
                        Console.WriteLine(item.Name + "\t" + item.Quantity);
                    }
                    break;
            }
        }

        // j
        public static void RequestsAmountGroupedByName(IEnumerable<Request> requests, string clientId, int orderBy = 0)
        {
            var requestsGroupedByName = requests.Where(c => c.ClientId == clientId).GroupBy(x => new { x.ClientId, x.RequestId, x.Name }).Select(x => new { x.Key.Name }).GroupBy(x => x.Name);

            var tmpObjList = new List<Request>();

            foreach (var item in requestsGroupedByName)
            {
                var tmpObj = new Request
                {
                    Name = item.Key,
                    Quantity = item.Count()
                };
                tmpObjList.Add(tmpObj);

            }

            switch (orderBy)
            {
                case 1:
                    foreach (var item in tmpObjList.OrderBy(x => x.Quantity))
                    {
                        Console.WriteLine(item.Name + "\t" + item.Quantity);
                    }
                    break;
                default:
                    foreach (var item in tmpObjList.OrderBy(x => x.Name))
                    {
                        Console.WriteLine(item.Name + "\t" + item.Quantity);
                    }
                    break;
            }
        }

        // k
        public static void RequestsInPriceRange(IEnumerable<Request> requests, double priceFrom, double priceTo, int orderBy = 0)
        {
            switch (orderBy)
            {
                case 1:
                    PrintReportPriceRange(requests.OrderBy(x => x.RequestId), priceFrom, priceTo);
                    break;
                case 2:
                    PrintReportPriceRange(requests.OrderBy(x => x.Name), priceFrom, priceTo);
                    break;
                case 3:
                    PrintReportPriceRange(requests.OrderBy(x => x.Quantity), priceFrom, priceTo);
                    break;
                case 4:
                    PrintReportPriceRange(requests.OrderBy(x => x.Price), priceFrom, priceTo);
                    break;
                default:
                    PrintReportPriceRange(requests.OrderBy(x => x.ClientId), priceFrom, priceTo);
                    break;
            }
        }

        private static void PrintReportPriceRange(IEnumerable<Request> requests, double priceFrom, double priceTo)
        {
            Console.WriteLine("C_Id \t R_Id \t name \t Qty \t Price");
            foreach (var item in requests.Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }))
            {
                Console.WriteLine(item.Key.ClientId + "\t " + item.Key.RequestId + "\t " + item.Key.Name + "\t " + item.Key.Quantity + "\t " + item.Key.Price);
            }
        }
    }
}