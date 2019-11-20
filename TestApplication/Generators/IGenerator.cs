using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandyFossilFuel.Generators
{
    public interface IGenerator
    {
        string FuelGenaratorType { get; set; }

        string FuelGenName { get; set; }
        DateTime Date { get; set; }
        double Energy { get; set; }
        double Price { get; set; }
        double Emission { get; set; }
        void Calculate();

    }
}
