using System;
using System.IO;

namespace BootcampCoreServices.Data
{
    public static class FilesReader
    {
        public static string[] ReadCsvFiles(string path)
        {
            string csvSearchPattern = "*.csv";

            try
            {
                return Directory.GetFiles(path, csvSearchPattern, SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
        }

        public static string[] ReadXmlFiles(string path)
        {
            string xmlSearchPattern = "*.xml";

            try
            {
                return Directory.GetFiles(path, xmlSearchPattern, SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
        }

        public static string[] ReadJsonFiles(string path)
        {
            string jsonSearchPattern = "*.json";

            try
            {
                return Directory.GetFiles(path, jsonSearchPattern, SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.GetType().FullName);
                Environment.Exit(0);
                throw;
            }
        }
    }
}
