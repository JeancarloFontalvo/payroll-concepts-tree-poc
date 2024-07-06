using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.models.business.concepts
{
    public class ValueConcept: ConceptBO, IPayrollNode
    {
        public override double Calculate(PayrollContext context)
        {
            var valueBase = this._dependencies.Sum(c => c.Calculate(context));

            return valueBase + Value;
        }
    }
}
