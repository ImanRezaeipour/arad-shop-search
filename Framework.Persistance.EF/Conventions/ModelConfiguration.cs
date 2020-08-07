using System.Data.Entity;

namespace Framework.Persistance.EF.Conventions
{
    public class ModelConfiguration : DbConfiguration
    {
        public ModelConfiguration()
        {
            //TODO: Salman Check it 
            this.SetHistoryContext("Oracle.ManagedDataAccess.Client",
                (connection, defaultSchema) => new MyHistoryContext(connection, defaultSchema));
        }
    }
}
