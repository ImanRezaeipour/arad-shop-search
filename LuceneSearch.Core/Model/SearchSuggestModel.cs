using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class SearchSuggestModel
    {
        #region =================== Public Property =======================

        public int Score { get; set; }
        public string Suggest { get; set; }

        #endregion =================== Public Property =======================
    }
}
