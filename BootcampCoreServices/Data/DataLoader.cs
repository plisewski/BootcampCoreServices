using BootcampCoreServices.Model;
using System;
using System.Collections.Generic;

namespace BootcampCoreServices.Data
{
    public static class DataLoader

    {
        public static List<Request> PopulateDbWithData(string path)
        {
            List<Request> requests = new List<Request>();

            string[] csvFiles = FilesReader.ReadCsvFiles(path);
            string[] xmlFiles = FilesReader.ReadXmlFiles(path);
            string[] jsonFiles = FilesReader.ReadJsonFiles(path);

            try
            {
                DataParser.DeserializeCsv(requests, csvFiles);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił problem podczas parsowania danych z plików csv " + e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
            }

            try
            {
                DataParser.DeserializeXml(requests, xmlFiles);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił problem podczas parsowania danych z plików xml " + e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
            }

            try
            {
                DataParser.DeserializeJson(requests, jsonFiles);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił problem podczas parsowania danych z plików json " + e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
            }

            return requests;
        }
    }
}