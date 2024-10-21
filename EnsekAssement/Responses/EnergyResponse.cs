using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekAssement.Responses
{
    public class EnergyResponse
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }
}
