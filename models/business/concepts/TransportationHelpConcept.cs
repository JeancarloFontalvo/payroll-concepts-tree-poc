using concepts_poc.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.models.business.concepts
{
    public class TransportationHelpConcept: ConceptBO, IPayrollNode
    {
        public override double Calculate(PayrollContext context)
        {
            var salary = context.GetVariable<double>("salary");
            var minSalary = context.GetVariable<double>("currentYearMinSalary");

            if(minSalary * context.GetVariable<int>("tranportationHelpLimit") > salary)
            {
                return 0;
            }

            return context.GetVariable<double>("tranportationHelpValue") / 30 * context.GetVariable<int>("workedDays");
        }
    }
}
