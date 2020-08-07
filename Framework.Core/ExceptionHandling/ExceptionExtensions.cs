using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ExceptionHandling
{
    public static class ExceptionExtensions
    {
        public static string ExtractFullMessage(this Exception exception)
        {
            var stringBuilder = new StringBuilder();
            var tmp = exception;

            while (tmp != null)
            {
                stringBuilder.Append(tmp.Message);
                tmp = tmp.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
