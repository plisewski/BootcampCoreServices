using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BootcampCoreServices.Model;

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

        public static List<Request> DeserializeXml(List<Request> requests, string[] xmlFiles)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Request>), new XmlRootAttribute("requests"));

            foreach (var item in xmlFiles)
            {
                try
                {
                    XmlReader reader = XmlReader.Create(item);
                    requests = requests.Concat((List<Request>)serializer.Deserialize(reader)).ToList();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Plik xml {item} zawiera błędy: {e.Message}");
                    Console.WriteLine(e.GetType().FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Wystąpił błąd: {e.Message}");
                    Console.WriteLine(e.GetType().FullName);
                }
            }
            return requests;
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
