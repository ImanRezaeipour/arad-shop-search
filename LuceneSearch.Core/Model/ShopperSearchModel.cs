using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class ShopperSearchModel
    {
        #region =================== Public Property =======================

        public List<ShopperProduct> Result { get; set; }
        public List<List<string>> Suggests { get; set; }
        public int count { get; set; }

        #endregion =================== Public Property =======================
    }
}
