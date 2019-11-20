using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossilFuel
{
    //public enum FuelType { Offshore, Onshore }

    class ClsGenaratorCoal:IGenerator
    {
        public string FuelGenaratorType { get; set; }
        public string FuelGenName { get; set; }
        public DateTime Date { get; set; }
        public double Energy { get; set; }
        public double Price { get; set; }
        public double Emission { get; set; }
                
        public double DailyGenerationValue { get; set; }
        public double DailyEmissionsValue { get; set; }

        public double ActualHeatRate { get; set; }
    }
}
