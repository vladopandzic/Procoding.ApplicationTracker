using Mono.Cecil;
using Mono.Cecil.Rocks;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Procoding.Architecture.Tests.Rules
{
    internal sealed class HaveParametersConstructor : ICustomRule
    {

        public HaveParametersConstructor()
        {
        }

        public bool MeetsRule(TypeDefinition type)
        {
            var constructors = type.GetConstructors();

            var privateParameterlessConstructors = constructors.Where(c => c.Parameters.Count == 0 && !c.IsStatic && c.IsPrivate);

            return privateParameterlessConstructors.Count() == 1;
        }
    }
}
