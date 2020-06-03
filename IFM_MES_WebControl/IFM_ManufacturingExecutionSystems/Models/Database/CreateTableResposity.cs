using IFM_ManufacturingExecutionSystems.Models.SQL;

namespace IFM_ManufacturingExecutionSystems.Models.Database
{
    public class CreateTableResposity
    {
        private n00 tmpTable;

        public CreateTableResposity()
        {
        }

        public int CreateTable(string tableName, int qtyTable)
        {
            var properties = tmpTable.GetType().GetProperties();
            string query = string.Empty;
            string columns = string.Empty;
            int result = 0;
            for (int i = 1; i <= qtyTable; i++)
            {
                string name = tableName + i.ToString("0000");
                columns = string.Format("[{0}] [int] IDENTITY(1,1) Primary Key not null ,\n", name + properties[0].Name);
                for (int j = 1; j < properties.Length; j++)
                {
                    columns += string.Format("[{0}] [nvarchar](max) null,\n", name + properties[j].Name);
                }
                query = string.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}'", name);
                mESSQL.OpenConnect();
                try
                {
                    string existTableName = mESSQL.DbCommand(query).ExecuteScalar().ToString();
                    qtyTable++;
                }
                catch
                { 
                    query = string.Format("CREATE TABLE {0} ({1})", name, columns);
                    query.Remove(query.Length - 1, 1);
                    result += mESSQL.DbCommand(query).ExecuteNonQuery();
                }
                mESSQL.CloseConnect();
            }
            return result;
        }
    }
}
