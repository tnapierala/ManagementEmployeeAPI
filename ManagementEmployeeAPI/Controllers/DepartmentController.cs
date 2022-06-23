using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudAngular.Models;

namespace CrudAngular.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select DepartmentId, DepartmentName,
                                CityName, PostalCode
                              from dbo.Department ";
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
        public string Post(Department dep)
        {
            try
            {
                string query = @" insert into dbo.Department values 
                                  (
                                    '" + dep.DepartmentName + @"'
                                    ,'" + dep.CityName + @"' 
                                    ,'" + dep.PostalCode + @"'
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
        public string Put(Department dep)
        {
            try
            {
                string query = @" update dbo.Department set 
                                    DepartmentName='"+ dep.DepartmentName + @"', 
                                    CityName='" + dep.CityName + @"', 
                                    PostalCode='" + dep.PostalCode + @"'
                                    where DepartmentId="+ dep.DepartmentId + @" ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Record Successfully!";
            }
            catch (Exception)
            {
                return "Failed Updated Record!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @" delete from dbo.Department
                                  where DepartmentId=" + id + @" ";
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

    }
}
