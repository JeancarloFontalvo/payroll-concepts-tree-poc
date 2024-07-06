using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.models.dto
{
    public class ConceptDto
    {
        public string Code { get; set; }

        public double Value { get; set; }

        public List<string> Dependencies { get; set; } = new List<string>();
    }
}
