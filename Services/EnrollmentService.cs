using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using courseraClone.Models;
using Microsoft.Extensions.Configuration;

namespace courseraClone.Services
{
    public class EnrollmentService
    {
        private readonly string _connectionString;

        public EnrollmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> EnrollUserAsync(string email, string username, string courseName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
              
                var result = await db.ExecuteAsync(
                    "INSERT INTO Enrollments (UserEmail, Username, CourseName) VALUES (@Email, @Username, @CourseName)",
                    new { Email = email, Username = username, CourseName = courseName }
                );

           
                return result > 0;
            }
        }

    
        public async Task<List<Course>> GetCoursesByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT CourseName FROM Enrollments WHERE UserEmail = @Email";
                var courses = await connection.QueryAsync<string>(query, new { Email = email });

            
                var courseList = new List<Course>();
                foreach (var courseName in courses)
                {
                    courseList.Add(new Course { CourseName = courseName });
                }

                return courseList;
            }
        }

        public async Task<bool> DropCourseAsync(string email, string courseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM Enrollments WHERE UserEmail = @Email AND CourseName = @CourseName";
                var result = await connection.ExecuteAsync(query, new { Email = email, CourseName = courseName });
                return result > 0; 
            }
        }

        public async Task<bool> CheckEnrollmentAsync(string email, string courseName)
        {
            try
            {
                var query = "SELECT COUNT(*) FROM Enrollments WHERE Email = @Email AND CourseName = @CourseName";

                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = await connection.ExecuteScalarAsync<int>(query, new { UserEmail = email, CourseName = courseName });
                    return result > 0; 
                }
            }
            catch (SqlException sqlEx)
            {
               
                Console.Error.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                
                Console.Error.WriteLine($"General Error in CheckEnrollmentAsync: {ex.Message}");
                throw;
            }
        }


        
        public async Task<List<Course>> GetCoursesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT CourseName FROM Courses";
                var courses = await connection.QueryAsync<string>(query);

              
                var courseList = new List<Course>();
                foreach (var courseName in courses)
                {
                    courseList.Add(new Course { CourseName = courseName });
                }

                return courseList;
            }
        }



       
    }

}


