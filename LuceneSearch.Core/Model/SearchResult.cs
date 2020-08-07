using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class SearchResult
    {
        #region =================== Public Property =======================

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string[] Suggests { get; set; }


        #endregion =================== Public Property =======================
    }
}
