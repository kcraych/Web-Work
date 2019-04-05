using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private string _connectionString = @"Data Source=DESKTOP-4GJ5Q0O\SQLEXPRESS;Initial Catalog=School;
                                            Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                            TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                            MultiSubnetFailover=False";

        // GET: Student
        public ActionResult Index()
        {
            string queryString = "select * from students";
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    students.Add(student);
                }
                connection.Close();
            }
            return View(students);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student student)
        {
            string queryString = @"Insert into students (FirstName, LastName)
                                   values (@FirstName, @LastName)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters.Add("@LastName", SqlDbType.VarChar);

                command.Parameters["@FirstName"].Value = student.FirstName;
                command.Parameters["@LastName"].Value = student.LastName;

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            string queryString = @"select * from students where Id = @id";
            Student student = new Student();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                }
                conn.Close();
            }
            return View(student);
        }

        public ActionResult Edit(int Id)
        {
            string queryString = @"select * from students where Id = @id";
            Student student = new Student();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                }
                conn.Close();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            string queryString = @"Update students
                                   Set FirstName = @FirstName, LastName = @LastName
                                   Where Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters["@Id"].Value = student.Id;
                command.Parameters["@FirstName"].Value = student.FirstName;
                command.Parameters["@LastName"].Value = student.LastName;

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}