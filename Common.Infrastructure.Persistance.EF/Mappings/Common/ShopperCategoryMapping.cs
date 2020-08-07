using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Domain.Model;
using Common.Domain.Model.Common;

namespace Common.Infrastructure.Persistance.EF.Mappings.Common
{
    internal class ShopperCategoryMapping : EntityTypeConfiguration<ShopperCategory>
    {
        public ShopperCategoryMapping()
        {
            ToTable("Category", SchemaContexts.Common);
            HasOptional(a => a.CategoryBase).WithMany().HasForeignKey(a => a.CategoryBase_Id);
        }
    }
}