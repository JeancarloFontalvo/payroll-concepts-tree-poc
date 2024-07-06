using concepts_poc.models.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concepts_poc.extensions
{
    public static class PayrollContextExtensions
    {
        public static T? GetVariable<T>(this PayrollContext context, string variable, T? defaultValue = default)
        {
			try
			{
                return (T?)Convert.ChangeType(context.Variables.GetValueOrDefault(variable) ?? defaultValue, typeof(T));
            }
			catch 
            {
                return defaultValue;
			};
        }
    }
}
