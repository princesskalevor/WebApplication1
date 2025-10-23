using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp2
{
    public class DatabaseService
    {
        string _connStr;

        public DatabaseService()
        {
            _connStr = "Server=DESKTOP-IF49OJQ\\SQLEXPRESS;Database=School;Trusted_Connection=True;TrustServerCertificate=True";
        }

        public void AddStudent(string fname, string lname,
            char gender, DateOnly birthDate)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"INSERT INTO Student (FirstName, LastName, Gender, BirthDate) 
                            VALUES (@fname, @lname, @gender, @bdate)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@bdate", birthDate);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Student added successfully");
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Student";

            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Student temp = new Student();
                temp.StudentId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                students.Add(temp);
            }

            conn.Close();

            return students;


        }

        public Student GetStudent(int id)
        {
            Student student = new Student();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Student Where studentId = @Id";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Student temp = new Student();
                temp.StudentId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                student = temp;
            }

            conn.Close();

            return student;


        }

        public void updateStudent(Student student)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"UPDATE Student set FirstName=@fname,
                    LastName=@lname, Gender=@gender,
                    BirthDate=@bdate WHERE StudentId=@studentId";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fname", student.FirstName);
            cmd.Parameters.AddWithValue("@lname", student.LastName);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@bdate", student.BirthDate);
            cmd.Parameters.AddWithValue("@studentId", student.StudentId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Student updated successfully");
        }

        public void deleteStudent(int id)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"DELETE FROM Student WHERE 
                StudentId=@studentId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@studentId", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Student deleted successfully");
        }

        public List<Student> FilterStudents(int year)
        {
            List<Student> students = new List<Student>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Student Where YEAR(BIRTHDATE) > @year";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@year", year);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Student temp = new Student();
                temp.StudentId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                students.Add(temp);
            }

            conn.Close();

            return students;


        }


        public List<StudentScores> GetStudentScores()
        {
            List<StudentScores> scores = new List<StudentScores>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"Select st.STUDENTID, st.FIRSTNAME, st.LASTNAME, 
                      cs.COURSENAME, sc.MARK, sc.GRADE 
                      from student st 
                      join SCORES sc on st.STUDENTID = sc.STUDENTID
                      join COURSE cs on sc.COURSEID = cs.COURSEID";

            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StudentScores temp = new StudentScores();
                temp.StudentId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Course = reader.GetString(3);
                temp.Mark = reader.GetDecimal(4);
                temp.Grade = reader.GetString(5);

                scores.Add(temp);
            }

            conn.Close();

            return scores;


        }

    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Char Gender { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class YearFilter
    {
        public int StartingYear { get; set; }
    }

    public class StudentScores
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Course { get; set; }
        public decimal Mark { get; set; }
        public string Grade { get; set; }
    }
}