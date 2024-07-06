using concepts_poc.extensions;

namespace concepts_poc.models.business.concepts
{
    public class SalaryConcept : ConceptBO, IPayrollNode
    {
        public override double Calculate(PayrollContext context)
        {
            var conceptBase = context.GetVariable<double>("salary") + this._dependencies.Sum(c => c.Calculate(context));

            return conceptBase / 30 * context.GetVariable<int>("workedDays");
        }
    }
}
