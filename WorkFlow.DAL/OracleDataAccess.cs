using System.Data;
using System.Data.OracleClient;

namespace WorkFlow.DAL
{
    public class OracleDataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; }
        public OracleDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new OracleConnection(ConnectionString);
        }
        public void CloseConnection(IDbConnection connection)
        {
            var OracleConnection = (OracleConnection)connection;
            OracleConnection.Close();
            OracleConnection.Dispose();
        }
        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OracleCommand
            {
                CommandText = commandText,
                Connection = (OracleConnection)connection,
                CommandType = commandType
            };
        }
        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new OracleDataAdapter((OracleCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            OracleCommand Oraclecommand = (OracleCommand)command;
            return Oraclecommand.CreateParameter();
        }
    }
}
