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
                    Console.WriteLine(requests.OrderBy(x => x.ClientId).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 2:
                    Console.WriteLine(requests.OrderBy(x => x.RequestId).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 3:
                    Console.WriteLine(requests.OrderBy(x => x.Name).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 4:
                    Console.WriteLine(requests.OrderBy(x => x.Quantity).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 5:
                    Console.WriteLine(requests.OrderBy(x => x.Price).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                default:
                    Console.WriteLine(requests.ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
            }
        }

        // f
        public static void ListOfAllRequests(IEnumerable<Request> requests, string clientId, int orderBy = 0)
        {
            switch (orderBy)
            {
                case 1:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.ClientId).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 2:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.RequestId).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 3:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Name).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 4:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Quantity).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                case 5:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).OrderBy(x => x.Price).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
                default:
                    Console.WriteLine(requests.Where(c => c.ClientId == clientId).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.ClientId, a => a.RequestId, a => a.Name, a => a.Quantity, a => a.Price));
                    break;
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
                    Console.WriteLine(tmpObjList.OrderBy(x => x.Name).ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
                    break;
                case 2:
                    Console.WriteLine(tmpObjList.OrderBy(x => x.Quantity).ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
                    break;
                default:
                    Console.WriteLine(tmpObjList.ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
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
                    Console.WriteLine(tmpObjList.OrderBy(x => x.Name).ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
                    break;
                case 2:
                    Console.WriteLine(tmpObjList.OrderBy(x => x.Quantity).ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
                    break;
                default:
                    Console.WriteLine(tmpObjList.ToStringTable(
                        new[] { "Name", "Quantity" },
                        a => a.Name, a => a.Quantity));
                    break;
            }
        }

        // k
        public static void RequestsInPriceRange(IEnumerable<Request> requests, double priceFrom, double priceTo, int orderBy = 0)
        {
            switch (orderBy)
            {
                case 1:
                    Console.WriteLine(requests.OrderBy(x => x.ClientId).Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
                case 2:
                    Console.WriteLine(requests.OrderBy(x => x.RequestId).Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
                case 3:
                    Console.WriteLine(requests.OrderBy(x => x.Name).Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
                case 4:
                    Console.WriteLine(requests.OrderBy(x => x.Quantity).Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
                case 5:
                    Console.WriteLine(requests.OrderBy(x => x.Price).Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
                default:
                    Console.WriteLine(requests.Where(c => c.Price >= priceFrom && c.Price <= priceTo).GroupBy(x => new { x.ClientId, x.RequestId, x.Name, x.Quantity, x.Price }).ToStringTable(
                        new[] { "ClientId", "RequestId", "Name", "Quantity", "Price" },
                        a => a.Key.ClientId, a => a.Key.RequestId, a => a.Key.Name, a => a.Key.Quantity, a => a.Key.Price));
                    break;
            }
        }
    }
}