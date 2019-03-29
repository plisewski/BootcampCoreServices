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

                try
                {
                    foreach (var item2 in csvContent)
                    {
                        var values = item2.Split(',');

                        var request = new Request
                        {
                            ClientId = values[0],
                            RequestId = Int64.Parse(values[1]),
                            Name = values[2],
                            Quantity = Int32.Parse(values[3]),
                            Price = Double.Parse(values[4], CultureInfo.InvariantCulture)
                        };

                        requests.Add(request);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Plik csv {item} zawiera błędy: {e.Message}");
                    Console.WriteLine(e.GetType().FullName);
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
                        Console.WriteLine($"Niektóre elementy <request> pliku xml {item} są niekompletne...");

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
                        requests.Add(request);
                    }
                }
            }
        }

        public static void DeserializeJson(List<Request> requests, string[] jsonFiles)
        {
            foreach (var item in jsonFiles)
            {
                try
                {
                    var requestData = JsonConvert.DeserializeObject<RequestDb>(File.ReadAllText(item));

                    foreach (var item2 in requestData.Requests)
                    {
                        requests.Add(item2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Wystąpił błąd: {e.Message}");
                    Console.WriteLine(e.GetType().FullName);
                }
            }
        }
    }
}
