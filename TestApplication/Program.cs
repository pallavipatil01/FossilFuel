using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace FossilFuel
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string inputPath = ConfigurationManager.AppSettings["InputFolderPath"];
            FileSystemWatcher fileWatcher = new FileSystemWatcher(inputPath);

            //Enable events
            fileWatcher.EnableRaisingEvents = true;

            //Add event watcher
            fileWatcher.Created += FileWatcher_Changed;
  
            //var maxThreads = 4;

            //// Times to as most machines have double the logic processers as cores
            //ThreadPool.SetMaxThreads(maxThreads, maxThreads * 2);

            Console.WriteLine("Listening");
            Console.ReadLine();

        }

        //This event adds the work to the Thread queue
        private static void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {

            ThreadPool.QueueUserWorkItem((o) => ProcessFile(e));
        }

        private static void ProcessFile(FileSystemEventArgs e)
        {
            Console.WriteLine("Reading File: " + e.Name);
            XDocument xdoc = XDocument.Load(e.FullPath);

            ParseData pData = new ParseData();

            List<ClsGenaratorWind> windGenerators = new List<ClsGenaratorWind>();
            List<ClsGenaratorGas> gasGenerators = new List<ClsGenaratorGas>();
            List<ClsGenaratorCoal> coalGenerators = new List<ClsGenaratorCoal>();

            windGenerators = pData.GetWindDataList(xdoc);
            gasGenerators = pData.GetGasDataList(xdoc);
            coalGenerators = pData.GetCoalDataList(xdoc);

            List<KeyValuePair<string, double>> totalGen = pData.GetTotalGeneratorValues(windGenerators, gasGenerators, coalGenerators);
            List<ClsEmissionData> emissionGen = pData.GetEmissionGeneratorValues(gasGenerators, coalGenerators);

            bool result = pData.GenerateOutput(totalGen, emissionGen,coalGenerators);
            if (result)
                Console.WriteLine("Successsfully Created File");
            else
                Console.WriteLine("Failed Generate Output");

        }

   }


}
