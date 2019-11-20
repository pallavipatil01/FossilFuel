using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossilFuel
{
    
    class ClsEmissionData
    {

        //string _fuelGenName;
        //DateTime _date;
        //double _dailyEmissionsValue;

        //ClsEmissionData(string fName,DateTime fDate, double dEmission)
        //{
        //    this._fuelGenName = fName;
        //    this._date = fDate;
        //    this._dailyEmissionsValue = dEmission;
        //}

        public string FuelGenName { get ; set; }
        public DateTime Date { get; set; }
        public double DailyEmissionsValue { get; set; }
    }
}
