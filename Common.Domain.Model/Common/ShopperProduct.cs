using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model.Common
{
    public class ShopperProduct
    {
        #region =================== Public Property =======================

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public decimal DiscountPrice { get; set; }
        public string MainImage { get; set; }
        public bool IsActive { get; set; }
        public ShopperBrand Brand { get; set; }
        //public string CategoryName { get; set; }
        public Guid? Category_Id { get; set; }
        public ShopperProductDiscount Discount { get; set; }
        public ShopperCategoryBase Category { get; set; }
        public Guid? Brand_Id { get; set; }
        public Guid? ProductDiscount_Id { get; set; }

        public int Discount_From { get; set; }
        public int Discount_To { get; set; }

        

        #endregion =================== Public Property =======================
    }
}
