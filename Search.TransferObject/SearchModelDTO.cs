using System.Collections.Generic;

namespace SearchEngine.TransferObject
{
    public class SearchModelDTO
    {
        #region =================== Public Property =======================

        public List<ShopperProductDTO> result { get; set; }
        public int count { get; set; }
        public List<List<string>> suggests { get; set; }
        #endregion =================== Public Property =======================
    }
}
