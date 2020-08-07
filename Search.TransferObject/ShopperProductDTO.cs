using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.TransferObject
{
    public class ShopperProductDTO
    {
        #region =================== Public Property =======================

        public Guid id { get; set; }
        public string name { get; set; }
        public string shortDescription { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal discountPrice { get; set; }
        public string mainImage { get; set; }
        public bool isActive { get; set; }
        public ShopperBrandDTO brand { get; set; }
        public string categoryName { get; set; }
        public Guid? categoryId { get; set; }
        public ShopperProductDiscountDTO discount { get; set; }
        public Guid? brand_Id { get; set; }
        public Guid? productDiscount_Id { get; set; }


        #endregion =================== Public Property =======================
    }
}
