using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ExceptionHandling
{
    public class ExceptionTranslator : IExceptionTranslator
    {

        #region =================== Private Variable ====================== 
        private Dictionary<Type, string> dictionary = new Dictionary<Type, string>();

        #endregion
        
        #region =================== Public Property ======================= 
        #endregion
        
        #region =================== Public Constructor ==================== 
        #endregion
        
        #region =================== Private Methods ======================= 
        #endregion
        
        #region =================== Public Methods ======================== 

        public void Register(Type typeOfException, string keyword)
        {
            dictionary.Add(typeOfException, keyword);
        }

        public Exception Translate(Exception exception)
        {
            var excpetionMessage = exception.ExtractFullMessage();
            var existing = dictionary.Where(a => excpetionMessage.ToLower().Contains(a.Value.ToLower())).ToList();
            if (existing.Any())
            {
                var exceptionToThrow = (Exception)Activator.CreateInstance(existing.First().Key);
                return exceptionToThrow;
            }
            return exception;
        }
        #endregion

    }
}
