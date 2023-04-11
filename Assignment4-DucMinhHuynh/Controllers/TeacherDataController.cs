using Assignment4_DucMinhHuynh.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment4_DucMinhHuynh.Controllers
{

    public class TeacherDataController : ApiController
    {
        /// <summary>
        ///     ListTeachers function was created to extract all date from teachers table, send GET request , recieve data from database query. 
        /// </summary>
        /// <result>
        /// Teacher Name
        /// Alexander Bennett
        /// Caitlin Cummings
        /// Linda Chan
        /// Lauren Smith
        /// Jessica Morris
        /// Thomas Hawkins
        /// Shannon Barton
        /// Dana Ford
        /// Cody Holland
        /// John Taram
        /// </result>
        // declare connection information
        private SchoolDbContext teacherDb = new SchoolDbContext();
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            // create connection
            MySqlConnection con = teacherDb.AccessDatabase();
            //open connection
            con.Open();
            //create query cmd.
            MySqlCommand cmd = con.CreateCommand();
            // implementing SQL in command.
            cmd.CommandText = "SELECT * FROM teachers";
            // extract data from data server.
            MySqlDataReader result = cmd.ExecuteReader();
            // Create empty List OF Teachers.
            List<Teacher> teachers = new List<Teacher>();
            while (result.Read()) {
                // extract data from database into temp variable
                int list_id = (int)result["teacherid"];
                string f_name = (string)result["teacherfname"];
                string l_name = (string)result["teacherlname"];
                DateTime hiring = (DateTime)result["hiredate"];
                Decimal sal = (Decimal)result["salary"];
                // convert temp variables into Models Teacher Object
                Teacher Newteachers = new Teacher();
                Newteachers.teacherid = list_id;
                Newteachers.teacherfname = f_name;
                Newteachers.teacherlname = l_name;
                Newteachers.hiredate = hiring;
                Newteachers.salary = sal;
                // add each row of data from database into each objects in List
                teachers.Add(Newteachers);
            }
            con.Close();
            return teachers;
        }
        /// <summary>
        ///  FindTeacher function take  int ID and connect to database , send ID and recieve data from database
        /// </summary>
        /// <param name="id">2</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher")]
        public Teacher FindTeacher(int id)
        {
            // create connection
            MySqlConnection con = teacherDb.AccessDatabase();
            //open connection
            con.Open();
            //create query cmd.
            MySqlCommand cmd = con.CreateCommand();
            // implementing SQL in command.
            cmd.CommandText = "SELECT * FROM teachers Where teacherid = (@inputkey)";
            // SQL validation for Input QUERY.
            cmd.Parameters.AddWithValue("@inputkey", id);
            // extract data from data server.
            MySqlDataReader result = cmd.ExecuteReader();
            // create Newteacher object to hold data from server.
            Teacher Newteacher = new Teacher();
            while (result.Read())
            {
                int find_id = (int)result["teacherid"];
                string f_name = (string)result["teacherfname"];
                string l_name = (string)result["teacherlname"];
                DateTime hiring = (DateTime)result["hiredate"];
                Decimal sal = (Decimal)result["salary"];
                Newteacher.teacherid = find_id;
                Newteacher.teacherfname = f_name;
                Newteacher.teacherlname = l_name;
                Newteacher.hiredate = hiring;
                Newteacher.salary = sal;
            }
            con.Close();

            return Newteacher;
        }
        /// <summary>
        /// AddTeacher function was created to add new Teacher in teacher table. Recieve data from HTTP Post request and extract data from body , write sql query to add those datas in database.
        /// </summary>
        /// <param name="newTeacher">{
        /// "Teacherfname" : "Julia",
        /// "Teacherlname" : "Julia",
        /// "Employeenumber" : "T102",
        /// "hiredate": "2022-01-12",
        /// "salary":10000.22
        /// }
        ///  
        /// </param><result>Redirect me back to List of teacher</result>
        /// 
        [HttpPost]
        public void AddTeacher([FromBody]Teacher newTeacher)
        {
            string MessageError = string.Empty;
            // create connection
            MySqlConnection con = teacherDb.AccessDatabase();
            //open connection
            con.Open();
            // Double check again if the input was valid or not
            if (newTeacher.teacherfname == null || newTeacher.teacherfname == string.Empty)
            {
                MessageError += "error_teacherFname";
            }
            else if (newTeacher.teacherlname == null || newTeacher.teacherlname == string.Empty)
            {
                MessageError += "error_teacherLname";
            }
            else if (newTeacher.employeeenumber == null || newTeacher.employeeenumber == string.Empty)
            {
                MessageError += "error_EmployeeNumber";
            }
            else if (newTeacher.hiredate == null || newTeacher.hiredate == DateTime.MinValue)
            {
                MessageError += "error_hiredate";
            }
            else if (newTeacher.salary == 0)
            {
                MessageError += "error_salary";
            }
            MySqlCommand cmd = con.CreateCommand();
            // implementing SQL in command.
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@Emplyeenumber,@hiredate,@salary)";
            // sanitize the user's input
            cmd.Parameters.AddWithValue("@TeacherFname", newTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", newTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@Emplyeenumber", newTeacher.employeeenumber);
            cmd.Parameters.AddWithValue("@hiredate", newTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", newTeacher.salary);
            if (MessageError == string.Empty)
            {
                cmd.ExecuteNonQuery();
            }
            //create query cmd.
            
            con.Close();
        }
        /// <summary>
        /// If user click delete it when fires Post http form method /Teacher/Delete/id
        /// </summary>
        /// <param name="id">1</param>
        [HttpPost]
        public void RemoveTeacher(int id)
        {
            // create connection
            MySqlConnection con = teacherDb.AccessDatabase();
            //open connection
            con.Open();
            //create query cmd.
            MySqlCommand cmd = con.CreateCommand();
            // implementing SQL in command. Delete teacherId and ClassID which has reference to teacherId.
            cmd.CommandText = "Delete t.*, cl.* from teachers t \r\n Left Join classes cl On cl.teacherid = t.teacherid \r\n WHERE t.teacherid = @id";
            // SQL validation for Input QUERY.
            cmd.Parameters.AddWithValue("@id", id);
            // extract data from data server.
            cmd.ExecuteNonQuery();
            con.Close() ;
        }
    }
}
