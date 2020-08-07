using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Config.AutoMapperConverter
{
    public class StringToNullableBooleanConverter : ITypeConverter<string, bool?>
    {
       
        public bool? Convert(string source, bool? destination, ResolutionContext context)
        {
            if (String.IsNullOrEmpty(source) || String.IsNullOrWhiteSpace(source))
            {
                return default(bool?);
            }
            else
            {
                bool? boolValue = null;
                if (bool.TryParse(source, out bool evalBool))
                {
                    boolValue = evalBool;
                }
                return boolValue;
            }
        }
    }
}
