namespace concepts_poc.models.business
{
    public abstract class ConceptBO : IPayrollNode
    {
        public string Code { get; set; }

        public double Value { get; set; }

        protected List<IPayrollNode> _dependencies { get; set; } = [];

        public bool HasDependencies => _dependencies.Count > 0;

        public ConceptBO AddDependency(IPayrollNode node)
        {

            _dependencies.Add(node);

            return this;
        }

        public ConceptBO AddDependencies(IEnumerable<IPayrollNode> node) { 
            
            _dependencies.AddRange(node);

            return this;
        }

        public virtual double Calculate(PayrollContext context) { 
            throw new NotImplementedException();
        }
    }
}
