using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Config.AutoMapperConverter
{
    public class StringToNullableLongConverter : ITypeConverter<string, long?>
    {
        public long? Convert(string source, long? destination, ResolutionContext context)
        {
            if (String.IsNullOrEmpty(source) || String.IsNullOrWhiteSpace(source))
            {
                return default(long?);
            }
            else
            {
                long? longValue = null;
                if (long.TryParse(source, out long evalLong))
                {
                    longValue = evalLong;
                }
                return longValue;
            }
        }
    }
}
