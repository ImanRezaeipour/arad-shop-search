using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ApplicationException
{
    public class ViewableException : Exception
    {

        #region =================== Private Variable ====================== 
        #endregion

        #region =================== Public Property ======================= 
        public int ErrorCode { get; set; }
        public string MessageForLog { get; set; }
        #endregion

        #region =================== Public Constructor ==================== 
        public ViewableException(string message):base(message)
        {
        }
        public ViewableException(string message, int code) : this(message)
        {
            ErrorCode = code; 
        }
        public ViewableException(string message, string messageForLog, int code) : this(message, code)
        {
            MessageForLog = messageForLog;
        }
        #endregion

        #region =================== Private Methods ======================= 
        #endregion

        #region =================== Public Methods ======================== 

        #endregion

    }
}
