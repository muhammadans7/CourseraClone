using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using courseraClone.Models; // Import the shared model namespace

namespace courseraClone.Services
{
    public class DropCourseService
    {
        private readonly string _connectionString;

        public DropCourseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Course>> GetCoursesByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT CourseName FROM Enrollments WHERE UserEmail = @Email";
                var courses = await connection.QueryAsync<Course>(query, new { Email = email });
                return courses.ToList();
            }
        }

        public async Task<bool> DropCourseAsync(string email, string courseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Enrollments WHERE UserEmail = @Email AND CourseName = @CourseName";
                int rowsAffected = await connection.ExecuteAsync(query, new { Email = email, CourseName = courseName });
                return rowsAffected > 0;
            }
        }
    }
}