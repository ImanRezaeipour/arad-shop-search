using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class SearchModel
    {
        #region =================== Public Property =======================

        public List<SearchResult> Results { get; set; }
        public List<List<string>> Suggests { get; set; }


        #endregion =================== Public Property =======================
    }
}
