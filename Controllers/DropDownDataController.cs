using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApplication4.Models;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class DropDownDataController : Controller
    {
        // GET: api/values
        [HttpGet("{dropDownName}")]
        public IEnumerable<clsDropDown> Get(string dropDownName, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            List<clsDropDown> listDDL = new List<clsDropDown>();

            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = Config.ConnectionString;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();

                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "SP_POPDDL";

                sqlCmd.Parameters.Add("@MODULENAME", SqlDbType.VarChar);
                sqlCmd.Parameters["@MODULENAME"].Value = dropDownName.ToString();

                if (!string.IsNullOrEmpty(param1))
                {
                    sqlCmd.Parameters.Add("@A", SqlDbType.VarChar);
                    sqlCmd.Parameters["@A"].Value = string.IsNullOrEmpty(param1) ? "" : param1;
                }

                //sqlCmd.Parameters.Add("@B", SqlDbType.VarChar);
                //sqlCmd.Parameters["@B"].Value = param2.ToString();
                //sqlCmd.Parameters.Add("@C", SqlDbType.VarChar);
                //sqlCmd.Parameters["@C"].Value = param3.ToString();
                //sqlCmd.Parameters.Add("@D", SqlDbType.VarChar);
                //sqlCmd.Parameters["@D"].Value = param4.ToString();
                //sqlCmd.Parameters.Add("@E", SqlDbType.VarChar);
                //sqlCmd.Parameters["@E"].Value = param5.ToString();

                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    listDDL.Add(new clsDropDown()
                    {
                        DisplayField = reader.GetValue(0).ToString(),
                        ValueField = reader.GetValue(1).ToString()
                    });
                }
                myConnection.Close();
            }
            catch(Exception ex) { }
                return listDDL;
            }

        public IEnumerable<Depatrment_PI> GetDeptPi(int deptId = 0)
        {
            List<Depatrment_PI> lstDeptPi = new List<Depatrment_PI>();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = Config.ConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spGetDepartmentPi";
                sqlCmd.Parameters.Add("@department_id", SqlDbType.Int);
                sqlCmd.Parameters["@department_id"].Value = deptId;
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    lstDeptPi.Add(new Depatrment_PI()
                    {
                        i_PI_ID = Convert.ToInt32(reader.GetValue(0)),
                        s_name = reader.GetValue(1).ToString(),
                        s_email = reader.GetValue(2).ToString(),
                        s_mcrno = reader.GetValue(3).ToString(),
                        s_phone = reader.GetValue(4).ToString(),
                        i_dept_id = Convert.ToInt32(reader.GetValue(5))
                    });
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
            return lstDeptPi;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
