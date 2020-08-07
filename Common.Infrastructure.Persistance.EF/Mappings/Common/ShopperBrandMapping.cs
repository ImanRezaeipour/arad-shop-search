using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Domain.Model;
using Common.Domain.Model.Common;

namespace Common.Infrastructure.Persistance.EF.Mappings.Common
{
    internal class ShopperBrandMapping : EntityTypeConfiguration<ShopperBrand>
    {
        public ShopperBrandMapping()
        {
            ToTable("Brand", SchemaContexts.Common);
        }
    }
}