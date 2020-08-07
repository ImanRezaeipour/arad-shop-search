using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class ShopperBrand
    {
        #region =================== Public Property =======================
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool IsActive { get; set; }
        #endregion =================== Public Property =======================
    }
}
