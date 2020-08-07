using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Domain.Model;
using Common.Domain.Model.Common;

namespace Common.Infrastructure.Persistance.EF.Mappings.Common
{
    internal class ShopperProductMapping : EntityTypeConfiguration<ShopperProduct>
    {
        public ShopperProductMapping()
        {
            ToTable("Product", SchemaContexts.Common);
            HasOptional(a => a.Brand).WithMany().HasForeignKey(a => a.Brand_Id);
            HasOptional(a => a.Discount).WithMany().HasForeignKey(a => a.ProductDiscount_Id);
            HasOptional(a => a.Category).WithMany().HasForeignKey(a => a.Category_Id);
        }
    }
}