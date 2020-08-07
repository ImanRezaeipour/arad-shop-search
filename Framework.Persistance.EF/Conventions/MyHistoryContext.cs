using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace Framework.Persistance.EF.Conventions
{
    public class MyHistoryContext : HistoryContext
    {
        public MyHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MIGRATION_HISTORY", schemaName: "TICS");
        }
    }
}
