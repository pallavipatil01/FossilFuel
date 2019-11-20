using BrandyFossilFuel.Generators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BrandyFossilFuel
{
    public class GeneratorProcessor
    {

        public void GenerateReport()
        {
            Initialize();

         }

        private void Initialize()
        {
            Generators.IGenerator windGenerator = new WindGenerator();
            Generators.IGenerator coalGenerator = new CoalGenerator();
            Generators.IGenerator gasGenerator = new GasGenerator();

            string val = ConfigurationManager.AppSettings["ReferenceDataPath"];
            string path = System.Environment.ExpandEnvironmentVariables(val);

            XDocument xdocument = XDocument.Load(path);
            IEnumerable<XElement> referenceDatas = xdocument.Elements();
            foreach (var referenceData in referenceDatas)
            {
                Console.WriteLine(referenceData);
            }

        }
    }
}
