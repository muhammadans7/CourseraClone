using courseraClone.Models; // Add this
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace courseraClone.Services
{
    public class CourseService
    {
        private readonly string _connectionString;

        public CourseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = new List<Course>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, CourseName FROM Courses";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            courses.Add(new Course
                            {
                                Id = reader.GetInt32(0),
                                CourseName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return courses;
        }
    }
}