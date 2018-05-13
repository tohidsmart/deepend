using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using deepend.common;
using deepend.entity.Response;


namespace deepend.data
{
    public class ChequeData : IChequeData
    {

        private readonly string _connectionString;

        public ChequeData(IConfigProvider configProvider)
        {
            _connectionString = configProvider.GetConnectionString();
        }

        public int AddCheque(ChequeResponse cheque)
        {
            int recordId = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@PersonName", cheque.PersonName);
                parameters.Add("@Amount", cheque.Amount);
                parameters.Add("@DateTime", cheque.DateTime);


                 recordId =connection.ExecuteScalar<int>("[dbo].[AddCheque]", parameters,
                                       commandType: CommandType.StoredProcedure);
            }
            return recordId;

        }

        public ChequeResponse FindCheque(int ChequeId)
        {
            ChequeResponse cheque = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                cheque = connection.QueryFirstOrDefault<ChequeResponse>("[dbo].[GetCheque]",
                   new { Id = ChequeId }, commandType: CommandType.StoredProcedure);
            }
            return cheque;
        }

        public IEnumerable<ChequeResponse> GetAllCheque()
        {
            IEnumerable<ChequeResponse> cheques;
            using (var connection = new SqlConnection(_connectionString))
            {
                cheques = connection.Query<ChequeResponse>("[dbo].[GetCheques]", commandType: CommandType.StoredProcedure);
            }
            return cheques;
        }
    }
}
