using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.Shared.Utils
{
    public static class ModelStateHelper
    {
        public static void AddError(this ModelStateDictionary modelState, 
            Dictionary<string, string[]> Errors, 
            Dictionary<string, string>? conditions = null)
        {
            foreach (var (key, value) in Errors)
            {
                var keyName = string.Empty;
                if (conditions != null && conditions.ContainsKey(key)) keyName = conditions[key];

                foreach (var error in value)
                {
                    modelState.AddModelError(keyName, error);
                }
            }
        }
    }
}
