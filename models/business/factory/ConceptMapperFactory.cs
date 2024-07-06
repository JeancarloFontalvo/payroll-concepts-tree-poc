using concepts_poc.models.dto;

namespace concepts_poc.models.business.factory
{
    public class ConceptMapperFactory
    {
        private Dictionary<string, Func<ConceptDto, IPayrollNode>> _mapper = new Dictionary<string, Func<ConceptDto, IPayrollNode>>();

        public ConceptMapperFactory Add(string key, Func<ConceptDto, IPayrollNode> builderFunc)
        {
            if (builderFunc == null) throw new ArgumentNullException();

            if(_mapper.ContainsKey(key))
            {
                throw new ArgumentException($"Already mapper for concept {key}");
            }

            _mapper[key] = builderFunc;

            return this;
        }

        public IPayrollNode Get(ConceptDto concept)
        {
            if (!_mapper.ContainsKey(concept.Code)) throw new ArgumentException($"Not registered mapper builder func for type {concept.Code}");

            return _mapper[concept.Code](concept);
        }
    }
}
