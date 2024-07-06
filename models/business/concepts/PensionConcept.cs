using concepts_poc.extensions;

namespace concepts_poc.models.business.concepts
{
    public class PensionConcept : ConceptBO, IPayrollNode
    {
        public override double Calculate(PayrollContext context)
        {
            var conceptBase = this._dependencies.Sum(c => c.Calculate(context));

            return conceptBase * .4;
        }
    }
}
