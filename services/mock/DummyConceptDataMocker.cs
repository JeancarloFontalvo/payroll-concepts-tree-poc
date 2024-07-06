using concepts_poc.extensions;
using concepts_poc.models.business;
using concepts_poc.models.business.concepts;
using concepts_poc.models.business.factory;
using concepts_poc.models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.services.mock
{
    internal class DummyConceptDataMocker
    {
        public static List<ConceptDto> GenerateDummyData()
        {
            var context = BuildPayrollContext();

            return new List<ConceptDto>
            {
                new ConceptDto { Code = "salary", Value = context.GetVariable<double>("salary") },
                new ConceptDto { Code = "commision", Value = 0 },
                new ConceptDto { Code = "extraHours", Value = 0, Dependencies = [ "transportationHelp" ] },
                new ConceptDto { Code = "health", Dependencies =  [ "salary", "commision", "extraHours" ] },
                new ConceptDto { Code = "pension", Dependencies = [ "salary", "commision", "health" ] },
                new ConceptDto { Code = "transportationHelp", Value = context.GetVariable<double>("tranportationHelpValue"), Dependencies = [ "" ] }
            };
        }

        public static PayrollContext BuildPayrollContext()
        {
            return new PayrollContext
            {
                Variables =
                {
                    { "salary", 2800000 },
                    { "workedDays", 30 },
                    { "tranportationHelpValue", 162000 },
                    { "currentYearMinSalary", 1300000 },
                    { "tranportationHelpLimit", 2 },
                }
            };
        }

        public static ConceptMapperFactory BuildConceptMapperFactory()
        {
            var factory = new ConceptMapperFactory();

            return factory
                .Add("salary", (concept) => new SalaryConcept { Code = concept.Code, Value = concept.Value })
                .Add("commision", (concept) => new ValueConcept { Code = concept.Code, Value = concept.Value })
                .Add("extraHours", (concept) => new ValueConcept { Code = concept.Code, Value = concept.Value })
                .Add("health", (concept) => new HealthConcept { Code = concept.Code, Value = concept.Value })
                .Add("pension", (concept) => new PensionConcept { Code = concept.Code, Value = concept.Value })
                .Add("transportationHelp", (concept) => new TransportationHelpConcept { Code = concept.Code, Value = concept.Value });
        }

        public static List<IPayrollNode> BuildDepedencyTree(List<ConceptDto> concepts)
        {
            var factory = BuildConceptMapperFactory();
            var conceptDict = new Dictionary<string, IPayrollNode>();

            foreach (var conceptDto in concepts)
            {
                conceptDict[conceptDto.Code] = factory.Get(conceptDto);
            }

            foreach (var conceptDto in concepts)
            {
                if(conceptDict[conceptDto.Code] is not ConceptBO conceptBO)
                {
                    continue;   
                }

                foreach (var dep in conceptDto.Dependencies.Where(dep => !string.IsNullOrWhiteSpace(dep)))
                {
                    if (!conceptDict.ContainsKey(dep))
                    {
                        throw new InvalidOperationException($"Dependency {dep} is not found in concepts");
                    }

                    conceptBO.AddDependency(conceptDict[dep]);
                }

            }

            return conceptDict.Values.Where(x => !x.HasDependencies).ToList();
        }
    }
}
