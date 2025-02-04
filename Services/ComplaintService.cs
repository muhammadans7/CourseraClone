using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace courseraClone.Services
{
    public class ComplaintService
    {
        private readonly string _connectionString;

        public ComplaintService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

      
        public async Task<bool> SubmitComplaintAsync(string name, string email, string complaintText)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Complaints (Name, Email, ComplaintText) VALUES (@Name, @Email, @ComplaintText)";
                var result = await connection.ExecuteAsync(query, new { Name = name, Email = email, ComplaintText = complaintText });

             
                return result > 0;
            }
        }

    }
}