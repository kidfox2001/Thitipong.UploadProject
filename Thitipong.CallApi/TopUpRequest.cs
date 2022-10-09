using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zort.Models
{
    public class TopUpRequest
    {
        public int merchantid { get; set; }

        public double amount { get; set; }

    }
}
