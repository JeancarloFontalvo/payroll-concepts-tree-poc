using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.models.business
{
    public interface IPayrollNode
    {
        public string Code { get; set; }

        public double Value { get; set; }

        public double Calculate(PayrollContext context);

        public bool HasDependencies { get; }
    }
}
