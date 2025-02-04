using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace courseraClone.Services
{
    public class UserService
    {
        
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to validate the user's credentials
        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Query to check if the user exists with the provided email and password
                var userExists = await db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM Users WHERE Email = @Email AND Password = @Password",
                    new { Email = email, Password = password }
                );

                // Return true if the user exists, otherwise false
                return userExists > 0;
            }
        }

        // Method to register a new user
        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Check if the user already exists
                var userExists = await db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM Users WHERE Email = @Email",
                    new { Email = email }
                );

                if (userExists > 0)
                {
                    return false; // User already exists
                }

                // Insert the new user into the database
                var result = await db.ExecuteAsync(
                    "INSERT INTO Users (Email, Password) VALUES (@Email, @Password)",
                    new { Email = email, Password = password }
                );

                return result > 0; // Return true if the insertion is successful
            }
        }
    }
}
