using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.TransferObject
{
    public class ShopperBrandDTO
    {
        #region =================== Public Property =======================
        public Guid id { get; set; }
        public string name { get; set; }
        public string latinName { get; set; }
        public bool isActive { get; set; }
        #endregion =================== Public Property =======================
    }
}
