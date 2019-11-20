using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace FossilFuel
{
    class ParseData
    {
        //This method processes the file
        public void ProcessFile(string FilePath)
        {
            Console.WriteLine("Reading File: " + FilePath);

            List<ClsGenaratorWind> windGenerators = new List<ClsGenaratorWind>();
            List<ClsGenaratorGas> gasGenerators = new List<ClsGenaratorGas>();
            List<ClsGenaratorCoal> coalGenerators = new List<ClsGenaratorCoal>();

            XDocument xdoc = XDocument.Load(FilePath);


            //Parse Wind Genarator Data
            var selectWindGen = from windResult in xdoc.Descendants("WindGenerator")
                                select windResult;

            foreach (var windData in selectWindGen)
            {
                ClsGenaratorWind windGenerator = new ClsGenaratorWind();
                var selectGen = from genResult in windData.Descendants("Day")
                                select genResult;

                windGenerator.FuelGenName = windData.Element("Name").Value;
                windGenerator.FuelGenaratorType = windData.Element("Location").Value;

                foreach (var genData in selectGen)
                {
                    if (windGenerator.FuelGenaratorType == "Offshore")
                    {
                        windGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorLow + windGenerator.DailyGenerationValue;
                    }
                    else if (windData.Element("Location").Value == "Onshore")
                    {
                        windGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorHigh + windGenerator.DailyGenerationValue;
                    }
                    Console.WriteLine("Total:-" + windGenerator.DailyGenerationValue);
                }

                windGenerators.Add(windGenerator);
            }


            //Parse Gas Genarator Data
            Console.WriteLine("Gas");
            var selectGasGen = from r in xdoc.Descendants("GasGenerator")
                               select r;

            foreach (var gasData in selectGasGen)
            {
                ClsGenaratorGas gasGenerator = new ClsGenaratorGas();

                var selectGen = from genResult in gasData.Descendants("Day")
                                select genResult;

                gasGenerator.FuelGenName = gasData.Element("Name").Value;

                foreach (var genData in selectGen)
                {
                    gasGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorMedium + gasGenerator.DailyGenerationValue;
                    gasGenerator.DailyEmissionsValue = double.Parse(genData.Element("Energy").Value) * double.Parse(gasData.Element("EmissionsRating").Value) * ClsFactors.EmissionFactorMedium + gasGenerator.DailyEmissionsValue;

                    Console.WriteLine("Total:-" + gasGenerator.DailyGenerationValue + "  Emission:- " + gasGenerator.DailyEmissionsValue);
                }

                gasGenerators.Add(gasGenerator);
            }


            //Parse Coal Generation Data
            Console.WriteLine("Coal");
            var selectCoalGen = from r in xdoc.Descendants("CoalGenerator")
                                select r;


            //            Daily Generation Value = Energy x Price x ValueFactor
            //Daily Emissions = Energy x EmissionRating x EmissionFactor
            //Actual Heat Rate = TotalHeatInput / ActualNetGeneration
            foreach (var coalData in selectCoalGen)
            {
                ClsGenaratorCoal coalGenerator = new ClsGenaratorCoal();

                var selectGen = from genResult in coalData.Descendants("Day")
                                select genResult;

                coalGenerator.FuelGenName = coalData.Element("Name").Value;

                coalGenerator.ActualHeatRate = double.Parse(coalData.Element("TotalHeatInput").Value) / double.Parse(coalData.Element("ActualNetGeneration").Value);

                foreach (var genData in selectGen)
                {
                    coalGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorMedium + coalGenerator.DailyGenerationValue;
                    coalGenerator.DailyEmissionsValue = double.Parse(genData.Element("Energy").Value) * double.Parse(coalData.Element("EmissionsRating").Value) * ClsFactors.EmissionFactorHigh + coalGenerator.DailyEmissionsValue;

                    Console.WriteLine("Total:-" + coalGenerator.DailyGenerationValue + "  Emission:- " + coalGenerator.DailyEmissionsValue + "  Actual Heat Rate:- " + coalGenerator.ActualHeatRate);
                }

                coalGenerators.Add(coalGenerator);
            }

            GetTotalGeneratorValues(windGenerators, gasGenerators, coalGenerators);
        }

        // Get Parsed Wind Generator Data 
        public List<ClsGenaratorWind> GetWindDataList(XDocument xdoc)
        {
            List<ClsGenaratorWind> windGenerators = new List<ClsGenaratorWind>();

            //Parse Wind Genarator Data
            var selectWindGen = from windResult in xdoc.Descendants("WindGenerator")
                                select windResult;

            foreach (var windData in selectWindGen)
            {
                ClsGenaratorWind windGenerator = new ClsGenaratorWind();
                var selectGen = from genResult in windData.Descendants("Day")
                                select genResult;

                windGenerator.FuelGenName = windData.Element("Name").Value;
                windGenerator.FuelGenaratorType = windData.Element("Location").Value;

                //Daily Generation Value = Energy x Price x ValueFactor
                foreach (var genData in selectGen)
                {
                    if (windGenerator.FuelGenaratorType == "Offshore")
                    {
                        windGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorLow + windGenerator.DailyGenerationValue;
                    }
                    else if (windData.Element("Location").Value == "Onshore")
                    {
                        windGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorHigh + windGenerator.DailyGenerationValue;
                    }
                    windGenerator.Date = DateTime.Parse(genData.Element("Date").Value).ToUniversalTime();

                    Console.WriteLine("Total:-" + windGenerator.DailyGenerationValue);
                }

                windGenerators.Add(windGenerator);
            }
            return windGenerators;
        }

        // Get Parsed Gas Generator Data 
        public List<ClsGenaratorGas> GetGasDataList(XDocument xdoc)
        {
            List<ClsGenaratorGas> gasGenerators = new List<ClsGenaratorGas>();

            //Parse Gas Genarator Data
            Console.WriteLine("Gas");
            var selectGasGen = from r in xdoc.Descendants("GasGenerator")
                               select r;

            foreach (var gasData in selectGasGen)
            {
                ClsGenaratorGas gasGenerator = new ClsGenaratorGas();

                var selectGen = from genResult in gasData.Descendants("Day")
                                select genResult;

                gasGenerator.FuelGenName = gasData.Element("Name").Value;

                //Daily Generation Value = Energy x Price x ValueFactor
                //Daily Emissions = Energy x EmissionRating x EmissionFactor
                foreach (var genData in selectGen)
                {
                    gasGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorMedium + gasGenerator.DailyGenerationValue;
                    gasGenerator.DailyEmissionsValue = double.Parse(genData.Element("Energy").Value) * double.Parse(gasData.Element("EmissionsRating").Value) * ClsFactors.EmissionFactorMedium + gasGenerator.DailyEmissionsValue;
                    gasGenerator.Date = DateTime.Parse(genData.Element("Date").Value).ToUniversalTime();
                    Console.WriteLine("Total:-" + gasGenerator.DailyGenerationValue + "  Emission:- " + gasGenerator.DailyEmissionsValue);
                }

                gasGenerators.Add(gasGenerator);
            }
            return gasGenerators;
        }

        // Get Parsed Coal Generator Data 
        public List<ClsGenaratorCoal> GetCoalDataList(XDocument xdoc)
        {
            List<ClsGenaratorCoal> coalGenerators = new List<ClsGenaratorCoal>();

            //Parse Coal Generation Data
            Console.WriteLine("Coal");
            var selectCoalGen = from r in xdoc.Descendants("CoalGenerator")
                                select r;


            foreach (var coalData in selectCoalGen)
            {
                ClsGenaratorCoal coalGenerator = new ClsGenaratorCoal();

                var selectGen = from genResult in coalData.Descendants("Day")
                                select genResult;

                coalGenerator.FuelGenName = coalData.Element("Name").Value;

                //Actual Heat Rate = TotalHeatInput / ActualNetGeneration
                coalGenerator.ActualHeatRate = double.Parse(coalData.Element("TotalHeatInput").Value) / double.Parse(coalData.Element("ActualNetGeneration").Value);

                //Daily Generation Value = Energy x Price x ValueFactor
                //Daily Emissions = Energy x EmissionRating x EmissionFactor
                foreach (var genData in selectGen)
                {
                    coalGenerator.DailyGenerationValue = double.Parse(genData.Element("Energy").Value) * double.Parse(genData.Element("Price").Value) * ClsFactors.ValueFactorMedium + coalGenerator.DailyGenerationValue;
                    coalGenerator.DailyEmissionsValue = double.Parse(genData.Element("Energy").Value) * double.Parse(coalData.Element("EmissionsRating").Value) * ClsFactors.EmissionFactorHigh + coalGenerator.DailyEmissionsValue;
                    coalGenerator.Date = DateTime.Parse(genData.Element("Date").Value).ToUniversalTime();
                    Console.WriteLine("Total:-" + coalGenerator.DailyGenerationValue + "  Emission:- " + coalGenerator.DailyEmissionsValue + "  Actual Heat Rate:- " + coalGenerator.ActualHeatRate);
                }

                coalGenerators.Add(coalGenerator);
            }
            return coalGenerators;
        }

        //Get List of Total Generator Values of all Fuels i.e Coal, Gas and Wind
        public List<KeyValuePair<string,double>> GetTotalGeneratorValues(List<ClsGenaratorWind> WindData, List<ClsGenaratorGas> GasData, List<ClsGenaratorCoal> CoalData)
        {
            List<KeyValuePair<string, double>> totalGen = new List<KeyValuePair<string, double>>();
           
            totalGen = CoalData.Select(x => new KeyValuePair<string, double>(x.FuelGenName,x.DailyGenerationValue)).ToList();
            totalGen.AddRange(GasData.Select(x => new KeyValuePair<string, double>(x.FuelGenName, x.DailyGenerationValue)).ToList());
            totalGen.AddRange(WindData.Select(x => new KeyValuePair<string, double>(x.FuelGenName, x.DailyGenerationValue)).ToList());
            return totalGen;
        }
  
        //Get Sorted List of Emission Generator Values of all Fuels i.e Coal, Gas and Wind
        public List<ClsEmissionData> GetEmissionGeneratorValues(List<ClsGenaratorGas> GasData, List<ClsGenaratorCoal> CoalData)
        {
            List<ClsEmissionData> emissionValue = new List<ClsEmissionData>();

            emissionValue = CoalData.Select(p => new ClsEmissionData() { FuelGenName = p.FuelGenName,Date = p.Date, DailyEmissionsValue=p.DailyEmissionsValue }).ToList();
            emissionValue.AddRange(GasData.Select(p => new ClsEmissionData() { FuelGenName = p.FuelGenName, Date = p.Date,  DailyEmissionsValue = p.DailyEmissionsValue }).ToList());

            return emissionValue.OrderBy(order => order.Date).ThenBy(order => order.DailyEmissionsValue).ToList();
        }

        public bool GenerateOutput(List<KeyValuePair<string, double>> TotalGenValues,List<ClsEmissionData> EmissionValues, List<ClsGenaratorCoal> CoalData)
        {
            try
            {
                var xOutPut = new XElement("GenerationOutput",
                            new XElement("Total",
                            from total in TotalGenValues
                            select new XElement("Generator",
                               new XElement("Name", total.Key),
                               new XElement("Total", total.Value))),

                            new XElement("MaxEmissionGenerators",
                            from eValue in EmissionValues
                            select new XElement("Day",
                               new XElement("Name", eValue.FuelGenName),
                               new XElement("Date", eValue.Date),
                               new XElement("Emission", eValue.DailyEmissionsValue))),

                           new XElement("ActualHeatRates",
                            from hRate in CoalData
                            select new XElement("Generator",
                               new XElement("Name", hRate.FuelGenName),
                               new XElement("HeatRate", hRate.ActualHeatRate)))
                           
                           );

                string outputPath = ConfigurationManager.AppSettings["OutputFolderPath"];

                xOutPut.Save(outputPath);
                Console.WriteLine("Converted to XML");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

       


    }


}