using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Common
{
    public static class EnumDisplayHelper
    {
        public static string GetDisplayName<T>(Enum enumkey) 
        {
            var result=enumkey.GetType()
                             .GetMember(enumkey.ToString())
                             .FirstOrDefault()
                             .GetCustomAttribute<DisplayAttribute>()
                             .GetName();
            if (result is null) { return string.Empty; }
            return result;
        }
    }
}
