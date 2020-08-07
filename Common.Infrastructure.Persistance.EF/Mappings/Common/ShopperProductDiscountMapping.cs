using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Domain.Model;
using Common.Domain.Model.Common;

namespace Common.Infrastructure.Persistance.EF.Mappings.Common
{
    internal class ShopperProductDiscountMapping : EntityTypeConfiguration<ShopperProductDiscount>
    {
        public ShopperProductDiscountMapping()
        {
            ToTable("ProductDiscountBase", SchemaContexts.Common);
            Property(a => a.From).HasColumnName("FromDate");
            Property(a => a.To).HasColumnName("ToDate");
        }
    }
}