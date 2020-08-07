using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class ShopperProductDiscount
    {
        #region =================== Public Property =======================
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        #endregion =================== Public Property =======================
    }
}
