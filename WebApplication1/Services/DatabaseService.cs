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
            _connStr = "Server=DESKTOP-IF49OJQ\\SQLEXPRESS;Database=BloodConnect;Trusted_Connection=True;TrustServerCertificate=True";
        }

        public void AddDonor(string fname, string lname,
            char gender, DateOnly birthDate)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"INSERT INTO Donor (FirstName, LastName, Gender, BirthDate) 
                            VALUES (@fname, @lname, @gender, @bdate)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@bdate", birthDate);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Donor added successfully");
        }

        public List<Donor> GetDonors()
        {
            List<Donor> donors = new List<Donor>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Donor";

            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Donor temp = new Donor();
                temp.DonorId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                donors.Add(temp);
            }

            conn.Close();

            return donors;


        }

        public Donor GetDonor(int id)
        {
            Donor donor = new Donor();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Donor Where donorId = @Id";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Donor temp = new Donor();
                temp.DonorId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                donor = temp;
            }

            conn.Close();

            return donor;


        }

        public void updateDonor(Donor donor)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"UPDATE Donor set FirstName=@fname,
                    LastName=@lname, Gender=@gender,
                    BirthDate=@bdate WHERE DonorId=@donorId";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fname", donor.FirstName);
            cmd.Parameters.AddWithValue("@lname", donor.LastName);
            cmd.Parameters.AddWithValue("@gender", donor.Gender);
            cmd.Parameters.AddWithValue("@bdate", donor.BirthDate);
            cmd.Parameters.AddWithValue("@donorId", donor.DonorId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Donor updated successfully");
        }

        public void deleteDonor(int id)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"DELETE FROM Donor WHERE 
                DonorId=@donorId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@donorId", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Donor deleted successfully");
        }

        public List<Donor> FilterDonors(int year)
        {
            List<Donor> donors = new List<Donor>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = "SELECT * FROM Donor Where YEAR(BIRTHDATE) > @year";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@year", year);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Donor temp = new Donor();
                temp.DonorId = reader.GetInt32(0);
                temp.FirstName = reader.GetString(1);
                temp.LastName = reader.GetString(2);
                temp.Gender = reader.GetString(3)[0];
                temp.BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                donors.Add(temp);
            }

            conn.Close();

            return donors;


        }


        public List<DonorScores> GetDonorScores()
        {
            List<DonorScores> scores = new List<DonorScores>();

            SqlConnection conn = new SqlConnection(_connStr);
            string sql = @"Select d.DONORID, d.FIRSTNAME, d.LASTNAME, 
                      cs.COURSENAME, sc.MARK, sc.GRADE 
                      from donor d 
                      join SCORES sc on d.DONORID = sc.DONORID
                      join COURSE cs on sc.COURSEID = cs.COURSEID";

            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DonorScores temp = new DonorScores();
                temp.DonorId = reader.GetInt32(0);
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

    public class Donor
    {
        public int DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Char Gender { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class YearFilter
    {
        public int StartingYear { get; set; }
    }

    public class DonorScores
    {
        public int DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Course { get; set; }
        public decimal Mark { get; set; }
        public string Grade { get; set; }
    }
}