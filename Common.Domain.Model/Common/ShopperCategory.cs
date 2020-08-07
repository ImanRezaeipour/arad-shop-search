using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class ShopperCategory
    {
        #region =================== Public Property =======================
        public Guid Id { get; set; }
        public Guid? CategoryBase_Id { get; set; }
        public ShopperCategoryBase CategoryBase { get; set; }
        
        #endregion =================== Public Property =======================
    }
}
