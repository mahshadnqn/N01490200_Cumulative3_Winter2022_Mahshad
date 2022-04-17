﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N01490200_Cumulative3_Winter2022_Mahshad.Models;
using MySql.Data.MySqlClient;

namespace N01490200_Cumulative3_Winter2022_Mahshad.Controllers
{
    public class TeacherDataController : Controller
    {

        ////Set up the database context class to access School MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/TeacherList/{SearchKey?}")]
        public IEnumerable<Teacher> TeacherList(string SearchKey = null)
        {
            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) " +
                "or lower(concat(teacherfname, ' ' ,teacherlname)) like lower(@key) or hiredate like (@key) or salary like (@key)";

            //Prevent SQL Injection Attack
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers 
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row the result set
            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];


                //Create a new Teacher object 
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Teacher Name to the list
                Teachers.Add(NewTeacher);
            }

            //CLose the connection between the MySQL Database and the Web server
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query - In  reality this line will be changed if needed.
            cmd.CommandText = "Select * from Teachers where teacherid =" + id;

            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;
        }


        [HttpPost]
        public void DeleteTeacher(int id)
        {
            // Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @teacher_id";

            cmd.Parameters.AddWithValue("@teacher_id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            // Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "values (@teacherfname, @teacherlname, @employeenumber, @hiredate, @salary)";


            //Create a new Teacher object to add value to the table
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }


        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            // Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, employeenumber=@employeenumber, hiredate=@hiredate, salary= @salary " +
                "where teacherid=@teacherid";


            //Create a new Teacher object to add value to the table
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@teacherid", id);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }
    }
}