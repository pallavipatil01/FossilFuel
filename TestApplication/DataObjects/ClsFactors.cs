using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FossilFuel
{
    //public enum FactorType { ValueFactor, EmissionFactor }
    //public enum AmtType { High, Medium, Low }


    public static class ClsFactors
    {
        //private const string _refFilePath = @"C:\Users\upatil\Documents\Visual Studio 2015\Projects\TestApplication\TestApplication\ReferenceData.xml";


        private static double _valueFactorHigh;
        private static double _valueFactorMedium;
        private static double _valueFactorLow;
        private static double _emissionFactorHigh;
        private static double _emissionFactorMedium;
        private static double _emissionFactorLow;

        static ClsFactors()
        {
            GetFactorValues();
        }


        public static double ValueFactorHigh
        {
            get
            {
                //if (_valueFactorHigh==0)
                //    _valueFactorHigh = GetFactorValues(FactorType.ValueFactor, AmtType.High);
                return _valueFactorHigh;
            }
        }

        public static double ValueFactorMedium
        {
            get
            {
                return _valueFactorMedium;
            }
        }

        public static double ValueFactorLow
        {
            get
            {
                return _valueFactorLow;
            }
        }

        public static double EmissionFactorHigh
        {
            get
            {
                return _emissionFactorHigh;
            }
        }

        public static double EmissionFactorMedium
        {
            get
            {
                 return _emissionFactorMedium;
            }
        }

        public static double EmissionFactorLow
        {
            get
            {
                return _emissionFactorLow;
            }
        }

        //Parse Reference Data File
        private static void GetFactorValues()
        {
            //string strRefFilePath= ConfigurationSettings.AppSettings.Get("ReferenceDataPath");
            string _refFilePath = ConfigurationManager.AppSettings["ReferenceDataPath"];

            XDocument xdoc = XDocument.Load(_refFilePath);

            //Parse Value Factor
            var selectFactorValue = from fResult in xdoc.Descendants("ValueFactor")
                                    select fResult;
            XElement nodeValue = selectFactorValue.ElementAt(0);
            _valueFactorHigh = double.Parse(nodeValue.Element("High").Value);
            _valueFactorMedium = double.Parse(nodeValue.Element("Medium").Value);
            _valueFactorLow = double.Parse(nodeValue.Element("Low").Value);

            //Parse EmissionFactor
            var selectEmissionValue = from fResult in xdoc.Descendants("EmissionsFactor")
                                      select fResult;
            nodeValue = selectEmissionValue.ElementAt(0);
            _emissionFactorHigh = double.Parse(nodeValue.Element("High").Value);
            _emissionFactorMedium = double.Parse(nodeValue.Element("Medium").Value);
            _emissionFactorLow = double.Parse(nodeValue.Element("Low").Value);
        }

        //public static void GetEmissionsValues()
        //{
        //    //string strRefFilePath= ConfigurationSettings.AppSettings.Get("ReferenceDataPath");
        //    XDocument xdoc = XDocument.Load(@"C: \Users\upatil\Documents\Visual Studio 2015\Projects\TestApplication\TestApplication\ReferenceData.xml");

        //    var selectFactorValue = from fResult in xdoc.Descendants("EmissionFactor")
        //                            select fResult;
        //    XElement nodeValue = selectFactorValue.ElementAt(0);
        //    High = double.Parse(nodeValue.Element("High").Value);
        //    Medium = double.Parse(nodeValue.Element("Medium").Value);
        //    Low = double.Parse(nodeValue.Element("Low").Value);
        //}

    }
}
