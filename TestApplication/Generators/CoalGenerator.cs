using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandyFossilFuel.Generators
{
    //public enum FuelType { Offshore, Onshore }

    public class CoalGenerator : IGenerator
    {

        private string filePath = null;

        public CoalGenerator(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new Exception("Input file path not provided for Coal Generator");

            this.filePath = filePath;
         }
        public DateTime Date
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Emission
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Energy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FuelGenaratorType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FuelGenName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Price
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
