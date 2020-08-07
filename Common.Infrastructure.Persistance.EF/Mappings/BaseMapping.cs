using System.Data.Entity.ModelConfiguration;
using Common.Domain.Model;

namespace Common.Infrastructure.Persistance.EF.Mappings
{
    internal class BaseMapping<T> : EntityTypeConfiguration<T> where T : BaseModel
    {
        public BaseMapping()
        {
            HasKey(a => a.Id);
        }
    }
}