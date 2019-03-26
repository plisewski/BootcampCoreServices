using System.Collections.Generic;
using BootcampCoreServices.Model;

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

            DataParser.DeserializeCsv(requests, csvFiles);
            requests = DataParser.DeserializeXml(requests, xmlFiles);            
            DataParser.DeserializeJson(requests, jsonFiles);

            return requests;
        }
    }
}
