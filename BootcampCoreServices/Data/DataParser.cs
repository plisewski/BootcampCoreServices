using BootcampCoreServices.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BootcampCoreServices.Data
{
    public static class DataParser
    {
        public static void DeserializeCsv(List<Request> requests, string[] csvFiles)
        {
            foreach (var item in csvFiles)
            {
                var csvContent = File.ReadAllLines(item).Skip(1);

                foreach (var item2 in csvContent)
                {

                    var values = item2.Split(',');

                    if (values.Length != 5 || values.Any(x => x.Equals("")))
                    {
                        Console.WriteLine($"W dokumencie csv {item} brakuje niektórych kolumn lub zawierają one puste wartości...");
                        continue;
                    }

                    var request = new Request
                    {
                        ClientId = values[0],
                        RequestId = long.Parse(values[1]),
                        Name = values[2],
                        Quantity = int.Parse(values[3]),
                        Price = double.Parse(values[4], CultureInfo.InvariantCulture)
                    };

                    if (request.ClientId.Contains(" "))
                        Console.WriteLine($"Pole ClientId w pliku {item} zawiera niedozwolone znaki (spacje)");
                    else if (request.ClientId.Length > 6)
                        Console.WriteLine($"Długość pola ClientId w pliku {item} przekracza dozwolony limit (6 znaków)");
                    else if (request.Name.Length > 255)
                        Console.WriteLine($"Długość pola Name w pliku {item} przekracza dozwolony limit (255 znaków)");
                    else
                        requests.Add(request);
                }
            }
        }

        public static void DeserializeXml(List<Request> requests, string[] xmlFiles)
        {
            foreach (var item in xmlFiles)
            {
                var doc = XElement.Load(item);

                foreach (var element in doc.Elements("request"))
                {
                    if (string.IsNullOrEmpty((string)element.Element("clientId")) ||
                        string.IsNullOrEmpty((string)element.Element("requestId")) ||
                        string.IsNullOrEmpty((string)element.Element("name")) ||
                        string.IsNullOrEmpty((string)element.Element("quantity")) ||
                        string.IsNullOrEmpty((string)element.Element("price")))
                    {
                        Console.WriteLine($"W dokumencie xml {item} brakuje niektórych tagów w elemencie <request> lub zawierają one puste wartości...");

                    }
                    else
                    {
                        var request = new Request
                        {
                            ClientId = element.Element("clientId").Value,
                            RequestId = long.Parse(element.Element("requestId").Value),
                            Name = element.Element("name").Value,
                            Quantity = int.Parse(element.Element("quantity").Value),
                            Price = double.Parse(element.Element("price").Value, CultureInfo.InvariantCulture)
                        };

                        if (request.ClientId.Contains(" "))
                            Console.WriteLine($"Pole ClientId w pliku {item} zawiera niedozwolone znaki (spacje)");
                        else if (request.ClientId.Length > 6)
                            Console.WriteLine($"Długość pola ClientId w pliku {item} przekracza dozwolony limit (6 znaków)");
                        else if (request.Name.Length > 255)
                            Console.WriteLine($"Długość pola Name w pliku {item} przekracza dozwolony limit (255 znaków)");
                        else
                            requests.Add(request);
                    }
                }
            }
        }

        public static void DeserializeJson(List<Request> requests, string[] jsonFiles)
        {
            foreach (var item in jsonFiles)
            {
                var requestData = JsonConvert.DeserializeObject<RequestDb>(File.ReadAllText(item));

                foreach (var item2 in requestData.Requests)
                {
                    if (string.IsNullOrEmpty(item2.ClientId) || item2.RequestId.Equals(null) ||
                        string.IsNullOrEmpty(item2.Name) || item2.Quantity.Equals(null) || item2.Price.Equals(null))
                    {
                        Console.WriteLine($"W dokumencie json {item} brakuje niektórych pól lub zawierają one puste wartości...");
                    }
                    else if (item2.ClientId.Contains(" "))
                        Console.WriteLine($"Pole ClientId w pliku {item} zawiera niedozwolone znaki (spacje)");
                    else if (item2.ClientId.Length > 6)
                        Console.WriteLine($"Długość pola ClientId w pliku {item} przekracza dozwolony limit (6 znaków)");
                    else if (item2.Name.Length > 255)
                        Console.WriteLine($"Długość pola Name w pliku {item} przekracza dozwolony limit (255 znaków)");
                    else
                        requests.Add(item2);
                }
            }
        }
    }
}