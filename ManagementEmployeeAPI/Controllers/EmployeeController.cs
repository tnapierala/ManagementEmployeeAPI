using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CrudAngular.Models;

namespace CrudAngular.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select 
                                EmployeeId, EmployeeName, Department,
                                DateJoin, PaymentDolar, PhotoFileName 
                              from dbo.Employee ";
            DataTable table = new DataTable();
            using(var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using(var cmd = new SqlCommand(query, con))
            using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }
        public string Post(Employee emp)
        {
            try
            {
                string query = @" insert into dbo.Employee values 
                                  (
                                     '" + emp.EmployeeName + @"'
                                    ,'" + emp.Department + @"' 
                                    ,'" + emp.DateJoin + @"'
                                    ,'" + emp.PaymentDolar + @"'
                                    ,'" + emp.PhotoFileName + @"'
                                  )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added to Database Successfully!";
            }
            catch (Exception)
            {
                return "Failed Add to Database!";
            }
        }
        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                    update dbo.Employee set 
                    EmployeeName='" + emp.EmployeeName + @"'
                    ,Department='" + emp.Department + @"'
                    ,DateJoin='" + emp.DateJoin + @"'
                    ,PaymentDolar='" + emp.PaymentDolar + @"'
                    ,PhotoFileName='" + emp.PhotoFileName + @"'
                    where EmployeeId=" + emp.EmployeeId + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Update!!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @" delete from dbo.Employee
                                  where EmployeeId=" + id + @" ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Delete from Database Successfully!";
            }
            catch (Exception)
            {
                return "Failed Delete from Database!";
            }
        }

        [Route("api/Employee/GetDepNames")]
        [HttpGet]
        public HttpResponseMessage GetDepNames()
        {
            string query = @" select DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var pytsicalPath = HttpContext.Current.Server.MapPath("~/Photos/"+filename);

                postedFile.SaveAs(pytsicalPath);

                return filename;
            }
            catch (Exception)
            {
                return "anonymus.png";
            }
        }
    }
}
